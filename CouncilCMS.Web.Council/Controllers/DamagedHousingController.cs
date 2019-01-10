﻿using Bissoft.CouncilCMS.BLL.Services;
using Bissoft.CouncilCMS.BLL.ViewModels;
using Bissoft.CouncilCMS.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bissoft.CouncilCMS.Web.Controllers
{
	public class DamagedHousingController : BaseCmsController
	{
		private readonly int perPage = 4;

		CmsDamagedHousingService damagedHousingService;
		CmsUserAdminService userAdminService;

		public DamagedHousingController()
		{
			damagedHousingService = new CmsDamagedHousingService(connectionString);
			userAdminService = new CmsUserAdminService(connectionString);
		}

		public ActionResult Category(String Url, String Date = null, String Query = null, Int32 Page = 1)
		{
			var model = damagedHousingService.CategoryList(articleUrl: Url, Mode: CmsSettings.ShowMenuItemMode, query: Query, date: Date, page: Page, perPage: perPage);
			if(model != null && model.CategoryId > 0)
			{
				ViewBag.PageType = MenuItemType.DamagedHousingCategory;
				ViewBag.PageValue = model.CategoryId;

				return View(model);
			}
			else
				return new HttpNotFoundResult();
		}

		public ActionResult Item(Int32 id, String Url)
		{
			var userId = User != null && User.Identity != null && User.Identity.UserId > 0 ? (int?)User.Identity.UserId : null;

			CmsDamagedHousing model = damagedHousingService.Article(id, CmsSettings.ShowMenuItemMode, userId);

			if(model != null)
			{
				ViewBag.PageValue = id;
				ViewBag.isAdmin = false;
				ViewBag.PageType = MenuItemType.DamagedHousing;
				ViewBag.SecondPageValue = model.CategoryId;
				ViewBag.SecondPageType = MenuItemType.DamagedHousingCategory;

				if(User.Identity.IsAuthenticated && userAdminService.IsAdmin(User.Identity.UserId))
					ViewBag.isAdmin = true;

				return View(model);
			}
			else
				return new HttpNotFoundResult();
		}

		public ActionResult AppForm()
		{
			var model = damagedHousingService.GetForEdit();

			return View(model);
		}

		public ActionResult EventCalendar(string Url)
		{
			var model = damagedHousingService.GetEventCalendar(Url);

			return Json(model, JsonRequestBehavior.AllowGet);
		}
	}
}