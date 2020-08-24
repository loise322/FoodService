using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using TravelLine.Food.Core.DeliveryOffices;
using TravelLine.Food.Core.Legals;
using TravelLine.Food.Core.Users;
using TravelLine.Food.Domain.Legals;
using TravelLine.Food.Domain.Users;
using TravelLine.Food.Site.Core;

namespace TravelLine.Food.Site.Admin
{
    public partial class UserEdit : System.Web.UI.Page
    {
        private readonly IDeliveryOfficeService _deliveryOfficeService;
        private readonly IUserService _userService;
        private readonly ILegalService _legalService;

        protected UserModel EditUser { get; set; }
        private List<LegalData> _legalData = new List<LegalData>();

        public UserEdit(
            IDeliveryOfficeService deliveryOfficeService,
            IUserService userService,
            ILegalService legalService )
        {
            _deliveryOfficeService = deliveryOfficeService;
            _userService = userService;
            _legalService = legalService;
        }

        private class LegalData
        {
            public int Id { get; set; }

            public int UserId { get; set; }

            public int LegalId { get; set; }

            public DateTime StartDate { get; set; }

            public DateTime? EndDate { get; set; }

            public virtual Legal Legal { get; set; }

            public string TransferReason { get; set; }

            public bool IsEditable { get; set; }

            public bool IsRemovable { get; set; }
        }

        protected void Page_Load( object sender, EventArgs e )
        {
            int id = WebHelper.GetRequestParamAsInt( "Id" );
            EditUser = _userService.GetUser( id );
            if ( EditUser.Id != 0 )
            {
                legalGroup.Visible = false;
                applicationGroup.Visible = false;

                foreach ( UserLegal legal in EditUser.UserLegals )
                {
                    LegalData legalData = new LegalData()
                    {
                        Id = legal.Id,
                        UserId = legal.UserId,
                        TransferReason = TranslateReason( legal.TransferReason ),
                        StartDate = legal.StartDate,
                        LegalId = legal.LegalId,
                        Legal = legal.Legal
                    };

                    if ( legal.TransferReason == TransferReasonsType.Application || legal.TransferReason == TransferReasonsType.Dismissal )
                    {
                        if ( EditUser.UserLegals.Last() == legal )
                        {
                            legalData.IsEditable = true;
                            legalData.IsRemovable = true;
                        }
                        else
                        {
                            legalData.IsEditable = true;
                            legalData.IsRemovable = false;
                        }

                        if ( EditUser.UserLegals[0].LegalId == legal.LegalId && EditUser.UserLegals.Count == 1 )
                        {
                            legalData.IsEditable = true;
                            legalData.IsRemovable = false;
                        }
                        else
                        {
                            legalData.IsEditable = false;
                        }
                    }
                    else
                    {
                        legalData.IsEditable = true;
                        legalData.IsRemovable = true;

                        UserLegal nextLegal = EditUser.UserLegals.FirstOrDefault( ul => ul.StartDate > legal.StartDate );
                        if ( legal.TransferReason == TransferReasonsType.NoReason || ( nextLegal != null && nextLegal.StartDate <= DateTime.Today ) )
                        {
                            legalData.IsEditable = false;
                            legalData.IsRemovable = false;
                        }
                    }

                    _legalData.Add( legalData );
                }

                int i = 0;
                _legalData = _legalData.OrderBy( l => l.StartDate ).ToList();
                foreach ( LegalData legal in _legalData )
                {
                    if ( _legalData.Count > i + 1 )
                    {
                        _legalData[ i ].EndDate = _legalData[ i + 1 ].StartDate.AddDays( -1 );
                    }
                    else
                    {
                        _legalData[ i ].EndDate = null;
                    }
                    i++;
                }
            }
            else
            {
                HeaderControl.Text = "Создание пользователя";
                applicationGroup.Visible = true;
                legalGroup.Visible = true;
                transfer.Visible = false;
                UserTransfer.Visible = false;
            }

            UserTransfer.User = EditUser;
        }

        private string TranslateReason( TransferReasonsType transferReason )
        {
            switch ( transferReason )
            {
                case TransferReasonsType.Holiday:
                    return "Отпуск";
                case TransferReasonsType.Sick:
                    return "Больничный";
                case TransferReasonsType.BusinessTrip:
                    return "Командировка";
                case TransferReasonsType.Dismissal:
                    return "Увольнение";
                case TransferReasonsType.Application:
                    return "Прием";
                default:
                    return "Автовозврат";
            }
        }

        protected void SaveUser( object sender, EventArgs e )
        {
            EditUser.DeliveryOffice.Id = Int32.Parse( lbDeliveryOffice.SelectedValue );
            EditUser.Name = tbUserName.Text;
            EditUser.IsEnabled = true;

            if ( EditUser.Legal == null )
            {
                EditUser.Legal = new Legal();
            }

            try
            {
                if ( EditUser.Id == 0 )
                {
                    EditUser.Legal.Id = Int32.Parse( lbLegal.SelectedValue );
                    DateTime startDate = new DateTime();
                    if ( !DateTime.TryParse( tbApplicationDate.Text, out startDate ) )
                    {
                        startDate = DateTime.Today;
                    }

                    var userLegal = new UserLegal()
                    {
                        StartDate = startDate,
                        UserId = EditUser.Id,
                        TransferReason = TransferReasonsType.Application,
                        LegalId = EditUser.Legal.Id
                    };
                    EditUser.UserLegals.Add( userLegal );
                }

                _userService.Save( EditUser );
                lblResult.Text = "Данные сохранены";
            }
            catch
            {
                lblResult.Text = "Ошибка при сохранении";
            }

            phResult.Visible = true;
            phEdit.Visible = false;
            lnkUser.NavigateUrl = String.Format( "~/Admin/UserEdit.aspx?id={0}", EditUser.Id );
        }

        protected override void OnPreRender( EventArgs e )
        {
            List<Legal> legals = _legalService.GetLegals();
            foreach ( UserLegal userLegal in EditUser.UserLegals )
            {
                userLegal.Legal = legals.Find( l => l.Id == userLegal.LegalId );
            }

            lbLegal.DataSource = _legalService.GetLegals();
            lbLegal.DataValueField = "Id";
            lbLegal.DataTextField = "Name";
            lbLegal.DataBind();

            LegalList.DataSource = _legalData;
            LegalList.DataBind();

            base.OnPreRender( e );
            lbDeliveryOffice.DataSource = _deliveryOfficeService.GetDeliveryOffices();
            lbDeliveryOffice.DataValueField = "Id";
            lbDeliveryOffice.DataTextField = "Name";
            lbDeliveryOffice.DataBind();

            if ( EditUser != null )
            {
                Title = EditUser.Name;
                tbUserName.Text = EditUser.Name;

                if ( EditUser.DeliveryOffice != null )
                {
                    lbDeliveryOffice.SelectedIndex = lbDeliveryOffice.Items.IndexOf( lbDeliveryOffice.Items.FindByValue( EditUser.DeliveryOffice.Id.ToString() ) );
                }
            }
        }

        protected void OnPageIndexChanging( object sender, GridViewPageEventArgs e )
        {
            ( ( GridView )sender ).PageIndex = e.NewPageIndex;
        }
    }
}
