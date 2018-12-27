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
    public class BannersController : BaseCmsController
    {
        private BannerAdminService bannerService;

        public BannersController()
        {
            bannerService = new BannerAdminService(this.ConnectionString);
        }

        public ActionResult List(Boolean AjaxMode = false)
        {
            if (AjaxMode)
            {
                var model = bannerService.GetList();

                return PartialView("_prtBannerList", model);
            }
            else
            {
                return View();
            }
        }


        public ActionResult GetForm(Int32 Id = 0)
        {
            var model = bannerService.GetForm(Id);

            return PartialView("_prtBannerEdit", model);
        }
       
        [HttpPost]
        public ActionResult Save(BannerEdit Model)
        {
            if (!ModelState.IsValid)
                return PartialView("_prtBannerEdit", Model);

            bannerService.SaveItem(Model);

            return PartialView("_prtBannerEditComplete", Model);
        }

        [HttpPost]
        public ActionResult UpdatePosition(Int32 Id, Int32 OldPosition, Int32 NewPosition)
        {
            bannerService.UpdatePosition(Id, OldPosition, NewPosition);

            return null;
        }
    }
}