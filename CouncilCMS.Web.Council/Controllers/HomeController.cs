using System.Web.Mvc;
using Bissoft.CouncilCMS.BLL;
using Bissoft.CouncilCMS.BLL.Services;
using Bissoft.CouncilCMS.BLL.ViewModels;

namespace Bissoft.CouncilCMS.Web.Controllers
{
	public class HomeController : BaseCmsController
	{
		public CmsArticleService articleService;
		public CmsMenuService menuService;

		public HomeController()
		{
			var uow = new UnitOfWork(this.ConnectionString);
		
			articleService = new CmsArticleService(uow);
			menuService = new CmsMenuService(uow);			
		}

		public ActionResult Index()
		{
			var model = new MkIndex
			{
				ImmigrationsNews = articleService.LastArticles("immigrations-news", 3).Articles,
				RestoreObjects = articleService.LastArticles("restore-objects", 3).Articles,
				MainNewsSecond = articleService.LastArticles("main-news-2", 2).Articles,
				FamaliesNews = articleService.LastArticles("famalies-news", 5).Articles,
				GeneralNews = articleService.LastArticles("general-news", 6).Articles,
				SlidersNews = articleService.LastArticles("sliders-news", 3).Articles,
				MainNews = articleService.LastArticles("main-news", 1).Articles,
			};

			return View(model);
		}

		public ActionResult MainMenu()
		{
			var model = menuService.GetMenu("main-menu", CmsSettings.ShowMenuItemMode);
			
			return PartialView("_prtMainMenu", model);
		}

		public ActionResult BenefitsAndGuarantees()
		{
			var model = menuService.GetMenu("benefits-and-guarantees", CmsSettings.ShowMenuItemMode);

			return PartialView("_prtMainMenu", model);
		}

		public ActionResult Consultations()
		{
			return PartialView("_prtConsultations");
		}
	}
}