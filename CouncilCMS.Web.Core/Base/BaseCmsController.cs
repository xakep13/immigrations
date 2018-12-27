using Bissoft.CouncilCMS.BLL.Services;
using Bissoft.CouncilCMS.BLL.ViewModels;
using Bissoft.CouncilCMS.Core.Interfaces;
using Bissoft.CouncilCMS.Web.Core;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using xTab.Tools.Extensions;
using xTab.Tools.Helpers;

namespace Bissoft.CouncilCMS.Web.Controllers
{
    public abstract class BaseCmsController : Controller, ICmsController
    {
        protected string connectionString;

        public BaseCmsController()
        {
            connectionString = ConfigurationManager.ConnectionStrings["CmsConnection"].ConnectionString;
        }

        public virtual new CmsPrincipal User
        {
            get { return base.User as CmsPrincipal; }
        }

        public CmsAppSettings CmsSettings
        {
            get
            {
                CmsAppSettings settings;
                var appSettings = HttpContext.Application["CmsSettings"];

                if (appSettings == null)
                {
                    var settingsService = new SettingsService(this.connectionString);
                    settings = settingsService.GetCmsSettings();
                    HttpContext.Application["CmsSettings"] = settings;
                }
                else
                {
                    settings = (CmsAppSettings)appSettings;
                }
                return settings;
            }
        }

        public string ConnectionString
        {
            get
            {
                return this.connectionString;
            }
        }

        public SmtpClient DefaultSmptClient
        {
            get
            {
				var smtpClient = new SmtpClient(CmsSettings.DefaultEmailHost, CmsSettings.DefaultEmailPort)
				{
					Credentials = new NetworkCredential(CmsSettings.DefaultEmailUser, CmsSettings.DefaultEmailPassword),
					EnableSsl = CmsSettings.DefaultEmailSsl
				};

				return smtpClient;
            }
        }

        protected void ResolveLocalModelState(String cultures = "uk")
        {
            var allCultures = new String[] { "uk", "ru", "en" };
            var allowedCultures = cultures.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var lang in allCultures)
            {
                if (!allowedCultures.Contains(lang))
                {
                    ModelState.RemoveLocalKeys(lang.Capitalize());
                }
            }
        }

        
        public String RenderToString(String ViewName, Object Model)
        {
            ViewData.Model = Model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, ViewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }
    }
}
