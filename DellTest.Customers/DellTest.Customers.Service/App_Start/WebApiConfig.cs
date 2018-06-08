using System.Web.Http;

namespace DellTest.Customers.Service
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                "DellTestApi",
                "delltestapi/{controller}/{action}/{id}",
                new {id = RouteParameter.Optional}
            );
        }
    }
}