using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PetShopWeb
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapMvcAttributeRoutes();

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("Home/ActionLink");
            routes.IgnoreRoute("Home/userDetail");

            //Example: How can pass many parameter on URL
            routes.MapRoute(
                name: "Mascota-Por-Raza",
                url: "Mascotas/Raza/{*generos}",
                defaults: new { controller = "Mascotas", action = "FindBy" }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
