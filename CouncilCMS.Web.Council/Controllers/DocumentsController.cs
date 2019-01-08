using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Bissoft.CouncilCMS.BLL.Services;
using Bissoft.CouncilCMS.BLL.ViewModels;
using Bissoft.CouncilCMS.Core.Enums;

namespace Bissoft.CouncilCMS.Web.Controllers
{
    public class DocumentsController : BaseCmsController
    {
        CmsDocService docService;
		readonly int perPage = 4;

        public DocumentsController()
        {
            docService = new CmsDocService(connectionString);
        }

        public ActionResult PersonDocuments(string categoryUrl, int personId, int page = 1)
        {
            var model = docService.CategoryList(categoryUrl, personId: personId, page: page);

            return PartialView("_prtItemDocList", model);            
        }

        public ActionResult OrgDocuments(string categoryUrl, int orgId, int page = 1)
        {
            var model = docService.CategoryList(categoryUrl, enterpriseId: orgId, page: page);

            return PartialView("_prtItemDocList", model);
        }

        public ActionResult LastDocuments(string url, Int32 limit = 5, int? excludeId = null)
        {
            var model = docService.CategoryList(url, null, null, null, null, 1, limit, excludeId);
            
            return PartialView("_prtLastDocs", model);
        }

        public ActionResult List()
        {
            var model = docService.GetList();
            return PartialView("_prtDocListShared", model);
        }

        public ActionResult Category(String Url, String QueryTitle = null, String QueryText = null, Int32? Type = null, String Date = null, String Query = null, Int32 Page = 1, int? personId = null)
        {
            var model = docService.CategoryList(Url, QueryTitle, QueryText, Type, Date, Page, perPage, null, personId);
            
            if (model != null && model.CategoryId > 0)
            {
                ViewBag.PageType = MenuItemType.DocumentCategory;
                ViewBag.PageValue = model.CategoryId;

                return View(model);
            }
            else
            {
                return new HttpNotFoundResult();
            }
        }

        [Route(Name = "LoadDocsAtFolder")]
        public ActionResult LoadSubFolders(int? Id)
        {
            var rd = Request.RequestContext.RouteData;
            
            CmsDocList model = docService.ItemList(Id.Value);
            return PartialView("_prtDocsAtFolder",model);
        }


        public ActionResult SearchDocument(String Url, String QueryTitle = null, String QueryText = null, Int32? Type = null, String Date = null, String Query = null, Int32 Page = 1, int? personId = null)
        {
            var model = docService.CategoryList(Url, QueryTitle, QueryText, Type, Date, Page, 20, null, personId);

            //List<CmsDocListItem> model = docService.LookingForDocs(QueryTitle, QueryText, Type, Date);

            if (model !=null)
            {
                return PartialView("_prtDocResultList",model);
            }
            else
            {
                return new HttpNotFoundResult();
            }
        }

        public ActionResult Item(Int32 id, String Url)
        {
            var userId = User != null && User.Identity != null && User.Identity.UserId > 0 ? (int?)User.Identity.UserId : null;
            var model = docService.Doc(id, userId);

            if (model != null)
            {
                ViewBag.PageType = MenuItemType.Document;
                ViewBag.PageValue = id;
                ViewBag.SecondPageType = MenuItemType.DocumentCategory;
                ViewBag.SecondPageValue = model.CategoryId;

                return View(model);
            }
            else
            {
                return new HttpNotFoundResult();
            }            
        }
    }
}