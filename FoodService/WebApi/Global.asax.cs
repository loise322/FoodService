using System.Web.Http;

namespace TravelLine.Food.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            UnityConfig.RegisterComponents();
            GlobalConfiguration.Configure( WebApiConfig.Register );
        }
    }
}
