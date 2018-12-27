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
using System.Web;
using Bissoft.CouncilCMS.Web.Core;

namespace Bissoft.CouncilCMS.BLL.Services
{
    public class EnterpriseAdminService : BaseService
    {
        private IRepository<Enterprise, int> enterpriseRepo;
        private IRepository<EnterpriseCategory, int> enterpriseCategoryRepo;
        private IRepository<Person, int> personRepo;
        private IRepository<CmsUser, int> userRepo;
        private ContentAdminService contentAdminService;
        private SelectListService selectListService;
        private CmsSearchAdminService cmsSearchService;

        public EnterpriseAdminService(string ConnectionString) : base(ConnectionString)
        {
            Initialize();
        }

        public EnterpriseAdminService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            Initialize();
        }

        private void Initialize()
        {
            enterpriseRepo = UnitOfWork.GetIntRepository<Enterprise>();
            enterpriseCategoryRepo = UnitOfWork.GetIntRepository<EnterpriseCategory>();
            personRepo = UnitOfWork.GetIntRepository<Person>();
            userRepo = UnitOfWork.GetIntRepository<CmsUser>();
            contentAdminService = new ContentAdminService(this.UnitOfWork);
            selectListService = new SelectListService(this.UnitOfWork);
            cmsSearchService = new CmsSearchAdminService(this.UnitOfWork);
        }
        public EnterpriseList List(string query = null, string dateRange = null, int dateType = 0, int? categoryId = null, CmsItemState itemState = CmsItemState.All, Int32 page = 1, string sortBy = "PublishDate", int sortDir = 1, int? user = null, int userAction = 0, int? userId = null)
        {
            var model = new EnterpriseList()
            {
                Page = page,
                Query = query,
                CategoryId = categoryId,
                DateRange = dateRange,
                DateType = dateType,
                ItemState = (int)itemState,
                SortBy = sortBy,
                SortDirection = sortDir,
                Categories = selectListService.CategorySelectList<EnterpriseCategory>(String.Empty, userId),
                User = user,
                UserAction = userAction,
                Users = selectListService.GetCmsUserSelectList(String.Empty)
            };

            return model;
        }
        public List<EnterpriseListItem> GetList(out int count, string query = null, string dateRange = null, int dateType = 0, int? categoryId = null, CmsItemState itemState = CmsItemState.All, Int32 page = 1, Int32 perPage = 20, string sortBy = "PublishDate", int sortDir = 1, int? user = null, int userAction = 0, int? userId = null)
        {
            var predicate = CreateStatePredicate<Enterprise>(itemState);

            if (!String.IsNullOrEmpty(query))
                predicate = TitleFilter(predicate, query);

            if (!String.IsNullOrEmpty(dateRange))
                predicate = DateRangeFilter(predicate, dateRange, dateType);

            if (user > 0)
                predicate = UserFilter(predicate, user, userAction);

            if (categoryId > 0)
                predicate = predicate.And(x => x.Categories.Any(c => c.CategoryId == categoryId));

            if (userId > 0)
            {
                var roles = userRepo.GetById(userId.Value, "Roles", true).Roles.Select(x => x.Id);
                predicate = predicate.And(x => x.AllowedUsers.Any(u => u.Id == userId) || x.Categories.Any(c => c.Category.AllowedUsers.Any(u => u.Id == userId)) || x.Categories.Any(c => c.Category.AllowedRoles.Any(r => roles.Contains(r.Id))));
            }

            var list = enterpriseRepo.GetList(out count, predicate, x => x.OrderByColumn(sortBy, sortDir > 0), "Categories,Categories.Category,CreateUser,LastEditUser", (page - 1) * perPage, perPage, true, false);
            
            var model = list.Select(x => new EnterpriseListItem()
                {
                    Id = x.Id,
                    Url = x.UrlNameUk,
                    TitleRu = x.TitleRu,
                    TitleUk = x.TitleUk,
                    TitleEn = x.TitleEn,                    
                    CreateDate = x.CreateDate,
                    LastEditDate = x.LastEditDate,
                    EditedDate = x.EditedDate,
                    PublishDate = x.PublishDate,
                    Deleted = x.Deleted,
                    Published = x.Published,
                    ViewCount = x.ViewCount,
                    CategoriesUk = x.Categories.Select(c => c.Category.TitleUk),
                    CategoriesRu = x.Categories.Select(c => c.Category.TitleRu),
                    CategoriesEn = x.Categories.Select(c => c.Category.TitleEn),
                    CreateUser = x.CreateUserId > 0 ? x.CreateUser.Name : null,
                    LastEditUser = x.LastEditUserId > 0 ? x.LastEditUser.Name : null
            }).ToList();                

            return model;
        }


        public EnterpriseEdit GetForm(Int32 Id, Int32? ParentId = null, int? userId = null)
        {
            var parent = enterpriseRepo.GetById(ParentId ?? 0) ?? new Enterprise();
            var entry = enterpriseRepo.GetById(Id);

            if (Id == 0)
            {
                entry = enterpriseRepo.GetById(Id) ??
                new Enterprise()
                {
                    TitleRu = "Без названия",
                    TitleUk = "Без назви",
                    TitleEn = "No name",
                    ContactAddressUk = parent.ContactAddressUk,
                    ContactAddressRu = parent.ContactAddressRu,
                    ContactAddressEn = parent.ContactAddressEn,
                    ContactDatesEn = parent.ContactDatesEn,
                    ContactDatesRu = parent.ContactDatesRu,
                    ContactDatesUk = parent.ContactDatesUk,
                    ContactEmailsEn = parent.ContactEmailsEn,
                    ContactEmailsRu = parent.ContactEmailsRu,
                    ContactEmailsUk = parent.ContactEmailsUk,
                    ContactPhonesEn = parent.ContactPhonesEn,
                    ContactPhonesRu = parent.ContactPhonesRu,
                    ContactPhonesUk = parent.ContactPhonesUk,
                    ContactTimeEn = parent.ContactTimeEn,
                    ContactTimeRu = parent.ContactTimeRu,
                    ContactTimeUk = parent.ContactTimeUk,
                    ContactWebsiteEn = parent.ContactWebsiteEn,
                    ContactWebsiteRu = parent.ContactWebsiteRu,
                    ContactWebsiteUk = parent.ContactWebsiteUk,
                    Categories = parent.Categories,
                    ParentEnterpriseId = ParentId,
                    CreateDate = DateTime.Now,     
                    PublishDate = DateTime.Now,
                    ShowPublihDate = true,
                    ShowEditDate = true,               
                    ContentRows = new List<ContentRow>(),
                    CreateUserId = (HttpContext.Current.User as CmsPrincipal).Identity.UserId
                };

                enterpriseRepo.Insert(entry);
                UnitOfWork.Commit();

                entry.TitleUk = null;
                entry.TitleRu = null;
                entry.TitleEn = null;
            }

            var model = new EnterpriseEdit();
            
            model.Id = entry.Id;
            model.TitleRu = entry.TitleRu;
            model.TitleUk = entry.TitleUk;
            model.TitleEn = entry.TitleEn;
            model.DescriptionRu = entry.DescriptionRu;
            model.DescriptionUk = entry.DescriptionUk;
            model.DescriptionEn = entry.DescriptionEn;
            model.ContactAddressRu = entry.ContactAddressRu;
            model.ContactAddressUk = entry.ContactAddressUk;
            model.ContactAddressEn = entry.ContactAddressEn;
            model.ContactEmailsUk = entry.ContactEmailsUk;
            model.ContactEmailsRu = entry.ContactEmailsRu;
            model.ContactEmailsEn = entry.ContactEmailsEn;
            model.ContactWebsiteEn = entry.ContactWebsiteEn;
            model.ContactWebsiteRu = entry.ContactWebsiteRu;
            model.ContactWebsiteUk = entry.ContactWebsiteUk;
            model.ContactPhonesUk = entry.ContactPhonesUk;
            model.ContactPhonesRu = entry.ContactPhonesRu;
            model.ContactPhonesEn = entry.ContactPhonesEn;
            model.ContactDatesRu = entry.ContactDatesRu;
            model.ContactDatesUk = entry.ContactDatesUk;
            model.ContactDatesEn = entry.ContactDatesEn;
            model.ContactTimeRu = entry.ContactTimeRu;
            model.ContactTimeUk = entry.ContactTimeUk;
            model.ContactTimeEn = entry.ContactTimeEn;
            model.ShowSecondPage = entry.ShowSecondPage;
            model.SecondPageTitleEn = entry.SecondPageTitleEn;
            model.SecondPageTitleRu = entry.SecondPageTitleRu;
            model.SecondPageTitleUk = entry.SecondPageTitleUk;
            model.SecondPageTextEn = entry.SecondPageTextEn;
            model.SecondPageTextRu = entry.SecondPageTextRu;
            model.SecondPageTextUk = entry.SecondPageTextUk;
            model.PersonsTitleEn = entry.PersonsTitleEn;
            model.PersonsTitleRu = entry.PersonsTitleRu;
            model.PersonsTitleUk = entry.PersonsTitleUk;
            model.MetaTitleRu = entry.MetaTitleRu;
            model.MetaTitleUk = entry.MetaTitleUk;
            model.MetaTitleEn = entry.MetaTitleEn;
            model.MetaKeywordsRu = entry.MetaKeywordsRu;
            model.MetaKeywordsUk = entry.MetaKeywordsUk;
            model.MetaKeywordsEn = entry.MetaKeywordsEn;
            model.MetaDescriptionRu = entry.MetaDescriptionRu;
            model.MetaDescriptionUk = entry.MetaDescriptionUk;
            model.MetaDescriptionEn = entry.MetaDescriptionEn;
            model.Image = entry.Image;
            model.NotificationEmail = entry.NotificationEmail;
            model.NotificationPhone = entry.NotificationPhone;
            model.Published = entry.Published;
            model.ShowEditDate = entry.ShowEditDate;
            model.ShowPublihDate = entry.ShowPublihDate;
            model.HasContactForm = entry.HasContactForm;
            model.Facebook = entry.Facebook;
            model.Twitter = entry.Twitter;
            model.FbLink = entry.FbLink;
            model.TwLink = entry.TwLink;
            model.GpLink = entry.GpLink;
            model.YtLink = entry.YtLink;
            model.IgLink = entry.IgLink;
            model.PublishDate = DateTimeHelper.NullableDateTimeString(entry.PublishDate);
            model.EditedDate = DateTimeHelper.NullableDateTimeString(entry.EditedDate);
            model.SidebarMenuId = entry.SidebarMenuId;
            model.ContentRows = contentAdminService.GetContentRowListItems(entry.ContentRows.ToList(), model.Id);
            model.SidebarMenus = selectListService.GetMenuSelectList(String.Empty);
            model.AllowedUsers = model.AllowedUsers != null ? entry.AllowedUsers.Select(x => new AllowedUser() { UserId = x.Id, ItemId = entry.Id, Name = x.Name, Email = x.Email }).ToList() : null;
            model.ParentEnterpriseId = entry.ParentEnterpriseId;
            model.ParentEnterpriseName = entry.ParentEnterpriseId > 0 ? entry.ParentEnterprise.GetLocalValue("Title") : null;
            model.TypeId = entry.TypeId;
            model.Types = selectListService.GetEnterpriseTypeList(String.Empty);
            model.CategoryList = selectListService.CategorySelectList<EnterpriseCategory>(null, userId);
            model.Users = selectListService.GetCmsUserSelectList(null, "ed_enterprises_allowed");

            model.Categories = entry.Categories
                .Select(x => new { Id = x.CategoryId, TitleRu = x.Category.TitleRu, TitleUk = x.Category.TitleUk, Position = x.Position })
                .Select(x => new EnterpriseCategoryItem() { CategoryId = x.Id, EnterpriseId = entry.Id, Title = x.GetLocalValue("Title"), Position = x.Position })
                .ToList();

            model.Persons = entry.Persons
                .Select(x => new { Id = x.PersonId, TitleRu = x.Person.TitleRu, TitleUk = x.Person.TitleUk, Position = x.Position, PostRu = x.PostRu, PostEn = x.PostEn, PostUk = x.PostUk })
                .Select(x => new EnterprisePersonItem() { PersonId = x.Id, EnterpriseId = entry.Id, Title = x.GetLocalValue("Title"), Position = x.Position, PostUk = x.PostUk, PostEn = x.PostEn, PostRu = x.PostRu })
                .ToList();
                              
            return model;
        }

        public void Save(EnterpriseEdit model, Int32? userId = null)
        {
            var item = enterpriseRepo.GetById(model.Id);

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
            item.ContactAddressRu = model.ContactAddressRu;
            item.ContactAddressUk = model.ContactAddressUk;
            item.ContactAddressEn = model.ContactAddressEn;
            item.ContactEmailsUk = model.ContactEmailsUk;
            item.ContactEmailsRu = model.ContactEmailsRu;
            item.ContactEmailsEn = model.ContactEmailsEn;
            item.ContactWebsiteEn = model.ContactWebsiteEn;
            item.ContactWebsiteRu = model.ContactWebsiteRu;
            item.ContactWebsiteUk = model.ContactWebsiteUk;
            item.ContactPhonesUk = model.ContactPhonesUk;
            item.ContactPhonesRu = model.ContactPhonesRu;
            item.ContactPhonesEn = model.ContactPhonesEn;
            item.ContactDatesRu = model.ContactDatesRu;
            item.ContactDatesUk = model.ContactDatesUk;
            item.ContactDatesEn = model.ContactDatesEn;
            item.ContactTimeRu = model.ContactTimeRu;
            item.ContactTimeUk = model.ContactTimeUk;
            item.ContactTimeEn = model.ContactTimeEn;
            item.ShowSecondPage = model.ShowSecondPage;
            item.SecondPageTitleEn = model.SecondPageTitleEn;
            item.SecondPageTitleRu = model.SecondPageTitleRu;
            item.SecondPageTitleUk = model.SecondPageTitleUk;
            item.SecondPageTextEn = model.SecondPageTextEn;
            item.SecondPageTextRu = model.SecondPageTextRu;
            item.SecondPageTextUk = model.SecondPageTextUk;
            item.PersonsTitleEn = model.PersonsTitleEn;
            item.PersonsTitleRu = model.PersonsTitleRu;
            item.PersonsTitleUk = model.PersonsTitleUk;
            item.UrlNameRu = model.TitleRu.Translit();
            item.UrlNameUk = model.TitleUk.Translit();
            item.UrlNameEn = (model.TitleEn ?? String.Empty).ToLower().Replace(" ", "-");
            item.MetaTitleRu = model.MetaTitleRu;
            item.MetaTitleUk = model.MetaTitleUk;
            item.MetaTitleEn = model.MetaTitleEn;
            item.MetaKeywordsRu = model.MetaKeywordsRu;
            item.MetaKeywordsUk = model.MetaKeywordsUk;
            item.MetaKeywordsEn = model.MetaKeywordsEn;
            item.MetaDescriptionRu = model.MetaDescriptionRu;
            item.MetaDescriptionUk = model.MetaDescriptionUk;
            item.MetaDescriptionEn = model.MetaDescriptionEn;
            item.Image = model.Image;
            item.NotificationEmail = model.NotificationEmail;
            item.NotificationPhone = model.NotificationPhone;
            item.Facebook = model.Facebook;
            item.Twitter = model.Twitter;
            item.FbLink = model.FbLink;
            item.TwLink = model.TwLink;
            item.GpLink = model.GpLink;
            item.YtLink = model.YtLink;
            item.IgLink = model.IgLink;
            item.HasContactForm = model.HasContactForm;
            item.SidebarMenuId = model.SidebarMenuId;
            item.Saved = true;
            item.Published = model.Published;
            item.ShowPublihDate = model.ShowPublihDate;
            item.ShowEditDate = model.ShowEditDate;
            item.LastEditDate = DateTime.Now;
            item.ParentEnterpriseId = model.ParentEnterpriseId;
            item.PublishDate = DateTimeHelper.ParseDateNullable(model.PublishDate, null, DateTimeHelper.DefaultTimeOfDay.None);
            item.EditedDate = DateTimeHelper.ParseDateNullable(model.EditedDate, null, DateTimeHelper.DefaultTimeOfDay.None);
            item.TypeId = model.TypeId;
            item.LastEditUserId = (HttpContext.Current.User as CmsPrincipal).Identity.UserId;

            enterpriseRepo.Update(item);
            UnitOfWork.Commit();

            cmsSearchService.Save(item.Id, textUk, textRu, textEn, item.CreateDate, CmsSearchType.Enterprise, item.Published);
        }


        public void Delete(Int32 Id)
        {
            var person = enterpriseRepo.GetById(Id);

            person.Deleted = true;

            enterpriseRepo.Update(person);
            cmsSearchService.Delete(Id, CmsSearchType.Enterprise);

            UnitOfWork.Commit();
        }

        public void Restore(Int32 Id)
        {
            var person = enterpriseRepo.GetById(Id);

            person.Deleted = false;

            enterpriseRepo.Update(person);
            cmsSearchService.Restore(Id, CmsSearchType.Enterprise);

            UnitOfWork.Commit();
        }

        public void Remove(Int32 Id)
        {
            var person = enterpriseRepo.GetById(Id);

            cmsSearchService.Remove(Id, CmsSearchType.Enterprise);
            enterpriseRepo.Delete(person);
            
            UnitOfWork.Commit();
        }

        public EnterprisePersonItem GetPerson(int personId, int enterpriseId)
        {
            var org = enterpriseRepo.GetById(enterpriseId);

            if (org != null && org.Persons != null && org.Persons.Any(x => x.PersonId == personId))
            {
                var enterprisePerson = org.Persons.FirstOrDefault(x => x.EnterpriseId == enterpriseId && x.PersonId == personId);
                var person = personRepo.GetById(personId);

                if (person != null)
                {
                    var model = new EnterprisePersonItem()
                    {
                        EnterpriseId = enterpriseId,
                        PersonId = personId,
                        Title = person.GetLocalValue("Title"),
                        Position = enterprisePerson.Position,
                        PostEn = enterprisePerson.PostEn,
                        PostRu = enterprisePerson.PostRu,
                        PostUk = enterprisePerson.PostUk,
                        IsDeputyFraction = person.DeputyFractionId == enterpriseId
                    };

                    return model;
                }
            }

            return null;
        }

        public EnterpriseCategoryItem GetCategory(Int32 CategoryId, Int32 EnterpriseId)
        {
            var org = enterpriseRepo.GetById(EnterpriseId);

            if (org != null && org.Categories != null && org.Categories.Any(x => x.CategoryId == CategoryId))
            {
                var orgCategory = org.Categories.FirstOrDefault(x => x.EnterpriseId == EnterpriseId && x.CategoryId == CategoryId);
                var category = enterpriseCategoryRepo.GetById(CategoryId);

                if (category != null)
                {
                    var model = new EnterpriseCategoryItem()
                    {
                        CategoryId = CategoryId,
                        EnterpriseId = EnterpriseId,
                        Title = category.GetLocalValue("Title"),
                        Position = orgCategory.Position
                    };

                    return model;
                }
            }

            return null;
        }

        public EnterprisePersonItem SavePerson(EnterprisePersonItem model)
        {            
            var org = enterpriseRepo.GetById(model.EnterpriseId);
            var per = personRepo.GetById(model.PersonId);

            if (org.Persons == null)
                org.Persons = new List<EnterprisePerson>();

            if (per != null)
            {
                var itemPerson = org.Persons.FirstOrDefault(x => x.EnterpriseId == model.EnterpriseId && x.PersonId == model.PersonId);

                if (itemPerson == null)
                {
                    var person = new EnterprisePerson()
                    {
                        PersonId = model.PersonId,
                        PostEn = model.PostEn,
                        PostRu = model.PostRu,
                        PostUk = model.PostUk,
                        Position = model.Position
                    };

                    org.Persons.Add(person);
                }
                else
                {
                    itemPerson.Position = model.Position;
                    itemPerson.PostEn = model.PostEn;
                    itemPerson.PostRu = model.PostRu;
                    itemPerson.PostUk = model.PostUk;
                }

                if (model.IsDeputyFraction)
                {
                    per.DeputyFractionId = model.EnterpriseId;
                    personRepo.Update(per);
                }
                
                enterpriseRepo.Update(org);
                UnitOfWork.Commit();

                model.Title = per.GetLocalValue("Title");

                return model;
            }                
            
            return null;
        }

        public EnterpriseCategoryItem SaveCategory(EnterpriseCategoryItem model)
        {
            var org = enterpriseRepo.GetById(model.EnterpriseId);
            var cat = enterpriseCategoryRepo.GetById(model.CategoryId);

            if (org.Categories == null)
                org.Categories = new List<EnterpriseEnterpriseCategory>();

            if (cat != null)
            {
                var itemCategory = org.Categories.FirstOrDefault(x => x.EnterpriseId == model.EnterpriseId && x.CategoryId == model.CategoryId);

                if (itemCategory == null)
                {
                    var category = new EnterpriseEnterpriseCategory()
                    {
                        CategoryId = model.CategoryId,
                        Position = model.Position
                    };

                    org.Categories.Add(category);
                }
                else
                {
                    itemCategory.Position = model.Position;
                }
                
                enterpriseRepo.Update(org);
                UnitOfWork.Commit();

                model.Title = cat.GetLocalValue("Title");

                return model;                
            }

            return null;
        }

        public void DeletePerson(Int32 PersonId, Int32 EnterpriseId)
        {
            var org = enterpriseRepo.GetById(EnterpriseId);
            var person = org.Persons.FirstOrDefault(x => x.PersonId == PersonId);
            
            if (person != null)
            {
                org.Persons.Remove(person);
            }

            enterpriseRepo.Update(org);
            UnitOfWork.DefineAsDeleted<EnterprisePerson, int>(person);
            UnitOfWork.Commit();
        }

        public void DeleteCategory(Int32 CategoryId, Int32 EnterpriseId)
        {
            var org = enterpriseRepo.GetById(EnterpriseId);
            var category = org.Categories.FirstOrDefault(x => x.CategoryId == CategoryId);

            if (category != null)
            {
                org.Categories.Remove(category);
            }

            enterpriseRepo.Update(org);
            UnitOfWork.DefineAsDeleted<EnterpriseEnterpriseCategory, int>(category);
            UnitOfWork.Commit();
        }

        public AllowedUser AddUser(AllowedUser model)
        {
            var item = enterpriseRepo.GetById(model.ItemId);
            var user = userRepo.GetById(model.UserId);

            if (item.AllowedUsers == null)
                item.AllowedUsers = new List<CmsUser>();

            if (user != null)
            {
                var allowedUser = item.AllowedUsers.FirstOrDefault(x => x.Id == model.UserId);

                if (allowedUser == null)
                {
                    item.AllowedUsers.Add(user);

                    enterpriseRepo.Update(item);
                    UnitOfWork.Commit();

                    model.Email = user.Email;

                    return model;
                }                
            }

            return null;
        }

        public void DeleteUser(Int32 ItemId, Int32 UserId)
        {
            var item = enterpriseRepo.GetById(ItemId);
            var user = item.AllowedUsers.FirstOrDefault(x => x.Id == ItemId);

            if (user != null)
            {
                item.AllowedUsers.Remove(user);
            }

            enterpriseRepo.Update(item);
            UnitOfWork.Commit();
        }
    }
}