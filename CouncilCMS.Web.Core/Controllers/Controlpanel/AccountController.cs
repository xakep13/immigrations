using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using Bissoft.CouncilCMS.BLL.Services;
using Bissoft.CouncilCMS.BLL.ViewModels;
using Bissoft.CouncilCMS.Web.Controllers;
using Bissoft.CouncilCMS.BLL;

namespace Bissoft.CouncilCMS.Web.Areas.ControlPanel.Controllers
{
    public class AccountController : BaseCmsController
    {
       
        private CmsUserAdminService userService;
        private AccountService accountService;
        public AccountController()
        {
            var uow = new UnitOfWork(this.connectionString);

            userService = new CmsUserAdminService(uow);
            accountService = new AccountService(uow);
        }

        public ActionResult Login(string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home", new { Area = string.Empty });
        }

        [HttpPost]
        public ActionResult Login(CmsLogin model)
        {
            var user = accountService.GetUser(model.Email, model.Password);

            if (user != null)
            {
                if (user.Blocked)
                {
                    ModelState.AddModelError("WrongLogin", "Користувач видалений або забокований");
                    return View(model);
                }
                else
                {
                    var serializer = new JavaScriptSerializer();
                    var modelJson = serializer.Serialize(user);
                    var ticket = new FormsAuthenticationTicket(1, "auth", DateTime.Now, DateTime.Now.AddDays(5), true, modelJson);
                    var encTicket = FormsAuthentication.Encrypt(ticket);

                    Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));

                    System.Web.HttpContext.Current.Session["CreateDate"] = DateTime.Now;

                    return RedirectToAction("LoginRedirect", "Account", new { Area = "ControlPanel" });
                }              
            }
            else
            {
                ModelState.AddModelError("WrongLogin", "Невірний логін або пароль");
                return View(model);
            }
        }

        public ActionResult LoginRedirect()
        {
            if (User.Premissions.Any(x => x.Contains("*")) || User.Premissions.Any(x => x.Contains("page")))
                return RedirectToAction("List", "Pages", new { Area = "ControlPanel" });
            else if (User.Premissions.Any(x => x.Contains("article")))
                return RedirectToAction("List", "Articles", new { Area = "ControlPanel" });
            else if (User.Premissions.Any(x => x.Contains("person")))
                return RedirectToAction("List", "Persons", new { Area = "ControlPanel" });
            else if (User.Premissions.Any(x => x.Contains("enterprise")))
                return RedirectToAction("List", "Enterprises", new { Area = "ControlPanel" });
            else if (User.Premissions.Any(x => x.Contains("doc")))
                return RedirectToAction("List", "Docs", new { Area = "ControlPanel" });
            else if (User.Premissions.Any(x => x.Contains("article_category")))
                return RedirectToAction("List", "ArticleCategories", new { Area = "ControlPanel" });
            else if (User.Premissions.Any(x => x.Contains("person_category")))
                return RedirectToAction("List", "PersonCategories", new { Area = "ControlPanel" });
            else if (User.Premissions.Any(x => x.Contains("enterprise_category")))
                return RedirectToAction("List", "EnterpriseCategories", new { Area = "ControlPanel" });
            else if (User.Premissions.Any(x => x.Contains("doc_category")))
                return RedirectToAction("List", "DocCategories", new { Area = "ControlPanel" });
            else if (User.Premissions.Any(x => x.Contains("menu")))
                return RedirectToAction("List", "Menus", new { Area = "ControlPanel" });
            else if (User.Premissions.Any(x => x.Contains("user")))
                return RedirectToAction("List", "Users", new { Area = "ControlPanel" });
            else if (User.Premissions.Any(x => x.Contains("settings")))
                return RedirectToAction("Edit", "CmsSettings", new { Area = "ControlPanel" });
            else
                return RedirectToAction("Index", "Index", new { Area = "ControlPanel" });
        }

        public ActionResult GetChangePasswordForm()
        {
            return PartialView("_prtChangePassword", new CmsChangePassword());
        }

        [HttpPost]
        public ActionResult ChangePassword(CmsChangePassword model)
        {
            var user = accountService.GetUser(User.Identity.UserId);

            if (accountService.GetUser(user.Email, model.OldPassword) == null)
                ModelState.AddModelError("WrongPassword", "Старий пароль вказано не вірно");

            if (!ModelState.IsValid)
                return PartialView("_prtChangePassword", model);

            var pass = accountService.ChangePassword(user.Id, model.OldPassword, model.NewPassword);

            return PartialView("_prtChangePasswordComplete", model.NewPassword);

        }
    }
}