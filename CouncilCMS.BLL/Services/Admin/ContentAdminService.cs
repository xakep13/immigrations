using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Bissoft.CouncilCMS.Core;
using Bissoft.CouncilCMS.Core.Enums;
using Bissoft.CouncilCMS.Core.Globalization;
using Bissoft.CouncilCMS.DAL.Entities;
using Bissoft.CouncilCMS.BLL.ViewModels;
using AngleSharp;
using AngleSharp.Parser.Html;
using AutoMapper;

namespace Bissoft.CouncilCMS.BLL.Services
{
    public class ContentAdminService : BaseService
    {
        private IRepository<ContentRow, int> contentRowRepo;
        private IRepository<ContentColumn, int> contentColumnRepo;
        private IRepository<Content, int> contentRepo;
        private IRepository<Page, int> pageRepo;
        private IRepository<Person, int> personRepo;
        private IRepository<Article, int> articleRepo;
        private IRepository<Enterprise, int> enterpriseRepo;        
        private SelectListService selectListService;

        public ContentAdminService(string ConnectionString) : base(ConnectionString)
        {
            Initialize();
        }

        public ContentAdminService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            Initialize();
        }

        private void Initialize()
        {
            contentRowRepo = UnitOfWork.GetIntRepository<ContentRow>();
            contentColumnRepo = UnitOfWork.GetIntRepository<ContentColumn>();
            contentRepo = UnitOfWork.GetIntRepository<Content>();
            pageRepo = UnitOfWork.GetIntRepository<Page>();
            personRepo = UnitOfWork.GetIntRepository<Person>();
            articleRepo = UnitOfWork.GetIntRepository<Article>();
            enterpriseRepo = UnitOfWork.GetIntRepository<Enterprise>();
            selectListService = new SelectListService(this.UnitOfWork);
        }

        public List<ContentRowItem> GetContentPreview(Int32 itemId, ContentRowType type)
        {
            var model = new List<ContentRowItem>();

            switch (type)
            {
                case ContentRowType.Page:
                    var page = pageRepo.GetById(itemId);

                    model = GetContentRowListItems(page.ContentRows, itemId);

                    break;
                case ContentRowType.Person:
                    var person = personRepo.GetById(itemId);

                    model = GetContentRowListItems(person.ContentRows, itemId);

                    break;
                case ContentRowType.Article:
                    var article = articleRepo.GetById(itemId);

                    model = GetContentRowListItems(article.ContentRows, itemId);

                    break;
                case ContentRowType.Enterprise:
                    var enterprise = enterpriseRepo.GetById(itemId);

                    model = GetContentRowListItems(enterprise.ContentRows, itemId);

                    break;
            }

            return model;
        }

        public List<ContentRowItem> GetContentRowListItems(ICollection<ContentRow> list, Int32 itemId)
        {
            var model = new List<ContentRowItem>();

            if (list != null)
            {
                foreach (var row in list.OrderBy(x => x.Position))
                {
                    var rowItem = Mapper.Map<ContentRowItem>(row);
                    rowItem.ItemId = itemId;

                    rowItem.ContentColumns = new List<ContentColumnItem>();

                    if (row.ContentColumns != null)
                    {
                        foreach (var col in row.ContentColumns.OrderBy(x => x.Position))
                        {
                            var colItem = Mapper.Map<ContentColumnItem>(col);

                            colItem.Contents = new List<ContentItem>();

                            if (col.Contents != null)
                            {
                                foreach (var cont in col.Contents.OrderBy(x => x.Position))
                                {
                                    var contItem = Mapper.Map<ContentItem>(cont);
                                    colItem.Contents.Add(contItem);
                                }
                            }

                            rowItem.ContentColumns.Add(colItem);
                        }
                    }

                    model.Add(rowItem);
                }
            }

            return model;
        }

        public ContentRowItem AddContentRowItem(int itemId, ContentRowType type, String ColType = null)
        {
            var contentRow = new ContentRow() { };

            contentRowRepo.Insert(contentRow);
            UnitOfWork.Commit();

            switch (type)
            {
                case ContentRowType.Page:
                    var page = pageRepo.GetById(itemId);

                    if (page != null)
                    {
                        page.ContentRows = page.ContentRows ?? new List<ContentRow>();
                        contentRow.Position = page.ContentRows.Select(x => x.Position).DefaultIfEmpty(0).Max() + 1;
                        page.ContentRows.Add(contentRow);

                        pageRepo.Update(page);
                    }
                    else
                    {
                        contentRowRepo.Delete(contentRow);
                    }
                    break;
                case ContentRowType.Person:
                    var person = personRepo.GetById(itemId);

                    if (person != null)
                    {
                        person.ContentRows = person.ContentRows ?? new List<ContentRow>();
                        contentRow.Position = person.ContentRows.Select(x => x.Position).DefaultIfEmpty(0).Max() + 1;
                        person.ContentRows.Add(contentRow);

                        personRepo.Update(person);
                    }
                    else
                    {
                        contentRowRepo.Delete(contentRow);
                    }
                    break;
                case ContentRowType.Article:
                    var article = articleRepo.GetById(itemId);

                    if (article != null)
                    {
                        article.ContentRows = article.ContentRows ?? new List<ContentRow>();
                        contentRow.Position = article.ContentRows.Select(x => x.Position).DefaultIfEmpty(0).Max() + 1;
                        article.ContentRows.Add(contentRow);

                        articleRepo.Update(article);
                    }
                    else
                    {
                        contentRowRepo.Delete(contentRow);
                    }
                    break;
                case ContentRowType.Enterprise:
                    var enterprise = enterpriseRepo.GetById(itemId);

                    if (enterprise != null)
                    {
                        enterprise.ContentRows = enterprise.ContentRows ?? new List<ContentRow>();
                        contentRow.Position = enterprise.ContentRows.Select(x => x.Position).DefaultIfEmpty(0).Max() + 1;
                        enterprise.ContentRows.Add(contentRow);

                        enterpriseRepo.Update(enterprise);
                    }
                    else
                    {
                        contentRowRepo.Delete(contentRow);
                    }
                    break;
            }

            UnitOfWork.Commit();

            var model = new ContentRowItem()
            {
                Id = contentRow.Id,
                ItemId = itemId,
                Position = contentRow.Position
            };


            if (!string.IsNullOrEmpty(ColType))
            {
                var columns = AddContentColumnItems(contentRow.Id, ColType);

                model.ContentColumns = columns;
            }

            
            return model;
        }

        public List<ContentColumnItem> AddContentColumnItems(int contentRowId, string type)
        {
            var model = new List<ContentColumnItem>();
            var row = contentRowRepo.GetById(contentRowId);

            row.ContentColumns = row.ContentColumns ?? new List<ContentColumn>();

            var position = row.ContentColumns.Select(x => x.Position).DefaultIfEmpty(0).Max() + 1;

            switch (type)
            {
                case ("col1"):
                    {
                        var contentColumn = GetNewColumn(12);

                        contentColumn.Position = position;
                        contentColumn.ContentRowId = contentRowId;

                        contentColumnRepo.Insert(contentColumn);
                        UnitOfWork.Commit();

                        model.Add(Mapper.Map<ContentColumnItem>(contentColumn));
                        break;
                    }
                case ("col2"):
                    {
                        for (var i = 0; i < 2; i++)
                        {
                            var contentColumn = GetNewColumn(6);

                            contentColumn.Position = position + i;
                            contentColumn.ContentRowId = contentRowId;

                            contentColumnRepo.Insert(contentColumn);
                            UnitOfWork.Commit();

                            model.Add(Mapper.Map<ContentColumnItem>(contentColumn));
                        }

                        break;
                    }
                case ("col2-1-2"):
                    {
                        var contentColumn1 = GetNewColumn(4);
                        var contentColumn2 = GetNewColumn(8);

                        contentColumn1.Position = position;
                        contentColumn2.Position = position + 1;

                        contentColumn1.ContentRowId = contentRowId;
                        contentColumn2.ContentRowId = contentRowId;

                        contentColumnRepo.Insert(contentColumn1);
                        contentColumnRepo.Insert(contentColumn2);

                        UnitOfWork.Commit();

                        model.Add(Mapper.Map<ContentColumnItem>(contentColumn1));
                        model.Add(Mapper.Map<ContentColumnItem>(contentColumn2));

                        break;
                    }
                case ("col2-2-1"):
                    {
                        var contentColumn1 = GetNewColumn(8);
                        var contentColumn2 = GetNewColumn(4);

                        contentColumn1.Position = position;
                        contentColumn2.Position = position + 1;

                        contentColumn1.ContentRowId = contentRowId;
                        contentColumn2.ContentRowId = contentRowId;

                        contentColumnRepo.Insert(contentColumn1);
                        contentColumnRepo.Insert(contentColumn2);

                        UnitOfWork.Commit();

                        model.Add(Mapper.Map<ContentColumnItem>(contentColumn1));
                        model.Add(Mapper.Map<ContentColumnItem>(contentColumn2));

                        break;
                    }
                case ("col2-1-3"):
                    {
                        var contentColumn1 = GetNewColumn(3);
                        var contentColumn2 = GetNewColumn(9);

                        contentColumn1.Position = position;
                        contentColumn2.Position = position + 1;

                        contentColumn1.ContentRowId = contentRowId;
                        contentColumn2.ContentRowId = contentRowId;

                        contentColumnRepo.Insert(contentColumn1);
                        contentColumnRepo.Insert(contentColumn2);

                        UnitOfWork.Commit();

                        model.Add(Mapper.Map<ContentColumnItem>(contentColumn1));
                        model.Add(Mapper.Map<ContentColumnItem>(contentColumn2));

                        break;
                    }
                case ("col2-3-1"):
                    {
                        var contentColumn1 = GetNewColumn(9);
                        var contentColumn2 = GetNewColumn(3);

                        contentColumn1.Position = position;
                        contentColumn2.Position = position + 1;

                        contentColumn1.ContentRowId = contentRowId;
                        contentColumn2.ContentRowId = contentRowId;

                        contentColumnRepo.Insert(contentColumn1);
                        contentColumnRepo.Insert(contentColumn2);

                        UnitOfWork.Commit();

                        model.Add(Mapper.Map<ContentColumnItem>(contentColumn1));
                        model.Add(Mapper.Map<ContentColumnItem>(contentColumn2));

                        break;
                    }
                case ("col3"):
                    {
                        for (var i = 0; i < 3; i++)
                        {
                            var contentColumn = GetNewColumn(4);

                            contentColumn.Position = position + i;
                            contentColumn.ContentRowId = contentRowId;

                            contentColumnRepo.Insert(contentColumn);
                            UnitOfWork.Commit();

                            model.Add(Mapper.Map<ContentColumnItem>(contentColumn));
                        }

                        break;
                    }
                case ("col4"):
                    {
                        for (var i = 0; i < 4; i++)
                        {
                            var contentColumn = GetNewColumn(3);

                            contentColumn.Position = position + i;
                            contentColumn.ContentRowId = contentRowId;

                            contentColumnRepo.Insert(contentColumn);
                            UnitOfWork.Commit();

                            model.Add(Mapper.Map<ContentColumnItem>(contentColumn));
                        }

                        break;
                    }
            }

            return model;
        }

        public ContentRowsEdit GetContentRowEdit(Int32 id)
        {
            var item = contentRowRepo.GetById(id) ?? new ContentRow();

            var model = Mapper.Map<ContentRowsEdit>(item);

            return model;
        }

        public ContentColumnEdit GetContentColumnEdit(Int32 Id)
        {
            var item = contentColumnRepo.GetById(Id) ?? new ContentColumn();

            var model = Mapper.Map<ContentColumnEdit>(item);

            return model;
        }

        public ContentEdit GetContentEdit(int id, int contentColumnId = 0, ContentType type = ContentType.Html)
        {
            var content = contentRepo.GetById(id) ?? new Content() { ContentColumnId = contentColumnId, Type = (int)type };

            if (content.Id == 0)
            {
                var column = contentColumnRepo.GetById(contentColumnId);
                var position = (column.Contents ?? new List<Content>()).Select(x => x.Position).DefaultIfEmpty(0).Max() + 1;

                content.Position = position;
                contentRepo.Insert(content);
                UnitOfWork.Commit();
            }

            var model = GetContentEditType(content, id <= 0);

            return model;
        }


        public ContentRowItem SaveContentRow(ContentRowsEdit model)
        {
            var item = contentRowRepo.GetById(model.Id);

            if (item != null)
            {
                Mapper.Map<ContentRowsEdit, ContentRow>(model, item);

                contentRowRepo.Update(item);
                UnitOfWork.Commit();

                var result = Mapper.Map<ContentRowItem>(item);

                return result;
            }
            else
                return null;
        }

        public ContentColumnItem SaveContentColumn(ContentColumnEdit model)
        {
            var item = contentColumnRepo.GetById(model.Id);

            if (item != null)
            {
                Mapper.Map<ContentColumnEdit, ContentColumn>(model, item);

                contentColumnRepo.Update(item);
                UnitOfWork.Commit();

                var result = Mapper.Map<ContentColumnItem>(item);

                return result;
            }
            else
                return null;
        }

        public ContentItem SaveContent(ContentEdit model)
        {
            var item = contentRepo.GetById(model.Id);
          
            if (item != null)
            {
                switch (item.Type)
                {
                    case (int)ContentType.Ckeditor:
                        {
                            var obj = model as CkeContent;
                            var formatString = "<div class='page-content content-text'>{0}</div>";
                            
                            item.BodyRu = String.Format(formatString, obj.TextRu);
                            item.BodyUk = String.Format(formatString, obj.TextUk);
                            item.BodyEn = String.Format(formatString, obj.TextEn);
                            break;
                        }
                    case (int)ContentType.Image:
                        {
                            var obj = model as UploadImage;
                            var formatString = "<div class='page-content content-image'><div class='slide-gallery'{0}><img data-link='{1}' src='{2}' title='{3}'/></div><div class='image-desc content-desc'>{3}</div></div>";

                            item.BodyRu = String.Format(formatString, obj.AllowFullscreen ? " data-allowfullscreen='true'" : null, obj.Url, obj.Image, obj.TitleRu);
                            item.BodyUk = String.Format(formatString, obj.AllowFullscreen ? " data-allowfullscreen='true'" : null, obj.Url, obj.Image, obj.TitleUk);
                            item.BodyEn = String.Format(formatString, obj.AllowFullscreen ? " data-allowfullscreen='true'" : null, obj.Url, obj.Image, obj.TitleEn);

                            break;
                        }
                    case (int)ContentType.Gallery:
                        {
                            var obj = model as GalleryEdit;

                            item.BodyRu = "<div class='page-content content-gallery'><div class='slide-gallery' data-allowfullscreen='true' data-width='100%'" + (String.IsNullOrEmpty(obj.Ratio) ? null : ("data-ratio='" + obj.Ratio + "'")) + "> ";
                            item.BodyUk = "<div class='page-content content-gallery'><div class='slide-gallery' data-allowfullscreen='true' data-width='100%'" + (String.IsNullOrEmpty(obj.Ratio) ? null : ("data-ratio='" + obj.Ratio + "'")) + ">";
                            item.BodyEn = "<div class='page-content content-gallery'><div class='slide-gallery' data-allowfullscreen='true' data-width='100%'" + (String.IsNullOrEmpty(obj.Ratio) ? null : ("data-ratio='" + obj.Ratio + "'")) + ">";

                            foreach (var i in obj.GalleryItems)
                            {
                                item.BodyRu += " <img src='" + i.Image + "' title='" + i.NameRu + "' />";
                                item.BodyUk += " <img src='" + i.Image + "' title='" + i.NameUk + "' />";
                                item.BodyEn += " <img src='" + i.Image + "' title='" + i.NameEn + "' />";
                            }

                            item.BodyRu += "</div><div class='gallery-desc content-desc'>" + obj.DescriptionRu + "</div></div>";
                            item.BodyUk += "</div><div class='gallery-desc content-desc'>" + obj.DescriptionUk + "</div></div>";
                            item.BodyEn += "</div><div class='gallery-desc content-desc'>" + obj.DescriptionEn + "</div></div>";
                            break;
                        }
                    case (int)ContentType.Video:
                        {
                            var obj = model as VideoEdit;
                            var formatString = "<div class='page-content content-video embed-responsive embed-responsive-16by9'>{0}</div>";

                            item.BodyRu = String.Format(formatString, obj.Body);
                            item.BodyUk = String.Format(formatString, obj.Body);
                            item.BodyEn = String.Format(formatString, obj.Body);
                            break;
                        }
                    case (int)ContentType.Map:
                        {
                            var obj = model as MapEdit;
                            item.BodyRu = "<div class='page-content content-map'><div class='content-map-container' data-lng ='" + obj.CenterLng + "' data-lat = '" + obj.CenterLat + "' data-zoom='" + obj.Zoom + "' data-height='" + obj.Height + "' style='height:" + obj.Height + "px'></div>";
                            item.BodyUk = "<div class='page-content content-map'><div class='content-map-container' data-lng ='" + obj.CenterLng + "' data-lat = '" + obj.CenterLat + "' data-zoom='" + obj.Zoom + "' data-height='" + obj.Height + "' style='height:" + obj.Height + "px'></div>";
                            item.BodyEn = "<div class='page-content content-map'><div class='content-map-container' data-lng ='" + obj.CenterLng + "' data-lat = '" + obj.CenterLat + "' data-zoom='" + obj.Zoom + "' data-height='" + obj.Height + "' style='height:" + obj.Height + "px'></div>";

                            if (obj != null && obj.MapPoints != null)
                            {
                                foreach (var i in obj.MapPoints)
                                {
                                    item.BodyRu += "<div class='map-point' data-title='" + i.TitleRu + "' data-lat='" + i.Lat + "' data-lng='" + i.Lng + "' data-desc='" + i.DescriptionRu + "' data-icon ='" + i.Icon + "' >" + "</div>";
                                    item.BodyUk += "<div class='map-point' data-title='" + i.TitleUk + "' data-lat='" + i.Lat + "' data-lng='" + i.Lng + "' data-desc='" + i.DescriptionUk + "' data-icon ='" + i.Icon + "' >" + "</div>";
                                    item.BodyEn += "<div class='map-point' data-title='" + i.TitleEn + "' data-lat='" + i.Lat + "' data-lng='" + i.Lng + "' data-desc='" + i.DescriptionEn + "' data-icon ='" + i.Icon + "' >" + "</div>";
                                }
                            }
                           
                            item.BodyRu += "</div>";
                            item.BodyUk += "</div>";
                            item.BodyEn += "</div>";

                            break;
                        }
                    case (int)ContentType.Html:
                        {
                            var obj = model as HtmlContent;
                            var formatString = "<div class='page-content content-html'>{0}</div>";

                            item.BodyRu = String.Format(formatString, obj.BodyRu);
                            item.BodyUk = String.Format(formatString, obj.BodyUk);
                            item.BodyEn = String.Format(formatString, obj.BodyEn);
                            
                            break;
                        }
                    case (int)ContentType.File:
                        {
                            var obj = model as FileUpload;
                            var file = new FileInfo(HttpContext.Current.Server.MapPath(obj.Url));

                            if (file.Exists)
                            {
                                var size = (file.Length / 1024) > 1024 ? ((file.Length / (1024 * 1024)) + " mb") : ((file.Length / (1024)) + " kb");
                                var ext = file.Extension;

                                var formatString = "<div class='page-content content-file text-{0}' style='margin-bottom:{1}px;{2}'>{9}<a style='font-weight:{3};font-style:{4};' href='{5}' download='{6}'>{6} ({7}, {8})</a></div>";

                                var name = file.Name;

                                item.BodyRu = String.Format(formatString, obj.TextAlign, obj.MarginBottom, obj.FontSize > 0 ? ("font-size:" + obj.FontSize + "px") : null, obj.FontWeight, obj.FontStyle, obj.Url, !String.IsNullOrEmpty(obj.NameRu) ? obj.NameRu : name, ext, size, obj.ShowIcon ? "<i class='fa fa-fw fa-file-text'></i>" : null);
                                item.BodyUk = String.Format(formatString, obj.TextAlign, obj.MarginBottom, obj.FontSize > 0 ? ("font-size:" + obj.FontSize + "px") : null, obj.FontWeight, obj.FontStyle, obj.Url, !String.IsNullOrEmpty(obj.NameUk) ? obj.NameUk : name, ext, size, obj.ShowIcon ? "<i class='fa fa-fw fa-file-text'></i>" : null);
                                item.BodyEn = String.Format(formatString, obj.TextAlign, obj.MarginBottom, obj.FontSize > 0 ? ("font-size:" + obj.FontSize + "px") : null, obj.FontWeight, obj.FontStyle, obj.Url, !String.IsNullOrEmpty(obj.NameEn) ? obj.NameEn : name, ext, size, obj.ShowIcon ? "<i class='fa fa-fw fa-file-text'></i>" : null);

                            }
                            else
                            {
                                item.BodyRu = "Файл отсутсвует";
                                item.BodyUk = "Файл відсутній";
                                item.BodyEn = "No file";
                            }

                            break;
                        }
                    case (int)ContentType.Table:
                        {
                            var obj = model as TableContent;

                            item.BodyRu = "<div class='page-content content-table'><div class='table-responsive'>" + obj.TextRu + "</div><div table='image-desc content-desc'>" + obj.TitleRu + "</div></div>";
                            item.BodyUk = "<div class='page-content content-table'><div class='table-responsive'>" + obj.TextUk + "</div><div table='image-desc content-desc'>" + obj.TitleUk + "</div></div>";
                            item.BodyEn = "<div class='page-content content-table'><div class='table-responsive'>" + obj.TextEn + "</div><div table='image-desc content-desc'>" + obj.TitleEn + "</div></div>";

                            break;
                        }
                    case (int)ContentType.Widget:
                        {
                            var obj = model as WidgetContent;

                            item.BodyRu = obj.Name;
                            item.BodyUk = obj.Name;
                            item.BodyEn = obj.Name;
                            break;
                        }
                    case (int)ContentType.TextBlock:
                        {
                            var obj = model as TextBlockContent;

                            item.BodyRu = "<div class='page-content content-textblock' style='background-color:" + obj.BlockColor + ";color:" + obj.TextColor + " '>" +
                                "<div class='content-textblock-title'>" + obj.TitleRu + "</div>" +
                                "<div class='content-textblock-body'>" + obj.TextRu + "</div></div>";

                            item.BodyUk = "<div class='page-content content-textblock' style='background-color:" + obj.BlockColor + ";color:" + obj.TextColor + " '>" +
                               "<div class='content-textblock-title'>" + obj.TitleUk + "</div>" +
                               "<div class='content-textblock-body'>" + obj.TextUk + "</div></div>";

                            item.BodyEn = "<div class='page-content content-textblock' style='background-color:" + obj.BlockColor + ";color:" + obj.TextColor + " '>" +
                               "<div class='content-textblock-title'>" + obj.TitleEn + "</div>" +
                               "<div class='content-textblock-body'>" + obj.TextEn + "</div></div>";

                            break;
                        }
                    case (int)ContentType.Collapse:
                        {
                            var obj = model as CollapseContent;
                            var id = Guid.NewGuid().ToString().Replace("-", String.Empty);

                            item.BodyRu = 
                                "<div class='page-content content-collapse'>" +
                                    "<div class='title'><a class='collapse-title' href='#" + id + "' data-toggle='collapse'>" + obj.TitleRu + "</a><i class='fa fa-fw fa-angle-down " + (obj.Expanded ? "rotate-180" : String.Empty) + "'></i></div>" +
                                    "<div class='collapse " + (obj.Expanded ? "in" : String.Empty) + "' id='" + id +"'>" + obj.TextRu + "</div>" +
                                "</div>";

                            item.BodyUk =
                                "<div class='page-content content-collapse'>" +
                                    "<div class='title'><a class='collapse-title' href='#" + id + "' data-toggle='collapse'>" + obj.TitleUk + "</a><i class='fa fa-fw fa-angle-down" + (obj.Expanded ? "rotate-180" : String.Empty) + "'></i></div>" +
                                    "<div class='collapse" + (obj.Expanded ? "in" : String.Empty) + "' id='" + id + "'>" + obj.TextUk + "</div>" +
                                "</div>";

                            item.BodyEn =
                                "<div class='page-content content-collapse'>" +
                                    "<div class='title'><a class='collapse-title' href='#" + id + "' data-toggle='collapse'>" + obj.TitleEn + "</a><i class='fa fa-fw fa-angle-down" + (obj.Expanded ? "rotate-180" : String.Empty) + "'></i></div>" +
                                    "<div class='collapse" + (obj.Expanded ? "in" : String.Empty) + "' id='" + id + "'>" + obj.TextEn + "</div>" +
                                "</div>";

                            break;
                        }
                    case (int)ContentType.Menu:
                        {
                            var obj = model as MenuContent;

                            item.BodyRu = obj.MenuId;
                            item.BodyUk = obj.MenuId;
                            item.BodyEn = obj.MenuId;

                            break;
                        }
                    case (int)ContentType.Accordion:
                        {
                            var obj = model as AccordionContent;

                            item.BodyRu = obj.MenuId;
                            item.BodyUk = obj.MenuId;
                            item.BodyEn = obj.MenuId;

                            break;
                        }
                }

                contentRepo.Update(item);
                UnitOfWork.Commit();

                var result = Mapper.Map<ContentItem>(item);               
                return result;
            }
            else
                return null;

        }

        public void MoveContentRow(int id, int neighbourId, string direction)
        {
            var item = contentRowRepo.GetById(id);
            var neighbour = contentRowRepo.GetById(neighbourId);

            switch (direction)
            {
                case "down":
                    item.Position = item.Position + 1;
                    neighbour.Position = neighbour.Position - 1;
                    break;

                case "up":
                    item.Position = item.Position - 1;
                    neighbour.Position = neighbour.Position + 1;
                    break;
            }

            contentRowRepo.Update(item);
            contentRowRepo.Update(neighbour);

            UnitOfWork.Commit();
        }

        public void MoveContent(int id, int neighbourId, string direction)
        {
            var item = contentRepo.GetById(id);
            var neighbour = contentRepo.GetById(neighbourId);

            switch (direction)
            {
                case "down":
                    item.Position = item.Position + 1;
                    neighbour.Position = neighbour.Position - 1;
                    break;

                case "up":
                    item.Position = item.Position - 1;
                    neighbour.Position = neighbour.Position + 1;
                    break;
            }

            contentRepo.Update(item);
            contentRepo.Update(neighbour);

            UnitOfWork.Commit();
        }

        public void UpdateContentPosition(Int32 id, int contentColumnId, Int32 oldPosition, Int32 newPosition)
        {
            var result = 0;

            if (oldPosition > newPosition)
            {
                result = contentRepo
                    .Context
                    .ExecuteSqlCommand("UPDATE Contents SET Position = Position + 1 WHERE Position >= {0} AND Position < {1} AND ContentColumnId = {2}", false, null, newPosition, oldPosition, contentColumnId);
            }
            else if (oldPosition < newPosition)
            {
                result = contentRepo
                   .Context
                   .ExecuteSqlCommand("UPDATE Contents SET Position = Position - 1 WHERE Position <= {0} AND Position > {1} AND ContentColumnId = {2}", false, null, newPosition, oldPosition, contentColumnId);
            }

            result = contentRepo
                   .Context
                   .ExecuteSqlCommand("UPDATE Contents SET Position = {0} WHERE Id = {1}", false, null, newPosition, id);
        }

        public void UpdateContentColumnPosition(Int32 id, int contentRowId, Int32 oldPosition, Int32 newPosition)
        {
            var result = 0;

            if (oldPosition > newPosition)
            {
                result = contentRepo
                    .Context
                    .ExecuteSqlCommand("UPDATE ContentColumns SET Position = Position + 1 WHERE Position >= {0} AND Position < {1} AND ContentRowId = {2}", false, null, newPosition, oldPosition, contentRowId);
            }
            else if (oldPosition < newPosition)
            {
                result = contentRepo
                   .Context
                   .ExecuteSqlCommand("UPDATE ContentColumns SET Position = Position - 1 WHERE Position <= {0} AND Position > {1} AND ContentRowId = {2}", false, null, newPosition, oldPosition, contentRowId);
            }

            result = contentRepo
                   .Context
                   .ExecuteSqlCommand("UPDATE ContentColumns SET Position = {0} WHERE Id = {1}", false, null, newPosition, id);
        }


        public void RemoveContent(Int32 id)
        {
            var content = contentRepo.GetById(id);

            if (content != null)
            {
                contentRepo.Delete(content);
                UnitOfWork.Commit();

                contentRepo.Context.ExecuteSqlCommand(
                    "UPDATE Contents SET Position = Position - 1 WHERE Position > {0} AND ContentColumnId = {1}", false, null, content.Position, content.ContentColumnId);

            }
        }

        public void RemoveContentColumn(Int32 id)
        {
            var contentColumn = contentColumnRepo.GetById(id);

            if (contentColumn != null)
            {
                contentColumnRepo.Delete(contentColumn);
                UnitOfWork.Commit();

                contentColumnRepo.Context.ExecuteSqlCommand(
                   "UPDATE ContentColumns SET Position = Position - 1 WHERE Position > {0} AND ContentRowId = {1}", false, null, contentColumn.Position, contentColumn.ContentRowId);

            }
        }

        public void RemoveContentRow(Int32 id, Int32 itemId, ContentRowType type)
        {
            var contentRow = contentRowRepo.GetById(id);

            if (contentRow != null)
            {
                contentRowRepo.Delete(contentRow);
                UnitOfWork.Commit();
            }

            switch (type)
            {
                case ContentRowType.Page:
                    {
                        var item = pageRepo.GetById(itemId);

                        if (item.ContentRows != null)
                        {
                            var rows = item.ContentRows.OrderBy(o => o.Position).ToList();

                            for (var i = 0; i < rows.Count(); i++)
                            {
                                rows[i].Position = i + 1;
                                contentRowRepo.Update(rows[i]);
                                UnitOfWork.Commit();
                            }
                        }
                        break;
                    }
                case ContentRowType.Person:
                    {
                        var item = personRepo.GetById(itemId);

                        if (item.ContentRows != null)
                        {
                            var rows = item.ContentRows.OrderBy(o => o.Position).ToList();

                            for (var i = 0; i < rows.Count(); i++)
                            {
                                rows[i].Position = i + 1;
                                contentRowRepo.Update(rows[i]);
                                UnitOfWork.Commit();
                            }
                        }
                        break;
                    }
                case ContentRowType.Article:
                    {
                        var item = articleRepo.GetById(itemId);

                        if (item.ContentRows != null)
                        {
                            var rows = item.ContentRows.OrderBy(o => o.Position).ToList();

                            for (var i = 0; i < rows.Count(); i++)
                            {
                                rows[i].Position = i + 1;
                                contentRowRepo.Update(rows[i]);
                                UnitOfWork.Commit();
                            }
                        }
                        break;
                    }
                case ContentRowType.Enterprise:
                    {
                        var item = enterpriseRepo.GetById(itemId);

                        if (item.ContentRows != null)
                        {
                            var rows = item.ContentRows.OrderBy(o => o.Position).ToList();

                            for (var i = 0; i < rows.Count(); i++)
                            {
                                rows[i].Position = i + 1;
                                contentRowRepo.Update(rows[i]);
                                UnitOfWork.Commit();
                            }
                        }
                        break;
                    }
            }
        }

        private ContentColumn GetNewColumn(int width, int offset = 0, int xsWidth = 12, int xsOffset = 0)
        {
            var column = new ContentColumn()
            {
                LgWidth = width,
                MdWidth = width,
                SmWidth = width,
                XsWidth = xsWidth,
                LgOffset = offset,
                MdOffset = offset,
                SmOffset = offset,
                XsOffset = xsOffset,
                BackgroundColor = "",
                BackgroundImage = "none",
                BackgroundRepeat = "no-repeat",
                BackgroundPosition = "center",
                BackgroundSize = "cover"
            };

            return column;
        }

        private ContentEdit GetContentEditType(Content content, Boolean isNew = false)
        {
            ContentEdit contentEdit;
            var config = Configuration.Default.WithCss();
            var parser = new HtmlParser(config);
            var type = content.Type;

            content.BodyRu = content.BodyRu ?? String.Empty;
            content.BodyUk = content.BodyUk ?? String.Empty;
            content.BodyEn = content.BodyEn ?? String.Empty;

            var documentRu = parser.Parse(content.BodyRu);
            var documentUk = parser.Parse(content.BodyUk);
            var documentEn = parser.Parse(content.BodyEn);

            switch (type)
            {
                case (int)ContentType.Ckeditor:
                    {
                        contentEdit = new CkeContent();

                        var textRu = documentRu.GetElementsByClassName("content-text").Select(x => x.InnerHtml).DefaultIfEmpty(String.Empty).FirstOrDefault();
                        var textUk = documentUk.GetElementsByClassName("content-text").Select(x => x.InnerHtml).DefaultIfEmpty(String.Empty).FirstOrDefault();
                        var textEn = documentEn.GetElementsByClassName("content-text").Select(x => x.InnerHtml).DefaultIfEmpty(String.Empty).FirstOrDefault();

                        (contentEdit as CkeContent).TextRu = textRu;
                        (contentEdit as CkeContent).TextUk = textUk;
                        (contentEdit as CkeContent).TextEn = textEn;

                        break;
                    }
                case (int)ContentType.Table:
                    {
                        contentEdit = new TableContent();

                        var titleRu = documentRu.GetElementsByClassName("content-desc").Select(x => x.InnerHtml).DefaultIfEmpty(String.Empty).FirstOrDefault();
                        var titleUk = documentUk.GetElementsByClassName("content-desc").Select(x => x.InnerHtml).DefaultIfEmpty(String.Empty).FirstOrDefault();
                        var titleEn = documentEn.GetElementsByClassName("content-desc").Select(x => x.InnerHtml).DefaultIfEmpty(String.Empty).FirstOrDefault();

                        var tableRu = documentRu.GetElementsByClassName("table-responsive").Select(x => x.InnerHtml).DefaultIfEmpty("<table style='width:100%;'><tbody><tr><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td>&nbsp;</td><td>&nbsp;</td></tr></tbody></table>").FirstOrDefault();
                        var tableUk = documentUk.GetElementsByClassName("table-responsive").Select(x => x.InnerHtml).DefaultIfEmpty("<table style='width:100%;'><tbody><tr><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td>&nbsp;</td><td>&nbsp;</td></tr></tbody></table>").FirstOrDefault();
                        var tableEn = documentEn.GetElementsByClassName("table-responsive").Select(x => x.InnerHtml).DefaultIfEmpty("<table style='width:100%;'><tbody><tr><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td>&nbsp;</td><td>&nbsp;</td></tr></tbody></table>").FirstOrDefault();

                        (contentEdit as TableContent).TextRu = tableRu;
                        (contentEdit as TableContent).TextUk = tableUk;
                        (contentEdit as TableContent).TextEn = tableEn;
                        (contentEdit as TableContent).TitleRu = titleRu;
                        (contentEdit as TableContent).TitleUk = titleUk;
                        (contentEdit as TableContent).TitleEn = titleEn;
                        break;
                    }

                case (int)ContentType.Image:
                    {
                        contentEdit = new UploadImage();

                        var titleRu = documentRu.GetElementsByClassName("content-desc").Select(x => x.InnerHtml).DefaultIfEmpty(String.Empty).FirstOrDefault();
                        var titleUk = documentUk.GetElementsByClassName("content-desc").Select(x => x.InnerHtml).DefaultIfEmpty(String.Empty).FirstOrDefault();
                        var titleEn = documentEn.GetElementsByClassName("content-desc").Select(x => x.InnerHtml).DefaultIfEmpty(String.Empty).FirstOrDefault();
                        var image = documentUk.GetElementsByTagName("img").Select(x => x.GetAttribute("src")).DefaultIfEmpty(null).FirstOrDefault();
                        var url = documentUk.GetElementsByTagName("img").Select(x => x.GetAttribute("data-link")).DefaultIfEmpty(String.Empty).FirstOrDefault();
                        var allowFullscreen = documentUk.GetElementsByClassName("slide-gallery").Select(x => x.HasAttribute("data-allowfullscreen")).DefaultIfEmpty(false).FirstOrDefault();

                        (contentEdit as UploadImage).TitleRu = titleRu;
                        (contentEdit as UploadImage).TitleUk = titleUk;
                        (contentEdit as UploadImage).TitleEn = titleEn;
                        (contentEdit as UploadImage).Url = titleEn;
                        (contentEdit as UploadImage).Image = image;
                        (contentEdit as UploadImage).AllowFullscreen = allowFullscreen;

                        break;
                    }

                case (int)ContentType.Gallery:
                    {

                        contentEdit = new GalleryEdit()
                        {
                            GalleryItems = new List<GalleryItemEdit>()
                        };

                        var descRu = documentRu.GetElementsByClassName("content-desc").Select(x => x.InnerHtml).DefaultIfEmpty(String.Empty).FirstOrDefault();
                        var descUk = documentUk.GetElementsByClassName("content-desc").Select(x => x.InnerHtml).DefaultIfEmpty(String.Empty).FirstOrDefault();
                        var descEn = documentEn.GetElementsByClassName("content-desc").Select(x => x.InnerHtml).DefaultIfEmpty(String.Empty).FirstOrDefault();

                        foreach (var img in documentUk.QuerySelectorAll("img"))
                        {
                            var galleryItem = new GalleryItemEdit()
                            {
                                Image = img.GetAttribute("src"),
                                NameUk = img.GetAttribute("title"),
                                NameRu = documentRu.QuerySelectorAll("img").Where(x => x.GetAttribute("src") == img.GetAttribute("src")).Select(x => x.GetAttribute("title")).DefaultIfEmpty(String.Empty).FirstOrDefault(),
                                NameEn = documentEn.QuerySelectorAll("img").Where(x => x.GetAttribute("src") == img.GetAttribute("src")).Select(x => x.GetAttribute("title")).DefaultIfEmpty(String.Empty).FirstOrDefault()
                            };

                            (contentEdit as GalleryEdit).GalleryItems.Add(galleryItem);
                        }

                        (contentEdit as GalleryEdit).DescriptionEn = descEn;
                        (contentEdit as GalleryEdit).DescriptionRu = descRu;
                        (contentEdit as GalleryEdit).DescriptionUk = descUk;

                        break;
                    }
                case (int)ContentType.Video:
                    {
                        contentEdit = new VideoEdit();

                        var video = documentUk.GetElementsByClassName("content-video").Select(x => x.InnerHtml).DefaultIfEmpty(String.Empty).FirstOrDefault();

                        (contentEdit as VideoEdit).Body = video;

                        break;
                    }

                case (int)ContentType.Map:
                    {
                        var elemUk = documentUk.GetElementsByClassName("content-map").FirstOrDefault();
                        var elemRu = documentRu.GetElementsByClassName("content-map").FirstOrDefault();
                        var elemEn = documentEn.GetElementsByClassName("content-map").FirstOrDefault();

                        var mapContainer = documentUk.GetElementsByClassName("content-map-container");

                        contentEdit = new MapEdit()
                        {
                            CenterLat = mapContainer.Select(x => x.GetAttribute("data-lat")).DefaultIfEmpty("0").FirstOrDefault(),
                            CenterLng = mapContainer.Select(x => x.GetAttribute("data-lng")).DefaultIfEmpty("0").FirstOrDefault(),
                            Zoom = mapContainer.Select(x => x.GetAttribute("data-zoom")).DefaultIfEmpty("13").FirstOrDefault(),
                            Height = mapContainer.Select(x => x.GetAttribute("data-height")).DefaultIfEmpty("300").FirstOrDefault(),
                            MapPoints = new List<MapPointEdit>(),
                        };

                        if (elemUk != null)
                        {
                            foreach (var x in elemUk.GetElementsByClassName("map-point"))
                            {
                                var mapPoint = new MapPointEdit();

                                mapPoint.Lat = x.GetAttribute("data-lat");
                                mapPoint.Lng = x.GetAttribute("data-lng");
                                mapPoint.DescriptionUk = x.GetAttribute("data-desc");
                                mapPoint.Icon = x.GetAttribute("data-icon");
                                mapPoint.TitleUk = x.GetAttribute("data-title");

                                (contentEdit as MapEdit).MapPoints.Add(mapPoint);
                            }

                            if (elemRu != null)
                            {
                                for (var i = 0; i < elemRu.GetElementsByClassName("map-point").Count(); i++)
                                {
                                    (contentEdit as MapEdit).MapPoints[i].DescriptionRu = elemRu.GetElementsByClassName("map-point")[i].GetAttribute("data-desc");
                                    (contentEdit as MapEdit).MapPoints[i].TitleRu = elemRu.GetElementsByClassName("map-point")[i].GetAttribute("data-title");
                                 }
                            }

                            if (elemEn != null)
                            {
                                for (var i = 0; i < elemEn.GetElementsByClassName("map-point").Count(); i++)
                                {
                                    (contentEdit as MapEdit).MapPoints[i].DescriptionEn = elemEn.GetElementsByClassName("map-point")[i].GetAttribute("data-desc");
                                    (contentEdit as MapEdit).MapPoints[i].TitleEn = elemEn.GetElementsByClassName("map-point")[i].GetAttribute("data-title");
                                }
                            }  
                        }

                        break;
                    }
                case (int)ContentType.Html:
                    {
                        contentEdit = new HtmlContent();

                        var htmlRu = documentRu.GetElementsByClassName("content-html").Select(x => x.InnerHtml).DefaultIfEmpty(String.Empty).FirstOrDefault();
                        var htmlUk = documentUk.GetElementsByClassName("content-html").Select(x => x.InnerHtml).DefaultIfEmpty(String.Empty).FirstOrDefault();
                        var htmlEn = documentEn.GetElementsByClassName("content-html").Select(x => x.InnerHtml).DefaultIfEmpty(String.Empty).FirstOrDefault();

                        (contentEdit as HtmlContent).BodyRu = htmlRu;
                        (contentEdit as HtmlContent).BodyUk = htmlUk;
                        (contentEdit as HtmlContent).BodyEn = htmlEn;

                        break;
                    }
                case (int)ContentType.File:
                    {
                        contentEdit = new FileUpload();

                        var textAlign = documentUk.GetElementsByClassName("content-file").Select(x => x.ClassName.Contains("text-") ? x.ClassList.Where(c => c.StartsWith("text-")).FirstOrDefault().Substring(5) : "left").DefaultIfEmpty("left").FirstOrDefault();
                        var marginBottom = documentUk.GetElementsByClassName("content-file").Select(x => (x.Style != null && !String.IsNullOrEmpty(x.Style.MarginBottom)) ? Int32.Parse(x.Style.MarginBottom.Replace("px", "")) : 0).DefaultIfEmpty(0).FirstOrDefault();
                        var fontSize = documentUk.GetElementsByClassName("content-file").Select(x => (x.Style != null && !String.IsNullOrEmpty(x.Style.FontSize)) ? Int32.Parse(x.Style.FontSize.Replace("px", "")) : 0).DefaultIfEmpty(0).FirstOrDefault();
                        var fontWeight = documentUk.GetElementsByTagName("a").Select(x => (x.Style != null && !String.IsNullOrEmpty(x.Style.FontWeight)) ? Int32.Parse(x.Style.FontWeight) : 400).DefaultIfEmpty(400).FirstOrDefault();
                        var fontStyle = documentUk.GetElementsByTagName("a").Select(x => (x.Style != null && !String.IsNullOrEmpty(x.Style.FontStyle)) ? x.Style.FontStyle : "normal").DefaultIfEmpty("normal").FirstOrDefault();
                        var showIcon = documentUk.GetElementsByClassName("fa").Count() > 0 ? true : (isNew ? true : false);
                        var nameRu = documentRu.GetElementsByTagName("a").Select(x => x.InnerHtml).DefaultIfEmpty(String.Empty).FirstOrDefault();
                        var nameUk = documentUk.GetElementsByTagName("a").Select(x => x.InnerHtml).DefaultIfEmpty(String.Empty).FirstOrDefault();
                        var nameEn = documentEn.GetElementsByTagName("a").Select(x => x.InnerHtml).DefaultIfEmpty(String.Empty).FirstOrDefault();

                        (contentEdit as FileUpload).NameRu = nameRu.EndsWith(")") ? nameRu.Substring(0, nameRu.LastIndexOf("(") - 1) : nameRu;
                        (contentEdit as FileUpload).NameUk = nameUk.EndsWith(")") ? nameUk.Substring(0, nameUk.LastIndexOf("(") - 1) : nameUk;
                        (contentEdit as FileUpload).NameEn = nameEn.EndsWith(")") ? nameEn.Substring(0, nameEn.LastIndexOf("(") - 1) : nameEn;
                        (contentEdit as FileUpload).Url = documentUk.GetElementsByTagName("a").Select(x => x.GetAttribute("href")).DefaultIfEmpty(null).FirstOrDefault();
                        (contentEdit as FileUpload).FontSize = fontSize;
                        (contentEdit as FileUpload).FontStyle = fontStyle;
                        (contentEdit as FileUpload).FontWeight = fontWeight;
                        (contentEdit as FileUpload).TextAlign = textAlign;
                        (contentEdit as FileUpload).MarginBottom = marginBottom;
                        (contentEdit as FileUpload).ShowIcon = showIcon;

                        break;
                    }
                case (int)ContentType.Widget:
                    {
                        contentEdit = new WidgetContent()
                        {
                            Name = content.BodyUk,
                            Widgets = selectListService.GetContentWidgetSelectList()
                        };

                        break;
                    }
                case (int)ContentType.Menu:
                    {
                        contentEdit = new MenuContent()
                        {
                            MenuId = content.BodyUk,
                            Menus = selectListService.GetMenuSelectList()
                        };

                        break;
                    }
                case (int)ContentType.Accordion:
                    {
                        contentEdit = new AccordionContent()
                        {
                            MenuId = content.BodyUk,
                            Menus = selectListService.GetMenuSelectList()
                        };

                        break;
                    }
                case (int)ContentType.TextBlock:
                    {
                        contentEdit = new TextBlockContent();

                        var textRu = documentRu.GetElementsByClassName("content-textblock-body").Select(x => x.InnerHtml).DefaultIfEmpty(String.Empty).FirstOrDefault();
                        var textUk = documentUk.GetElementsByClassName("content-textblock-body").Select(x => x.InnerHtml).DefaultIfEmpty(String.Empty).FirstOrDefault();
                        var textEn = documentEn.GetElementsByClassName("content-textblock-body").Select(x => x.InnerHtml).DefaultIfEmpty(String.Empty).FirstOrDefault();
                        var titleRu = documentRu.GetElementsByClassName("content-textblock-title").Select(x => x.InnerHtml).DefaultIfEmpty(String.Empty).FirstOrDefault();
                        var titleUk = documentUk.GetElementsByClassName("content-textblock-title").Select(x => x.InnerHtml).DefaultIfEmpty(String.Empty).FirstOrDefault();
                        var titleEn = documentEn.GetElementsByClassName("content-textblock-title").Select(x => x.InnerHtml).DefaultIfEmpty(String.Empty).FirstOrDefault();
                        var styleString = documentUk.GetElementsByClassName("content-textblock").Select(x => x.GetAttribute("style")).DefaultIfEmpty("background-color:#5089a0;color:#ffffff").FirstOrDefault().Split(';');

                        (contentEdit as TextBlockContent).BlockColor = styleString[0].Split(':')[1];
                        (contentEdit as TextBlockContent).TextColor = styleString[1].Split(':')[1];
                        (contentEdit as TextBlockContent).TextEn = textEn;
                        (contentEdit as TextBlockContent).TextRu = textRu;
                        (contentEdit as TextBlockContent).TextUk = textUk;
                        (contentEdit as TextBlockContent).TitleEn = titleEn;
                        (contentEdit as TextBlockContent).TitleRu = titleRu;
                        (contentEdit as TextBlockContent).TitleUk = titleUk;

                        break;
                    }
                case (int)ContentType.Collapse:
                    {
                        contentEdit = new CollapseContent();

                        var textRu = documentRu.GetElementsByClassName("collapse").Select(x => x.InnerHtml).DefaultIfEmpty(String.Empty).FirstOrDefault();
                        var textUk = documentUk.GetElementsByClassName("collapse").Select(x => x.InnerHtml).DefaultIfEmpty(String.Empty).FirstOrDefault();
                        var textEn = documentEn.GetElementsByClassName("collapse").Select(x => x.InnerHtml).DefaultIfEmpty(String.Empty).FirstOrDefault();

                        var titleRu = documentRu.GetElementsByClassName("collapse-title").Select(x => x.InnerHtml).DefaultIfEmpty(String.Empty).FirstOrDefault();
                        var titleUk = documentUk.GetElementsByClassName("collapse-title").Select(x => x.InnerHtml).DefaultIfEmpty(String.Empty).FirstOrDefault();
                        var titleEn = documentEn.GetElementsByClassName("collapse-title").Select(x => x.InnerHtml).DefaultIfEmpty(String.Empty).FirstOrDefault();

                        var expanded = documentRu.GetElementsByClassName("rotate-180").FirstOrDefault() != null;

                        (contentEdit as CollapseContent).TextRu = textRu;
                        (contentEdit as CollapseContent).TextUk = textUk;
                        (contentEdit as CollapseContent).TextEn = textEn;
                        (contentEdit as CollapseContent).TitleRu = titleRu;
                        (contentEdit as CollapseContent).TitleUk = titleUk;
                        (contentEdit as CollapseContent).TitleEn = titleEn;
                        (contentEdit as CollapseContent).Expanded = expanded;

                        break;
                    }
                default:
                    contentEdit = null;
                    break;
            }

            if (contentEdit != null)
            {
                contentEdit.Id = content.Id;
                contentEdit.Position = content.Position;
                contentEdit.Type = (ContentType)type;
                contentEdit.ContentColumnId = content.ContentColumnId;
                contentEdit.IsNew = isNew;

                return contentEdit;
            }
            else
                return null;

        }



        public List<String> GetContentText(ICollection<ContentRow> list, bool includeTables = true, bool includeImgDescription = true)
        {
            var parser = new HtmlParser();
            var model = new List<string>();
            

            var bodyTextRu = String.Empty;
            var bodyTextUk = String.Empty;
            var bodyTextEn = String.Empty;

            if (list != null)
            {
                foreach (var row in list)
                {
                    foreach (var col in row.ContentColumns)
                    {
                        foreach (var content in col.Contents)
                        {
                            switch (content.Type)
                            {
                                case (int)ContentType.Ckeditor:
                                    {
                                        if (content.BodyRu != null)
                                        {
                                            var documentRu = parser.Parse(content.BodyRu);
                                            bodyTextRu = bodyTextRu + " " + documentRu.GetElementsByClassName("content-text").Select(x => x.TextContent).DefaultIfEmpty(String.Empty).FirstOrDefault();
                                        }
                                        if (content.BodyUk != null)
                                        {
                                            var documentUk = parser.Parse(content.BodyUk);
                                            bodyTextUk = bodyTextUk + " " + documentUk.GetElementsByClassName("content-text").Select(x => x.TextContent).DefaultIfEmpty(String.Empty).FirstOrDefault();
                                        }
                                        if (content.BodyEn != null)
                                        {
                                            var documentEn = parser.Parse(content.BodyEn);                                           
                                            bodyTextEn = bodyTextEn + " " + documentEn.GetElementsByClassName("content-text").Select(x => x.TextContent).DefaultIfEmpty(String.Empty).FirstOrDefault();
                                        }
                                        break;
                                    }
                                case (int)ContentType.Table:
                                    {
                                        if (includeTables)
                                        {
                                            if (content.BodyRu != null)
                                            {
                                                var documentRu = parser.Parse(content.BodyRu);
                                                bodyTextRu = bodyTextRu + " " + documentRu.GetElementsByClassName("content-table").Select(x => x.TextContent).DefaultIfEmpty(String.Empty).FirstOrDefault();
                                            }
                                            if (content.BodyUk != null)
                                            {
                                                var documentUk = parser.Parse(content.BodyUk);
                                                bodyTextUk = bodyTextUk + " " + documentUk.GetElementsByClassName("content-table").Select(x => x.TextContent).DefaultIfEmpty(String.Empty).FirstOrDefault();
                                            }
                                            if (content.BodyEn != null)
                                            {
                                                var documentEn = parser.Parse(content.BodyEn);
                                                bodyTextEn = bodyTextEn + " " + documentEn.GetElementsByClassName("content-table").Select(x => x.TextContent).DefaultIfEmpty(String.Empty).FirstOrDefault();
                                            }
                                        }                                        
                                        break;
                                    }
                                case (int)ContentType.Image:
                                    {
                                        if (includeImgDescription)
                                        {
                                            if (content.BodyRu != null)
                                            {
                                                var documentRu = parser.Parse(content.BodyRu);
                                                bodyTextRu = bodyTextRu + " " + documentRu.QuerySelector("img").GetAttribute("title");
                                            }
                                            if (content.BodyUk != null)
                                            {
                                                var documentUk = parser.Parse(content.BodyUk);
                                                bodyTextUk = bodyTextUk + " " + documentUk.QuerySelector("img").GetAttribute("title");
                                            }
                                            if (content.BodyEn != null)
                                            {
                                                var documentEn = parser.Parse(content.BodyEn);
                                                bodyTextEn = bodyTextEn + " " + documentEn.QuerySelector("img").GetAttribute("title");
                                            }
                                        }
                                                                                
                                        break;
                                    }
                                case (int)ContentType.Gallery:
                                    {
                                        if (includeImgDescription)
                                        {
                                            var galleryRu = parser.Parse(content.BodyRu);
                                            var galleryUk = parser.Parse(content.BodyUk);
                                            var galleryEn = parser.Parse(content.BodyEn);

                                            if (galleryRu.GetElementsByClassName("gallery-desc").FirstOrDefault() != null)
                                                bodyTextRu = bodyTextRu + " " + galleryRu.GetElementsByClassName("gallery-desc").FirstOrDefault().InnerHtml;

                                            if (galleryUk.GetElementsByClassName("gallery-desc").FirstOrDefault() != null)
                                                bodyTextUk = bodyTextUk + " " + galleryUk.GetElementsByClassName("gallery-desc").FirstOrDefault().InnerHtml;

                                            if (galleryEn.GetElementsByClassName("gallery-desc").FirstOrDefault() != null)
                                                bodyTextEn = bodyTextEn + " " + galleryEn.GetElementsByClassName("gallery-desc").FirstOrDefault().InnerHtml;

                                            foreach (var img in galleryUk.QuerySelectorAll("img"))
                                            {
                                                bodyTextUk = bodyTextUk + " " + img.GetAttribute("title");
                                                bodyTextRu = bodyTextRu + " " + galleryRu.QuerySelectorAll("img").FirstOrDefault(x => x.GetAttribute("src") == img.GetAttribute("src")).GetAttribute("title");
                                                bodyTextEn = bodyTextEn + " " + galleryRu.QuerySelectorAll("img").FirstOrDefault(x => x.GetAttribute("src") == img.GetAttribute("src")).GetAttribute("title");
                                            }
                                        }
                                       
                                        break;
                                    }
                                case (int)ContentType.Collapse:
                                    {
                                        var documentRu = parser.Parse(content.BodyRu);
                                        var documentUk = parser.Parse(content.BodyUk);
                                        var documentEn = parser.Parse(content.BodyEn);

                                        bodyTextRu = bodyTextRu + " " + documentRu.GetElementsByClassName("collapse").Select(x => x.TextContent).DefaultIfEmpty(String.Empty).FirstOrDefault();
                                        bodyTextUk = bodyTextUk + " " + documentUk.GetElementsByClassName("collapse").Select(x => x.TextContent).DefaultIfEmpty(String.Empty).FirstOrDefault();
                                        bodyTextEn = bodyTextEn + " " + documentEn.GetElementsByClassName("collapse").Select(x => x.TextContent).DefaultIfEmpty(String.Empty).FirstOrDefault();

                                        break;
                                    }
                                case (int)ContentType.File:
                                    {
                                        if (includeImgDescription)
                                        {
                                            var documentRu = parser.Parse(content.BodyRu);
                                            var documentUk = parser.Parse(content.BodyUk);
                                            var documentEn = parser.Parse(content.BodyEn);

                                            bodyTextRu = bodyTextRu + " " + documentRu.GetElementsByTagName("a").Select(x => x.GetAttribute("data-name")).DefaultIfEmpty(String.Empty).FirstOrDefault();
                                            bodyTextUk = bodyTextUk + " " + documentRu.GetElementsByTagName("a").Select(x => x.GetAttribute("data-name")).DefaultIfEmpty(String.Empty).FirstOrDefault();
                                            bodyTextEn = bodyTextEn + " " + documentRu.GetElementsByTagName("a").Select(x => x.GetAttribute("data-name")).DefaultIfEmpty(String.Empty).FirstOrDefault();

                                        }

                                        break;
                                    }
                            }
                        }
                    }
                }
            }

            model.Add(bodyTextRu);
            model.Add(bodyTextUk);
            model.Add(bodyTextEn);

            return model;
        }

        public List<String> GetContentImages(ICollection<ContentRow> list)
        {
            var parser = new HtmlParser();
            var result = new List<string>();

            var bodyTextRu = String.Empty;
            var bodyTextUk = String.Empty;
            var bodyTextEn = String.Empty;

            if (list != null)
            {
                foreach (var row in list)
                {
                    foreach (var col in row.ContentColumns)
                    {
                        foreach (var content in col.Contents)
                        {
                            switch (content.Type)
                            {
                                case (int)ContentType.Image:
                                    {
                                            if (content.BodyUk != null)
                                            {
                                                var documentUk = parser.Parse(content.BodyUk);
                                                result.Add(documentUk.QuerySelector("img").GetAttribute("src"));
                                            }
                                        }

                                        break;
                                    
                                case (int)ContentType.Gallery:
                                    {
                                        
                                            var galleryUk = parser.Parse(content.BodyUk);
                                          
                                            foreach (var img in galleryUk.QuerySelectorAll("img"))
                                            {
                                                result.Add(img.GetAttribute("src"));                                                
                                            }
                                        

                                        break;
                                    }
                               
                            }
                        }
                    }
                }
            }

           
            return result;
        }

    }
}
