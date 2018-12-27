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
    public class PersonAdminService : BaseService
    {
        private IRepository<Person, int> personRepo;
        private IRepository<PersonCategory, int> personCategoryRepo;
        private IRepository<CmsUser, int> userRepo;
        private ContentAdminService contentAdminService;
        private SelectListService selectListService;
        private CmsSearchAdminService cmsSearchService;

        public PersonAdminService(string ConnectionString) : base(ConnectionString)
        {
            Initialize();
        }

        public PersonAdminService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            Initialize();
        }

        private void Initialize()
        {
            personRepo = UnitOfWork.GetIntRepository<Person>();
            personCategoryRepo = UnitOfWork.GetIntRepository<PersonCategory>();
            userRepo = UnitOfWork.GetIntRepository<CmsUser>();
            contentAdminService = new ContentAdminService(this.UnitOfWork);
            selectListService = new SelectListService(this.UnitOfWork);
            cmsSearchService = new CmsSearchAdminService(this.UnitOfWork);
        }
        public PersonList List(string query = null, string dateRange = null, int dateType = 0, int? categoryId = null, CmsItemState itemState = CmsItemState.All, Int32 page = 1, string sortBy = "TitleUk", int sortDir = 1, int? user = null, int userAction = 0, int? userId = null)
        {
            var model = new PersonList()
            {
                Page = page,
                Query = query,
                CategoryId = categoryId,
                DateRange = dateRange,
                DateType = dateType,
                ItemState = (int)itemState,
                SortBy = sortBy,
                SortDirection = sortDir,
                Categories = selectListService.CategorySelectList<PersonCategory>(String.Empty, userId),
                User = user,
                UserAction = userAction,
                Users = selectListService.GetCmsUserSelectList(String.Empty)
            };

            return model;
        }
        public List<PersonListItem> GetList(out int count, string query = null, string dateRange = null, int dateType = 0, int? categoryId = null, CmsItemState itemState = CmsItemState.All, Int32 page = 1, Int32 perPage = 20, string sortBy = "TitleUk", int sortDir = 1, int? user = null, int userAction = 0, int? userId = null)
        {
            var predicate = CreateStatePredicate<Person>(itemState);

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

            var list = personRepo.GetList(out count, predicate, x => x.OrderByColumn(sortBy, sortDir > 0), "Categories,Categories.Category,CreateUser,LastEditUser", (page - 1) * perPage, perPage, true, false);

            var model = list.Select(x => new PersonListItem()
                {
                    Id = x.Id,
                    TitleRu = x.TitleRu,
                    TitleUk = x.TitleUk,
                    TitleEn = x.TitleEn,
                    Url = x.UrlNameUk,
                    CreateDate = x.CreateDate,
                    LastEditDate = x.LastEditDate,
                    EditedDate = x.EditedDate,
                    PublishDate = x.PublishDate,                   
                    Deleted = x.Deleted,
                    Published = x.Published,
                    ViewCount = x.ViewCount ?? 0,
                    CategoriesUk = x.Categories.Select(c => c.Category.TitleUk),
                    CategoriesRu = x.Categories.Select(c => c.Category.TitleRu),
                    CategoriesEn = x.Categories.Select(c => c.Category.TitleEn),
                    CreateUser = x.CreateUserId > 0 ? x.CreateUser.Name : null,
                    LastEditUser = x.LastEditUserId > 0 ? x.LastEditUser.Name : null
            }).ToList();

            return model;
        }


        public PersonEdit GetForm(Int32 id, int? userId = null)
        {
            var entry = personRepo.GetById(id);

            if (id == 0)
            {
                entry = new Person()
                {
                    TitleRu = "Без названия",
                    TitleUk = "Без назви",
                    TitleEn = "No name",
                    CreateDate = DateTime.Now,  
                    PublishDate = DateTime.Now,                  
                    ContentRows = new List<ContentRow>(),
                    ShowPublihDate = true,
                    ShowEditDate = true,
                    ViewCount = 0,
                    CreateUserId = (HttpContext.Current.User as CmsPrincipal).Identity.UserId
                };

                personRepo.Insert(entry);
                UnitOfWork.Commit();

                entry.TitleUk = null;
                entry.TitleRu = null;
                entry.TitleEn = null;
            }

            var model = new PersonEdit();
            
            model.Id = entry.Id;
            model.TitleRu = entry.TitleRu;
            model.TitleUk = entry.TitleUk;
            model.TitleEn = entry.TitleEn;
            model.PostRu = entry.PostRu;
            model.PostUk = entry.PostUk;            
            model.PostEn = entry.PostEn;
            model.ProfessionEn = entry.ProfessionEn;
            model.ProfessionRu = entry.ProfessionRu;
            model.ProfessionUk = entry.ProfessionUk;
            model.DescriptionRu = entry.DescriptionRu;
            model.DescriptionUk = entry.DescriptionUk;
            model.DescriptionEn = entry.DescriptionEn;
            model.ContactAddressRu = entry.ContactAddressRu;
            model.ContactAddressUk = entry.ContactAddressUk;
            model.ContactAddressEn = entry.ContactAddressEn;
            model.ContactEmailsUk = entry.ContactEmailsUk;
            model.ContactEmailsRu = entry.ContactEmailsRu;
            model.ContactEmailsEn = entry.ContactEmailsEn;
            model.ContactPhonesUk = entry.ContactPhonesUk;
            model.ContactPhonesRu = entry.ContactPhonesRu;
            model.ContactPhonesEn = entry.ContactPhonesEn;
            model.ReceptionAddressRu = entry.ReceptionAddressRu;
            model.ReceptionAddressUk = entry.ReceptionAddressUk;
            model.ReceptionAddressEn = entry.ReceptionAddressEn;
            model.ReceptionDatesRu = entry.ReceptionDatesRu;
            model.ReceptionDatesUk = entry.ReceptionDatesUk;
            model.ReceptionDatesEn = entry.ReceptionDatesEn;
            model.ReceptionTimeRu = entry.ReceptionTimeRu;
            model.ReceptionTimeUk = entry.ReceptionTimeUk;
            model.ReceptionTimeEn = entry.ReceptionTimeEn;
            model.ShowSecondPage = entry.ShowSecondPage;
            model.SecondPageTitleEn = entry.SecondPageTitleEn;
            model.SecondPageTitleRu = entry.SecondPageTitleRu;
            model.SecondPageTitleUk = entry.SecondPageTitleUk;
            model.SecondPageTextEn = entry.SecondPageTextEn;
            model.SecondPageTextRu = entry.SecondPageTextRu;
            model.SecondPageTextUk = entry.SecondPageTextUk;
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
            model.HasReceptionForm = entry.HasReceptionForm;
            model.Facebook = entry.Facebook;
            model.Twitter = entry.Twitter;
            model.FbLink = entry.FbLink;
            model.TwLink = entry.TwLink;
            model.GpLink = entry.GpLink;
            model.YtLink = entry.YtLink;
            model.IgLink = entry.IgLink;
            model.Birthday = DateTimeHelper.NullableDateTimeString(entry.Birthday, null, false, "dd.MM.yyyy");
            model.EditedDate = null;
            model.PublishDate = DateTimeHelper.NullableDateTimeString(entry.PublishDate, Format: "dd.MM.yyyy HH:mm");
            model.SidebarMenuId = entry.SidebarMenuId;
            model.ContentRows = contentAdminService.GetContentRowListItems(entry.ContentRows.ToList(), model.Id);
            model.SidebarMenus = selectListService.GetMenuSelectList(String.Empty);
            model.Users = selectListService.GetCmsUserSelectList(null, "ed_persons_allowed");
            model.CategoryList = selectListService.CategorySelectList<PersonCategory>(null, userId);
            model.AllowedUsers = entry.AllowedUsers != null ? entry.AllowedUsers.Select(x => new AllowedUser() { UserId = x.Id, ItemId = entry.Id, Name = x.Name, Email = x.Email }).ToList() : null;
            model.Categories = entry.Categories
                .Select(x => new { Id = x.CategoryId, TitleRu = x.Category.TitleRu, TitleUk = x.Category.TitleUk, Position = x.Position })
                .Select(x => new PersonCategoryItem() { CategoryId = x.Id, PersonId = entry.Id, Title = x.GetLocalValue("Title"), Position = x.Position })
                .ToList();
                               
            return model;
        }

        public void Save(PersonEdit model, Int32? userId = null)
        {
            var person = personRepo.GetById(model.Id);

            model.TitleUk = String.IsNullOrEmpty(model.TitleUk) ? "Без назви" : model.TitleUk;
            model.TitleRu = String.IsNullOrEmpty(model.TitleRu) ? "Без названия" : model.TitleRu;
            model.TitleEn = String.IsNullOrEmpty(model.TitleEn) ? "No name" : model.TitleEn;

            var texts = contentAdminService.GetContentText(person.ContentRows);
            var textRu = model.TitleRu + " " + model.DescriptionRu + " " + texts[0];
            var textUk = model.TitleUk + " " + model.DescriptionUk + " " + texts[1];
            var textEn = model.TitleEn + " " + model.DescriptionEn + " " + texts[2];

            person.Id = model.Id;
            person.TitleRu = model.TitleRu;
            person.TitleUk = model.TitleUk;
            person.TitleEn = model.TitleEn;
            person.DescriptionRu = model.DescriptionRu;
            person.DescriptionUk = model.DescriptionUk;
            person.DescriptionEn = model.DescriptionEn;
            person.PostRu = model.PostRu;
            person.PostUk = model.PostUk;
            person.PostEn = model.PostEn;
            person.ProfessionRu = model.ProfessionRu;
            person.ProfessionUk = model.ProfessionUk;
            person.ProfessionEn = model.ProfessionEn;
            person.ContactAddressRu = model.ContactAddressRu;
            person.ContactAddressUk = model.ContactAddressUk;
            person.ContactAddressEn = model.ContactAddressEn;
            person.ContactEmailsUk = model.ContactEmailsUk;
            person.ContactEmailsRu = model.ContactEmailsRu;
            person.ContactEmailsEn = model.ContactEmailsEn;
            person.ContactPhonesUk = model.ContactPhonesUk;
            person.ContactPhonesRu = model.ContactPhonesRu;
            person.ContactPhonesEn = model.ContactPhonesEn;
            person.ReceptionAddressRu = model.ReceptionAddressRu;
            person.ReceptionAddressUk = model.ReceptionAddressUk;
            person.ReceptionAddressEn = model.ReceptionAddressEn;
            person.ReceptionDatesRu = model.ReceptionDatesRu;
            person.ReceptionDatesUk = model.ReceptionDatesUk;
            person.ReceptionDatesEn = model.ReceptionDatesEn;
            person.ReceptionTimeRu = model.ReceptionTimeRu;
            person.ReceptionTimeUk = model.ReceptionTimeUk;
            person.ReceptionTimeEn = model.ReceptionTimeEn;
            person.ShowSecondPage = model.ShowSecondPage;
            person.SecondPageTitleEn = model.SecondPageTitleEn;
            person.SecondPageTitleRu = model.SecondPageTitleRu;
            person.SecondPageTitleUk = model.SecondPageTitleUk;
            person.SecondPageTextEn = model.SecondPageTextEn;
            person.SecondPageTextRu = model.SecondPageTextRu;
            person.SecondPageTextUk = model.SecondPageTextUk;
            person.UrlNameRu = model.TitleRu.Translit();
            person.UrlNameUk = model.TitleUk.Translit();
            person.UrlNameEn = (model.TitleEn ?? String.Empty).ToLower().Replace(" ", "-");
            person.MetaTitleRu = model.MetaTitleRu;
            person.MetaTitleUk = model.MetaTitleUk;
            person.MetaTitleEn = model.MetaTitleEn;
            person.MetaKeywordsRu = model.MetaKeywordsRu;
            person.MetaKeywordsUk = model.MetaKeywordsUk;
            person.MetaKeywordsEn = model.MetaKeywordsEn;
            person.MetaDescriptionRu = model.MetaDescriptionRu;
            person.MetaDescriptionUk = model.MetaDescriptionUk;
            person.MetaDescriptionEn = model.MetaDescriptionEn;
            person.Image = model.Image;
            person.NotificationEmail = model.NotificationEmail;
            person.NotificationPhone = model.NotificationPhone;
            person.Facebook = model.Facebook;
            person.Twitter = model.Twitter;
            person.FbLink = model.FbLink;
            person.TwLink = model.TwLink;
            person.GpLink = model.GpLink;
            person.YtLink = model.YtLink;
            person.IgLink = model.IgLink;            
            person.Birthday = DateTimeHelper.ParseDateNullable(model.Birthday, null);
            person.HasContactForm = model.HasContactForm;
            person.HasReceptionForm = model.HasReceptionForm;
            person.SidebarMenuId = model.SidebarMenuId;
            person.Saved = true;
            person.Published = model.Published;
            person.ShowPublihDate = model.ShowPublihDate;
            person.ShowEditDate = model.ShowEditDate;
            person.LastEditDate = DateTime.Now;
            person.EditedDate = DateTimeHelper.ParseDateNullable(model.EditedDate, DateTime.Now, DateTimeHelper.DefaultTimeOfDay.None);
            person.PublishDate = DateTimeHelper.ParseDateNullable(model.PublishDate);
            person.LastEditUserId = (HttpContext.Current.User as CmsPrincipal).Identity.UserId;            

            personRepo.Update(person);
            UnitOfWork.Commit();

            cmsSearchService.Save(person.Id, textUk, textRu, textEn, person.PublishDate, CmsSearchType.Person, person.Published);
        }


        public void Delete(Int32 Id)
        {
            var person = personRepo.GetById(Id);

            person.Deleted = true;

            personRepo.Update(person);
            cmsSearchService.Delete(Id, CmsSearchType.Person);

            UnitOfWork.Commit();
        }

        public void Restore(Int32 Id)
        {
            var person = personRepo.GetById(Id);

            person.Deleted = false;

            personRepo.Update(person);
            cmsSearchService.Restore(Id, CmsSearchType.Person);

            UnitOfWork.Commit();
        }

        public void Remove(Int32 Id)
        {
            var person = personRepo.GetById(Id);

            personRepo.Delete(person);
            cmsSearchService.Remove(Id, CmsSearchType.Person);

            UnitOfWork.Commit();
        }

        public PersonCategoryItem GetCategory(Int32 CategoryId, Int32 PersonId)
        {
            var org = personRepo.GetById(PersonId);

            if (org != null && org.Categories != null && org.Categories.Any(x => x.CategoryId == CategoryId))
            {
                var personCategory = org.Categories.FirstOrDefault(x => x.PersonId == PersonId && x.CategoryId == CategoryId);
                var category = personCategoryRepo.GetById(CategoryId);

                if (category != null)
                {
                    var model = new PersonCategoryItem()
                    {
                        CategoryId = CategoryId,
                        PersonId = PersonId,
                        Title = category.GetLocalValue("Title"),
                        Position = personCategory.Position
                    };

                    return model;
                }
            }
            
            return null;
        }

        public PersonCategoryItem SaveCategory(PersonCategoryItem model)
        {
            var org = personRepo.GetById(model.PersonId);
            var cat = personCategoryRepo.GetById(model.CategoryId);

            if (org.Categories == null)
                org.Categories = new List<PersonPersonCategory>();
            
            if (cat != null)
            {
                var itemCategory = org.Categories.FirstOrDefault(x => x.PersonId == model.PersonId && x.CategoryId == model.CategoryId);

                if (itemCategory == null)
                {
                    var category = new PersonPersonCategory()
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
                
                personRepo.Update(org);
                UnitOfWork.Commit();

                model.Title = cat.GetLocalValue("Title");

                return model;
            }                
            

            return null;
        }

        public void DeleteCategory(Int32 CategoryId, Int32 PersonId)
        {
            var org = personRepo.GetById(PersonId);
            var category = org.Categories.FirstOrDefault(x => x.CategoryId == CategoryId);

            if (category != null)
            {
                org.Categories.Remove(category);
            }

            personRepo.Update(org);
            UnitOfWork.DefineAsDeleted<PersonPersonCategory, int>(category);
            UnitOfWork.Commit();
        }

        public AllowedUser AddUser(AllowedUser model)
        {
            var item = personRepo.GetById(model.ItemId);
            var user = userRepo.GetById(model.UserId);

            if (item.AllowedUsers == null)
                item.AllowedUsers = new List<CmsUser>();

            if (user != null)
            {
                var allowedUser = item.AllowedUsers.FirstOrDefault(x => x.Id == model.UserId);

                if (allowedUser == null)
                {
                    item.AllowedUsers.Add(user);

                    personRepo.Update(item);
                    UnitOfWork.Commit();

                    model.Email = user.Email;
                }

                

                return model;
            }

            return null;
        }

        public void DeleteUser(Int32 ItemId, Int32 UserId)
        {
            var item = personRepo.GetById(ItemId);
            var user = item.AllowedUsers.FirstOrDefault(x => x.Id == ItemId);

            if (user != null)
            {
                item.AllowedUsers.Remove(user);
            }

            personRepo.Update(item);
            UnitOfWork.Commit();
        }
    }
}