using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Xml.Linq;

namespace Coolntern
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("CategoryNews", "{type}/{meta}",

                new { controller = "News", action = "Index", meta = UrlParameter.Optional },
                new RouteValueDictionary
                {
                     {"type" , "thong-bao" }
                },
                namespaces: new[] { "Coolntern.Controllers" });

            routes.MapRoute("News", "{type}",

                new { controller = "News", action = "Index", meta = UrlParameter.Optional },
                new RouteValueDictionary
                {
                     {"type" , "thong-bao" }
                },
                namespaces: new[] { "Coolntern.Controllers" });

            routes.MapRoute("DetailNews", "{type}/{meta_category}/{meta}",

                new { controller = "News", action = "Detail", meta_category = UrlParameter.Optional, meta = UrlParameter.Optional },
                new RouteValueDictionary
                {
                    {"type" , "thong-bao" }
                },
                new[] { "Coolntern.Controllers" });

            routes.MapRoute("Contact", "{type}/{meta}",

                new { controller = "Home", action = "Contact", meta = UrlParameter.Optional },
                new RouteValueDictionary
                {
                     {"type" , "contact" }
                },
                new[] { "Coolntern.Controllers" });


            routes.MapRoute("About", "{type}/{meta}",

                new { controller = "Home", action = "About", meta = UrlParameter.Optional },
                new RouteValueDictionary
                {
                                {"type" , "about" }
                },
                new[] { "Coolntern.Controllers" });

            routes.MapRoute("JobCategory", "{type}/{meta}",

                new { controller = "Job", action = "Index", meta = UrlParameter.Optional },
                new RouteValueDictionary
                {
                    {"type" , "cong-viec" }
                },
                new[] { "Coolntern.Controllers" });

            routes.MapRoute("Details", "{type}/{meta_category}/{meta}",

                new { controller = "Job", action = "Detail", meta_category = UrlParameter.Optional, meta = UrlParameter.Optional },
                new RouteValueDictionary
                {
                    {"type" , "cong-viec" }
                },
                new[] { "Coolntern.Controllers" });


            routes.MapRoute("Jobs", "{type}/{meta_category}",

                new { controller = "Job", action = "Index", meta_category = UrlParameter.Optional, meta = UrlParameter.Optional },
                new RouteValueDictionary
                {
                    {"type" , "cong-viec" }
                },
                new[] { "Coolntern.Controllers" });

            routes.MapRoute("YourJob", "{type}",

                new { controller = "DetailJob", action = "GetJobApply" },
                new RouteValueDictionary
                {
                    {"type" , "cong-viec-cua-ban" }
                },
                new[] { "Coolntern.Controllers" });

            routes.MapRoute("AcceptJob", "{type}/{meta}",

                new { controller = "DetailJob", action = "GetAcceptJob" },
                new RouteValueDictionary
                {
                    {"type" , "cong-viec-cua-ban" },
                    {"meta", "da-duyet" }
                },
                new[] { "Coolntern.Controllers" });

            routes.MapRoute("DenyJob", "{type}/{meta}",

                new { controller = "DetailJob", action = "GetDenyJob" },
                new RouteValueDictionary
                {
                    {"type" , "cong-viec-cua-ban" },
                    {"meta", "tu-choi" }
                },
                new[] { "Coolntern.Controllers" });

            routes.MapRoute("Register", "{type}",

                new { controller = "Account", action = "Register"},
                new RouteValueDictionary
                {
                    {"type" , "dang-ki" }
                },
                new[] { "Coolntern.Controllers" });

            routes.MapRoute("Login", "{type}",

                new { controller = "Account", action = "Login" },
                new RouteValueDictionary
                {
                    {"type" , "dang-nhap" }
                },
                new[] { "Coolntern.Controllers" });

            routes.MapRoute("EditProfile", "{type}",

              new { controller = "Account", action = "EditProfile" },
              new RouteValueDictionary
              {
                    {"type" , "chinh-sua-thong-tin" }
              },
              new[] { "Coolntern.Controllers" });

             routes.MapRoute("ChangePassword", "{type}",

              new { controller = "Account", action = "ChangePassword" },
              new RouteValueDictionary
              {
                    {"type" , "doi-mat-khau" }
              },
              new[] { "Coolntern.Controllers" });

            routes.MapRoute("Logot", "{type}",

                new { controller = "Account", action = "Logout" },
                new RouteValueDictionary
                {
                    {"type" , "dang-xuat" }
                },
                new[] { "Coolntern.Controllers" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "Coolntern.Controllers" }
            );
        }
    }
}
