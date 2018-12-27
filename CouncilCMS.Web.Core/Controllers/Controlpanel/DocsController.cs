using System;
using System.Configuration;
using System.Web.Mvc;
using Bissoft.CouncilCMS.Core.Enums;
using Bissoft.CouncilCMS.BLL.ViewModels;
using Bissoft.CouncilCMS.BLL.Services;
using System.Collections.Generic;
using Bissoft.CouncilCMS.Web.Controllers;
using Bissoft.CouncilCMS.Web.Filters;

namespace Bissoft.CouncilCMS.Web.Areas.ControlPanel.Controllers
{
    [CmsAuthorize]
    public class DocsController : BaseCmsController
    {
        private DocAdminService docsService;

        public DocsController() : base()
        {
            docsService = new DocAdminService(this.ConnectionString);
        }


        public ActionResult List(string query = null, string dateRange = null, int dateType = 0, int? categoryId = null, int? typeId = null, int itemState = 1, int page = 1, String sortBy = "PublishDate", int sortDirection = 1, int? user = null, int userAction = 0, Boolean AjaxMode = false)
        {
            if (User.HasPremissions("docs"))
            {
                var userId = User.HasPremission("cr_docs_full") ? null : (int?)User.Identity.UserId;

                if (AjaxMode)
                {
                    var count = 0;
                    var model = docsService.GetList(
                        query: query,
                        dateRange: dateRange,
                        dateType: dateType,
                        page: page,
                        categoryId: categoryId,
                        typeId: typeId,
                        sortBy: sortBy,
                        sortDir: sortDirection,
                        itemState: (CmsItemState)itemState,
                        userId: userId,
                        user: user,
                        userAction: userAction,
                        count: out count);

                    ViewBag.Count = count;
                    ViewBag.Page = page;
                    ViewBag.PerPage = 20;

                    return PartialView("_prtDocList", model);
                }
                else
                {
                    var model = docsService.List(
                        query: query,
                        dateRange: dateRange,
                        dateType: dateType,
                        page: page,
                        categoryId: categoryId,
                        typeId: typeId,
                        sortBy: sortBy,
                        sortDir: sortDirection,
                        itemState: (CmsItemState)itemState,
                        user: user,
                        userAction: userAction,
                        userId: userId);

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
            var userId = User.HasPremission("cr_docs_full") ? null : (int?)User.Identity.UserId;

            var model = docsService.GetForEdit(Id, userId);

            return PartialView("_prtDocEdit", model);
        }

        public ActionResult GetFileForm(String File, String Name, String Ext, Int32 Size, Int32 DocId)
        {
            var model = new DocFileUpload()
            {
                Id = 0,
                TitleEn = Name,
                TitleRu = Name,
                TitleUk = Name,
                Size = Size,
                Extension = Ext,
                File = File,
                DocId = DocId,
            };

            return PartialView("_prtDocFileAdd", model);
        }

        public ActionResult AddFile(DocFileUpload model)
        {
            docsService.AddFile(model);

            return PartialView("_prtDocFileListItem", model);
        }

        public ActionResult DeleteFile(int id)
        {
            docsService.DeleteFile(id);
            return null;
        }

        public ActionResult AddPerson(Int32 DocId, Int32 PersonId)
        {
            var model = docsService.AddPerson(PersonId, DocId);

            if (model != null)
                return PartialView("_prtDocPersonListItem", model);
            else
                return null;
        }

        public ActionResult AddEnterprise(Int32 DocId, Int32 EnterpriseId)
        {
            var model = docsService.AddEnterprise(EnterpriseId, DocId);

            if (model != null)
                return PartialView("_prtDocEnterpriseListItem", model);
            else
                return null;
        }


        public ActionResult DeletePerson(Int32 DocId, Int32 PersonId)
        {
            docsService.DeletePerson(PersonId, DocId);
            return null;
        }

        public ActionResult DeleteEnterprise(Int32 DocId, Int32 EnterpriseId)
        {
            docsService.DeleteEnterprise(EnterpriseId, DocId);
            return null;
        }

        [HttpPost]
        public ActionResult Save(DocEdit model)
        {
            docsService.Save(model);

            return PartialView("_prtDocEditComplete", model);
        }

        public ActionResult Delete(Int32 Id)
        {
            docsService.Delete(Id);

            return null;
        }

        public ActionResult Restore(Int32 Id)
        {
            docsService.Restore(Id);

            return null;
        }
    }
}