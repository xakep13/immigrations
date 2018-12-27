using System;
using System.Configuration;
using System.Web.Mvc;
using Bissoft.CouncilCMS.Core.Enums;
using Bissoft.CouncilCMS.BLL.ViewModels;
using Bissoft.CouncilCMS.BLL.Services;
using System.Collections.Generic;
using Bissoft.CouncilCMS.Web.Controllers;
using Bissoft.CouncilCMS.Web.Filters;

using System.Linq;

namespace Bissoft.CouncilCMS.Web.Areas.ControlPanel.Controllers
{
    [CmsAuthorize]
    public class MenusController : BaseCmsController
    {
        private MenuAdminService menuAdminService;

        public MenusController()
        {
            menuAdminService = new MenuAdminService(this.connectionString);
        }

        public ActionResult List(Boolean AjaxMode = false)
        {
            if (User.HasPremissions("menu"))
            {
                var userId = User.HasPremission("menu") ? null : (int?)User.Identity.UserId;

                if (AjaxMode)
                {
                    List<MenuListItem> model = menuAdminService.GetListItems(userId);
                    /*
                    foreach (var menuItem in model)
                    {
                        if(menuItem.Index == null)
                    }
                    */
                    model.OrderBy(x => x.Index);
                    /*
                    foreach (var list in model)
                    {
                        list.MenuItems = list.MenuItems.OrderBy(x => x.Position).ToList();
                    }
                    */
                    return PartialView("_prtMenuList", model);
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

        public ActionResult GetMenuForm(Int32 Id = 0)
        {
            var model = menuAdminService.GetMenuEdit(Id);

            return PartialView("_prtMenuEdit", model);
        }

        public ActionResult GetMenuItemForm(Int32 MenuId, Int32 Id = 0, Int32? ParentId = null)
        {
            var model = menuAdminService.GetMenuItemEdit(Id, MenuId, ParentId);

            return PartialView("_prtMenuItemEdit", model);
        }

        [HttpPost]
        public ActionResult SaveMenu(MenuEdit Model)
        {
            if (!ModelState.IsValid)
                return PartialView("_prtMenuEdit", Model);

            var model = menuAdminService.SaveMenu(Model);

            if (Model.Id > 0)
            {
                return PartialView("_prtMenuListItem", model);
            }
            else
            {
                return PartialView("_prtMenuList", new List<MenuListItem> { model });
            }
        }

        [HttpPost]
        public ActionResult SaveMenuItem(MenuItemEdit Model)
        {
            if (!ModelState.IsValid)
                return PartialView("_prtMenuItemEdit", Model);

            var model = menuAdminService.SaveMenuItem(Model);

            if (Model.Id > 0)
            {
                return PartialView("_prtMenuItemListItem", model);
            }
            else
            {
                return PartialView("_prtMenuItemList", new List<MenuItemListItem> { model });
            }
        }
        
        [HttpPost]
        public ActionResult UpdateSubMenuPosition(BLL.ViewModels.Admin.MenuSubItemDTO body)
        {
            
            menuAdminService.UpdateSubMenuPosition(body.MenuId, body.ParentId, body.MenuItems);
            
            return null;
            
        }

        [HttpPost]
        public ActionResult RemoveMenu(Int32 Id)
        {
            menuAdminService.Remove(Id);

            return null;
        }

        [HttpPost]
        public ActionResult RemoveMenuItem(Int32 Id)
        {
            menuAdminService.RemoveMenuItem(Id);

            return null;
        }

        public ActionResult AddRole(AllowedRole model)
        {
            var result = menuAdminService.AddRole(model);

            if (result != null)
                return PartialView("_prtAllowedRoleItem", result);
            else
                return null;
        }

        public ActionResult DeleteRole(Int32 ItemId, Int32 UserId)
        {
            menuAdminService.DeleteRole(ItemId, UserId);
            return null;
        }
    }
}