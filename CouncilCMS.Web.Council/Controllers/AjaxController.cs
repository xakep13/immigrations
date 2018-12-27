using System;
using System.Configuration;
using System.Web.Mvc;
using Bissoft.CouncilCMS.Core.Enums;
using Bissoft.CouncilCMS.BLL.ViewModels;
using Bissoft.CouncilCMS.BLL.Services;
using Bissoft.CouncilCMS.Web.Controllers;
using Bissoft.CouncilCMS.Web.Filters;
using Bissoft.CouncilCMS.BLL;

namespace Bissoft.CouncilCMS.Web.Controllers
{

    public class AjaxController : BaseCmsController
    {
        private RemoteAdminService remoteAdminService;
       
        public AjaxController() : base()
        {
            var uow = new UnitOfWork(this.connectionString);

            remoteAdminService = new RemoteAdminService(uow);
        }

        public ActionResult FindCmsLink(String Query)
        {
            var list = remoteAdminService.FindCmsLink(Query, true);

            return Json(list, JsonRequestBehavior.AllowGet);
        }


        public ActionResult FindPage(String Query, String ExcludeIds = null)
        {
            var list = remoteAdminService.FindPage(Query, ExcludeIds, false, true);

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FindArticle(String Query, String ExcludeIds = null)
        {
            var list = remoteAdminService.FindArticle(Query, ExcludeIds, false, true);

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FindPerson(String Query, String ExcludeIds = null)
        {
         var list = remoteAdminService.FindPerson(Query, ExcludeIds, false, true);

            list.ForEach(x => { x.DisplayText = x.DisplayText.Substring(0, x.DisplayText.LastIndexOf("[") - 1); });

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FindDoc(String Query = null, String category = null, String ExcludeIds = null, Boolean OnlyParents = true)
        {
            var list = remoteAdminService.FindDoc(Query, category, ExcludeIds, false, true);

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FindEnterprise(String Query, String ExcludeIds = null, Boolean OnlyParents = true)
        {
            var list = remoteAdminService.FindEnterprise(Query, ExcludeIds, OnlyParents, false, true);

            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}