using System;
using System.Configuration;
using System.Web.Mvc;
using Bissoft.CouncilCMS.Core.Enums;
using Bissoft.CouncilCMS.BLL.ViewModels;
using Bissoft.CouncilCMS.BLL.Services;
using Bissoft.CouncilCMS.Web.Controllers;
using Bissoft.CouncilCMS.Web.Filters;
using Bissoft.CouncilCMS.BLL;

namespace Bissoft.CouncilCMS.Web.Areas.ControlPanel.Controllers
{

    [CmsAuthorize]
    public class AjaxController : BaseCmsController
    {
        private RemoteAdminService remoteAdminService;
        private ContentAdminService contentAdminService;

        public AjaxController() : base()
        {
            var uow = new UnitOfWork(this.connectionString);

            remoteAdminService = new RemoteAdminService(uow);
            contentAdminService = new ContentAdminService(uow);
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

		public ActionResult FindDamagedHousing(String Query, String ExcludeIds = null)
		{
			var list = remoteAdminService.FindDamagedHousing(Query, ExcludeIds, false, true);

			return Json(list, JsonRequestBehavior.AllowGet);
		}

		public ActionResult FindPerson(String Query, String ExcludeIds = null, Boolean OnlyParents = true)
        {
            var list = remoteAdminService.FindPerson(Query, ExcludeIds, false, true);

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

        #region Content

        public ActionResult PreviewContent(Int32 ItemId, ContentRowType Type)
        {
            var model = contentAdminService.GetContentPreview(ItemId, Type);

            return PartialView("_prtContentPreview", model);
        }

        [HttpPost]
        public ActionResult AddContentRow(Int32 ItemId, ContentRowType Type, String ColType = "col1")
        {
            var model = contentAdminService.AddContentRowItem(ItemId, Type, ColType);

            return PartialView("_prtContentRowItem", model);
        }

        [HttpPost]
        public ActionResult AddContentColumn(Int32 ContentRowId, String Type = "col2")
        {
            var model = contentAdminService.AddContentColumnItems(ContentRowId, Type);

            return PartialView("_prtContentColumnItems", model);
        }

        public ActionResult GetContentRowEditForm(Int32 Id)
        {
            var model = contentAdminService.GetContentRowEdit(Id);

            return PartialView("_prtContentRowEdit", model);
        }

        public ActionResult GetContentColumnEditForm(Int32 Id)
        {
            var model = contentAdminService.GetContentColumnEdit(Id);

            return PartialView("_prtContentColumnEdit", model);
        }

        public ActionResult GetRowSelect(Int32 ItemId, String Type)
        {
            ViewBag.ItemId = ItemId;
            ViewBag.Type = Type;

            return PartialView("_prtRowSelect");
        }

        public ActionResult GetContentSelect(Int32 ContentColumnId = 0, Int32 Position = 0)
        {
            ViewBag.ContentColumnId = ContentColumnId;
            ViewBag.Position = Position;

            return PartialView("_prtContentSelect");
        }

        public ActionResult GetContentEditForm(Int32 Id, Int32 ContentColumnId = 0, ContentType Type = ContentType.Html)
        {
            var model = contentAdminService.GetContentEdit(Id, ContentColumnId, Type);

            switch (model.Type)
            {
                case ContentType.Ckeditor:
                    return PartialView("_prtContentCkeEdit", model);
                case ContentType.Table:
                    return PartialView("_prtContentTableEdit", model);
                case ContentType.Html:
                    return PartialView("_prtContentHtmlEdit", model);
                case ContentType.Gallery:
                    return PartialView("_prtContentGalleryEdit", model);
                case ContentType.Image:
                    return PartialView("_prtContentImageEdit", model);
                case ContentType.Video:
                    return PartialView("_prtContentVideoEdit", model);
                case ContentType.Map:
                    if (Double.Parse((model as MapEdit).CenterLat) == 0)
                    {
                        (model as MapEdit).CenterLat = CmsSettings.DefaultLat.ToString();
                    }
                    if (Double.Parse((model as MapEdit).CenterLng) == 0)
                    {
                        (model as MapEdit).CenterLng = CmsSettings.DefaultLng.ToString();
                    }
                    return PartialView("_prtContentMapEdit", model);
                case ContentType.File:
                    return PartialView("_prtContentFileEdit", model);
                case ContentType.Widget:
                    return PartialView("_prtContentWidgetEdit", model);
                case ContentType.TextBlock:
                    return PartialView("_prtContentTextBlockEdit", model);
                case ContentType.Menu:
                    return PartialView("_prtContentMenuEdit", model);
                case ContentType.Accordion:
                    return PartialView("_prtContentAccordionEdit", model);
                case ContentType.Collapse:
                    return PartialView("_prtContentCollpaseEdit", model);
                default:
                    return null;
            }
        }

        [HttpPost]
        public ActionResult SaveContentRow(ContentRowsEdit Model)
        {
            var model = contentAdminService.SaveContentRow(Model);

            return Json(model);
        }

        [HttpPost]
        public ActionResult SaveContentColumn(ContentColumnEdit Model)
        {
            var model = contentAdminService.SaveContentColumn(Model);

            return Json(model);
        }

        [HttpPost]
        public ActionResult SaveCkeContent(CkeContent Model)
        {
            var model = contentAdminService.SaveContent(Model);

            return PartialView("_prtContentItem", model);
           
        }
        [HttpPost]
        public ActionResult SaveMenuContent(MenuContent Model)
        {
            var model = contentAdminService.SaveContent(Model);

            return PartialView("_prtContentItem", model);
        }
        public ActionResult SaveAccordionContent(AccordionContent Model)
        {
            var model = contentAdminService.SaveContent(Model);

            return PartialView("_prtContentItem", model);
        }
        [HttpPost]
        public ActionResult SaveTableContent(TableContent Model)
        {
            Model.Type = ContentType.Table;
            var model = contentAdminService.SaveContent(Model);

            return PartialView("_prtContentItem", model);
        }

        [HttpPost]
        public ActionResult SaveImageContent(UploadImage Model)
        {
            var model = contentAdminService.SaveContent(Model);

            return PartialView("_prtContentItem", model);

        }
        [HttpPost]
        public ActionResult SaveTextBlockContent(TextBlockContent Model)
        {
            var model = contentAdminService.SaveContent(Model);

            return PartialView("_prtContentItem", model);

        }
        [HttpPost]
        public ActionResult SaveGalleryContent(GalleryEdit Model)
        {
            var model = contentAdminService.SaveContent(Model);

            return PartialView("_prtContentItem", model);

        }
        [HttpPost]
        public ActionResult SaveHtmlContent(HtmlContent Model)
        {
            var model = contentAdminService.SaveContent(Model);

            return PartialView("_prtContentItem", model);

        }
        [HttpPost]
        public ActionResult SaveVideoContent(VideoEdit Model)
        {
            var model = contentAdminService.SaveContent(Model);

            return PartialView("_prtContentItem", model);

        }
        [HttpPost]
        public ActionResult SaveMapContent(MapEdit Model)
        {

            Model.Type = ContentType.Map;
            var model = contentAdminService.SaveContent(Model);
           
            return PartialView("_prtContentItem", model);

        }

        [HttpPost]
        public ActionResult SaveFileContent(FileUpload Model)
        {
            Model.Type = ContentType.File;
            var model = contentAdminService.SaveContent(Model);

            return PartialView("_prtContentItem", model);
        }

        [HttpPost]
        public ActionResult SaveWidgetContent(WidgetContent Model)
        {
            Model.Type = ContentType.Widget;
            var model = contentAdminService.SaveContent(Model);

            return PartialView("_prtContentItem", model);
        }

        [HttpPost]
        public ActionResult SaveCollapseContent(CollapseContent Model)
        {
            Model.Type = ContentType.Collapse;
            var model = contentAdminService.SaveContent(Model);

            return PartialView("_prtContentItem", model);
        }

        [HttpPost]
        public ActionResult AddGalleryItem()
        {
            return PartialView("_prtContentGalleryItemEdit");
        }

        [HttpPost]
        public ActionResult RemoveContent(Int32 Id)
        {
            contentAdminService.RemoveContent(Id);
            return null;
        }

        [HttpPost]
        public ActionResult RemoveContentColumn(Int32 Id)
        {
            contentAdminService.RemoveContentColumn(Id);
            return null;
        }
        [HttpPost]
        public ActionResult RemoveContentRow(Int32 Id, Int32 ItemId, ContentRowType Type)
        {
            contentAdminService.RemoveContentRow(Id, ItemId, Type);
            return null;
        }

        [HttpPost]
        public ActionResult MoveContentRow(int id, int neighbourId, string direction)
        {
            contentAdminService.MoveContentRow(id, neighbourId, direction);
            return null;
        }

        [HttpPost]
        public ActionResult MoveContent(int id, int neighbourId, string direction)
        {
            contentAdminService.MoveContent(id, neighbourId, direction);
            return null;
        }

        [HttpPost]
        public ActionResult UpdateContentPosition(Int32 id, int contentColumnId, Int32 oldPosition, Int32 newPosition)
        {
            contentAdminService.UpdateContentPosition(id, contentColumnId, oldPosition, newPosition);
            return null;
        }


        #endregion


    }
}