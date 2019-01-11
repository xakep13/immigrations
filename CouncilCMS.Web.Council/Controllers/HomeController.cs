using System;
using System.IO;
using System.Web;
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

		public ActionResult SaveUploadedFile()
		{
			bool isSavedSuccessfully = true;
			string fName = "";
			try
			{
				foreach(string fileName in Request.Files)
				{
					HttpPostedFileBase file = Request.Files[fileName];
					//Save file content goes here
					fName = file.FileName;
					if(file != null && file.ContentLength > 0)
					{

						var originalDirectory = new DirectoryInfo(string.Format("{0}upload", Server.MapPath(@"\")));

						string pathString = System.IO.Path.Combine(originalDirectory.ToString(), "imagepath");

						var fileName1 = Path.GetFileName(file.FileName);

						bool isExists = System.IO.Directory.Exists(pathString);

						if(!isExists)
							System.IO.Directory.CreateDirectory(pathString);

						var path = string.Format("{0}\\{1}", pathString, file.FileName);
						fName = path;
						file.SaveAs(path);

					}

				}

			}
			catch(Exception)
			{
				isSavedSuccessfully = false;
			}


			if(isSavedSuccessfully)
			{
				return Json(new { Message = fName });
			}
			else
			{
				return Json(new { Message = "Error in saving file" });
			}
		}
	}
}