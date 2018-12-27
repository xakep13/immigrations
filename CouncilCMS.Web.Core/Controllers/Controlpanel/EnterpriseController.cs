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
    public class EnterprisesController : BaseCmsController
    {       
        private EnterpriseAdminService enterpriseService;

        public EnterprisesController() : base()
        {
            enterpriseService = new EnterpriseAdminService(this.ConnectionString);
        }


        public ActionResult List(string query = null, string dateRange = null, int dateType = 0, int? categoryId = null, int itemState = 1, int page = 1, String sortBy = "CreateDate", int sortDirection = 1, int? user = null, int userAction = 0, bool ajaxMode = false)
        {
            if (User.HasPremissions("enterprises"))
            {
                var userId = User.HasPremission("cr_enterprises_full") ? null : (int?)User.Identity.UserId;

                if (ajaxMode)
                {
                    var count = 0;
                    var model = enterpriseService.GetList(query: query,
                        page: page,
                        categoryId: categoryId,
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

                    return PartialView("_prtEnterpriseList", model);
                }
                else
                {
                    var model = enterpriseService.List(query: query,
                        page: page,
                        categoryId: categoryId,
                        dateRange: dateRange,
                        dateType: dateType,
                        sortBy: sortBy,
                        sortDir: sortDirection,
                        itemState: (CmsItemState)itemState,
                        user: user,
                        userAction: userAction,
                        userId: userId);

                    return View(model);
                }
            }
            else
            {
                return new HttpUnauthorizedResult();
            }
        }

        public ActionResult GetForm(Int32 Id = 0, Int32? ParentId = null)
        {
            var userId = User.HasPremission("cr_enterprises_full") ? null : (int?)User.Identity.UserId;

            var model = enterpriseService.GetForm(Id, ParentId, userId);

            return PartialView("_prtEnterpriseEdit", model);
        }

        public ActionResult GetPersonForm(Int32 PersonId, Int32 EnterpriseId)
        {
            var model = new EnterprisePersonItem()
            {
                Id = 0,
                PersonId = PersonId,
                EnterpriseId = EnterpriseId                
            };

            return PartialView("_prtEnterprisePersonAdd", model);
        }

        public ActionResult Save(EnterpriseEdit model)
        {
            enterpriseService.Save(model);

            return PartialView("_prtEnterpriseEditComplete", model);
        }

        public ActionResult Delete(Int32 Id)
        {
            enterpriseService.Delete(Id);

            return null;
        }

        public ActionResult Restore(Int32 Id)
        {
            enterpriseService.Restore(Id);

            return null;
        }

        public ActionResult GetCategory(Int32 EnterpriseId, Int32 CategoryId)
        {
            var result = enterpriseService.GetCategory(CategoryId, EnterpriseId);

            if (result != null)
                return PartialView("_prtEnterpriseCategoryItemEdit", result);
            else
                return null;
        }

        public ActionResult GetPerson(Int32 EnterpriseId, Int32 PersonId)
        {
            var result = enterpriseService.GetPerson(PersonId, EnterpriseId);

            if (result != null)
                return PartialView("_prtEnterprisePersonItemEdit", result);
            else
                return null;
        }

        public ActionResult SaveCategory(EnterpriseCategoryItem model)
        {
            var result = enterpriseService.SaveCategory(model);

            if (result != null)
                return PartialView("_prtEnterpriseCategoryItem", result);
            else
                return null;
        }

        public ActionResult SavePerson(EnterprisePersonItem model)
        {
            var result = enterpriseService.SavePerson(model);

            if (result != null)
                return PartialView("_prtEnterprisePersonItem", result);
            else
                return null;
        }

        public ActionResult DeleteCategory(Int32 EnterpriseId, Int32 CategoryId)
        {
            enterpriseService.DeleteCategory(CategoryId, EnterpriseId);
            return null;
        }
        public ActionResult DeletePerson(Int32 EnterpriseId, Int32 PersonId)
        {
            enterpriseService.DeletePerson(PersonId, EnterpriseId);
            return null;
        }

        public ActionResult AddUser(AllowedUser model)
        {
            var result = enterpriseService.AddUser(model);

            if (result != null)
                return PartialView("_prtAllowedUserItem", result);
            else
                return null;
        }

        public ActionResult DeleteUser(Int32 ItemId, Int32 UserId)
        {
            enterpriseService.DeleteUser(ItemId, UserId);
            return null;
        }
    }
}