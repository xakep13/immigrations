using System;
using System.Web.Mvc;
using Bissoft.CouncilCMS.BLL.Services;
using Bissoft.CouncilCMS.Core.Enums;
using Bissoft.CouncilCMS.BLL.ViewModels;
using reCAPTCHA.MVC;

namespace Bissoft.CouncilCMS.Web.Controllers
{
    public class OrganizationsController : BaseCmsController
    {
        CmsEnterpriseService enterpriseService;

        public OrganizationsController()
        {
            enterpriseService = new CmsEnterpriseService(connectionString);
        }

        public ActionResult Category(String Url, Int32 Page = 1)
        {
            var model = enterpriseService.CategoryList(Url, Page);

            if (model != null && model.CategoryId > 0)
            {
                ViewBag.PageType = MenuItemType.EnterpriseCategory;
                ViewBag.PageValue = model.CategoryId;

                return View(model);
            }
            else
            {
                return new HttpNotFoundResult();
            }
        }

        public ActionResult Item(Int32 id, String Url)
        {
            var userId = User != null && User.Identity != null && User.Identity.UserId > 0 ? (int?)User.Identity.UserId : null;
            var model = enterpriseService.Enterprise(id, userId);

            if (model != null)
            {
                ViewBag.PageType = MenuItemType.Enterprise;
                ViewBag.PageValue = model.ParentId > 0 ? model.ParentId : id;
                ViewBag.SecondPageType = MenuItemType.EnterpriseCategory;
                ViewBag.SecondPageValue = model.CategoryId;

                return View(model);
            }
            else
            {
                return new HttpNotFoundResult();
            }            
        }

        [CaptchaValidator]
        public ActionResult SendMail(CmsEnterpriseReception model, bool captchaValid)
        {
            var email = enterpriseService.GetEmail(model.EnterpriseId);

            if (ModelState.IsValid)
            {
                if (email != null)
                {
                    var viewBag = new RazorEngine.Templating.DynamicViewBag();
                    model.IpAddress = Request.UserHostAddress;

                    viewBag.AddValue("Domain", CmsSettings.BaseDomain);

                    var emailService = new EmailService(this.DefaultSmptClient, CmsSettings.DefaultEmail, CmsSettings.DefaultEmailName);
                    emailService.SendEmailWithModel(email, "Отримано запит через офіційний портал ЗМР", "EnterpriseReception", model, viewBag);

                    return PartialView("_prtContactFormSent", model);
                }
            }
                
            return null;
        }
    }
}