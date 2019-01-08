using Bissoft.CouncilCMS.BLL.Services;
using Bissoft.CouncilCMS.BLL.ViewModels;
using Bissoft.CouncilCMS.Web.Controllers;
using Bissoft.CouncilCMS.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Bissoft.CouncilCMS.Web.Areas.ControlPanel.Controllers
{
	[CmsAuthorize]
	public class DamagedHousingCategoriesController: BaseCmsController
	{
		private DamagedHousingCategoryAdminService damagedHousingCategoryAdminService;

		public DamagedHousingCategoriesController()
		{
			damagedHousingCategoryAdminService = new DamagedHousingCategoryAdminService(this.ConnectionString);
		}

		public ActionResult List(Boolean AjaxMode = false)
		{
			if(User.HasPremissions("_cat"))
			{
				if(AjaxMode)
				{
					var userId = User.HasPremission("cr_cat") ? null : (int?)User.Identity.UserId;

					var model = damagedHousingCategoryAdminService.GetList(userId);

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
			var model = damagedHousingCategoryAdminService.GetCategoryEdit(Id, ParentId);

			return PartialView("_prtDamagedHousingCategoryItemEdit", model);
		}

		[HttpPost]
		public ActionResult SaveItem(DamagedHousingCategoryEdit Model)
		{
			this.ResolveLocalModelState("uk");

			if(!ModelState.IsValid)
				return PartialView("_prDamagedHousingCategoryItemEdit", Model);

			var model = damagedHousingCategoryAdminService.SaveItem(Model);

			if(Model.Id > 0)
			{
				return PartialView("_prtCategoryListItem", model);
			}
			else
			{
				return PartialView("_prtCategoryList", new List<CategoryListItem> { model });
			}
		}

		[HttpPost]
		public ActionResult UpdatePosition(Int32 Id, Int32 OldPosition, Int32 NewPosition, Int32? ParentId = null)
		{
			damagedHousingCategoryAdminService.UpdatePosition(Id, ParentId, OldPosition, NewPosition);

			return null;
		}

		public ActionResult RemoveItem(int id)
		{
			damagedHousingCategoryAdminService.Remove(id);
			return null;
		}

		public ActionResult AddUser(AllowedUser model)
		{
			var result = damagedHousingCategoryAdminService.AddUser(model);

			if(result != null)
				return PartialView("_prtAllowedUserItem", result);
			else
				return null;
		}

		public ActionResult DeleteUser(Int32 ItemId, Int32 UserId)
		{
			damagedHousingCategoryAdminService.DeleteUser(ItemId, UserId);
			return null;
		}

		public ActionResult AddRole(AllowedRole model)
		{
			var result = damagedHousingCategoryAdminService.AddRole(model);

			if(result != null)
				return PartialView("_prtAllowedRoleItem", result);
			else
				return null;
		}

		public ActionResult DeleteRole(Int32 ItemId, Int32 UserId)
		{
			damagedHousingCategoryAdminService.DeleteRole(ItemId, UserId);
			return null;
		}


	}
}
