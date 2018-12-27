using System;
using System.Collections.Generic;
using System.Linq;
using xTab.Tools.Helpers;
using Bissoft.CouncilCMS.Core;
using Bissoft.CouncilCMS.Core.Enums;
using Bissoft.CouncilCMS.Core.Globalization;
using Bissoft.CouncilCMS.DAL.Entities;
using Bissoft.CouncilCMS.BLL.ViewModels;

namespace Bissoft.CouncilCMS.BLL.Services
{
    public class CmsPersonService : BaseService
    {
        private IRepository<Person, int> personRepo;
        private IRepository<PersonCategory, int> categoryRepo;
        private IRepository<Doc, int> docRepo;
        private CmsContentService contentService;
        private CmsMenuService menuService;
        public CmsPersonService(string ConnectionString) : base(ConnectionString)
        {
            Initialize();
        }

        public CmsPersonService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            Initialize();
        }

        private void Initialize()
        {            
            personRepo = UnitOfWork.GetIntRepository<Person>();
            categoryRepo = UnitOfWork.GetIntRepository<PersonCategory>();
            docRepo = UnitOfWork.GetIntRepository<Doc>();
            contentService = new CmsContentService(this.UnitOfWork);
            menuService = new CmsMenuService(this.UnitOfWork);
        }

        public CmsPersonList CategoryList(string articleUrl, ShowMenuItemMode Mode)
        {
            var category = categoryRepo.GetSingle(x => x.UrlName == articleUrl, null, true, true);

            if (category != null)
            {
                var now = DateTime.Now;
                var count = 0;

                var predicate = PredicateBuilder.True<Person>();
                
                predicate = predicate.And(x => x.Categories.Any(c => c.CategoryId == category.Id) && x.Published && x.PublishDate != null && x.PublishDate <= now);

                var list = personRepo
                    .GetList(out count,
                    predicate,
                    x => x.OrderBy(o => o.Categories.FirstOrDefault(c => c.CategoryId == category.Id).Position),
                    "Categories,DeputyFraction", null, null).Select(x => 
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

                var model = new CmsPersonList()
                {
                    CategoryId = category.Id,
                    CategoryName = category.GetLocalValue("Title"),
                    CategoryDescription = category.GetLocalValue("Description"),
                    CategoryUrl = category.UrlName,
                    CategoryTemplate = category.Template != null ? category.Template.Name : "default",                    
                    Persons = list,
                    ShowSearchForm = category.ShowSearchForm,
                    CategoryMenu = category.SidebarMenuId > 0 ? menuService.GetMenu(category.SidebarMenuId.Value, Mode, MenuItemType.PersonCategory, null, category.UrlName) : null
                };

                return model;

            }

            return null;
        }

        public CmsPerson Person(Int32 id, ShowMenuItemMode Mode = ShowMenuItemMode.AllLangualges, int? userId = null)
        {
            var model = new CmsPerson();
            var item = personRepo.GetById(id);

            if (item == null || (userId == null && (!item.Published || item.Deleted || item.PublishDate == null || item.PublishDate > DateTime.Now)))
                return null;

            var category = (item.Categories != null && item.Categories.Count > 0) ? item.Categories.OrderByDescending(o => o.Category.Priority).ThenBy(o => o.Category.TitleUk).FirstOrDefault().Category : null;

            model.Id = item.Id;
            model.Email = item.NotificationEmail;
            model.Post = item.GetLocalValue("Post");
            model.Profession = item.GetLocalValue("Profession");
            model.Url = item.GetLocalValue("UrlName");
            model.Description = item.GetLocalValue("Description");
            model.ContactAddress = item.GetLocalValue("ContactAddress");
            model.ContactEmails = item.GetLocalValue("ContactEmails");
            model.ContactPhones = item.GetLocalValue("ContactPhones");
            model.ReceptionAddress = item.GetLocalValue("ReceptionAddress");
            model.ReceptionDates = item.GetLocalValue("ReceptionDates");
            model.ReceptionTime = item.GetLocalValue("ReceptionTime");
            model.FbLink = item.FbLink;
            model.TwLink = item.TwLink;
            model.YtLink = item.YtLink;
            model.IgLink = item.IgLink;
            model.GpLink = item.GpLink;
            model.DeputyFractionId = item.DeputyFractionId;
            model.DeputyFractionName = item.DeputyFractionId > 0 ? item.DeputyFraction.GetLocalValue("Title") : null;
            model.DeputyFractionUrl = item.DeputyFractionId > 0 ? item.DeputyFraction.UrlNameUk : null;
            model.Image = item.Image;
            model.ShowSecondPage = item.ShowSecondPage;
            model.SecondPageTitle = item.GetLocalValue("SecondPageTitle");
            model.SecondPageText = item.GetLocalValue("SecondPageText");
            model.MetaDescription = item.GetLocalValue("MetaDescriptionRu");
            model.MetaKeywords = item.GetLocalValue("MetaKeywords");
            model.MetaTitle = item.GetLocalValue("MetaTitle");
            model.PublishDate = item.ShowPublihDate ? DateTimeHelper.NullableDateTimeString(item.PublishDate) : String.Empty;
            model.LastEditDate = item.ShowEditDate ? DateTimeHelper.NullableDateTimeString(item.EditedDate) : String.Empty;
            model.Reception = new CmsPersonReception() { PersonId = model.Id };
            model.ShowReceptionForm = item.HasReceptionForm && !String.IsNullOrEmpty(item.NotificationEmail);
            model.Title = item.GetLocalValue("Title");
            model.PersonMenu = item.SidebarMenuId > 0 ? menuService.GetMenu(item.SidebarMenuId.Value, Mode) : null;
            model.ContentRows = contentService.GetContentRows(item.ContentRows);

            if (category != null)
            {
                model.CurrentCategoryId = category.Id;
                model.CurrentCategoryTitle = category.GetLocalValue("Title");
                model.CurrentCategoryUrl = category.UrlName;
            }

            model.Enterprises = item.Enterprises == null ?
                null :
                item.Enterprises.Where(x => x.Position == 1 && x.Enterprise.Type != null && !x.Enterprise.Type.Name.Contains("deputy")).ToList()
                .Select(x => new CmsEnterpriseListItem() { Id = x.Enterprise.Id, Title = x.Enterprise.GetLocalValue("Title"), UrlName = x.Enterprise.UrlNameUk }).ToList();

            model.DocCategories = null;

            var now = DateTime.Now;
            var docCats = docRepo
                .GetList(d => d.Published && !d.Deleted && d.PublishDate <= now && d.Persons.Any(p => p.Id == model.Id), null, "Categories")
                .SelectMany(x => x.Categories).Select(c => c).GroupBy(x => x.Id).Select(g => g.First()).Select(x => new { Id = x.Id, TitleUk = x.TitleUk, TitleRu = x.TitleRu, TitleEn = x.TitleEn, ParentId = x.ParentCategoryId, Url = x.UrlName })
                .ToList()
                .Select(x => new CmsDocCategoryListItem() { Id = x.Id, Title = x.GetLocalValue("Title"), ParentId = x.ParentId, Url = x.Url })
                .ToList();

            docCats.ToList().ForEach(i => { if (docCats.Any(c => c.ParentId == i.Id)) { docCats.Remove(i); } });
            docCats.ForEach(i => i.Count = docRepo.DbSet.Count(d => d.Published && !d.Deleted && d.PublishDate <= now && d.Categories.Any(c => c.Id == i.Id) && d.Persons.Any(p => p.Id == model.Id)));

            model.DocCategories = docCats;

            AddView(id);

            return model;
        }

        public List<CmsPersonListItem> TopPersons(string url, int limit = 3)
        {
            var category = categoryRepo.GetSingle(x => x.UrlName == url, null, true, true);

            if (category != null)
            {
                var now = DateTime.Now;
                var count = 0;

                var predicate = PredicateBuilder.True<Person>();
                
                predicate = predicate.And(x => x.Categories.Any(c => c.CategoryId == category.Id) && x.Published && x.PublishDate != null && x.PublishDate <= now);

                var model = personRepo
                    .GetList(out count,
                    predicate,
                    x => x.OrderBy(o => o.Categories.FirstOrDefault(c => c.CategoryId == category.Id).Position),
                    "Categories", 0, limit).Select(x =>
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
                        Image = x.Image,

                    }).ToList().Select(x => new CmsPersonListItem()
                    {
                        Id = x.Id,
                        Title = x.GetLocalValue("Title"),
                        Post = x.GetLocalValue("Post"),
                        Description = x.GetLocalValue("Description"),
                        ContactAddress = x.GetLocalValue("ContactAddress"),
                        ContactPhones = x.GetLocalValue("ContactPhones"),
                        ContactEmail = x.GetLocalValue("ContactEmails"),
                        UrlName = x.GetLocalValue("UrlName"),
                        Image = x.Image
                    }).ToList();

               
                return model;

            }

            return new List<CmsPersonListItem>();
        }

        public void AddView(Int32 id)
        {
            personRepo.Context.ExecuteSqlCommand("UPDATE Persons SET ViewCount = ViewCount + 1 WHERE Id = {0}", false, null, id);
        }

        public string GetEmail(int id)
        {
            return personRepo.DbSet.Where(x => x.Id == id).Select(x => x.NotificationEmail).DefaultIfEmpty(null).FirstOrDefault();
        }
    }
}
