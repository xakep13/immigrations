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
    public class DocTypesController : BaseCmsController
    {
        private DocTypeAdminService docTypeAdminService;

        public DocTypesController()
        {
            docTypeAdminService = new DocTypeAdminService(this.ConnectionString);
        }

        public ActionResult List(Boolean AjaxMode = false)
        {
            if (AjaxMode)
            {
                var model = docTypeAdminService.GetList();

                return PartialView("_prtDocTypeList", model);
            }
            else
            {
                return View();
            }
        }


        public ActionResult GetForm(Int32 Id = 0)
        {
            var model = docTypeAdminService.GetEdit(Id);

            return PartialView("_prtDocTypeEdit", model);
        }

        [HttpPost]
        public ActionResult SaveItem(DocTypeEdit Model)
        {
            this.ResolveLocalModelState();

            if (!ModelState.IsValid)
                return PartialView("_prtDocTypeEdit", Model);

            var model = docTypeAdminService.SaveItem(Model);
            
            return PartialView("_prtDocTypeListItem", model);
        }

        [HttpPost]
        public ActionResult UpdatePosition(Int32 Id, Int32 OldPosition, Int32 NewPosition, Int32 MenuId, Int32? ParentId)
        {
            docTypeAdminService.UpdatePosition(Id, ParentId, OldPosition, NewPosition);

            return null;
        }
    }
}