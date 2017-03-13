using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.SessionState;
using System.Web.Http.WebHost;
using System.Web.Routing;
using System.Web;

namespace Iso8583WebClient
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapHttpRoute(
                 name: "DefaultApi",
                 routeTemplate: "api/{controller}/{id}",
                 defaults: new { id = RouteParameter.Optional }
            ).RouteHandler = new HttpSessionRouteHandler();
        }
    }

    public class HttpSessionHandler : HttpControllerHandler, IRequiresSessionState
    {
        public HttpSessionHandler(RouteData routeData) : base(routeData)
        {
        }
    }
    public class HttpSessionRouteHandler : HttpControllerRouteHandler
    {
        protected override IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return new HttpSessionHandler(requestContext.RouteData);
        }
    }
}
