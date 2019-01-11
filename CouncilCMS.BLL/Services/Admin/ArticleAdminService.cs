using System;
using System.Collections.Generic;
using System.Linq;
using Bissoft.CouncilCMS.Core;
using Bissoft.CouncilCMS.Core.Enums;
using Bissoft.CouncilCMS.Core.Globalization;
using Bissoft.CouncilCMS.DAL.Entities;
using Bissoft.CouncilCMS.BLL.ViewModels;
using xTab.Tools.Extensions;
using xTab.Tools.Helpers;
using System.Data.Entity;
using System.Web;
using Bissoft.CouncilCMS.Web.Core;

namespace Bissoft.CouncilCMS.BLL.Services
{
    public class ArticleAdminService : BaseService
    {
        private IRepository<Article, int> articleRepo;
        private IRepository<ArticleCategory, int> categoryRepo;
        private IRepository<CmsUser, int> userRepo;
        private ContentAdminService contentAdminService;
        private SelectListService selectListService;
        private CmsSearchAdminService cmsSearchService;
        
        public ArticleAdminService(string ConnectionString) : base(ConnectionString)
        {
            Initialize();
        }

        public ArticleAdminService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            Initialize();
        }

        private void Initialize()
        {
            articleRepo = UnitOfWork.GetRepository<Article, int>();
            categoryRepo = UnitOfWork.GetRepository<ArticleCategory, int>();
            userRepo = UnitOfWork.GetIntRepository<CmsUser>();
            contentAdminService = new ContentAdminService(this.UnitOfWork);
            selectListService = new SelectListService(this.UnitOfWork);
            cmsSearchService = new CmsSearchAdminService(this.UnitOfWork);            
        }

        public ArticleList List(string query = null, string dateRange = null, int dateType = 0, int? categoryId = null, CmsItemState itemState = CmsItemState.All, Int32 page = 1, string sortBy = "PublishDate", int sortDir = 1, int? user = null, int userAction = 0, int? userId = null)
        {
            var model = new ArticleList()
            {
                Page = page,
                Query = query,                
                CategoryId = categoryId,
                DateRange = dateRange,
                DateType = dateType,
                ItemState = (int)itemState,
                SortBy = sortBy,
                SortDirection = sortDir,
                Categories = selectListService.CategorySelectList<ArticleCategory>(String.Empty, userId),
                User = user,
                UserAction = userAction,
                Users = selectListService.GetCmsUserSelectList(String.Empty)
            };

            return model;
        }
        public List<ArticleListItem> GetList(out int count, string query = null, string dateRange = null, int dateType = 0, int? categoryId = null, CmsItemState itemState = CmsItemState.All, Int32 page = 1, Int32 perPage = 20, string sortBy = "PublishDate", int sortDir = 1, int? user = null, int userAction = 0, int? userId = null)
        {
            var predicate = CreateStatePredicate<Article>(itemState);

            if (!String.IsNullOrEmpty(query))
                predicate = TitleFilter(predicate, query);

            if (!String.IsNullOrEmpty(dateRange))
                predicate = DateRangeFilter(predicate, dateRange, dateType);

            if (user > 0)
                predicate = UserFilter(predicate, user, userAction);

            if (categoryId > 0)
                predicate = predicate.And(x => x.Categories.Any(c => c.Id == categoryId));

            if (userId > 0)
            {
                var roles = userRepo.GetById(userId.Value, "Roles", true).Roles.Select(x => x.Id);
                predicate = predicate.And(x => x.Categories.Any(c => c.AllowedUsers.Any(u => u.Id == userId)) || x.Categories.Any(c => c.AllowedRoles.Any(r => roles.Contains(r.Id))));
            }
            
            var list = articleRepo.GetList(out count, predicate, x => x.OrderByColumn(sortBy, sortDir > 0), "Categories,CreateUser,LastEditUser", (page - 1) * perPage, perPage, true, false);
           
            var model = list.Select(x => new ArticleListItem()
                {
                    Id = x.Id,
                    TitleRu = x.TitleRu,
                    TitleUk = x.TitleUk,
                    TitleEn = x.TitleEn,
                    Deleted = x.Deleted,
                    CreateDate = x.CreateDate,
                    LastEditDate = x.LastEditDate,
                    PublishDate = x.PublishDate,
                    Published = x.Published,
                    EditedDate = x.EditedDate,
                    ViewCount = x.ViewCount,
                    CategoriesUk = x.Categories.Select(c => c.TitleUk),
                    CategoriesRu = x.Categories.Select(c => c.TitleRu),
                    CategoriesEn = x.Categories.Select(c => c.TitleEn),
                    CreateUser = x.CreateUserId > 0 ? x.CreateUser.Name : null,
                    LastEditUser = x.LastEditUserId > 0 ? x.LastEditUser.Name : null
            }).ToList();
                
            return model;
        }

        public ArticleEdit GetForEdit(Int32 id, int? userId = null)
        {
            var article = articleRepo.GetById(id);
            var categories = categoryRepo.GetList(includeProperties: "AllowedUsers,AllowedRoles").ToList();
            
            if (id == 0)
            {
                article = new Article()
                {
                    Id = 0,
                    TitleUk = "Без назви",
                    TitleRu = "Без названия",
                    TitleEn = "No name",
                    Categories = new List<ArticleCategory>(),
                    ContentRows = new List<ContentRow>(),
                    CreateDate = DateTime.Now,
                    PublishDate = DateTime.Now,
                    ShowPublihDate = true,
                    ShowEditDate = false,
                    Published = true,
                    CreateUserId = (HttpContext.Current.User as CmsPrincipal).Identity.UserId
                };

                articleRepo.Insert(article);
                UnitOfWork.Commit();

                article.TitleUk = null;
                article.TitleRu = null;
                article.TitleEn = null;
            }

            var model = new ArticleEdit();

            model.Id = article.Id;
            model.TitleRu = article.TitleRu;
            model.TitleUk = article.TitleUk;
            model.TitleEn = article.TitleEn;
            model.DescriptionRu = article.DescriptionRu;
            model.DescriptionUk = article.DescriptionUk;
            model.DescriptionEn = article.DescriptionEn;
            model.MetaTitleRu = article.MetaTitleRu;
            model.MetaTitleUk = article.MetaTitleUk;
            model.MetaTitleEn = article.MetaTitleEn;
            model.MetaKeywordsRu = article.MetaKeywordsRu;
            model.MetaKeywordsUk = article.MetaKeywordsUk;
            model.MetaKeywordsEn = article.MetaKeywordsEn;
            model.MetaDescriptionRu = article.MetaDescriptionRu;
            model.MetaDescriptionUk = article.MetaDescriptionUk;
            model.MetaDescriptionEn = article.MetaDescriptionEn;
            model.Image = article.Image;
            model.ExportToOpenData = false;
            model.AllowComments = article.AllowComments;
            model.Published = article.Published;
            model.ShowEditDate = article.ShowEditDate;
            model.ShowPublihDate = article.ShowPublihDate;
            model.OpenDataId = article.OpenDataId;
            model.OpenDataTitle = article.OpenDataTitle;
            model.OpenDataDescription = article.OpenDataDescription;
            model.OpenDataCategory = article.OpenDataCategory;
            model.OpenDataEmail = article.OpenDataEmail;
            model.OpenDataName = article.OpenDataName;
            model.OpenDataRefresh = article.OpenDataRefresh;
            model.OpenDataKeywords = article.OpenDataKeywords;            
            model.PublishDate = DateTimeHelper.NullableDateTimeString(article.PublishDate, null, false, "dd.MM.yyyy HH:mm");
            model.EventDate = DateTimeHelper.NullableDateTimeString(article.EventDate, null, false, "dd.MM.yyyy HH:mm");
            model.EditedDate = null;
            model.ArticleCategories =
                GetCategoryCheckedList(
                    model.ArticleCategories,
                    categories.Where(x => x.ParentCategoryId == null).OrderBy(x => x.Position).ToList(),
                    categories,
                    0, userId);

            foreach (var item in article.Categories.Select(x => x.Id).ToList())
            {
                model.ArticleCategories.Where(x => x.Value == item).FirstOrDefault().IsChecked = true;
            }

            model.ContentRows = contentAdminService.GetContentRowListItems(article.ContentRows, model.Id);

            return model;
        }

        public ArticleEdit Save(ArticleEdit model)
        {
            var item = articleRepo.GetById(model.Id);

            model.TitleUk = String.IsNullOrEmpty(model.TitleUk) ? "Без назви" : model.TitleUk;
            model.TitleRu = String.IsNullOrEmpty(model.TitleRu) ? "Без названия" : model.TitleRu;
            model.TitleEn = String.IsNullOrEmpty(model.TitleEn) ? "No name" : model.TitleEn;

            var texts = contentAdminService.GetContentText(item.ContentRows);
            var textRu = model.TitleRu + " " + model.DescriptionRu + " " + texts[0];
            var textUk = model.TitleUk + " " + model.DescriptionUk + " " + texts[1];
            var textEn = model.TitleEn + " " + model.DescriptionEn + " " + texts[2];

            item.Id = model.Id;
            item.TitleRu = model.TitleRu;
            item.TitleUk = model.TitleUk;
            item.TitleEn = model.TitleEn;
            item.DescriptionRu = model.DescriptionRu;
            item.DescriptionUk = model.DescriptionUk;
            item.DescriptionEn = model.DescriptionEn;
            item.UrlNameRu = model.TitleRu.Translit();
            item.UrlNameUk = model.TitleUk.Translit();
            item.UrlNameEn = model.TitleEn.Translit();
            item.MetaTitleRu = model.MetaTitleRu;
            item.MetaTitleUk = model.MetaTitleUk;
            item.MetaTitleEn = model.MetaTitleEn;
            item.MetaKeywordsRu = model.MetaKeywordsRu;
            item.MetaKeywordsUk = model.MetaKeywordsUk;
            item.MetaKeywordsEn = model.MetaKeywordsEn;
            item.MetaDescriptionRu = model.MetaDescriptionRu;
            item.MetaDescriptionUk = model.MetaDescriptionUk;
            item.MetaDescriptionEn = model.MetaDescriptionEn;
            item.ExportToOpenData = model.ExportToOpenData;
            item.AllowComments = model.AllowComments;
            item.ShowPublihDate = model.ShowPublihDate;
            item.ShowEditDate = model.ShowEditDate;
            item.OpenDataId = model.OpenDataId;
            item.OpenDataTitle = model.OpenDataTitle;
            item.OpenDataDescription = model.OpenDataDescription;
            item.OpenDataCategory = model.OpenDataCategory;
            item.OpenDataEmail = model.OpenDataEmail;
            item.OpenDataName = model.OpenDataName;
            item.OpenDataRefresh = model.OpenDataRefresh;
            item.OpenDataKeywords = model.OpenDataKeywords;
            item.Image = model.Image;            
            item.Published = model.Published;
            item.LastEditDate = DateTime.Now;
            item.PublishDate = DateTimeHelper.ParseDateNullable(model.PublishDate, null, DateTimeHelper.DefaultTimeOfDay.None);
            item.EventDate = DateTimeHelper.ParseDateNullable(model.EventDate, null, DateTimeHelper.DefaultTimeOfDay.None);
            item.EditedDate = DateTimeHelper.ParseDateNullable(model.EditedDate, DateTime.Now, DateTimeHelper.DefaultTimeOfDay.None);
            item.LastEditUserId = (HttpContext.Current.User as CmsPrincipal).Identity.UserId;

            if (item.Categories != null)
                item.Categories.Clear();
            else
                item.Categories = new List<ArticleCategory>();

            articleRepo.Update(item);
            
            foreach (var cat in model.ArticleCategories)
            {
                if (cat.IsChecked)
                    item.Categories.Add(categoryRepo.GetById(cat.Value));
            }

            if (item.Id > 0)
            {
                articleRepo.Update(item);
            }
            else
            {
                articleRepo.Insert(item);
            }

            UnitOfWork.Commit();

            cmsSearchService.Save(item.Id, textUk, textRu, textEn, item.PublishDate, CmsSearchType.Article, item.Published);

            return model;
        }

        public void Delete(Int32 Id)
        {
            var article = articleRepo.GetById(Id);

            article.Deleted = true;
            articleRepo.Update(article);
            cmsSearchService.Delete(Id, CmsSearchType.Article);

            UnitOfWork.Commit();
        }

        public void Restore(Int32 Id)
        {
            var article = articleRepo.GetById(Id);

            article.Deleted = false;
            articleRepo.Update(article);
            cmsSearchService.Restore(Id, CmsSearchType.Article);

            UnitOfWork.Commit();
        }

        public void Remove(Int32 Id)
        {
            var article = articleRepo.GetById(Id);

            cmsSearchService.Remove(Id, CmsSearchType.Article);
            articleRepo.Delete(article);

            UnitOfWork.Commit();
        }

        private List<CheckedListItem> GetCategoryCheckedList(List<CheckedListItem> targetList, List<ArticleCategory> levelList, List<ArticleCategory> sourceList, int level = 0, int? userId = null, int? roleId = null)
        {
            targetList = targetList ?? new List<CheckedListItem>();
            var user = userRepo.GetById(userId ?? 0);

            foreach (var cat in levelList)
            {
                targetList.Add(
                    new CheckedListItem()
                    {
                        Value = cat.Id,
                        Name = cat.GetLocalValue("Title"),
                        Level = level,
                        Allowed = user != null ? (cat.AllowedUsers.Any(x => x.Id == userId.Value) || cat.AllowedRoles.Any(r => user.Roles.Select(x => x.Id).Contains(r.Id))) : true
                    });

                if (sourceList.Count(x => x.ParentCategoryId == cat.Id) > 0)
                {
                    GetCategoryCheckedList(targetList, sourceList.Where(x => x.ParentCategoryId == cat.Id).OrderBy(x => x.Position).ToList(), sourceList, level + 1, userId);
                }
            }

            return targetList;
        }
    }
}