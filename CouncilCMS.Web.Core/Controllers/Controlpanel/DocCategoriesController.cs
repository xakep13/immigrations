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
    public class DocCategoriesController : BaseCmsController
    {
        private DocCategoryAdminService categoryAdminService;

        public DocCategoriesController()
        {
            categoryAdminService = new DocCategoryAdminService(this.ConnectionString);
        }

        public ActionResult List(Boolean AjaxMode = false)
        {
            if (User.HasPremissions("_cat"))
            {
                if (AjaxMode)
                {
                    var userId = User.HasPremission("cr_cat") ? null : (int?)User.Identity.UserId;

                    var model = categoryAdminService.GetList(userId);

                    return PartialView("_prtCategoryList", model);
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return new HttpUnauthorizedResult();
            }
        }


        public ActionResult GetItemForm(Int32 Id = 0, Int32? ParentId = null)
        {
            var model = categoryAdminService.GetCategoryEdit(Id, ParentId);

            return PartialView("_prtDocCategoryItemEdit", model);
        }

        [HttpPost]
        public ActionResult SaveItem(DocCategoryEdit Model)
        {
            this.ResolveLocalModelState();

            if (!ModelState.IsValid)
                return PartialView("_prtCategoryItemEdit", Model);

            var model = categoryAdminService.SaveItem(Model);

            if (Model.Id > 0)
            {
                return PartialView("_prtCategoryListItem", model);
            }
            else
            {
                return PartialView("_prtCategoryList", new List<CategoryListItem> { model });
            }
        }

        [HttpPost]
        public ActionResult UpdatePosition(Int32 Id, Int32 OldPosition, Int32 NewPosition, Int32 MenuId, Int32? ParentId)
        {
            categoryAdminService.UpdatePosition(Id, ParentId, OldPosition, NewPosition);

            return null;
        }
        [HttpPost]
        public ActionResult UpdateSubPosition(BLL.ViewModels.Admin.MenuSubItemDTO body)
        {
            categoryAdminService.UpdatePositionNew(body.ParentId, body.MenuId, body.MenuItems);
            return null;
        }
        
        public ActionResult RemoveItem(int id)
        {
            categoryAdminService.Remove(id);
            return null;
        }

        public ActionResult AddUser(AllowedUser model)
        {
            var result = categoryAdminService.AddUser(model);

            if (result != null)
                return PartialView("_prtAllowedUserItem", result);
            else
                return null;
        }

        public ActionResult DeleteUser(Int32 ItemId, Int32 UserId)
        {
            categoryAdminService.DeleteUser(ItemId, UserId);
            return null;
        }

        public ActionResult AddRole(AllowedRole model)
        {
            var result = categoryAdminService.AddRole(model);

            if (result != null)
                return PartialView("_prtAllowedRoleItem", result);
            else
                return null;
        }

        public ActionResult DeleteRole(Int32 ItemId, Int32 UserId)
        {
            categoryAdminService.DeleteRole(ItemId, UserId);
            return null;
        }
    }
}