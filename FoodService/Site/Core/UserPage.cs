using TravelLine.Food.Core.Users;
using TravelLine.Food.Domain.Users;
using TravelLine.Food.Site.Core;

namespace TravelLine.Food.Site.Site
{
    public abstract class UserPage : System.Web.UI.Page
    {
        private UserModel _user;

        public UserModel CurrentUser
        {
            get
            {
                if( _user == null )
                {
                    _user = WebHelper.GetCurrentUser();
                }

                return _user;
            }
        }
    }
}
