using System;
using TravelLine.Food.Core.Legals;
using TravelLine.Food.Domain.Legals;
using TravelLine.Food.Site.Core;

namespace TravelLine.Food.Site.Admin
{
    public partial class LegalEdit : System.Web.UI.Page
    {
        private readonly ILegalService _legalService;

        private Legal _legal;
        private int _legalId = 0;

        public LegalEdit( ILegalService LegalService )
        {
            _legalService = LegalService;
        }

        protected void Page_Load( object sender, EventArgs e )
        {
            _legalId = WebHelper.GetRequestParamAsInt( "Id" );
            _legal = _legalService.GetLegal( _legalId );            
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if ( _legal != null )
            {
                tbLegalName.Text = _legal.Name;
                tbFullName.Text = _legal.FullName;
            }
        }

        protected void SaveLegal( object sender, EventArgs e )
        {
            _legalId = _legalService.SaveLegal( _legalId, tbLegalName.Text, tbFullName.Text );
            lblResult.Text = _legalId != 0 ? "Данные сохранены" : "Ошибка при сохранении";
            
            phResult.Visible = true;
            phEdit.Visible = false;
            lnkUser.NavigateUrl = String.Format( "~/Admin/LegalEdit.aspx?id={0}", _legalId );
        }
    }
}
