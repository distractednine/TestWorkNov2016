using System.IO;
using System.Web;
using System.Web.Routing;

namespace TestWorkNov2016.Infrastructure
{
    public class StaticPageContraint : IRouteConstraint
    {
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values,
            RouteDirection routeDirection)
        {
            var viewPath =
                httpContext.Server.MapPath(string.Format("~/Views/Templates/{0}.cshtml", values[parameterName]));

            return File.Exists(viewPath);
        }
    }
}