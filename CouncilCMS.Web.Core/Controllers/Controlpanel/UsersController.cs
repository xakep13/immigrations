using System;
using System.Configuration;
using System.Web.Mvc;
using Bissoft.CouncilCMS.Core.Enums;
using Bissoft.CouncilCMS.BLL.ViewModels;
using Bissoft.CouncilCMS.BLL.Services;
using System.Collections.Generic;
using Bissoft.CouncilCMS.Web.Controllers;
using Bissoft.CouncilCMS.Web.Filters;

namespace Bissoft.CouncilCMS.Web.Areas.ControlPanel.Controllers
{
    [CmsAuthorize]
    public class UsersController : BaseCmsController
    {
        private CmsUserAdminService userService;

        public UsersController() : base()
        {
            userService = new CmsUserAdminService(this.ConnectionString);
        }

        public ActionResult List(string query = null, CmsUserState userState = CmsUserState.Active, string dateRange = null, int? role = null, int page = 1, string sortBy = "RegisterDate", int sortDir = 1, bool ajaxMode = false)
        {
            if (ajaxMode)
            {
                var count = 0;
                var model = userService.GetList(
                    query: query,
                    dateRange: dateRange,
                    userState: userState,                    
                    role: role,
                    sortBy: sortBy,
                    sortDir: sortDir,
                    page: page,
                    perPage: 20,                    
                    count: out count);

                ViewBag.Count = count;
                ViewBag.Page = page;
                ViewBag.PerPage = 20;

                return PartialView("_prtUserList", model);
            }
            else
            {
                var model = userService.List(
                    query: query,
                    dateRange: dateRange,
                    userState: userState,
                    role: role,
                    sortBy: sortBy,
                    sortDir: sortDir,
                    page: page);

                return View(model);
            }
        }

        public ActionResult GetForm(Int32 Id = 0)
        {
            var model = userService.GetForm(Id);

            return PartialView("_prtUserEdit", model);
        }

        public ActionResult Save(CmsUserEdit Model)
        {
            var model = userService.SaveUser(Model);

            if (Model.Id == 0)
            {
                var viewBag = new RazorEngine.Templating.DynamicViewBag();

                viewBag.AddValue("Domain", CmsSettings.BaseDomain);

                var emailService = new EmailService(this.DefaultSmptClient, CmsSettings.DefaultEmail, CmsSettings.DefaultEmailName);
                emailService.SendEmailWithModel(model.Email, "Ви зараєстровані у CMS", "RegisterAdminUser", model, viewBag);
            }
            
            return PartialView("_prtUserEditComplete", model);
        }

        public ActionResult ResetPassword(int id)
        {
            var model = userService.ResetPassword(id);

            var viewBag = new RazorEngine.Templating.DynamicViewBag();

            viewBag.AddValue("Domain", CmsSettings.BaseDomain);

            var emailService = new EmailService(this.DefaultSmptClient, CmsSettings.DefaultEmail, CmsSettings.DefaultEmailName);
            emailService.SendEmailWithModel(model.Email, "Ваш пароль було скинуто", "PasswordReseted", model, viewBag);

            return PartialView("_prtUserEditComplete", model);
        }

        public ActionResult Block(int id)
        {
            userService.Block(id);
            return null;
        }

        public ActionResult Unblock(int id)
        {
            userService.Unblock(id);
            return null;
        }

        public ActionResult Remove(int id)
        {
            userService.Remove(id);
            return null;
        }
        
        public ActionResult EmailFree(int id, string email)
        {
           return Json(userService.EmailFree(id, email), JsonRequestBehavior.AllowGet);
        }
    }
}