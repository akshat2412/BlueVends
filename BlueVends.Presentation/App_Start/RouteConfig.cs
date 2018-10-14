using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BlueVends.Presentation
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Login",
                url: "Login",
                new { controller = "Login", action = "Login" }
            );

            routes.MapRoute(
                name: "Register",
                url: "Register",
                new { controller = "Register", action = "Register" }
            );

            routes.MapRoute(
                name: "ProductDetail",
                url: "Products/ProductDetail/{ProductID}",
                new { controller = "Products", action = "ProductDetail" }
            );

            routes.MapRoute(
                name: "ProductSearch",
                url: "Products/SearchProducts",
                new { controller = "Products", action = "SearchProducts" }
            );

            routes.MapRoute(
                name: "Products",
                url: "Products/{CategoryName}",
                new { controller = "Products", action = "CategoryProducts" }
            );

            routes.MapRoute(
                name: "OrderDetail",
                url: "Products",
                new { controller = "Products", action = "CategoryProducts", CategoryID = "b0ee3b07-87ef-4091-a97d-a8268ecab37d" }
            );

            routes.MapRoute(
                name: "Order",
                url: "MyOrders/{orderId}",
                new { controller = "User", action = "Order" }
            );
            routes.MapRoute(
                name: "Orders",
                url: "MyOrders",
                new { controller = "User", action = "Orders" }
            );

            routes.MapRoute(
                name: "Admin",
                url: "admin",
                new { controller = "User", action = "CheckAdmin" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
