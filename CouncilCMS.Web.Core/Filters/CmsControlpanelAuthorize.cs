using System;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Web.Security;
using System.Web.Script.Serialization;
using Bissoft.CouncilCMS.BLL.Services;
using Bissoft.CouncilCMS.Web.Core;
using Bissoft.CouncilCMS.Web.Controllers;

namespace Bissoft.CouncilCMS.Web.Filters
{
    public class CmsAuthorize : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
                throw new ArgumentNullException("filterContext");
           
            var accountService = new AccountService((filterContext.Controller as BaseCmsController).ConnectionString);
            var currentUser = (filterContext.HttpContext.User as CmsPrincipal);
            var loginUrl = "~/ControlPanel/Account/Login";

            if (!currentUser.Identity.IsAuthenticated)
            {
                if (filterContext.HttpContext.Request != null)
                    loginUrl += "?ReturnUrl=" + filterContext.HttpContext.Request.Url.AbsoluteUri;

                filterContext.Result = new RedirectResult(loginUrl);
            }
            else
            {
                var checkSessionDate = (DateTime?)HttpContext.Current.Session["CheckSessionDate"] ?? DateTime.Now.AddMinutes(-4);
                
                if (checkSessionDate.AddMinutes(3) <= DateTime.Now)
                {                
                    var user = accountService.GetUser(currentUser.Identity.UserId);

                    if (user != null)
                    {
                        if (accountService.IsBlocked(currentUser.Identity.UserId))
                            throw new HttpException(403, "Forbidden");

                        currentUser = new CmsPrincipal(new CmsIdentity(user.Name, "Password", true, user.Id, user.Email), user.Premissions, user.Roles);

                        var serializer = new JavaScriptSerializer();
                        var modelJson = serializer.Serialize(user);
                        var ticket = new FormsAuthenticationTicket(1, "auth", DateTime.Now, DateTime.Now.AddDays(5), true, modelJson);
                        var encTicket = FormsAuthentication.Encrypt(ticket);
                        
                        HttpContext.Current.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
                        HttpContext.Current.Session["CheckSessionDate"] = DateTime.Now;
                    }
                    else
                    {
                        throw new HttpException(403, "Forbidden");
                    }
                }

                if (!String.IsNullOrEmpty(Roles))
                {
                    bool check = false;
                    string[] rolesAllow = Roles.Split(',');

                    foreach (var item in rolesAllow)
                    {                        
                        if (currentUser.IsInRole(item))
                        {
                            check = true;
                        }
                    }

                    if (check == false)
                    {
                        throw new HttpException(403, "Forbidden");
                    }
                }
            }
        }
    }
}