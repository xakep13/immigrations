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
    public class PagesController : BaseCmsController
    {
        private PageAdminService pageService;

        public PagesController() : base()
        {
            pageService = new PageAdminService(this.ConnectionString);
        }

        public ActionResult List(String query = null, string dateRange = null, int dateType = 0, int itemState = 1, int page = 1, String sortBy = "CreateDate", int sortDirection = 1, int? user = null, int userAction = 0, bool ajaxMode = false)
        {
            if (User.HasPremissions("pages"))
            {
                if (ajaxMode)
                {
                    var userId = User.HasPremission("ed_pages_full") ? null : (int?)User.Identity.UserId;

                    var count = 0;
                    var model = pageService.GetList(
                        query: query,
                        page: page,
                        dateRange: dateRange,
                        dateType: dateType,
                        sortBy: sortBy,
                        sortDir: sortDirection,
                        itemState: (CmsItemState)itemState,
                        userId: userId,
                        user: user,
                        userAction: userAction,
                        count: out count);

                    ViewBag.Count = count;
                    ViewBag.Page = page;
                    ViewBag.PerPage = 20;

                    return PartialView("_prtPageList", model);
                }
                else
                {
                    var model = pageService.List(
                        query: query,
                        page: page,
                        dateRange: dateRange,
                        dateType: dateType,
                        sortBy: sortBy,
                        sortDir: sortDirection,
                        user: user,
                        userAction: userAction,
                        itemState: (CmsItemState)itemState);
                    return View(model);
                }
            }
            else
            {
                return new HttpUnauthorizedResult();
            } 
        }

        public ActionResult GetForm(Int32 Id = 0)
        {
            var model = pageService.GetForEdit(Id);

            return PartialView("_prtPageEdit", model);
        }
        public ActionResult PagePrev(Int32 id = 0)
        {
            var model = pageService.GetForEdit(id);
            return PartialView("_prtPagePrev", model);
        }
        public ActionResult Save(PageEdit model)
        {
            model = pageService.Save(model);

            return PartialView("_prtPageEditComplete", model);
        }

        public ActionResult Delete(Int32 Id)
        {
            pageService.Delete(Id);

            return null;
        }

        public ActionResult Restore(Int32 Id)
        {
            pageService.Restore(Id);

            return null;
        }

        public ActionResult AddUser(AllowedUser model)
        {
            var result = pageService.AddUser(model);

            if (result != null)
                return PartialView("_prtAllowedUserItem", result);
            else
                return null;
        }

        public ActionResult DeleteUser(Int32 ItemId, Int32 UserId)
        {
            pageService.DeleteUser(ItemId, UserId);
            return null;
        }
    }
}