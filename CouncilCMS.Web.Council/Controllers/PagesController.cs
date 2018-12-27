using System;
using System.Web.Mvc;
using Bissoft.CouncilCMS.BLL.Services;
using Bissoft.CouncilCMS.Core.Enums;
using System.Web;

namespace Bissoft.CouncilCMS.Web.Controllers
{
    public class PagesController : BaseCmsController
    {
        private CmsPageService pageService;

        public PagesController()
        {
            pageService = new CmsPageService(connectionString);
        }
       
        public ActionResult Page(String Url)
        {
            var userId = User != null && User.Identity != null && User.Identity.UserId > 0 ? (int?)User.Identity.UserId : null;
            var model = pageService.GetPage(Url, CmsSettings.ShowMenuItemMode, userId);

            if (model != null)
            {
                ViewBag.PageType = MenuItemType.Page;
                ViewBag.PageValue = model.Id;

                return PartialView("Page", model);
            }
            else
            {
                return new HttpNotFoundResult();
            }            
        }
    }
}