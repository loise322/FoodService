using System;

namespace TravelLine.Food.Site.Master
{
    public partial class Main : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ( !IsPostBack )
            {
                MainMenu.IsMainPage = true;
            }
        }
    }
}
