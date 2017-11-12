using System.Web.Mvc;
using System.Web.Routing;
using TestWorkNov2016.Infrastructure;

namespace TestWorkNov2016.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Templates",
                "Templates/{*viewName}",
                new {controller = "Templates", action = "Page"},
                new {viewName = new StaticPageContraint()}
                );

            routes.MapRoute("Default", "{controller}/{action}/{id}",
                new {controller = "Home", action = "Index", id = UrlParameter.Optional}
                );
        }
    }
}