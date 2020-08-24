using System;

namespace TravelLine.Food.Site.Master
{
    public partial class Admin : System.Web.UI.MasterPage
    {
        protected void Page_Load( object sender, EventArgs e )
        {
            MainMenu.IsAdminPage = true;
        }
    }
}
