using System;
using System.Linq;
using Bissoft.CouncilCMS.Core;
using Bissoft.CouncilCMS.DAL.Entities;
using Bissoft.CouncilCMS.BLL.ViewModels;
using xTab.Tools.Helpers;
using LemmaSharp;
using xTab.Tools.Extensions;
using Bissoft.CouncilCMS.BLL.ViewModels.Public;
using System.Collections.Generic;

namespace Bissoft.CouncilCMS.BLL.Services
{
    public class CmsDocService : BaseService
    {
        private IRepository<Doc, int> docRepo;
        private IRepository<DocCategory, int> categoryRepo;
        private IRepository<Person, int> personRepo;
        private IRepository<Enterprise, int> enterpriseRepo;
        private CmsMenuService menuService;
        private SelectListService selectListService;
        public CmsDocService(string ConnectionString) : base(ConnectionString)
        {
            Initialize();
        }

        public CmsDocService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            Initialize();
        }

        public void Initialize()
        {
            docRepo = UnitOfWork.GetIntRepository<Doc>();
            categoryRepo = UnitOfWork.GetIntRepository<DocCategory>();
            personRepo = UnitOfWork.GetIntRepository<Person>();
            enterpriseRepo = UnitOfWork.GetIntRepository<Enterprise>();
            menuService = new CmsMenuService(this.UnitOfWork);
            selectListService = new SelectListService(this.UnitOfWork);
        }

        public CmsDocList CategoryList(
            string articleUrl, 
            string queryTitle = null, 
            string queryText = null, 
            int? type = null, 
            string date = null, 
            int page = 1, 
            int perPage = 20, 
            int? excludeId = null, 
            int? personId = null, 
            int? enterpriseId = null, 
            string order = "AcceptDate")
        {
            var category = categoryRepo.GetSingle(x => x.UrlName == articleUrl, null, true, true);

            if (category != null)
            {
                var relCategory = categoryRepo.GetById(category.RelatedCategoryId ?? 0) ?? new DocCategory();
                var now = DateTime.Now;
                var count = 0;

                var predicate = CreateStatePredicate<Doc>(Core.Enums.CmsItemState.Published);

                if (!String.IsNullOrEmpty(queryTitle))
                    predicate = predicate.And(x => x.TitleEn.Contains(queryTitle) || x.TitleRu.Contains(queryTitle) || x.TitleUk.Contains(queryTitle) || x.Number == queryTitle);                                

                if (!String.IsNullOrEmpty(queryText))
                {
                    var lemmaProc = new LemmaProcessor(CurrentCulture.Name.ToLower());
                    var against = lemmaProc.PrepareText(queryText);

                    var ids = docRepo.FullTextSearch("CleanText", against, "Docs", 0, type != null ? ("DocTypeId = " + (int)type.Value) : null);
                    
                    predicate = predicate.And(x => ids.Contains(x.Id));
                }

                if (type > 0)
                    predicate = predicate.And(x => x.DocTypeId == type.Value);
               
                if (!String.IsNullOrEmpty(date))
                {
                    var dates = date.Split(new string[] { " - " }, StringSplitOptions.RemoveEmptyEntries);

                    if (dates.Length == 2)
                    {
                        var fromDate = DateTimeHelper.ParseDateNullable(dates[0]);
                        var toDate = DateTimeHelper.ParseDateNullable(dates[1]);

                        if (fromDate != null && toDate != null)
                        {
                            fromDate = fromDate.Value.Date;
                            toDate = toDate.Value.Date.AddDays(1).AddSeconds(-1);

                            predicate = predicate.And(x => x.AcceptDate != null && x.AcceptDate >= fromDate && x.AcceptDate <= toDate);
                        }
                    }                    
                }

                if (enterpriseId > 0)
                    predicate = predicate.And(x => x.Enterprises.Any(e => e.Id == enterpriseId));

                if (personId > 0)
                    predicate = predicate.And(x => x.Persons.Any(p => p.Id == personId));

                if (excludeId > 0)
                {
                    predicate = predicate.And(x => x.Id != excludeId.Value);
                }

                predicate = predicate.And(x => x.Categories.Any(c => c.Id == category.Id) && x.PublishDate <= now);

                var list = docRepo
                    .GetList(out count,
                        predicate,
                        x => x.OrderByColumn(order, order.ToLower() == "acceptdate"),
                        "DocType", (page - 1) * perPage, perPage)
                    .Select(x =>
                    new
                    {
                        Id = x.Id,
                        Number = x.Number,
                        TitleRu = x.TitleRu,
                        TitleUk = x.TitleUk,
                        TitleEn = x.TitleEn,
                        TypeRu = x.DocType.TitleRu,
                        TypeEn = x.DocType.TitleEn,
                        TypeUk = x.DocType.TitleUk,
                        PublishDate = x.PublishDate,
                        PostDate = x.PostDate,
                        AcceptDate = x.AcceptDate,
                        ViewCount = x.ViewCount,
                    }).ToList()
                    .Select(x => new CmsDocListItem()
                    {
                        Id = x.Id,
                        Title = x.GetLocalValue("Title"),
                        Type = x.GetLocalValue("Type"),
                        Number = x.Number,
                        PostDate = x.PostDate,
                        AcceptDate = x.AcceptDate,
                        PublishDate = DateTimeHelper.NullableDateTimeString(x.PublishDate, Format: "dd MMMM yyyy HH:mm"),
                        ViewCount = x.ViewCount,
                    }).ToList();

                var person = personId > 0 ? personRepo.GetSingle(x => x.Id == personId, asNoTracking: true, notDeleted: true) : null;
                var enterprise = enterpriseId > 0 ? enterpriseRepo.GetSingle(x => x.Id == enterpriseId, asNoTracking: true, notDeleted: true) : null;

                var model = new CmsDocList()
                {
                    QueryText = queryText,
                    QueryTitle = queryTitle,
                    Date = date,
                    Type = type,
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
                    Docs = list,
                    Order = order,
                    PersonId = personId,
                    PersonName = person != null ? person.GetLocalValue("Title") : null,
                    EnterpriseId = enterpriseId,
                    EnterpriseName = enterprise != null ? enterprise.GetLocalValue("Title") : null,
                    Types = category.ShowSearchForm ? selectListService.GetDocTypeSelectList(String.Empty) : null
                };

                if (category.ChildCategories != null && category.ChildCategories.Count > 0)
                {
                    model.HasChildren = true;
                }

                return model;
            }

            return null;
        }


        public CmsDocList ItemList(Int32 categoryId, int? personId = null, int? enterpriseId = null, int page = 1, int perPage = 20)
        {
            var category = categoryRepo.GetById(categoryId);

            if (category != null)
            {                
                var now = DateTime.Now;
                var count = 0;

                var predicate = PredicateBuilder.True<Doc>();

                if (personId > 0)
                    predicate = predicate.And(x => x.Persons.Any(p => p.Id == personId));

                if (enterpriseId > 0)
                    predicate = predicate.And(x => x.Enterprises.Any(p => p.Id == enterpriseId));

                predicate = predicate.And(x => x.Categories.Any(c => c.Id == category.Id) && x.Published && x.PublishDate != null && x.PublishDate <= now);

                var list = docRepo
                    .GetList(out count,
                    predicate,
                    x => x.OrderByDescending(o => o.AcceptDate).ThenByDescending(o => o.PublishDate),
                    "DocType", (page - 1) * perPage, perPage).Select(x =>
                    new
                    {
                        Id = x.Id,
                        Number = x.Number,
                        TitleRu = x.TitleRu,
                        TitleUk = x.TitleUk,
                        TitleEn = x.TitleEn,
                        TypeRu = x.DocType.TitleRu,
                        TypeEn = x.DocType.TitleEn,
                        TypeUk = x.DocType.TitleUk,
                        PublishDate = x.PublishDate,
                        PostDate = x.PostDate,
                        AcceptDate = x.AcceptDate,
                        ViewCount = x.ViewCount,
                    }).ToList().Select(x => new CmsDocListItem()
                    {
                        Id = x.Id,
                        Title = x.GetLocalValue("Title"),
                        Type = x.GetLocalValue("Type"),
                        Number = x.Number,
                        PostDate = x.PostDate,
                        AcceptDate = x.AcceptDate,
                        PublishDate = DateTimeHelper.NullableDateTimeString(x.PublishDate, Format: "dd MMMM yyyy HH:mm"),
                        ViewCount = x.ViewCount,
                    }).ToList();

                var model = new CmsDocList()
                {                    
                    Page = page,
                    PerPage = perPage,
                    Count = count,
                    CategoryId = category.Id,
                    CategoryName = category.GetLocalValue("Title"),
                    CategoryDescription = category.GetLocalValue("Description"),
                    CategoryUrl = category.UrlName,                                     
                    Docs = list,
                    PersonId = personId,
                    EnterpriseId = enterpriseId                    
                };

                return model;
            }

            return null;
        }

        public CmsDoc Doc(Int32 id, int? userId = null)
        {
            var model = new CmsDoc();
            var item = docRepo.GetById(id);

            if (item == null || (userId == null && (!item.Published || item.Deleted || item.PublishDate == null || item.PublishDate > DateTime.Now)))
                return null;

            var category = item.Categories.OrderByDescending(x => x.Priority).ThenBy(x => x.TitleUk).FirstOrDefault() ?? new DocCategory() { RelatedCategoryId = 0 };
            var relCategory = category.ParentCategoryId > 0 ? categoryRepo.GetById(category.ParentCategoryId ?? 0) : category;

            model.Id = item.Id;
            model.Title = item.GetLocalValue("Title");
            model.Number = item.Number;
            model.Type = item.DocType.GetLocalValue("Title");
            model.CategoryId = category.Id;
            model.CategoryUrl = category.UrlName;
            model.CategoryName = category.GetLocalValue("Title");
            model.RelatedCategoryId = relCategory.Id;
            model.RelatedCategoryUrl = relCategory.UrlName;
            model.MetaDescription = item.GetLocalValue("MetaDescriptionRu");
            model.MetaKeywords = item.GetLocalValue("MetaKeywords");
            model.MetaTitle = item.GetLocalValue("MetaTitle");
            model.PublishDate = item.ShowPublihDate ? DateTimeHelper.NullableDateTimeString(item.PublishDate) : String.Empty;
            model.LastEditDate = item.ShowEditDate ? DateTimeHelper.NullableDateTimeString(item.LastEditDate) : String.Empty;
            model.PostDate = item.PostDate;
            model.AcceptDate = item.AcceptDate;

            model.Text = item.Text;

            model.Files = item.Files == null ?
                null :
                item.Files.Select(x => new CmsDocFile()
                {
                    Id = x.Id,
                    Title = x.GetLocalValue("Title"),
                    Size = (int)x.Size,
                    Extension = x.Extension,
                    File = x.File
                }).ToList();

            model.Enterprises = item.Enterprises == null ? null : item.Enterprises.Select(x =>
                    new
                    {
                        Id = x.Id,
                        UrlNameRu = x.UrlNameRu,
                        UrlNameUk = x.UrlNameUk,
                        UrlNameEn = x.UrlNameEn,
                        TitleRu = x.TitleRu,
                        TitleUk = x.TitleUk,
                        TitleEn = x.TitleEn,
                        DescriptionRu = x.DescriptionRu,
                        DescriptionUk = x.DescriptionUk,
                        DescriptionEn = x.DescriptionEn,
                        ContactAddressUk = x.ContactAddressUk,
                        ContactAddressRu = x.ContactAddressRu,
                        ContactAddressEn = x.ContactAddressEn,
                        ContactPhonesUk = x.ContactPhonesUk,
                        ContactPhonesRu = x.ContactPhonesRu,
                        ContactPhonesEn = x.ContactPhonesEn,
                        ContactEmailsUk = x.ContactEmailsUk,
                        ContactEmailsRu = x.ContactEmailsRu,
                        ContactEmailsEn = x.ContactEmailsEn,
                        Image = x.Image,
                        PersonCount = x.Persons.Count,
                        TypePosition = x.TypeId > 0 ? x.Type.Position : 0,
                        TypeUk = x.TypeId > 0 ? x.Type.TitleUk : null,
                        TypeRu = x.TypeId > 0 ? x.Type.TitleRu : null,
                        TypeEn = x.TypeId > 0 ? x.Type.TitleEn : null,
                    }).ToList().Select(x => new CmsEnterpriseListItem()
                    {
                        Id = x.Id,
                        Title = x.GetLocalValue("Title"),
                        Description = x.GetLocalValue("Description"),
                        ContactAddress = x.GetLocalValue("ContactAddress"),
                        ContactPhones = x.GetLocalValue("ContactPhones"),
                        ContactEmails = x.GetLocalValue("ContactEmails"),
                        UrlName = x.GetLocalValue("UrlName"),
                        Type = x.GetLocalValue("Type"),
                        TypePosition = x.TypePosition,
                        Image = x.Image,
                        PersonCount = x.PersonCount
                    }).ToList();

            model.Persons = item.Persons == null ? null : item.Persons.Select(x =>
                    new
                    {
                        Id = x.Id,
                        UrlName = x.UrlNameRu,
                        UrlNameUk = x.UrlNameUk,
                        TitleRu = x.TitleRu,
                        TitleUk = x.TitleUk,
                        TitleEn = x.TitleEn,
                        PostRu = x.PostRu,
                        PostUk = x.PostUk,
                        PostEn = x.PostEn,
                        DescriptionRu = x.DescriptionRu,
                        DescriptionUk = x.DescriptionUk,
                        DescriptionEn = x.DescriptionEn,
                        ContactAddressUk = x.ContactAddressUk,
                        ContactAddressRu = x.ContactAddressRu,
                        ContactAddressEn = x.ContactAddressEn,
                        ContactPhonesUk = x.ContactPhonesUk,
                        ContactPhonesRu = x.ContactPhonesRu,
                        ContactPhonesEn = x.ContactPhonesEn,
                        ContactEmailsUk = x.ContactEmailsUk,
                        ContactEmailsRu = x.ContactEmailsRu,
                        ContactEmailsEn = x.ContactEmailsEn,
                        DeputyFractionId = x.DeputyFraction != null ? x.DeputyFraction.Id : 0,
                        DeputyFractionNameUk = x.DeputyFraction != null ? x.DeputyFraction.TitleUk : null,
                        DeputyFractionNameRu = x.DeputyFraction != null ? x.DeputyFraction.TitleRu : null,
                        DeputyFractionNameEn = x.DeputyFraction != null ? x.DeputyFraction.TitleEn : null,
                        DeputyFractionUrl = x.DeputyFraction != null ? x.DeputyFraction.UrlNameUk : null,
                        Image = x.Image,

                    }).ToList().Select(x => new CmsPersonListItem()
                    {
                        Id = x.Id,
                        Title = x.GetLocalValue("Title"),
                        Post = x.GetLocalValue("Post"),
                        DeputyFractionId = x.DeputyFractionId,
                        DeputyFractionName = x.GetLocalValue("DeputyFractionName"),
                        DeputyFractionUrl = x.DeputyFractionUrl,
                        Description = x.GetLocalValue("Description"),
                        ContactAddress = x.GetLocalValue("ContactAddress"),
                        ContactPhones = x.GetLocalValue("ContactPhones"),
                        ContactEmail = x.GetLocalValue("ContactEmails"),
                        UrlName = x.GetLocalValue("UrlName"),
                        Image = x.Image
                    }).ToList();

            AddView(id);

            return model;
        }

        public CmsDoc LastDocInCategory(Int32 id)
        {
            var category = categoryRepo.GetById(id);

            var date = DateTime.Now;

            var item = docRepo.GetList(
                x => x.Categories.Any(c => x.Id == id) && x.Published && x.Saved && x.PublishDate <= date,
                x => x.OrderByDescending(o => o.PublishDate), "Categories,DocType",
                0, 1).FirstOrDefault();

            if (item != null)
            {

                var model = new CmsDoc()
                {
                    Id = item.Id,
                    Type = item.DocType.GetLocalValue("Title"),
                    Number = item.Number,
                    PublishDate = DateTimeHelper.NullableDateTimeString(item.PublishDate, Format: "dd MMMM yyyy HH:mm"),
                    Title = item.GetLocalValue("Title"),
                    ViewCount = item.ViewCount
                };

                return model;
            }
            else
            {
                return null;
            }
        }

        public CmsDoc LastDocInCategory(String url)
        {
            var category = categoryRepo.GetSingle(x => x.UrlName == url, null, true, true);

            var date = DateTime.Now;

            var item = docRepo.GetList(
                x => x.Categories.Any(c => c.Id == category.Id) && x.Published && x.Saved && x.PublishDate <= date,
                x => x.OrderByDescending(o => o.PublishDate), "Categories,DocType").FirstOrDefault();

            if (item != null)
            {

                var model = new CmsDoc()
                {
                    Id = item.Id,
                    Type = item.DocType.GetLocalValue("Title"),
                    Number = item.Number,
                    PublishDate = DateTimeHelper.NullableDateTimeString(item.PublishDate, Format: "dd MMMM yyyy HH:mm"),
                    Title = item.GetLocalValue("Title"),
                    ViewCount = item.ViewCount
                };

                return model;
            }
            else
            {
                return null;
            }
        }

        public CmsDocList LastDocs(string articleUrl, int limit = 5, int? excludeId = null)
        {
            var category = categoryRepo.GetSingle(x => x.UrlName == articleUrl, null, true, true);

            if (category != null)
            {
                var now = DateTime.Now;
                var count = 0;

                var predicate = PredicateBuilder.True<Doc>();

                if (excludeId > 0)
                    predicate = predicate.And(x => x.Id != excludeId.Value);

                predicate = predicate.And(x => x.Categories.Any(c => c.Id == category.Id) && x.Published && x.Saved && x.PublishDate <= now);

                var list = docRepo
                    .GetList(out count,
                    predicate,
                    x => x.OrderByDescending(o => o.PublishDate),
                    "Categories,DocType", 0, limit).Select(x =>
                    new
                    {
                        Id = x.Id,
                        Number = x.Number,
                        TitleRu = x.TitleRu,
                        TitleUk = x.TitleUk,
                        TitleEn = x.TitleEn,
                        TypeRu = x.DocType.TitleRu,
                        TypeEn = x.DocType.TitleEn,
                        TypeUk = x.DocType.TitleUk,
                        PublishDate = x.PublishDate,
                        PostDate = x.PostDate,
                        AcceptDate = x.AcceptDate,
                        ViewCount = x.ViewCount,
                    }).ToList().Select(x => new CmsDocListItem()
                    {
                        Id = x.Id,
                        Title = x.GetLocalValue("Title"),
                        Type = x.GetLocalValue("Type"),
                        Number = x.Number,
                        AcceptDate = x.AcceptDate,
                        PostDate = x.PostDate,
                        PublishDate = DateTimeHelper.NullableDateTimeString(x.PublishDate, Format: "dd MMMM yyyy HH:mm"),
                        ViewCount = x.ViewCount,
                    }).ToList();

                var model = new CmsDocList()
                {
                    CategoryId = category.Id,
                    CategoryName = category.GetLocalValue("Title"),
                    CategoryDescription = category.GetLocalValue("Description"),
                    CategoryUrl = category.UrlName,
                    Docs = list,
                };

                return model;

            }

            return new CmsDocList();
        }

        public void AddView(Int32 id)
        {
            docRepo.Context.ExecuteSqlCommand("UPDATE Docs SET ViewCount = ViewCount + 1 WHERE Id = {0}", false, null, id);
        }

        public List<CategoryListItem> GetList(int? userId = null)
        {
            List<DocCategory> сategoryList = categoryRepo.GetList(includeProperties: "AllowedRoles", notDeleted: false).Where(x => x.ParentCategoryId != null).ToList();
            List<CategoryListItem> categoryItems = categoryRepo.GetList(includeProperties: "AllowedRoles", notDeleted: false).Where(x => x.ParentCategoryId == null).OrderBy(x => x.Position)
                        .Select(x => FillMenuItem(x, сategoryList, null))
                        .ToList();
            return categoryItems;
        }

        private CategoryListItem FillMenuItem(DocCategory item, List<DocCategory> list, CmsUser user = null)
        {
            var model = new CategoryListItem()
            {
                Id = item.Id,
                TitleRu = item.TitleRu,
                TitleUk = item.TitleUk,
                TitleEn = item.TitleEn,
                UrlName = item.UrlName,
                Priority = item.Priority,
                Position = item.Position,
                Image = item.Image,
                //Type = type.ToLower(),
                ParentCategoryId = item.ParentCategoryId,
                HasDocuments = (item.Docs.Count > 0) ? true : false, 
                HasChilds = list.Count(x => x.ParentCategoryId == item.Id) > 0,
                Allowed = user != null ? item.AllowedRoles.Any(r => user.Roles.Select(x => x.Id).DefaultIfEmpty(0).Contains(r.Id)) : true,
                ChildCategoryItems = list.Count(x => x.ParentCategoryId == item.Id) > 0 ?
                    list.Where(x => x.ParentCategoryId == item.Id).OrderBy(x => x.Position).Select(x => FillMenuItem(x, list, user)).ToList() :
                    null
            };

            return model;
        }

        public List<CmsDocListItem> LookingForDocs(string queryTitle = null,string queryText = null,int? type = null,string date = null)
        {
            //
            docRepo.FullTextSearch(queryTitle, queryText);
            //

            var now = DateTime.Now;
            var count = 0;

            var predicate = PredicateBuilder.True<Doc>();

            predicate = predicate.And(x => x.Published && x.Saved && x.PublishDate <= now);

            var list = docRepo
                .GetList(out count,
                predicate,
                x => x.OrderByDescending(o => o.PublishDate),
                "Categories,DocType", 0).Select(x =>
                new
                    {
                        Id = x.Id,
                        Number = x.Number,
                        TitleRu = x.TitleRu,
                        TitleUk = x.TitleUk,
                        TitleEn = x.TitleEn,
                        TypeRu = x.DocType.TitleRu,
                        TypeEn = x.DocType.TitleEn,
                        TypeUk = x.DocType.TitleUk,
                        PublishDate = x.PublishDate,
                        PostDate = x.PostDate,
                        AcceptDate = x.AcceptDate,
                        ViewCount = x.ViewCount,
                    }).ToList().Select(x => new CmsDocListItem()
                    {
                        Id = x.Id,
                        Title = x.GetLocalValue("Title"),
                        Type = x.GetLocalValue("Type"),
                        Number = x.Number,
                        AcceptDate = x.AcceptDate,
                        PostDate = x.PostDate,
                        PublishDate = DateTimeHelper.NullableDateTimeString(x.PublishDate, Format: "dd MMMM yyyy HH:mm"),
                        ViewCount = x.ViewCount,
                    });

                var predic = CreatePredicate(queryTitle,queryText,type,date); 
                var found = list.Where(predic).ToList();
                return found;

            }
        private Func<CmsDocListItem,bool> CreatePredicate(
            string queryTitle ,
            string queryText ,
            int? type ,
            string date )
        {
            Func<CmsDocListItem, bool> func = x =>
            x.Title == queryTitle;
            return func;
        }
    }
}
