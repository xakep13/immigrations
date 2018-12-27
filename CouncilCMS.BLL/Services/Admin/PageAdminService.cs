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
    public class PageAdminService : BaseService
    {
        private ContentAdminService contentAdminService;
        private SelectListService selectListService;
        private IRepository<Page, int> pageRepo;
        private IRepository<CmsUser, int> userRepo;
        private CmsSearchAdminService cmsSearchService;

        public PageAdminService(string ConnectionString) : base(ConnectionString)
        {
            Initialize();
        }

        public PageAdminService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            Initialize();
        }

        private void Initialize()
        {
            pageRepo = UnitOfWork.GetIntRepository<Page>();
            userRepo = UnitOfWork.GetIntRepository<CmsUser>();
            selectListService = new SelectListService(this.UnitOfWork);
            contentAdminService = new ContentAdminService(this.UnitOfWork);
            cmsSearchService = new CmsSearchAdminService(this.UnitOfWork);
        }

        public PageList List(String query = null, string dateRange = null, int dateType = 0, CmsItemState itemState = CmsItemState.All, Int32 page = 1, string sortBy = "CreateDate", int sortDir = 1, int? user = null, int userAction = 0)
        {
            var model = new PageList()
            {
                Page = page,
                Query = query,
                DateRange = dateRange,
                DateType = dateType,
                ItemState = (int)itemState,
                SortBy = sortBy,
                SortDirection = sortDir,
                User = user,
                UserAction = userAction,
                Users = selectListService.GetCmsUserSelectList(String.Empty)
            };

            return model;
        }

        public List<PageListItem> GetList(out int count, string query = null, string dateRange = null, int dateType = 0, CmsItemState itemState = CmsItemState.All, Int32 page = 1, Int32 perPage = 20, string sortBy = "CreateDate", int sortDir = 1, int? user = null, int userAction = 0, Int32? userId = null)
        {
            var predicate = CreateStatePredicate<Page>(itemState);

            if (!String.IsNullOrEmpty(query))
                predicate = TitleFilter(predicate, query);

            if (!String.IsNullOrEmpty(dateRange))
                predicate = DateRangeFilter(predicate, dateRange, dateType);

            if (user > 0)
                predicate = UserFilter(predicate, user, userAction);

            if (userId > 0)
                predicate = predicate.And(x => x.AllowedUsers.Any(u => u.Id == userId));

            var list = pageRepo.GetList(out count, predicate, x => x.OrderByColumn(sortBy, sortDir > 0), "CreateUser,LastEditUser", (page - 1) * perPage, perPage, true, false);
          
            var model = list.Select(x => new PageListItem()
                {
                    Id = x.Id,
                    Url = x.UrlName,
                    TitleUk = x.TitleUk,
                    TitleRu = x.TitleRu,
                    TitleEn = x.TitleEn,
                    CreateDate = x.CreateDate,
                    LastEditDate = x.LastEditDate,
                    PublishDate = x.PublishDate,
                    EditedDate = x.EditedDate,
                    Deleted = x.Deleted,
                    Published = x.Published,
                    ViewCount = x.ViewCount,
                    CreateUser = x.CreateUserId > 0 ? x.CreateUser.Name : null,
                    LastEditUser = x.LastEditUserId > 0 ? x.LastEditUser.Name : null
                }).ToList();

            return model;
        }

        public PageEdit GetForEdit(Int32 id, Int32? userId = null)
        {
            var entry = pageRepo.GetById(id);

            if (id == 0)
            {
                entry = new Page()
                {
                    Id = 0,
                    TitleRu = "Без названия",
                    TitleUk = "Без назви",
                    TitleEn = "No name",
                    CreateDate = DateTime.Now,
                    PublishDate = DateTime.Now,
                    AllowedUsers = new List<CmsUser>(),
                    ShowPublihDate = true,
                    ShowEditDate = false,
                    Published = true,
                    CreateUserId = (HttpContext.Current.User as CmsPrincipal).Identity.UserId
                };

                pageRepo.Insert(entry);
                UnitOfWork.Commit();

                entry.TitleUk = null;
                entry.TitleRu = null;
                entry.TitleEn = null;
            }

            var model = new PageEdit() { ContentRows = new List<ContentRowItem>() };

            model.Id = entry.Id;
            model.UrlName = entry.UrlName;
            model.TitleRu = entry.TitleRu;
            model.TitleUk = entry.TitleUk;
            model.TitleEn = entry.TitleEn;
            model.DescriptionRu = entry.DescriptionRu;
            model.DescriptionUk = entry.DescriptionUk;
            model.DescriptionEn = entry.DescriptionEn;
            model.Published = entry.Published;
            model.ShowPublihDate = entry.ShowPublihDate;
            model.ShowEditDate = entry.ShowEditDate;
            model.PageTemplateId = entry.PageTemplateId ?? 1;
            model.Type = entry.Type;            
            model.PageTemplates = selectListService.GetPageTemplateSelectList();
            model.SidebarMenus = selectListService.GetMenuSelectList(String.Empty);
            model.SidebarMenuId = entry.SidebarMenuId;
            model.ContentRows = contentAdminService.GetContentRowListItems(entry.ContentRows, model.Id);
            model.Users = selectListService.GetCmsUserSelectList(null, "ed_pages_allowed");
            model.EditedDate = null;
            model.PublishDate = DateTimeHelper.NullableDateTimeString(entry.PublishDate, Format: "dd.MM.yyyy HH:mm");
            model.AllowedUsers = entry.AllowedUsers != null ? entry.AllowedUsers.Select(x => new AllowedUser() { UserId = x.Id, ItemId = entry.Id, Name = x.Name, Email = x.Email }).ToList() : null;

            model.MetaTitleRu = entry.MetaTitleRu;
            model.MetaTitleUk = entry.MetaTitleUk;
            model.MetaTitleEn = entry.MetaTitleEn;
            model.MetaKeywordsRu = entry.MetaKeywordsRu;
            model.MetaKeywordsUk = entry.MetaKeywordsUk;
            model.MetaKeywordsEn = entry.MetaKeywordsEn;
            model.MetaDescriptionRu = entry.MetaDescriptionRu;
            model.MetaDescriptionUk = entry.MetaDescriptionUk;
            model.MetaDescriptionEn = entry.MetaDescriptionEn;

            if (model.AllowedUsers != null && model.AllowedUsers.Count > 0)
            {
                foreach (var user in model.AllowedUsers)
                {
                    var userToRemove = model.Users.Where(x => x.Value == user.UserId.ToString()).FirstOrDefault();

                    if (userToRemove != null)
                        model.Users.Remove(userToRemove);
                }
            }

            return model;
        }
        
        public PageEdit Save(PageEdit model, Int32? userId = null)
        {
            var entry = pageRepo.GetById(model.Id);

            model.TitleUk = String.IsNullOrEmpty(model.TitleUk) ? "Без назви" : model.TitleUk;
            model.TitleRu = String.IsNullOrEmpty(model.TitleRu) ? "Без названия" : model.TitleRu;
            model.TitleEn = String.IsNullOrEmpty(model.TitleEn) ? "No name" : model.TitleEn;

            var texts = contentAdminService.GetContentText(entry.ContentRows);
            var textRu = model.TitleRu + " " + model.DescriptionRu + " " + texts[0];
            var textUk = model.TitleUk + " " + model.DescriptionUk + " " + texts[1];
            var textEn = model.TitleEn + " " + model.DescriptionEn + " " + texts[2];

            entry.Id = model.Id;
            entry.UrlName = GetUrlName(model.Id, model.UrlName, model.TitleUk);
            entry.TitleRu = model.TitleRu;
            entry.TitleUk = model.TitleUk;
            entry.TitleEn = model.TitleEn;
            entry.DescriptionRu = model.DescriptionRu;
            entry.DescriptionUk = model.DescriptionUk;
            entry.DescriptionEn = model.DescriptionEn;
            entry.Type = model.Type;                        
            entry.Published = model.Published;
            entry.ShowEditDate = model.ShowEditDate;
            entry.ShowPublihDate = model.ShowPublihDate;
            
            entry.EditedDate = DateTimeHelper.ParseDateNullable(model.EditedDate, DateTime.Now, DateTimeHelper.DefaultTimeOfDay.None);
            entry.PublishDate = DateTimeHelper.ParseDateNullable(model.PublishDate, null, DateTimeHelper.DefaultTimeOfDay.None);
            entry.PageTemplateId = model.PageTemplateId;
            entry.SidebarMenuId = model.SidebarMenuId;

            entry.MetaTitleRu = model.MetaTitleRu;
            entry.MetaTitleUk = model.MetaTitleUk;
            entry.MetaTitleEn = model.MetaTitleEn;
            entry.MetaKeywordsRu = model.MetaKeywordsRu;
            entry.MetaKeywordsUk = model.MetaKeywordsUk;
            entry.MetaKeywordsEn = model.MetaKeywordsEn;
            entry.MetaDescriptionRu = model.MetaDescriptionRu;
            entry.MetaDescriptionUk = model.MetaDescriptionUk;
            entry.MetaDescriptionEn = model.MetaDescriptionEn;

            entry.LastEditUserId = (HttpContext.Current.User as CmsPrincipal).Identity.UserId;
            entry.LastEditDate = DateTime.Now;

            pageRepo.Update(entry);
            UnitOfWork.Commit();

            cmsSearchService.Save(entry.Id, textUk, textRu, textEn, entry.CreateDate, CmsSearchType.Page, entry.Published);

            return model;
        }
        
        public void Delete(Int32 Id)
        {
            var page = pageRepo.GetById(Id);

            page.Deleted = true;
            pageRepo.Update(page);
            cmsSearchService.Delete(Id, CmsSearchType.Page);

            UnitOfWork.Commit();
        }

        public void Restore(Int32 Id)
        {
            var page = pageRepo.GetById(Id);

            page.Deleted = false;
            pageRepo.Update(page);
            cmsSearchService.Restore(Id, CmsSearchType.Page);

            UnitOfWork.Commit();
        }

        public void Remove(Int32 Id)
        {
            var page = pageRepo.GetById(Id);

            if (page != null)
            {
                cmsSearchService.Remove(Id, CmsSearchType.Page);
                pageRepo.Delete(page);                
                UnitOfWork.Commit();
            }
        }

        private string GetUrlName(Int32 id, String urlName, String Title = null)
        {
            var i = 1;
            var result = String.IsNullOrEmpty(urlName) ? Title.Translit() : urlName;

            while (pageRepo.GetSingle(x => x.UrlName == result && x.Id != id) != null)
            {
                result = urlName + "-" + i;
                i++;
            }

            return result;
        }


        public AllowedUser AddUser(AllowedUser model)
        {
            var item = pageRepo.GetById(model.ItemId);
            var user = userRepo.GetById(model.UserId);

            if (item.AllowedUsers == null)
                item.AllowedUsers = new List<CmsUser>();

            if (user != null)
            {
                var allowedUser = item.AllowedUsers.FirstOrDefault(x => x.Id == model.UserId);

                if (allowedUser == null)
                {                    
                    item.AllowedUsers.Add(user);

                    pageRepo.Update(item);
                    UnitOfWork.Commit();

                    model.Email = user.Email;

                    return model;
                }                               
            }

            return null;
        }

        public void DeleteUser(Int32 ItemId, Int32 UserId)
        {
            var item = pageRepo.GetById(ItemId);
            var user = item.AllowedUsers.FirstOrDefault(x => x.Id == ItemId);

            if (user != null)
            {
                item.AllowedUsers.Remove(user);
            }

            pageRepo.Update(item);            
            UnitOfWork.Commit();
        }
    }
}
