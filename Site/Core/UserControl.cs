using TravelLine.Food.Core.Users;

namespace TravelLine.Food.Site.Site
{
    public abstract class UserControl : System.Web.UI.UserControl
    {
        protected UserModel CurrentUser
        {
            get
            {
                var page = Page as UserPage;
                if ( page != null )
                {
                    return page.CurrentUser;
                }

                return null;
            }
        }
    }
}
