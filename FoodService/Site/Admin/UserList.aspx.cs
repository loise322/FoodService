using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using TravelLine.Food.Core.DeliveryOffices;
using TravelLine.Food.Core.Legals;
using TravelLine.Food.Core.Orders;
using TravelLine.Food.Core.Users;

namespace TravelLine.Food.Site.Admin
{
    public partial class UserManage : System.Web.UI.Page
    {
        private readonly IDeliveryOfficeService _deliveryOfficeService;
        private readonly IUserService _userService;
        private readonly ILegalService _legalService;
        private readonly IOrderService _orderService;

        private List<UserModel> _users = null;
        private List<UserViewModel> _userViewModels { get; set; }
        private int _deliveryOfficeSelectedIndex = 0;
        private int _legalSelectedIndex = 0;

        public UserManage(
            IDeliveryOfficeService deliveryOfficeService,
            IUserService userService,
            ILegalService legalService,
            IOrderService orderService )
        {
            _deliveryOfficeService = deliveryOfficeService;
            _userService = userService;
            _legalService = legalService;
            _orderService = orderService;
        }

        private class UserViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Legal { get; set; }
            public bool IsEnabled { get; set; }
            public string DeliveryOffice { get; set; }
            public bool IsRemovable { get; set; }
        }

        protected void Page_Load( object sender, EventArgs e )
        {
            List<UserModel> usersWithoutOrders = _userService.GetUsersWithoutOrders();

            int deliveryOfficeId = lbDeliveryOffice.SelectedItem != null ? Int32.Parse( lbDeliveryOffice.SelectedItem.Value ) : 0;
            int legalId = lbLegal.SelectedItem != null ? Int32.Parse( lbLegal.SelectedItem.Value ) : 0;
            bool viewEnabled = chbEnable != null ? chbEnable.Checked : false;

            _users = _userService.GetUsers( deliveryOfficeId, legalId, !viewEnabled );
            if ( !String.IsNullOrWhiteSpace( tbName.Text ) )
            {
                _users = _users.FindAll( x => x.Name.ToLower().Contains( tbName.Text.ToLower() ) );
            }

            _userViewModels = _users.Select( user => new UserViewModel()
            {
                Id = user.Id,
                Name = user.Name,
                Legal = user.Legal != null ? user.Legal.Name : String.Empty,
                IsEnabled = user.IsEnabled,
                DeliveryOffice = user.DeliveryOffice.Name,
                IsRemovable = usersWithoutOrders.Any( u => u.Id == user.Id ),
            } ).ToList();

            _deliveryOfficeSelectedIndex = lbDeliveryOffice.SelectedItem != null ? lbDeliveryOffice.SelectedIndex : 0;
            _legalSelectedIndex = lbLegal.SelectedItem != null ? lbLegal.SelectedIndex : 0;
        }

        protected override void OnPreRender( EventArgs e )
        {
            base.OnPreRender( e );

            UserList.RowCreated += new GridViewRowEventHandler( UserList_RowCreated );
            UserList.DataSource = _userViewModels;
            UserList.DataBind();

            lbDeliveryOffice.DataSource = _deliveryOfficeService.GetDeliveryOffices();
            lbDeliveryOffice.DataValueField = "Id";
            lbDeliveryOffice.DataTextField = "Name";
            lbDeliveryOffice.DataBind();
            lbDeliveryOffice.Items.Insert( 0, new ListItem( "Все группы", "0" ) );

            lbLegal.DataSource = _legalService.GetLegals();
            lbLegal.DataValueField = "Id";
            lbLegal.DataTextField = "Name";
            lbLegal.DataBind();
            lbLegal.Items.Insert( 0, new ListItem( "Все команды", "0" ) );

            lbDeliveryOffice.SelectedIndex = _deliveryOfficeSelectedIndex;
            lbLegal.SelectedIndex = _legalSelectedIndex;
        }

        protected void UserList_RowCreated( object sender, GridViewRowEventArgs e )
        {
            e.Row.DataBind();
            if ( e.Row.DataItem != null && ( ( ( UserViewModel )e.Row.DataItem ).Legal == String.Empty || ( ( UserViewModel )e.Row.DataItem ).IsEnabled == false ) )
            {
                e.Row.BackColor = System.Drawing.Color.LightGray;
            }
        }

        protected void OnPageIndexChanging( object sender, GridViewPageEventArgs e )
        {
            ( ( GridView )sender ).PageIndex = e.NewPageIndex;
        }

        protected void RowCreating( object sender, GridViewRowEventArgs e )
        {
            if ( e.Row.RowType == DataControlRowType.DataRow )
            {
                bool isRemovable = ( bool )DataBinder.Eval( e.Row.DataItem, "IsRemovable" );
                if ( isRemovable )
                {
                    e.Row.Cells[ 6 ].Visible = true;
                }
                else
                {
                    e.Row.Cells[ 6 ].Text = "";
                }
            }
        }

        protected void ApplyFilter( object sender, EventArgs e )
        {
            Page_Load( sender, e );
        }

        protected void ClearFilter( object sender, EventArgs e )
        {
            Response.Redirect( Request.RawUrl );
        }

        protected void RowDeleting( object sender, GridViewDeleteEventArgs e )
        {
            int userId = Convert.ToInt32( UserList.DataKeys[ e.RowIndex ].Value );
            UserModel user = _userService.GetUser( userId );

            _userService.DeleteUser( userId );

            Page_Load( sender, e );
        }
    }
}
