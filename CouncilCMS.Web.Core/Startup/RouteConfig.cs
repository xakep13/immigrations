using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Bissoft.CouncilCMS.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                 name: "LoadDocsAtFolder",
                 url: "{lang}/Documents/LoadSubFolders/{Id}",
                 defaults: new
                 {
                     controller = "Documents",
                     action = "LoadSubFolders",
                     lang = UrlParameter.Optional,
                     Id = UrlParameter.Optional
                 },
                 constraints: new { lang = @"ru|uk|en", url = @"[a-zA-Z0-9-_]{1,}" },
                 namespaces: new string[] {
                    "Bissoft.CouncilCMS.Web.Controllers"
                 }
            );

            routes.MapRoute(
                 name: "RecordingAtReception",
                 url: "{lang}/Widgets/RecordingAtReceptionPost",
                 defaults: new
                 {
                     controller = "Widgets",
                     action = "RecordingAtReceptionPost",
                     lang = UrlParameter.Optional
                 },
                 constraints: new { lang = @"ru|uk|en", url = @"[a-zA-Z0-9-_]{1,}" },
                 namespaces: new string[] {
                    "Bissoft.CouncilCMS.Web.Controllers"
                 }
            );

            routes.MapRoute(
                 name: "OpenLoginDialog",
                 url: "{lang}/FacebookLogin/InvokeTheLoginDialogWindow",
                 defaults: new
                 {
                     controller = "FacebookLogin",
                     action = "InvokeTheLoginDialogWindow",
                 },
                 constraints: new { lang = @"ru|uk|en", url = @"[a-zA-Z0-9-_]{1,}" },
                 namespaces: new string[] {
                        "Bissoft.CouncilCMS.Web.Areas.ControlPanel.Controllers"
                 }
            );

            routes.MapRoute(
                 name: "PostOnPage",
                 url: "FacebookLogin/PostOnPage",
                 defaults: new
                 {
                     controller = "FacebookLogin",
                     action = "PostOnPage",
                 },
                 namespaces: new string[] {
                        "Bissoft.CouncilCMS.Web.Areas.ControlPanel.Controllers"
                 }
            );

            routes.MapRoute(
                name: "MenuItemUpdate",
                url: "Menus/UpdateMenuPosition",
                defaults: new { controller = "Menus", action = "UpdateMenuPosition" },
                namespaces: new string[] {
                    "Bissoft.CouncilCMS.Web.Areas.ControlPanel.Controllers"
                }
            );

            routes.MapRoute(
                name: "PageLocal",
                url: "{lang}/page/{url}",
                defaults: new { controller = "Pages", action = "Page", url = UrlParameter.Optional },
                constraints: new { lang = @"ru|uk|en", url = @"[a-zA-Z0-9-_]{1,}" },
                namespaces: new string[] { "Bissoft.CouncilCMS.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Page",
                url: "page/{url}",
                defaults: new { controller = "Pages", action = "Page", url = UrlParameter.Optional },
                constraints: new { url = @"[a-zA-Z0-9-_]{1,}" },
                namespaces: new string[] { "Bissoft.CouncilCMS.Web.Controllers" }
            );


            routes.MapRoute(
               name: "ArticleCategoryLocal",
               url: "{lang}/articles/category/{url}/{page}",
               defaults: new { controller = "Articles", action = "Category", url = UrlParameter.Optional, page = 1 },
               constraints: new { lang = @"ru|uk|en", url = @"[a-zA-Z0-9-_]{1,}", page = @"[0-9]{1,}" },
               namespaces: new string[] { "Bissoft.CouncilCMS.Web.Controllers" }
           );

            routes.MapRoute(
                name: "ArticleCategory",
                url: "articles/category/{url}/{page}",
                defaults: new { controller = "Articles", action = "Category", url = UrlParameter.Optional, page = 1 },
                constraints: new { url = @"[a-zA-Z0-9-_]{1,}", page = @"[0-9]{1,}" },
                namespaces: new string[] { "Bissoft.CouncilCMS.Web.Controllers" }
            );


            routes.MapRoute(
              name: "ArticleItemLocal",
              url: "{lang}/articles/item/{id}/{url}",
              defaults: new { controller = "Articles", action = "Item", url = UrlParameter.Optional, id = UrlParameter.Optional },
              constraints: new { lang = @"ru|uk|en", url = @"[a-zA-Z0-9-_]{0,}", id = @"[0-9]{1,}" },
              namespaces: new string[] { "Bissoft.CouncilCMS.Web.Controllers" }
          );

            routes.MapRoute(
                name: "ArticleItem",
                url: "articles/item/{id}/{url}",
                defaults: new { controller = "Articles", action = "Item", url = UrlParameter.Optional, id = UrlParameter.Optional },
                constraints: new { url = @"[a-zA-Z0-9-_]{0,}", id = @"[0-9]{1,}" },
                namespaces: new string[] { "Bissoft.CouncilCMS.Web.Controllers" }
            );


            routes.MapRoute(
                name: "PersonCategoryLocal",
                url: "{lang}/persons/category/{url}/{page}",
                defaults: new { controller = "Persons", action = "Category", url = UrlParameter.Optional, page = 1 },
                constraints: new { lang = @"ru|uk|en", url = @"[a-zA-Z0-9-_]{1,}" },
                namespaces: new string[] { "Bissoft.CouncilCMS.Web.Controllers" }
            );

            routes.MapRoute(
                name: "PersonCategory",
                url: "persons/category/{url}/{page}",
                defaults: new { controller = "Persons", action = "Category", url = UrlParameter.Optional, page = 1 },
                constraints: new { url = @"[a-zA-Z0-9-_]{1,}" },
                namespaces: new string[] { "Bissoft.CouncilCMS.Web.Controllers" }
            );


            routes.MapRoute(
             name: "PersonItemLocal",
             url: "{lang}/persons/item/{id}/{url}",
             defaults: new { controller = "Persons", action = "Item", url = UrlParameter.Optional, id = UrlParameter.Optional },
             constraints: new { lang = @"ru|uk|en", url = @"[a-zA-Z0-9-_]{0,}", id = @"[0-9]{1,}" },
             namespaces: new string[] { "Bissoft.CouncilCMS.Web.Controllers" }
         );

            routes.MapRoute(
                name: "PersonItem",
                url: "persons/item/{id}/{url}",
                defaults: new { controller = "Persons", action = "Item", url = UrlParameter.Optional, id = UrlParameter.Optional },
                constraints: new { url = @"[a-zA-Z0-9-_]{0,}", id = @"[0-9]{1,}" },
                namespaces: new string[] { "Bissoft.CouncilCMS.Web.Controllers" }
            );


            routes.MapRoute(
               name: "DocCategoryLocal",
               url: "{lang}/documents/category/{url}/{page}",
               defaults: new { controller = "Documents", action = "Category", url = UrlParameter.Optional, page = 1 },
               constraints: new { lang = @"ru|uk|en", url = @"[a-zA-Z0-9-_]{1,}", page = @"[0-9]{1,}" },
               namespaces: new string[] { "Bissoft.CouncilCMS.Web.Controllers" }
           );

            routes.MapRoute(
                name: "DocCategory",
                url: "documents/category/{url}/{page}",
                defaults: new { controller = "Documents", action = "Category", url = UrlParameter.Optional, page = 1 },
                constraints: new { url = @"[a-zA-Z0-9-_]{1,}", page = @"[0-9]{1,}" },
                namespaces: new string[] { "Bissoft.CouncilCMS.Web.Controllers" }
            );


            routes.MapRoute(
               name: "OrgCategoryLocal",
               url: "{lang}/organizations/category/{url}/{page}",
               defaults: new { controller = "Organizations", action = "Category", url = UrlParameter.Optional, page = 1 },
               constraints: new { lang = @"ru|uk|en", url = @"[a-zA-Z0-9-_]{1,}", page = @"[0-9]{1,}" },
               namespaces: new string[] { "Bissoft.CouncilCMS.Web.Controllers" }
           );

            routes.MapRoute(
                name: "OrgCategory",
                url: "organizations/category/{url}/{page}",
                defaults: new { controller = "Organizations", action = "Category", url = UrlParameter.Optional, page = 1 },
                constraints: new { url = @"[a-zA-Z0-9-_]{1,}", page = @"[0-9]{1,}" },
                namespaces: new string[] { "Bissoft.CouncilCMS.Web.Controllers" }
            );

            routes.MapRoute(
             name: "OrgItemLocal",
             url: "{lang}/organizations/item/{id}/{url}",
             defaults: new { controller = "Organizations", action = "Item", url = UrlParameter.Optional, id = UrlParameter.Optional },
             constraints: new { lang = @"ru|uk|en", url = @"[a-zA-Z0-9-_]{0,}", id = @"[0-9]{1,}" },
             namespaces: new string[] { "Bissoft.CouncilCMS.Web.Controllers" }
         );

            routes.MapRoute(
                name: "OrgItem",
                url: "organizations/item/{id}/{url}",
                defaults: new { controller = "Organizations", action = "Item", url = UrlParameter.Optional, id = UrlParameter.Optional },
                constraints: new { url = @"[a-zA-Z0-9-_]{0,}", id = @"[0-9]{1,}" },
                namespaces: new string[] { "Bissoft.CouncilCMS.Web.Controllers" }
            );

            routes.MapRoute(
                name: "SessionsLocal",
                url: "{lang}/sessions/{sessionid}/{action}/{page}",
                defaults: new { controller = "Sessions", action = "Projects", sessionid = UrlParameter.Optional, page = 1 },
                constraints: new { lang = @"ru|uk|en", sessionid = @"[a-zA-Z0-9-_]{0,}" },
                namespaces: new string[] { "Bissoft.CouncilCMS.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Sessions",
                url: "sessions/{sessionid}/{action}/{page}",
                defaults: new { controller = "Sessions", action = "Projects", sessionid = UrlParameter.Optional, page = 1 },
                constraints: new { sessionid = @"[a-zA-Z0-9-_]{0,}" },
                namespaces: new string[] { "Bissoft.CouncilCMS.Web.Controllers" }
            );

            routes.MapRoute(
                name: "DefaultLocal",
                url: "{lang}/{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                constraints: new { lang = @"ru|uk|en" },
                namespaces: new string[] { "Bissoft.CouncilCMS.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "Bissoft.CouncilCMS.Web.Controllers" }
            );
        }
    }
}
