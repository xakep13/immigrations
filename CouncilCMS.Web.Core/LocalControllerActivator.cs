using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using xTab.Tools.Extensions;

namespace Bissoft.CouncilCMS.Web
{
    public class LocalControllerActivator : IControllerActivator
    {
        public IController Create(RequestContext requestContext, Type controllerType)
        {
            return Create(requestContext, controllerType, "ControlPanel", "CmsSettings");
        }

        public IController Create(RequestContext requestContext, Type controllerType, String projectName)
        {
            return Create(requestContext, controllerType, projectName, "CmsSettings");
        }

        public IController Create(RequestContext requestContext, Type controllerType, String projectName, String settingsName)
        {
            var appSettings = requestContext.HttpContext.Application[settingsName];
            var defaultCultureValue = appSettings.GetPropertyValue("Default" + projectName + "Culture") ?? "uk";
            var allowedCulturesValue = appSettings.GetPropertyValue(projectName + "Cultures") ?? "uk;ru;en";            
            var allowedCultures = allowedCulturesValue != null ? (String)allowedCulturesValue : "uk";
            var defaultLang = defaultCultureValue != null ? (String)defaultCultureValue : "uk";
            var cookieLang = requestContext.HttpContext.Response.Cookies["lang"] != null ? requestContext.HttpContext.Response.Cookies["lang"].Value : null;
            var routeLang = requestContext.RouteData.Values["lang"] != null ? (string)requestContext.RouteData.Values["lang"] : null;
            var currentCulture = routeLang != null ? routeLang : (cookieLang != null ? cookieLang : defaultLang);

            if (allowedCultures.Split(new String[] { ";" }, StringSplitOptions.RemoveEmptyEntries).Count() > 1 && allowedCultures.Contains(currentCulture))
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(currentCulture);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(currentCulture);
            }
            else
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(defaultLang);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(defaultLang);
            }

            var langCookie = new HttpCookie("lang", currentCulture);
            langCookie.Expires = DateTime.Now.AddYears(10);

            if (requestContext.HttpContext.Response.Cookies["lang"] == null)
            {
                requestContext.HttpContext.Response.Cookies.Add(langCookie);
            }
            else
            {
                requestContext.HttpContext.Response.Cookies.Set(langCookie);
            }
            
           

            try
            {
                return DependencyResolver.Current.GetService(controllerType) as IController;
            }
            catch
            {
                return null;
            }
        }
    }
}
