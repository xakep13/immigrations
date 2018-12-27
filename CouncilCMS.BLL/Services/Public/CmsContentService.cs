using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using Bissoft.CouncilCMS.Core;
using Bissoft.CouncilCMS.Core.Enums;
using Bissoft.CouncilCMS.Core.Globalization;
using Bissoft.CouncilCMS.DAL.Entities;
using Bissoft.CouncilCMS.BLL.ViewModels;
using AngleSharp.Parser.Html;

namespace Bissoft.CouncilCMS.BLL.Services
{
    class CmsContentService : BaseService
    {
        public CmsContentService(string connectionString) : base(connectionString) { }
        public CmsContentService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public List<CmsContentRow> GetContentRows(ICollection<ContentRow> list)
        {
            var model = new List<CmsContentRow>();

            if (list != null)
            {
                foreach (var row in list.OrderBy(x => x.Position))
                {
                    var rowItem = Mapper.Map<CmsContentRow>(row);
                  
                    foreach (var col in row.ContentColumns.OrderBy(x => x.Position))
                    {
                        var colItem = Mapper.Map<CmsContentColumn>(col);
                       
                        foreach (var content in col.Contents.OrderBy(x => x.Position))
                        {
                            var contentItem = Mapper.Map<CmsContent>(content);

                            colItem.Contents.Add(contentItem);
                        }

                        rowItem.Columns.Add(colItem);
                    }

                    model.Add(rowItem);
                }
            }

            return model;
        }


        public List<String> GetContentText(List<ContentRow> list, Int32 itemId)
        {
            var parser = new HtmlParser();
            var model = new List<string>();
            var bodyTextUk = "";
            var bodyTextRu = "";            
            var bodyTextEn = "";
            
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
                                       
                                        if (content.BodyUk != null)
                                        {
                                            var documentRu = parser.Parse(content.BodyRu);
                                            var documentUk = parser.Parse(content.BodyUk);
                                            var documentEn = parser.Parse(content.BodyEn);

                                            bodyTextUk += (" " + documentUk.GetElementsByClassName("content-text").FirstOrDefault().TextContent);
                                            bodyTextRu += (" " + documentRu.GetElementsByClassName("content-text").FirstOrDefault().TextContent);
                                            bodyTextEn += (" " + documentEn.GetElementsByClassName("content-text").FirstOrDefault().TextContent);

                                        }
                                        break;
                                    }
                                case (int)ContentType.Table:
                                    {                                        
                                        if (content.BodyUk != null)
                                        {
                                            var documentRu = parser.Parse(content.BodyRu);
                                            var documentUk = parser.Parse(content.BodyUk);
                                            var documentEn = parser.Parse(content.BodyEn);

                                            bodyTextUk += (" " + documentUk.GetElementsByClassName("content-table").FirstOrDefault().TextContent);
                                            bodyTextRu += (" " + documentRu.GetElementsByClassName("content-table").FirstOrDefault().TextContent);
                                            bodyTextEn += (" " + documentEn.GetElementsByClassName("content-table").FirstOrDefault().TextContent);
                                        }
                                        break;
                                    }
                                case (int)ContentType.Image:
                                    {                                       
                                        if (content.BodyUk != null)
                                        {
                                            var documentRu = parser.Parse(content.BodyRu);
                                            var documentUk = parser.Parse(content.BodyUk);
                                            var documentEn = parser.Parse(content.BodyEn);

                                            bodyTextUk += (" " + documentUk.QuerySelector("img").GetAttribute("title"));
                                            bodyTextRu += (" " + documentRu.QuerySelector("img").GetAttribute("title"));
                                            bodyTextEn += (" " + documentEn.QuerySelector("img").GetAttribute("title"));
                                        }
                                        break;
                                    }
                                case (int)ContentType.Gallery:
                                    {
                                        var documentRu = parser.Parse(content.BodyRu);
                                        var documentUk = parser.Parse(content.BodyUk);
                                        var documentEn = parser.Parse(content.BodyEn);

                                        if (documentUk.GetElementsByClassName("gallery-desc").FirstOrDefault() != null)
                                            bodyTextUk += (" " + documentUk.GetElementsByClassName("gallery-desc").FirstOrDefault().InnerHtml);

                                        if (documentRu.GetElementsByClassName("gallery-desc").FirstOrDefault() != null)
                                            bodyTextRu += (" " + documentRu.GetElementsByClassName("gallery-desc").FirstOrDefault().InnerHtml);

                                        if (documentEn.GetElementsByClassName("gallery-desc").FirstOrDefault() != null)
                                            bodyTextEn += (" " + documentEn.GetElementsByClassName("gallery-desc").FirstOrDefault().InnerHtml);

                                        foreach (var img in documentUk.QuerySelectorAll("img"))
                                        {
                                            bodyTextUk += (" " + img.GetAttribute("title"));
                                            bodyTextRu += (" " + documentRu.QuerySelectorAll("img").FirstOrDefault(x => x.GetAttribute("src") == img.GetAttribute("src")).GetAttribute("title"));
                                            bodyTextEn += (" " + documentEn.QuerySelectorAll("img").FirstOrDefault(x => x.GetAttribute("src") == img.GetAttribute("src")).GetAttribute("title"));
                                        }

                                        break;
                                    }                                
                                case (int)ContentType.File:
                                    {
                                        var documentRu = parser.Parse(content.BodyRu);
                                        var documentUk = parser.Parse(content.BodyUk);
                                        var documentEn = parser.Parse(content.BodyEn);

                                        bodyTextUk += (" " + documentRu.GetElementsByTagName("a").FirstOrDefault().GetAttribute("data-name"));
                                        bodyTextRu += (" " + documentRu.GetElementsByTagName("a").FirstOrDefault().GetAttribute("data-name"));
                                        bodyTextEn += (" " + documentEn.GetElementsByTagName("a").FirstOrDefault().GetAttribute("data-name"));

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
    }
}
