using System;
using System.Web.Mvc;
using Bissoft.CouncilCMS.BLL.Services;
using Bissoft.CouncilCMS.Core.Enums;
using Bissoft.CouncilCMS.BLL.ViewModels;
using reCAPTCHA.MVC;
using Bissoft.CouncilCMS.BLL.ViewModels.Public;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace Bissoft.CouncilCMS.Web.Controllers
{
    public class WidgetsController : BaseCmsController
    {
        private CmsSessionService sessionService;
        private CmsArticleService articleService;
        private CmsWidgetService widgetService;
        private CmsBannerService bannerService;
        private CmsMenuService menuService;

        public WidgetsController()
        {
            articleService = new CmsArticleService(connectionString);
            sessionService = new CmsSessionService(connectionString);
            widgetService = new CmsWidgetService(connectionString);
            menuService = new CmsMenuService(connectionString);
        }

        public ActionResult GetMajorLast(String Url)
        {
            var model = articleService.LastArticleInCategory(Url);

            return PartialView("_prtMajorLast", model);
        }

        public ActionResult Eservices()
        {
            var model = menuService.GetMenu("service-menu", CmsSettings.ShowMenuItemMode);

            return PartialView("_prtEservices", model);
        }

        public ActionResult ContactForm()
        {
            return PartialView("_prtContactForm");
        }

        public ActionResult SessionFilter(String page = "projects", int? current = null)
        {
            var model = sessionService.List();

            model.SessionPage = page;
            ViewBag.Id = current;

            return PartialView("_prtSessionFilter", model);
        }

        [CaptchaValidator]
        public ActionResult SendFeedback(CmsContactReception model, bool captchaValid)
        {
            if (ModelState.IsValid)
            {
                var viewBag = new RazorEngine.Templating.DynamicViewBag();
                model.IpAddress = Request.UserHostAddress;

                viewBag.AddValue("Domain", CmsSettings.BaseDomain);

                var emailService = new EmailService(this.DefaultSmptClient, CmsSettings.DefaultEmail, CmsSettings.DefaultEmailName);
                emailService.SendEmailWithModel(CmsSettings.FeedbackEmail, "Отримано запит через офіційний портал ЗМР", "ContactReception", model, viewBag);

                return PartialView("_prtContactFormSent", model);
            }
            return null;
        }

        public ActionResult Banners()
        {
            bannerService = new CmsBannerService(this.ConnectionString);

            var model = bannerService.Banners();

            return PartialView("_prtBanners", model);
        }

        public ActionResult SimpleMenu(Int32 Id = 1)
        {
            var model = menuService.GetMenu(Id, CmsSettings.ShowMenuItemMode);

            return PartialView("_prtSimpleMenu", model);
        }

        public ActionResult AccordionMenu(Int32 Id = 1)
        {
            var model = menuService.GetMenu(Id, CmsSettings.ShowMenuItemMode);

            return PartialView("_prtAccordionMenu", model);
        }

        public ActionResult FooterMenu(string name)
        {
            var model = menuService.GetMenu(name, CmsSettings.ShowMenuItemMode);

            return PartialView("_prtFooterMenu", model);
        }

        [Route(Name = "RecordingAtReception")]
        public PartialViewResult RecordingAtReceptionPost(RecordingAtReceptionModel model)
        {
            if (ModelState.IsValid)
            {
                SmtpClient smtpClient = this.DefaultSmptClient;

                EmailService emailService = new EmailService(smtpClient, model.email , model.name);

                bool result = emailService.SendEmail(this.CmsSettings.DefaultEmail,"Звернення на прийом",model.comment);

                smtpClient.Dispose();
                ViewData["result"] = true;
                return PartialView("_prtRecordingAtReceptionDone");
            }
            else
            {
                ViewData["result"] = false;
                return PartialView("_prtRecordingAtReceptionForm",model);
            }
            
        }

        public PartialViewResult RecordingAtReceptionForm()
        {
            return PartialView("_prtRecordingAtReceptionForm");
        }

        public ActionResult RecordingAtReception()
		{
			return PartialView("_prtRecordingAtReception");
		}

		public ActionResult Poll(List<CmsPoll> polls)
		{
			return PartialView("_prtPollHome", polls);
		}

		public ActionResult EmergencyCalls()
		{
			return PartialView("_prtEmergencyCalls");
		}

		public ActionResult Subscribe()
		{
			return PartialView("_prtSubscribe");
		}
	}
}