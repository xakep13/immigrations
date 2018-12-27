using System;
using System.Web.Mvc;
using Bissoft.CouncilCMS.BLL.Services;
using Bissoft.CouncilCMS.Core.Enums;
using Bissoft.CouncilCMS.BLL.ViewModels;
using System.Threading.Tasks;
using xTab.Tools.Helpers;
using System.Linq;
using Bissoft.CouncilCMS.BLL;
using System.Web;
using xTab.Tools.Extensions;
using Bissoft.CouncilCMS.BLL.Services.Public;
using Bissoft.CouncilCMS.BLL.ViewModels.Public;
using Bissoft.CouncilCMS.Core;

namespace Bissoft.CouncilCMS.Web.Controllers
{
	public class HomeController : BaseCmsController
	{
		public WeatherService weather = new WeatherService();
		public CmsArticleService articleService;
		public CmsPersonService personService;
		private CmsPollService pollService;
		public CmsMenuService menuService;
		public CmsPageService pageSrvice;
		public CmsDocService docService;


		public HomeController()
		{
			var uow = new UnitOfWork(this.ConnectionString);

			pollService = new CmsPollService(this.ConnectionString);
			articleService = new CmsArticleService(uow);
			personService = new CmsPersonService(uow);
			menuService = new CmsMenuService(uow);
			pageSrvice = new CmsPageService(uow);
			docService = new CmsDocService(uow);

		}

		public ActionResult Index()
		{
			var model = new MkIndex();

			model.ServiceMenu = menuService.GetMenu("services-menu", CmsSettings.ShowMenuItemMode);
			model.ThemeMenu = menuService.GetMenu("information-menu", CmsSettings.ShowMenuItemMode);
			model.LinkMenu = menuService.GetMenu("kor-menu", CmsSettings.ShowMenuItemMode);
			model.MainNewsSecond = articleService.LastArticles("main-news-2", 6).Articles;
			model.AreaMenu = menuService.GetMenu("gromady", CmsSettings.ShowMenuItemMode);
			model.MainNews = articleService.LastArticles("main-news", 3).Articles;
			model.AreaNews = articleService.LastArticles("video", 4).Articles;
			model.Mayor = articleService.LastArticles("mayor").Articles;
			model.Polls = pollService.GetPolls().Take(3).ToList();
			model.EmailStatus = CmsSettings.IsEmailActive;
			model.PhoneStatus = CmsSettings.IsPhoneActive;

			return View(model);
		}

		public ActionResult MainMenu(Int32 Id = 2)
		{
			var model = menuService.GetMenu("main-menu", CmsSettings.ShowMenuItemMode);
			return PartialView("_prtMainMenu", model);
		}

		public ActionResult TopMenu()
		{
			var model = menuService.GetMenu("top-menu", CmsSettings.ShowMenuItemMode);
			return PartialView("_prtTopMenu", model);
		}

		public ActionResult Header(Int32 Id = 1, MenuItemType? Type = null, String Value = null, MenuItemType? SecondType = null, String SecondValue = null)
		{
			var model = HttpRuntime.Cache.GetOrStore<CmsMenu>("main-menu-" + CurrentCulture.Name.ToLower(), 15, () => menuService.GetMenu("main-menu", ShowMenuItemMode.PerLanguage));

			menuService.SetCurrent(model.Items, Type, SecondType, Value, SecondValue);

			return PartialView("_prtHeader", model);
		}

		public ActionResult SubMenu(Int32 Id = 3)
		{
			var model = menuService.GetMenu(3, CmsSettings.ShowMenuItemMode);
			return PartialView("_prtSubMenu", model);
		}

		public ActionResult FooterMenu(string name)
		{
			var model = menuService.GetMenu(1, CmsSettings.ShowMenuItemMode);
			return PartialView("_prtFooterMenu", model);
		}
		public ActionResult FooterServices()
		{
			var items = menuService.GetMenu("services-menu", CmsSettings.ShowMenuItemMode);
			return PartialView("_prtServicesMenu", items);
		}

		public async Task<ActionResult> Weather()
		{
			var model = await weather.GetCurrentWeather(CmsSettings.DefaultLat, CmsSettings.DefaultLng, CurrentCulture.Name.ToLower());
			return PartialView("_prtWeather", model);
		}

		public ActionResult LocalNews()
		{
			var model = articleService.LastArticles("local-news", 3).Articles;
			return PartialView("_prtSimpleLocalNews", model);
		}
		public ActionResult HotLineBlock()
		{
			var model = "";
			ViewData["EmailStatus"] = CmsSettings.IsEmailActive;
			ViewData["PhoneStatus"] = CmsSettings.IsPhoneActive;

			return PartialView("_prtHotLineBlock", model);
		}

		private bool GetIsActive(CmsPoll model)
		{
			var pollIdCookieKey = string.Format("pollId-{0}", model.Id);

			return DateTime.ParseExact(model.DateTo, "MM/dd/yyyy", null) >= DateTime.Now && (Session[pollIdCookieKey] == null);
		}
	}
}