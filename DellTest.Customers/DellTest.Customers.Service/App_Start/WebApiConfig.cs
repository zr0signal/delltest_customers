using System.Web.Http;
using System.Web.Http.Cors;

namespace DellTest.Customers.Service
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.EnableCors(new EnableCorsAttribute("http://localhost:63403", "*", "*"));

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                "DellTestApi",
                "delltestapi/{controller}/{action}/{id}",
                new {id = RouteParameter.Optional}
            );
        }
    }
}