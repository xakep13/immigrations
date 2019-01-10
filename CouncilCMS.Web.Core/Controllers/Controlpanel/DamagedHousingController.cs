using Bissoft.CouncilCMS.BLL.Services;
using Bissoft.CouncilCMS.BLL.ViewModels;
using Bissoft.CouncilCMS.Core.Enums;
using Bissoft.CouncilCMS.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Bissoft.CouncilCMS.Web.Areas.ControlPanel.Controllers
{
	public class DamagedHousingController : BaseCmsController
	{
		private DamagedHousingAdminService damagedHousingService;
		private CmsSettingsAdminService cmsSettingsAdminService;
		private readonly CmsSettingsEdit cmsSettingEdit;

		public DamagedHousingController() : base()
		{
			damagedHousingService = new DamagedHousingAdminService(ConnectionString);
			cmsSettingsAdminService = new CmsSettingsAdminService(connectionString);
			cmsSettingEdit = cmsSettingsAdminService.GetForEdit();
		}

		public ActionResult List(string query = null, string dateRange = null, int dateType = 0, int? categoryId = null, int itemState = 1, int page = 1, String sortBy = "PublishDate", int sortDirection = 1, int? user = null, int userAction = 0, Boolean AjaxMode = false)
		{
			if(User.HasPremissions("articles"))
			{
				var userId = User.HasPremission("cr_articles_full") ? null : (int?)User.Identity.UserId;

				if(AjaxMode)
				{
					var model = damagedHousingService.GetList(
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
						count: out int count);

					ViewBag.Count = count;
					ViewBag.Page = page;
					ViewBag.PerPage = 20;

					return PartialView("_prtDamagedHousingList", model);
				}
				else
				{
					var model = damagedHousingService.List(
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

			var model = damagedHousingService.GetForEdit(Id, userId);

			return PartialView("_prtDamagedHousingEdit", model);
		}

		public ActionResult PagePrev(Int32 id = 0)
		{
			var model = damagedHousingService.GetForEdit(id);
			return PartialView("_prtDamagedHousingPrev", model);
		}

		[HttpPost]
		public ActionResult Save(DamagedHousingEdit model)
		{

			damagedHousingService.Save(model);

			if(model.Published)
				return PartialView("_prtDamagedHousingEditComplete", model);
			else
				return Redirect("/uk/damagedhousing/category/all-houses/");
			;
		}

		public ActionResult Delete(Int32 Id)
		{
			damagedHousingService.Delete(Id);

			return null;
		}

		public ActionResult Restore(Int32 Id)
		{
			damagedHousingService.Restore(Id);

			return null;
		}
	}

}
