using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using TravelLine.Food.Core.Legals;
using TravelLine.Food.Domain.Legals;

namespace TravelLine.Food.Site.Admin
{
    public partial class LegalManage : System.Web.UI.Page
    {
        private readonly ILegalService _legalService;

        private List<Legal> _legals = null;

        public LegalManage( ILegalService legalService )
        {
            _legalService = legalService;
        }

        protected void Page_Load( object sender, EventArgs e )
        {
            _legals = _legalService.GetLegals();
        }

        protected void OnPreRender( object sender, EventArgs e )
        {
            base.OnPreRender( e );

            LegalList.DataSource = _legals;
            LegalList.DataBind();
        }

        protected void OnPageIndexChanging( object sender, GridViewPageEventArgs e )
        {
            ( (GridView)sender ).PageIndex = e.NewPageIndex;
        }

        protected void ProcessRow( object sender, CommandEventArgs e )
        {
            int param = Convert.ToInt32( e.CommandArgument );
            if ( param != LegalService.DefaultLegal )
            {
                _legalService.RemoveLegal( param );
            }

            Page_Load( sender, e );
        }
    }
}
