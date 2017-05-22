using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WineOutlet
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "DefaultApi1",
                routeTemplate: "api/{controller}/{action}/{objectId}",
                defaults: new { objectId = RouteParameter.Optional }  );
            
        }
    }
}
