using System;
using System.Collections.Generic;
using System.Linq;
using Bissoft.CouncilCMS.Core;
using Bissoft.CouncilCMS.Core.Enums;
using Bissoft.CouncilCMS.Core.Globalization;
using Bissoft.CouncilCMS.DAL.Entities;
using Bissoft.CouncilCMS.BLL.ViewModels;
using xTab.Tools.Helpers;


namespace Bissoft.CouncilCMS.BLL.Services
{
    public class CmsArticleService : BaseService
    {
        private IRepository<Article, int> articleRepo;
        private IRepository<ArticleCategory, int> articleCatRepo;
        private CmsContentService contentService;
        private CmsSearchService cmsSearchService;
        private CmsMenuService menuService;
        public CmsArticleService(string ConnectionString) : base(ConnectionString)
        {
            Initialize();
        }

        public CmsArticleService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            Initialize();
        }

        private void Initialize()
        {
            articleRepo = UnitOfWork.GetIntRepository<Article>();
            articleCatRepo = UnitOfWork.GetIntRepository<ArticleCategory>();
            contentService = new CmsContentService(this.UnitOfWork);            
            cmsSearchService = new CmsSearchService(this.UnitOfWork);
            menuService = new CmsMenuService(this.UnitOfWork);
        }

        public CmsArticleList CategoryList(string articleUrl, ShowMenuItemMode Mode, string query = null, string date = null, int page = 1, int perPage = 20, int? excludeId = null, string excludeCategory = null)
        {
            var category = articleCatRepo.GetSingle(x => x.UrlName == articleUrl, null, true, true);

            if (category != null)
            {
                if (category.Template == null || category.Template.Name != "news-by-days")
                {
                    var relCategory = articleCatRepo.GetById(category.RelatedCategoryId ?? 0, asNoTracking:true) ?? new ArticleCategory();
                    var now = DateTime.Now;
                    var count = 0;

                    var predicate = PredicateBuilder.True<Article>();

                    if (!String.IsNullOrEmpty(query))
                    {
                        var ids = cmsSearchService.Find(query, CmsSearchType.Article);

                        if (ids != null && ids.Count > 0)
                        {
                            var itemIds = cmsSearchService.GetItemIds(ids);

                            predicate = predicate.And(x => itemIds.Contains(x.Id));
                        }
                        
                    }

                    if (!String.IsNullOrEmpty(date))
                    {
                        var fromDateParse = DateTimeHelper.ParseDateNullable(date, null);

                        if (fromDateParse != null)
                        {
                            var fromDate = fromDateParse.Value.Date;
                            var toDate = fromDateParse.Value.AddDays(1).AddSeconds(-1);

                            predicate = predicate.And(x => (x.PublishDate >= fromDate && x.PublishDate <= toDate) || (x.EventDate >= fromDate && x.EventDate <= toDate));
                        }
                    }

                    if (excludeId > 0)
                    {
                        predicate = predicate.And(x => x.Id != excludeId.Value);
                    }

                    if (!String.IsNullOrEmpty(excludeCategory))
                    {                        
                        predicate = predicate.And(x => !x.Categories.Any(c => c.UrlName == excludeCategory));
                    }


                    predicate = predicate.And(x => x.Categories.Any(c => c.Id == category.Id) && x.Published && x.PublishDate != null && x.PublishDate <= now);

                    var list = articleRepo
                        .GetList(out count,
                        predicate,
                        x => x.OrderByDescending(o => o.PublishDate),
                        "Categories", (page - 1) * perPage, perPage).Select(x =>
                        new
                        {
                            Id = x.Id,
                            UrlName = x.UrlNameRu,
                            UrlNameUk = x.UrlNameUk,
                            TitleRu = x.TitleRu,
                            TitleUk = x.TitleUk,
                            TitleEn = x.TitleEn,
                            DescriptionRu = x.DescriptionRu,
                            DescriptionUk = x.DescriptionUk,
                            DescriptionEn = x.DescriptionEn,
                            Image = x.Image,
                            ViewCount = x.ViewCount,
                            EventDate = x.EventDate,
                            PublishDate = x.PublishDate,
                            CategoryTitle = x.Categories.OrderByDescending(c => c.Priority).FirstOrDefault()?.TitleUk,
                            IsAnons = x.Categories.Any(c => c.Id == 16)
                        }).ToList().Select(x => new CmsArticleListItem()
                        {
                            Id = x.Id,
                            Title = x.GetLocalValue("Title"),
                            Description = x.GetLocalValue("Description"),
                            UrlName = x.GetLocalValue("UrlName"),
                            PublishDateTime = x.PublishDate,
                            PublishDate = DateTimeHelper.DateTimeString(x.PublishDate.Value, format: "dd MMMM yyyy HH:mm"),
                            EventDate = x.EventDate,
                            CategoryTitle = x.CategoryTitle,
                            Image = x.Image,
                            IsAnons = x.IsAnons,
                            ViewCount = x.ViewCount,
                        }).ToList();

                    var model = new CmsArticleList()
                    {
                        Page = page,
                        PerPage = perPage,
                        Count = count,
                        CategoryId = category.Id,
                        CategoryName = category.GetLocalValue("Title"),
                        CategoryDescription = category.GetLocalValue("Description"),
                        CategoryUrl = category.UrlName,
                        RelatedCategoryId = relCategory.Id,
                        RelatedCategoryName = relCategory.GetLocalValue("Title"),
                        RelatedCategoryUrl = relCategory.UrlName,                        
                        TemplateId = category.TemplateId ?? 0,
                        TemplateName = category.Template != null ? category.Template.Name : string.Empty,
                        ShowSearchForm = category.ShowSearchForm,
                        Articles = list,
                        CategoryMenu = category.SidebarMenuId > 0 ? menuService.GetMenu(category.SidebarMenuId.Value, Mode) : null
                    };

                    return model;
                }
                else
                {
                    var relCategory = articleCatRepo.GetById(category.RelatedCategoryId ?? 0) ?? new ArticleCategory();
                    var now = DateTime.Now;

                    var dates = articleRepo.GetList(filter: x => x.Categories.Any(c => c.Id == category.Id) && x.Published && x.PublishDate != null && x.PublishDate <= now).Select(x => x.PublishDate.Value).ToList().Select(x => x.Date).Distinct().OrderByDescending(d => d).ToList();
                    var count = dates.Count();

                    var curDates = dates.Skip((page - 1) * 3).Take(3).OrderBy(x => x.Date).ToList();

                    if (curDates != null && curDates.Count() > 0)
                    {
                        var fromDate = curDates.First().Date;
                        var toDate = curDates.Last().AddDays(1).AddTicks(-1);

                        var predicate = PredicateBuilder.True<Article>();

                        predicate = predicate.And(x => x.PublishDate >= fromDate && x.PublishDate <= toDate && x.Categories.Any(c => c.Id == category.Id) && x.Published && x.PublishDate != null);

                        if (!String.IsNullOrEmpty(excludeCategory))
                        {
                            predicate = predicate.And(x => !x.Categories.Any(c => c.UrlName == excludeCategory));
                        }

                        var list = articleRepo
                            .GetList(
                            predicate,
                            x => x.OrderByDescending(o => o.PublishDate)).Select(x =>
                            new
                            {
                                Id = x.Id,
                                UrlName = x.UrlNameRu,
                                UrlNameUk = x.UrlNameUk,
                                TitleRu = x.TitleRu,
                                TitleUk = x.TitleUk,
                                TitleEn = x.TitleEn,
                                DescriptionRu = x.DescriptionRu,
                                DescriptionUk = x.DescriptionUk,
                                DescriptionEn = x.DescriptionEn,
                                Image = x.Image,
                                PublishDate = x.PublishDate,
                                EventDate = x.EventDate,                               
                            }).ToList().Select(x => new CmsArticleListItem()
                            {
                                Id = x.Id,
                                Title = x.GetLocalValue("Title"),
                                Description = x.GetLocalValue("Description"),
                                UrlName = x.GetLocalValue("UrlName"),
                                PublishDateTime = x.PublishDate,
                                PublishDate = DateTimeHelper.DateTimeString(x.PublishDate.Value, format: "dd MMMM yyyy HH:mm"),
                                EventDate = x.EventDate,
                                Image = x.Image,                                
                            }).ToList();

                        var model = new CmsArticleList()
                        {
                            Page = page,
                            PerPage = 3,
                            Count = count,
                            CategoryId = category.Id,
                            CategoryName = category.GetLocalValue("Title"),
                            CategoryDescription = category.GetLocalValue("Description"),
                            CategoryUrl = category.UrlName,
                            RelatedCategoryId = relCategory.Id,
                            RelatedCategoryName = relCategory.GetLocalValue("Title"),
                            RelatedCategoryUrl = relCategory.UrlName,                            
                            TemplateId = category.TemplateId ?? 0,
                            TemplateName = category.Template != null ? category.Template.Name : string.Empty,
                            ShowSearchForm = category.ShowSearchForm,
                            Articles = list,
                            CategoryMenu = category.SidebarMenuId > 0 ? menuService.GetMenu(category.SidebarMenuId.Value, Mode) : null
                        };

                        return model;
                    }
                    
                }
            }

            return null;
        }

        
        public CmsArticle Article(Int32 id, ShowMenuItemMode Mode, int? userId = null)
        {
            var model = new CmsArticle();
            var item = articleRepo.GetById(id, asNoTracking: true);
            var category = item.Categories.OrderByDescending(x => x.Priority).ThenBy(x => x.TitleUk).FirstOrDefault() ?? new ArticleCategory() { RelatedCategoryId = 0 };
            var relCategory = category.ParentCategoryId > 0 ? articleCatRepo.GetById(category.ParentCategoryId ?? 0) : category;

            if (item == null || (userId == null && (!item.Published || item.Deleted || item.PublishDate == null || item.PublishDate > DateTime.Now)))
                return null;
            
            model.Id = item.Id;
            model.Url = item.GetLocalValue("UrlName");
            model.CategoryId = category.Id;
            model.CategoryUrl = category.UrlName;
            model.CategoryName = category.GetLocalValue("Title");
            model.RelatedCategoryId = relCategory.Id;
            model.RelatedCategoryUrl = relCategory.UrlName;
            model.Description = item.GetLocalValue("Description");
            model.EventDate = DateTimeHelper.NullableDateTimeString(item.EventDate);
            model.Image = item.Image;
            model.MetaDescription = item.GetLocalValue("MetaDescriptionRu");
            model.MetaKeywords = item.GetLocalValue("MetaKeywords");
            model.MetaTitle = item.GetLocalValue("MetaTitle");
            model.PublishDate = item.ShowPublihDate ? DateTimeHelper.NullableDateTimeString(item.PublishDate) : String.Empty;
            model.LastEditDate = item.ShowEditDate ? DateTimeHelper.NullableDateTimeString(item.EditedDate) : String.Empty;
            model.ShowComments = item.AllowComments;
            model.Title = item.GetLocalValue("Title");
            model.ArticleMenu = category.SidebarMenuId > 0 ? menuService.GetMenu(category.SidebarMenuId.Value, Mode) : null;
            model.ContentRows = contentService.GetContentRows(item.ContentRows);

            AddView(id);

            return model;            
        }

        public CmsArticle LastArticleInCategory(Int32 id, Boolean withContent = false)
        {
            var category = articleCatRepo.GetById(id);

            var date = DateTime.Now;

            var item = articleRepo.GetList(
                x => x.Categories.Any(c => x.Id == id) && x.Published && x.PublishDate != null && x.PublishDate <= date,
                x => x.OrderByDescending(o => o.PublishDate), (withContent ? "ContentRows" : null),
                0, 1).FirstOrDefault();

            if (item != null)
            {

                var model = new CmsArticle()
                {
                    Id = item.Id,
                    Url = item.GetLocalValue("UrlName"),
                    Description = item.GetLocalValue("Description"),
                    EventDate = item.EventDate.ToString(),
                    Image = item.Image,
                    PublishDate = DateTimeHelper.NullableDateTimeString(item.PublishDate, Format: "dd MMMM yyyy HH:mm"),
                    Title = item.GetLocalValue("Title"),
                    ContentRows = withContent ? contentService.GetContentRows(item.ContentRows) : null,
                    ViewCount = item.ViewCount
                };

                return model;
            }
            else
            {
                return null;
            }
        }

        public CmsArticle LastArticleInCategory(String url, Boolean withContent = false)
        {
            var category = articleCatRepo.GetSingle(x => x.UrlName == url, null, true, true);

            var date = DateTime.Now;

            var item = articleRepo.GetList(
                x => x.Categories.Any(c => c.Id == category.Id) && x.Published && x.PublishDate != null && x.PublishDate <= date,
                x => x.OrderByDescending(o => o.PublishDate), "Categories" + (withContent ? ",ContentRows" : null)).FirstOrDefault();

            if (item != null)
            {

                var model = new CmsArticle()
                {
                    Id = item.Id,
                    Url = item.GetLocalValue("UrlName"),
                    Description = item.GetLocalValue("Description"), 
                    Image = item.Image,
                    PublishDate = DateTimeHelper.NullableDateTimeString(item.PublishDate, Format: "dd MMMM yyyy HH:mm"),
                    EventDate = DateTimeHelper.NullableDateTimeString(item.PublishDate, Format: "dd MMMM yyyy HH:mm"),
                    Title = item.GetLocalValue("Title"),
                    ContentRows = withContent ? contentService.GetContentRows(item.ContentRows) : null,
                    ViewCount = item.ViewCount
                };

                return model;
            }
            else
            {
                return null;
            }
        }

        public CmsArticleList LastArticles(string articleUrl, int limit = 5, int? excludeId = null)
        {
            var category = articleCatRepo.GetSingle(x => x.UrlName == articleUrl, null, true, true);

            if (category != null)
            {
                var now = DateTime.Now;
                var count = 0;

                var predicate = PredicateBuilder.True<Article>();

                if (excludeId > 0)
                    predicate = predicate.And(x => x.Id != excludeId.Value);

                predicate = predicate.And(x => x.Categories.Any(c => c.Id == category.Id) && x.Published && x.PublishDate != null && x.PublishDate <= now);

                var list = articleRepo
                    .GetList(out count,
                    predicate,
                    x => x.OrderByDescending(o => o.PublishDate),
                    "Categories", 0, limit).Select(x =>
                    new
                    {
                        Id = x.Id,
                        UrlName = x.UrlNameRu,
                        UrlNameUk = x.UrlNameUk,
                        TitleRu = x.TitleRu,
                        TitleUk = x.TitleUk,
                        TitleEn = x.TitleEn,
                        DescriptionRu = x.DescriptionRu,
                        DescriptionUk = x.DescriptionUk,
                        DescriptionEn = x.DescriptionEn,
                        Image = x.Image,
                        ViewCount = x.ViewCount,
                        CategoryTitle = x.Categories.OrderByDescending(c => c.Priority).FirstOrDefault()?.TitleUk,
                        PublishDate = x.PublishDate,
                        EventDate = x.EventDate,
                        IsAnons = x.Categories.Any(c => c.Id == 16)
                    }).ToList().Select(x => new CmsArticleListItem()
                    {
                        Id = x.Id,
                        Title = x.GetLocalValue("Title"),
                        Description = x.GetLocalValue("Description"),
                        UrlName = x.GetLocalValue("UrlName"),
                        PublishDate = DateTimeHelper.NullableDateTimeString(x.PublishDate, Format: "dd MMMM yyyy HH:mm"),
                        EventDate = x.EventDate,
                        PublishDateTime = x.PublishDate,
                        Image = x.Image,
                        IsAnons = x.IsAnons,
                        ViewCount = x.ViewCount,
                        CategoryTitle = x.CategoryTitle
                    }).ToList();


                var model = new CmsArticleList()
                {
                    
                    CategoryId = category.Id,
                    CategoryName = category.GetLocalValue("Title"),
                    CategoryDescription = category.GetLocalValue("Description"),
                    CategoryUrl = category.UrlName,                                        
                    TemplateId = category.TemplateId ?? 0,
                    TemplateName = category.Template != null ? category.Template.Name : string.Empty,                    
                    Articles = list                    
                };

                return model;

            }

            return new CmsArticleList();
        }

        public CalendarWidget GetEventCalendar(string url)
        {
            var now = DateTime.Now;
            var category = articleCatRepo.GetSingle(x => x.UrlName == url, null, true, true);

            var firstDate = articleRepo.DbSet.Where(x => !x.Deleted && x.Published && x.PublishDate <= now && x.EventDate != null && x.Categories.Any(c => c.Id == category.Id)).Min(x => x.EventDate);
            var lastDate = articleRepo.DbSet.Where(x => !x.Deleted && x.Published && x.PublishDate <= now && x.EventDate != null && x.Categories.Any(c => c.Id == category.Id)).Max(x => x.EventDate);
            var dates = articleRepo.DbSet.Where(x => !x.Deleted && x.Published && x.PublishDate <= now && x.EventDate != null && x.Categories.Any(c => c.Id == category.Id)).OrderByDescending(x => x.EventDate).Select(x => x.EventDate).ToList();

            var model = new CalendarWidget()
            {
                FirstDate = firstDate.Value,
                LastDate = lastDate.Value,
                Events = dates.Select(x => x.Value).ToList()
            };

            return model;
        }


        public void AddView(Int32 id)
        {
            articleRepo.Context.ExecuteSqlCommand("UPDATE Articles SET ViewCount = ViewCount + 1 WHERE Id = {0}", false, null, id);
        }
    }
}
