using System;
using System.Web.Mvc;
using Bissoft.CouncilCMS.BLL.Services;
using Bissoft.CouncilCMS.BLL.ViewModels;
using Bissoft.CouncilCMS.Core.Enums;

namespace Bissoft.CouncilCMS.Web.Controllers
{
	public class ArticlesController : BaseCmsController
	{
		private Int32 perPage = 5;

		CmsArticleService articleService;
		CmsUserAdminService userAdminService;

		public ArticlesController()
		{
			articleService = new CmsArticleService(connectionString);
			userAdminService = new CmsUserAdminService(connectionString);
		}

		public ActionResult LastArticles(string url, Int32 limit = 5, int? excludeId = null)
		{
			var model = articleService.LastArticles(url, limit, excludeId);
			return PartialView("_prtLastArticles", model);
		}

		public ActionResult Category(String Url, String Date = null, String Query = null, Int32 Page = 1)
		{
			var model = articleService.CategoryList(articleUrl: Url, Mode: CmsSettings.ShowMenuItemMode, query: Query, date: Date, page: Page, perPage: perPage);
			if(model != null && model.CategoryId > 0)
			{
				ViewBag.PageType = MenuItemType.ArticleCategory;
				ViewBag.PageValue = model.CategoryId;

				return View(model);
			}
			else
			{
				return new HttpNotFoundResult();
			}
		}

		public ActionResult Item(Int32 id, String Url)
		{
			var userId = User != null && User.Identity != null && User.Identity.UserId > 0 ? (int?)User.Identity.UserId : null;


			CmsArticle model = articleService.Article(id, CmsSettings.ShowMenuItemMode, userId);

			if(model != null)
			{
				ViewBag.PageType = MenuItemType.Article;
				ViewBag.PageValue = id;
				ViewBag.SecondPageType = MenuItemType.ArticleCategory;
				ViewBag.SecondPageValue = model.CategoryId;

				if(User.Identity.IsAuthenticated)
				{
					if(userAdminService.IsAdmin(User.Identity.UserId))
						ViewBag.isAdmin = true;
					else
						ViewBag.isAdmin = false;
				}
				else
					ViewBag.isAdmin = false;

				return View(model);
			}
			else
			{
				return new HttpNotFoundResult();
			}
		}

		public ActionResult EventCalendar(string Url)
		{
			var model = articleService.GetEventCalendar(Url);

			return Json(model, JsonRequestBehavior.AllowGet);
		}

		public ActionResult GetMajorLast(String Url)
		{
			var model = articleService.LastArticleInCategory(Url);

			return PartialView("_prtMajorLast", model);
		}
	}
}