using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;

namespace TravelLine.Food.WebApi
{
    public static class WebApiConfig
    {
        public static void Register( HttpConfiguration config )
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "rest/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            MediaTypeHeaderValue appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault( t => t.MediaType == "application/xml" );
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove( appXmlType );
        }
    }
}
