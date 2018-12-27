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
    public class CmsSettingsController : BaseCmsController
    {
        private CmsSettingsAdminService cmsSettingsAdminService;

        public CmsSettingsController()
        {
             cmsSettingsAdminService = new CmsSettingsAdminService(this.connectionString);
        }

        public ActionResult Edit()
        {
            var model = cmsSettingsAdminService.GetForEdit();

            return View(model);
        }

        [HttpPost]
        public ActionResult Save(CmsSettingsEdit Model)
        {
            cmsSettingsAdminService.Save(Model);

            return PartialView("_prtCmsSettingsEditComplete", Model);
        }
    }
}