using System;
using System.Web;
using TravelLine.Food.Core.Configs;
using TravelLine.Food.Core.Users;
using TravelLine.Food.Domain.Users;

namespace TravelLine.Food.Site.Core
{
    internal static class WebHelper
    {
        public static int GetRequestParamAsInt( string name )
        {
            int result;

            string value = GetRequestParam( name );
            Int32.TryParse( value, out result );

            return result;
        }

        public static string GetRequestParam( string name )
        {
            return HttpContext.Current.Request.Params[ name ];
        }

        public static UserModel GetCurrentUser()
        {
            if ( HttpContext.Current.Request.Cookies.Get( ConfigService.CookieUserID ) != null && HttpContext.Current.Request.Cookies.Get( ConfigService.CookieUserID ).Value != String.Empty )
            {
                var userService = DependencyResolver.GetService<IUserService>();

                return userService.GetUser( Convert.ToInt32( HttpContext.Current.Request.Cookies.Get( ConfigService.CookieUserID ).Value ) );
            }

            return null;
        }
    }
}
