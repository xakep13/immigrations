using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Bissoft.CouncilCMS.BLL.AutoMapper;
using Bissoft.CouncilCMS.BLL.ViewModels;
using Bissoft.CouncilCMS.Web.Core;
using Bissoft.CouncilCMS.Web.Core.Themes;

namespace Bissoft.CouncilCMS.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            //remove all view engines
            ViewEngines.Engines.Clear();
            //except the themeable razor view engine we use
            ViewEngines.Engines.Add(new ThemeableRazorViewEngine());

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            AutoMapperConfiguration.Configure();
            ControllerBuilder.Current.SetControllerFactory(new AppControllerFactory(String.Empty, "CmsSettings"));
        }

        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

            if (authCookie != null)
            {
                var ticket = FormsAuthentication.Decrypt(authCookie.Value);
                var userData = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<AuthorizeUser>(ticket.UserData);
                var newUserIdentity = new CmsIdentity(userData.Name, "Password", true, userData.Id, userData.Email);

                HttpContext.Current.User = new CmsPrincipal(newUserIdentity, userData.Premissions, userData.Roles);
            }
            else
            {
                var newUserIdentity = new CmsIdentity(null, "Password", false, 0, null);
                var newUser = new CmsPrincipal(newUserIdentity);

                HttpContext.Current.User = newUser;
            }
        }
    }
}
