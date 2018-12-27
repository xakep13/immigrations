using System;
using System.Web.Mvc;
using Bissoft.CouncilCMS.BLL.Services;
using Bissoft.CouncilCMS.Core.Enums;

namespace Bissoft.CouncilCMS.Web.Controllers
{
    public class SearchController : BaseCmsController
    {
        CmsSearchService searchService;
		CmsDocService docService;

		public SearchController()
        {
            searchService = new CmsSearchService(connectionString);
			docService = new CmsDocService(connectionString);
		}

        public ActionResult Search(string query, Int32 page = 1)
        {
			if(string.IsNullOrEmpty(query))
			{
				return View();
			}
			else
			{
				var model = searchService.Search(query, page);

				return View(model);
			}
        }
    }
}