using System;
using System.Web.Mvc;
using Bissoft.CouncilCMS.BLL.Services;
using Bissoft.CouncilCMS.BLL.ViewModels;
using Bissoft.CouncilCMS.Core.Enums;

namespace Bissoft.CouncilCMS.Web.Controllers
{
	public class ArticlesController : BaseCmsController
	{
		private readonly int perPage = 9;

		CmsArticleService articleService;
		CmsUserAdminService userAdminService;

		public ArticlesController()
		{
			articleService = new CmsArticleService(connectionString);
			userAdminService = new CmsUserAdminService(connectionString);
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
				ViewBag.PageValue = id;
				ViewBag.isAdmin = false;
				ViewBag.PageType = MenuItemType.Article;
				ViewBag.SecondPageValue = model.CategoryId;
				ViewBag.SecondPageType = MenuItemType.ArticleCategory;

				if(User.Identity.IsAuthenticated && userAdminService.IsAdmin(User.Identity.UserId))
					ViewBag.isAdmin = true;

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

		public ActionResult LastGeneralNews(string url= "general-news", int count = 6)
		{
			var model = articleService.LastArticles(url, count);

			return PartialView("_prtLastNews", model);
		}

		public ActionResult LastSecondNews(string url = "main-news-2", int count = 3)
		{
			var model = articleService.LastArticles(url, count);

			return PartialView("_prtSecondNews", model);
		}
	}
}