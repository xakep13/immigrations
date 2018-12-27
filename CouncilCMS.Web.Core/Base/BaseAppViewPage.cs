using Bissoft.CouncilCMS.BLL.Services;
using Bissoft.CouncilCMS.BLL.ViewModels;
using Bissoft.CouncilCMS.Web.Core;
using System;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace Bissoft.CouncilCMS.Web
{
    public abstract class BaseCmsViewPage : WebViewPage
    {
        protected string connectionString;

        public BaseCmsViewPage() : base()
        {
            connectionString = ConfigurationManager.ConnectionStrings["CmsConnection"].ConnectionString;
        }

        public BaseCmsViewPage(String connectionString) : base()
        {
            this.connectionString = connectionString;
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
                var appSettings = HttpContext.Current.Application["CmsSettings"];

                if (appSettings == null)
                {
                    var settingsService = new SettingsService(this.connectionString);
                    settings = settingsService.GetCmsSettings();
                    HttpContext.Current.Application["CmsSettings"] = settings;
                }
                else
                {
                    settings = (CmsAppSettings)appSettings;
                }


                return settings;
            }
        }
    }

    public abstract class BaseCmsViewPage<TModel> : WebViewPage<TModel>
    {
        protected string connectionString;
        public BaseCmsViewPage() : base()
        {
            connectionString = ConfigurationManager.ConnectionStrings["CmsConnection"].ConnectionString;
        }
        public BaseCmsViewPage(String connectionString) : base()
        {
            this.connectionString = connectionString;
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
                var appSettings = HttpContext.Current.Application["CmsSettings"];

                if (appSettings == null)
                {
                    var settingsService = new SettingsService(this.connectionString);
                    settings = settingsService.GetCmsSettings();
                    HttpContext.Current.Application["CmsSettings"] = settings;
                }
                else
                {
                    settings = (CmsAppSettings)appSettings;
                }


                return settings;
            }
        }

        public override string Layout
        {
            get
            {
                var layout = base.Layout;

                if (!string.IsNullOrEmpty(layout))
                {
                    var filename = Path.GetFileNameWithoutExtension(layout);
                    ViewEngineResult viewResult = System.Web.Mvc.ViewEngines.Engines.FindView(ViewContext.Controller.ControllerContext, filename, "");

                    if (viewResult.View != null && viewResult.View is RazorView)
                    {
                        layout = (viewResult.View as RazorView).ViewPath;
                    }
                }

                return layout;
            }
            set
            {
                base.Layout = value;
            }
        }
    }
}
