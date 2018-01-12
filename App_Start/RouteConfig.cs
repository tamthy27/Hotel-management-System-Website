using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApplication1
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "chi tiet phieu dat",
                url: "chi-tiet-phieu-dat/{id}",
                defaults: new { controller = "Home", action = "OrderDetails" }
            );

            routes.MapRoute(
                name: "ds phong da dat",
                url: "phong-da-dat",
                defaults: new { controller = "Home", action = "OrderList" }
            );

            routes.MapRoute(
                name: "huong dan",
                url: "huong-dan",
                defaults: new { controller = "Home", action = "Instruction" }
            );

            routes.MapRoute(
                name: "huy phieu dat",
                url: "huy-phieu-dat/{id}",
                defaults: new { controller = "Home", action = "CancelOrder" }
            );

            routes.MapRoute(
                name: "thay doi phieu dat",
                url: "sua-phieu-dat",
                defaults: new { controller = "Home", action = "ChangeOrder" }
            );

          
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            // routes.MapRoute(
            //    name: "Home",
            //    url: "home",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);
            // routes.MapRoute(
            //     name: "About",
            //     url: "about",
            //     defaults: new { controller = "About", action = "Index", id = UrlParameter.Optional }
            // );

            // routes.MapRoute(
            // name: "Contact",
            // url: "contact",
            // defaults: new { controller = "Contact", action = "Index", id = UrlParameter.Optional }
            // );

            // routes.MapRoute(
            // name: "PayRoom",
            // url: "payroom",
            // defaults: new { controller = "PayRoom", action = "Index", id = UrlParameter.Optional }
            // );
        }
    }
}
