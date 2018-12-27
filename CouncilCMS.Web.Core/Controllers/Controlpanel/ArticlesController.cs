using System;
using System.Configuration;
using System.Web.Mvc;
using Bissoft.CouncilCMS.Core.Enums;
using Bissoft.CouncilCMS.BLL.ViewModels;
using Bissoft.CouncilCMS.BLL.Services;
using System.Collections.Generic;
using Bissoft.CouncilCMS.Web.Controllers;
using Bissoft.CouncilCMS.Web.Filters;
using System.Net.Http;
using Bissoft.CouncilCMS.Web;
using System.Web;
using System.Net;
using System.Threading.Tasks;
using System.Threading;
using Bissoft.CouncilCMS.Web.Core.ExternalLogins;
using Bissoft.CouncilCMS.BLL.ViewModels.Admin;

namespace Bissoft.CouncilCMS.Web.Areas.ControlPanel.Controllers
{
    [CmsAuthorize]
    public class ArticlesController : BaseCmsController
    {
        private ArticleAdminService articleService;
        private CmsSettingsAdminService cmsSettingsAdminService;
        private CmsSettingsEdit cmsSettingEdit;

        private FacebookPagePostService facebookPagePostService;

        public ArticlesController() : base()
        {
            articleService = new ArticleAdminService(this.ConnectionString);
            cmsSettingsAdminService = new CmsSettingsAdminService(this.connectionString);
            cmsSettingEdit = cmsSettingsAdminService.GetForEdit();
        }

        public ActionResult List(string query = null, string dateRange = null, int dateType = 0, int? categoryId = null, int itemState = 1, int page = 1, String sortBy = "PublishDate", int sortDirection = 1, int? user = null, int userAction = 0, Boolean AjaxMode = false)
        {
            if (User.HasPremissions("articles"))
            {
                var userId = User.HasPremission("cr_articles_full") ? null : (int?)User.Identity.UserId;

                if (AjaxMode)
                {
                    var count = 0;
                    var model = articleService.GetList(
                        query: query,
                        page: page,
                        categoryId: categoryId,
                        dateRange: dateRange,
                        dateType: dateType,
                        sortBy: sortBy,
                        sortDir: sortDirection,
                        itemState: (CmsItemState)itemState,
                        user: user,
                        userAction: userAction,
                        userId: userId,
                        count: out count);

                    ViewBag.Count = count;
                    ViewBag.Page = page;
                    ViewBag.PerPage = 20;

                    return PartialView("_prtArticleList", model);
                }
                else
                {
                    var model = articleService.List(
                        query: query,
                        page: page,
                        categoryId: categoryId,
                        dateRange: dateRange,
                        dateType: dateType,
                        sortBy: sortBy,
                        sortDir: sortDirection,
                        userId: userId,
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
            var userId = User.HasPremission("cr_articles_full") ? null : (int?)User.Identity.UserId;

            var model = articleService.GetForEdit(Id, userId);

            return PartialView("_prtArticleEdit", model);
        }
        public ActionResult PagePrev(Int32 id = 0)
        {
            var model = articleService.GetForEdit(id);
            return PartialView("_prtArticlePrev", model);
        }

        [HttpPost]
        public async Task<ActionResult> Save(ArticleEdit model)
        {
            if (model.FacebookSharing == true)
            {
                ;
            }
            articleService.Save(model);

            return PartialView("_prtArticleEditComplete", model);
        }

        public ActionResult Delete(Int32 Id)
        {
            articleService.Delete(Id);

            return null;
        }

        public ActionResult Restore(Int32 Id)
        {
            articleService.Restore(Id);

            return null;
        }
    }
}