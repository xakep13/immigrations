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
    public class RolesController : BaseCmsController
    {
        private CmsRoleAdminService roleService;

        public RolesController() : base()
        {
            roleService = new CmsRoleAdminService(this.ConnectionString);
        }

        public ActionResult List(bool AjaxMode = false)
        {
            if (AjaxMode)
            {
                var model = roleService.GetList();

                return PartialView("_prtRoleList", model);
            }
            else
            {
                return View();
            }
        }

        public ActionResult GetForm(Int32 Id = 0)
        {
            var model = roleService.GetForm(Id);

            return PartialView("_prtRoleEdit", model);
        }

        [HttpPost]
        public ActionResult Save(CmsRoleEdit Model)
        {
            this.ResolveLocalModelState();

            if (!ModelState.IsValid)
                return PartialView("_prtRoleEdit", Model);

            var result = roleService.Save(Model);

            return PartialView("_prtRoleListItem", result);
        }

        public ActionResult Remove(int id)
        {
            roleService.Remove(id);

            return null;
        }
    }
}