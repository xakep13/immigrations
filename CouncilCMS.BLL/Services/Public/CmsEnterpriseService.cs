using Bissoft.CouncilCMS.DAL.Entities;
using Bissoft.CouncilCMS.BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using xTab.Tools.Helpers;
using Bissoft.CouncilCMS.Core;

namespace Bissoft.CouncilCMS.BLL.Services
{
    public class CmsEnterpriseService : BaseService
    {
        private IRepository<Enterprise, int> enterpriseRepo;
        private IRepository<EnterpriseCategory, int> categoryRepo;
        private IRepository<DocCategory, int> docCategoryRepo;
        private IRepository<Doc, int> docRepo;
        private CmsContentService contentService;
        private CmsMenuService menuService;
        public CmsEnterpriseService(string ConnectionString) : base(ConnectionString)
        {
            Initialize();
        }

        public CmsEnterpriseService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            Initialize();
        }

        private void Initialize()
        {            
            enterpriseRepo = UnitOfWork.GetIntRepository<Enterprise>();
            categoryRepo = UnitOfWork.GetIntRepository<EnterpriseCategory>();
            docCategoryRepo = UnitOfWork.GetIntRepository<DocCategory>();
            docRepo = UnitOfWork.GetIntRepository<Doc>();
            contentService = new CmsContentService(this.UnitOfWork);
            menuService = new CmsMenuService(this.UnitOfWork);
        }

        public CmsEnterpriseList CategoryList(string articleUrl, int page = 1, int perPgae = 30)
        {
            var category = categoryRepo.GetSingle(x => x.UrlName == articleUrl, null, true, true);

            if (category != null)
            {
                var now = DateTime.Now;
                var count = 0;

                var predicate = PredicateBuilder.True<Enterprise>();
                
                predicate = predicate.And(x => x.Categories.Any(c => c.CategoryId == category.Id) && x.ParentEnterpriseId == null && x.Published && x.PublishDate != null && x.PublishDate <= now);

                var list = enterpriseRepo
                    .GetList(out count,
                    predicate,
                    x => x.OrderBy(o => o.Categories.FirstOrDefault(c => c.CategoryId == category.Id).Position),
                    "Categories,Type,Persons", null, null).Select(x => 
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

                var model = new CmsEnterpriseList()
                {

                    CategoryId = category.Id,
                    CategoryName = category.GetLocalValue("Title"),
                    CategoryDescription = category.GetLocalValue("Description"),
                    CategoryUrl = category.UrlName,
                    TemplateId = category.TemplateId ?? 0,
                    TemplateName = category.Template.Name,
                    Page = page,
                    PerPage = perPgae,
                    Count = count,
                    Enterprises = list
                };

                return model;
            }

            return null;
        }

        public CmsEnterprise Enterprise(Int32 id, int? userId = null)
        {
            var model = new CmsEnterprise();
            var item = enterpriseRepo.GetById(id);

            if (item == null || (userId == null && (!item.Published || item.Deleted || item.PublishDate == null || item.PublishDate > DateTime.Now)))
                return null;

            
                var category = item.Categories.OrderByDescending(x => x.Category.Priority).ThenBy(x => x.Category.TitleUk).FirstOrDefault();

                model.Id = item.Id;
                model.Title = item.GetLocalValue("Title");
                model.Url = item.GetLocalValue("UrlName");
                model.ContactAddress = item.GetLocalValue("ContactAddress");
                model.ContactEmails = item.GetLocalValue("ContactEmails");
                model.ContactPhones = item.GetLocalValue("ContactPhones");
                model.ContactDates = item.GetLocalValue("ContactDates");
                model.ContactTimes = item.GetLocalValue("ContactTime");
                model.ContactWebsite = item.GetLocalValue("ContactWebsite");
                model.FbLink = item.FbLink;
                model.TwLink = item.TwLink;
                model.YtLink = item.YtLink;
                model.IgLink = item.IgLink;
                model.GpLink = item.GpLink;
                model.Image = item.Image;
                model.ShowSecondPage = item.ShowSecondPage;
                model.SecondPageTitle = item.GetLocalValue("SecondPageTitle");
                model.SecondPageText = item.GetLocalValue("SecondPageText");
                model.MetaDescription = item.GetLocalValue("MetaDescriptionRu");
                model.MetaKeywords = item.GetLocalValue("MetaKeywords");
                model.MetaTitle = item.GetLocalValue("MetaTitle");
                model.ShowReceptionForm = item.HasContactForm && !String.IsNullOrEmpty(item.NotificationEmail);
                model.PublishDate = item.ShowPublihDate ? DateTimeHelper.DateTimeString(item.CreateDate) : String.Empty;
                model.LastEditDate = item.ShowEditDate ? DateTimeHelper.NullableDateTimeString(item.LastEditDate) : String.Empty;
                model.PersonsTitle = item.GetLocalValue("PersonsTitle");
                model.ContentRows = contentService.GetContentRows(item.ContentRows);
                model.Reception = new CmsEnterpriseReception() { EnterpriseId = model.Id };

                if (category != null && category.Category != null)
                {
                    model.CategoryId = category.CategoryId;
                    model.CategoryName = category.Category.GetLocalValue("Title");
                    model.CategoryUrlName = category.Category.UrlName;
                }

                model.ParentId = item.ParentEnterpriseId;

                if (model.ParentId > 0)
                {
                    model.ParentName = item.ParentEnterprise.GetLocalValue("Title");
                    model.ParentUrlName = item.ParentEnterprise.GetLocalValue("UrlName");
                }

                model.ChildEnterprises = item.ChildEnterprises != null ?
                    item.ChildEnterprises.Select(x => new CmsEnterpriseListItem()
                    {
                        Id = x.Id,
                        Title = x.GetLocalValue("Title"),
                        Description = x.GetLocalValue("Description"),
                        ContactAddress = x.GetLocalValue("ContactAddress"),
                        ContactPhones = x.GetLocalValue("ContactAddress"),
                        ContactEmails = x.GetLocalValue("ContactEmails"),
                        UrlName = x.GetLocalValue("UrlName"),
                        Image = x.Image
                    }).ToList() :
                    null;

                model.Persons = item.Persons != null ? 
                    item.Persons.Select(x => new CmsPersonListItem()
                    {
                        Id = x.PersonId,
                        ContactAddress = x.Person.GetLocalValue("ContactAddress"),
                        ContactPhones = x.Person.GetLocalValue("ContactPhones"),
                        Description = x.Person.GetLocalValue("Description"),
                        Title = x.Person.GetLocalValue("Title"),
                        Post = x.Person.GetLocalValue("Post"),
                        EnterprisePost = x.GetLocalValue("Post"),
                        Image = x.Person.Image,
                        UrlName = x.Person.GetLocalValue("UrlName"),
                    }).ToList() 
                    : null;
            var now = DateTime.Now;
            var docCats = docRepo
                .GetList(d => d.Published && !d.Deleted && d.PublishDate <= now && d.Enterprises.Any(e => e.Id == model.Id), null, "Categories")
                .SelectMany(x => x.Categories).Select(c => c).GroupBy(x => x.Id).Select(g => g.First()).Select(x => new { Id = x.Id, TitleUk = x.TitleUk, TitleRu = x.TitleRu, TitleEn = x.TitleEn, ParentId = x.ParentCategoryId, Url = x.UrlName })
                .ToList()
                .Select(x => new CmsDocCategoryListItem() { Id = x.Id, Title = x.GetLocalValue("Title"), ParentId = x.ParentId, Url = x.Url })
                .ToList();

            docCats.ToList().ForEach(i => { if (docCats.Any(c => c.ParentId == i.Id)) { docCats.Remove(i); } });
            docCats.ForEach(i => i.Count = docRepo.DbSet.Count(d => d.Published && !d.Deleted && d.PublishDate <= now && d.Categories.Any(c => c.Id == i.Id) && d.Enterprises.Any(e => e.Id == model.Id)));
            
            model.DocCategories = docCats;

            AddView(id);

            return model;           
        }

        public void AddView(Int32 id)
        {
            enterpriseRepo.Context.ExecuteSqlCommand("UPDATE Enterprises SET ViewCount = ViewCount + 1 WHERE Id = {0}", false, null, id);
        }

        public string GetEmail(int id)
        {
            return enterpriseRepo.DbSet.Where(x => x.Id == id).Select(x => x.NotificationEmail).DefaultIfEmpty(null).FirstOrDefault();
        }
    }
}
