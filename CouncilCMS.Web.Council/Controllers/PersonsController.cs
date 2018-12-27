using System;
using System.Web.Mvc;
using Bissoft.CouncilCMS.BLL.Services;
using Bissoft.CouncilCMS.Core.Enums;
using Bissoft.CouncilCMS.BLL.ViewModels;
using reCAPTCHA.MVC;

namespace Bissoft.CouncilCMS.Web.Controllers
{
    public class PersonsController : BaseCmsController
    {
        CmsPersonService personService;

        public PersonsController()
        {
            personService = new CmsPersonService(this.ConnectionString);
        }

        public ActionResult Category(String Url, Int32 Page = 1)
        {
            var model = personService.CategoryList(Url, CmsSettings.ShowMenuItemMode);

            if (model != null && model.CategoryId > 0)
            {
                ViewBag.PageType = MenuItemType.PersonCategory;
                ViewBag.PageValue = model.CategoryId;

                return View(model);
            }
            else
                return new HttpNotFoundResult();
                
        }

        public ActionResult Item(Int32 id, String Url)
        {
            var userId = User != null && User.Identity != null && User.Identity.UserId > 0 ? (int?)User.Identity.UserId : null;
            var model = personService.Person(id, CmsSettings.ShowMenuItemMode, userId);

            if (model != null)
            {
                ViewBag.PageType = MenuItemType.Person;
                ViewBag.PageValue = id;
                ViewBag.SecondPageType = MenuItemType.PersonCategory;
                ViewBag.SecondPageValue = model.CurrentCategoryId;

                return View(model);
            }
            else
            {
                return new HttpNotFoundResult();
            }            
        }


        [CaptchaValidator]
        public ActionResult SendMail(CmsPersonReception model, bool captchaValid)
        {
            if (ModelState.IsValid)
            {
                var email = personService.GetEmail(model.PersonId);

                if (email != null)
                {
                    model.IpAddress = Request.UserHostAddress;
                    var viewBag = new RazorEngine.Templating.DynamicViewBag();

                    viewBag.AddValue("Domain", CmsSettings.BaseDomain);

                    var emailService = new EmailService(this.DefaultSmptClient, CmsSettings.DefaultEmail, CmsSettings.DefaultEmailName);
                    emailService.SendEmailWithModel(email, "Отримано запит через офіційний портал ЗМР", "PersonReception", model, viewBag);

                    return PartialView("_prtContactFormSent", model);
                }
            }

            return null;
        }
    }
}