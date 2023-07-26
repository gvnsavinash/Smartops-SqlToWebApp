using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SqlToWebApp
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

           /* config.Routes.MapHttpRoute(
             name: "Helper",
             routeTemplate: "{controller}/{action}",
             defaults: new { controller = "Helper", id = RouteParameter.Optional }
         );
           */
           

            config.Routes.MapHttpRoute(
             name: "RestAPI",
             routeTemplate: "{controller}/{action}",
             defaults: new { controller = "RestAPI", id = RouteParameter.Optional }
         );

           config.Routes.MapHttpRoute(
            name: "WS",
            routeTemplate: "{controller}/{action}",
            defaults: new { controller = "WS", id = RouteParameter.Optional }
        );



            config.Routes.MapHttpRoute(
                    name: "Home",
                    routeTemplate: "Home/{action}",
                    defaults: new { controller = "home", id = RouteParameter.Optional }
                );

            config.Routes.MapHttpRoute(
               name: "HYG",
               routeTemplate: "HYG/{action}",
               defaults: new { controller = "HYG", id = RouteParameter.Optional }
           );

            config.Routes.MapHttpRoute(
              name: "HygWS",
              routeTemplate: "HygWS/{action}",
              defaults: new { controller = "HygWS", id = RouteParameter.Optional }
          );

        }
    }
}
