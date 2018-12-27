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
using AutoMapper;

namespace Bissoft.CouncilCMS.BLL.Services
{
    public abstract class BaseCategoryAdminService<TCategory, TCategoryEdit, TTemplate> : BaseService 
        where TCategory : BaseCategoryEntity, new()
        where TCategoryEdit : BaseCategoryEdit, new()
        where TTemplate : BaseCategoryTemplate, new()
    {
        private IRepository<TCategory, int> categoryRepo;
        private IRepository<TTemplate, int> templateRepo;
        private IRepository<CmsUser, int> userRepo;
        private IRepository<CmsRole, int> roleRepo;
        private SelectListService selectListService;
        private string type;
        private string allowedPremissionName;

        public BaseCategoryAdminService(string ConnectionString, string Type, string AllowedPremissionName) : base(ConnectionString)
        {
            Initialize(Type, AllowedPremissionName);            
        }

        public BaseCategoryAdminService(IUnitOfWork unitOfWork, string Type, string AllowedPremissionName) : base(unitOfWork)
        {
            Initialize(Type, AllowedPremissionName);            
        }

        private void Initialize(string Type, string AllowedPremissionName)
        {
            categoryRepo = UnitOfWork.GetRepository<TCategory, int>();
            userRepo = UnitOfWork.GetRepository<CmsUser, int>();
            roleRepo = UnitOfWork.GetRepository<CmsRole, int>();
            selectListService = new SelectListService(this.UnitOfWork);

            this.type = Type;
            this.allowedPremissionName = AllowedPremissionName;
        }
        public List<CategoryListItem> GetList(int? userId = null)
        {
            var user = userId > 0 ? userRepo.GetById(userId.Value) : null;
            var сategoryList = categoryRepo.GetList(includeProperties:"AllowedRoles", notDeleted: false).Where(x => x.ParentCategoryId != null).ToList();
            var categoryItems = categoryRepo.GetList(includeProperties: "AllowedRoles", notDeleted: false).Where(x => x.ParentCategoryId == null).OrderBy(x => x.Position)
                        .Select(x => FillMenuItem(x, сategoryList, user))
                        .ToList();
                      
            return categoryItems;
        }
        public TCategoryEdit GetCategoryEdit(Int32 Id, Int32? parentId = null)
        {
            var category = (TCategory)categoryRepo.GetById(Id) ?? new TCategory() { Id = 0 , ParentCategoryId = parentId };
            var model = Mapper.Map<TCategoryEdit>(category);

            

            model.RelatedCategories = selectListService.CategorySelectList<TCategory>(String.Empty);
            model.SidebarMenus = selectListService.GetMenuSelectList(String.Empty);
            model.Templates = selectListService.CategoryTemplateSelectList<TTemplate>();
            model.Users = selectListService.GetCmsUserSelectList(null, this.allowedPremissionName);
            model.Roles = selectListService.GetCmsRoleSelectList(null, this.allowedPremissionName);
            model.AllowedUsers = category.AllowedUsers != null ? category.AllowedUsers.Select(x => new AllowedUser() { UserId = x.Id, ItemId = category.Id, Name = x.Name, Email = x.Email }).ToList() : null;
            model.AllowedRoles = category.AllowedRoles != null ? category.AllowedRoles.Select(x => new AllowedRole() { RoleId = x.Id, ItemId = category.Id, Name = x.TitleUk }).ToList() : null;

            return model;
        }
        public CategoryListItem SaveItem(TCategoryEdit model)
        {
            var category = categoryRepo.GetById(model.Id) ?? new TCategory();

            model.TitleUk = String.IsNullOrEmpty(model.TitleUk) ? "Без назви" : model.TitleUk;
            model.TitleRu = String.IsNullOrEmpty(model.TitleRu) ? "Без названия" : model.TitleRu;
            model.TitleEn = String.IsNullOrEmpty(model.TitleEn) ? "No name" : model.TitleEn;

            Mapper.Map<TCategoryEdit, TCategory>(model, category);

            category.Position = model.Id > 0 ? category.Position : GetMaxPosition(model.ParentCategoryId) + 1;
            category.UrlName = String.IsNullOrEmpty(model.UrlName) ? model.TitleUk.Translit() : model.UrlName;

            if (category.Id > 0)
                categoryRepo.Update(category);
            else
                categoryRepo.Insert(category);

            UnitOfWork.Commit();

            var result = FillMenuItem(category, new List<TCategory>());

            result.HasChilds = categoryRepo.DbSet.Any(x => x.ParentCategoryId == category.Id);

            return result;
        }

        public void Delete(int Id)
        {
            var category = categoryRepo.GetById(Id);
            category.Deleted = true;           
            categoryRepo.Update(category);
            UnitOfWork.Commit();
        }

        public void Remove(int Id)
        {
            var category = categoryRepo.GetById(Id);
            categoryRepo.Delete(category);
            UnitOfWork.Commit();
        }

        private Int32 GetMaxPosition(Int32? parentId)
        {
            var max = 0;
            try
            {
               max = categoryRepo.DbSet.Where(x => x.ParentCategoryId == parentId).Select(x => x.Position).DefaultIfEmpty(0).Max();
            }
            catch { }
            
            return max;
        }

        private CategoryListItem FillMenuItem(TCategory item, List<TCategory> list, CmsUser user = null)
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
                Type = type.ToLower(),
                ParentCategoryId = item.ParentCategoryId,                
                HasChilds = list.Count(x => x.ParentCategoryId == item.Id) > 0,
                Allowed = user != null ? item.AllowedRoles.Any(r => user.Roles.Select(x => x.Id).DefaultIfEmpty(0).Contains(r.Id)) : true,
                ChildCategoryItems = list.Count(x => x.ParentCategoryId == item.Id) > 0 ?
                    list.Where(x => x.ParentCategoryId == item.Id).OrderBy(x => x.Position).Select(x => FillMenuItem(x, list, user)).ToList() :
                    null
            };

            return model;
        }

        

        public void UpdatePosition(Int32 id, int? parentId, Int32 oldPosition, Int32 newPosition)
        {
            var result = 0;

            if (oldPosition > newPosition)
            {
                result = categoryRepo
                    .Context
                    .ExecuteSqlCommand("UPDATE " + this.type + " SET Position = Position + 1 WHERE Position >= {0} AND Position < {1} AND ParentCategoryId " + (parentId.HasValue ? (" = " + parentId) : "is null"), false, null, newPosition, oldPosition);
            }
            else if (oldPosition < newPosition)
            {
                result = categoryRepo
                   .Context
                   .ExecuteSqlCommand("UPDATE " + this.type + " SET Position = Position - 1 WHERE Position <= {0} AND Position > {1} AND ParentCategoryId " + (parentId.HasValue ? (" = " + parentId) : "is null"), false, null, newPosition, oldPosition);
            }

            result = categoryRepo
                   .Context
                   .ExecuteSqlCommand("UPDATE " + this.type + " SET Position = {0} WHERE Id = {1}", false, null, newPosition, id);
        }

        private string GetUrlName(Int32 id, String urlName, String title = null)
        {
            var i = 1;
            var result = String.IsNullOrEmpty(urlName) ? title.Translit() : urlName;

            while (categoryRepo.GetSingle(x => x.UrlName == result && x.Id != id) != null)
            {
                result = urlName + "-" + i;
                i++;
            }

            return result;
        }

        public AllowedUser AddUser(AllowedUser model)
        {
            var item = categoryRepo.GetById(model.ItemId);
            var user = userRepo.GetById(model.UserId);

            if (item.AllowedUsers == null)
                item.AllowedUsers = new List<CmsUser>();

            if (user != null)
            {
                var allowedUser = item.AllowedUsers.FirstOrDefault(x => x.Id == model.UserId);

                if (allowedUser == null)
                {
                    item.AllowedUsers.Add(user);

                    categoryRepo.Update(item);
                    UnitOfWork.Commit();

                    model.Email = user.Email;

                    return model;
                }                
            }

            return null;
        }

        public void DeleteUser(Int32 ItemId, Int32 UserId)
        {
            var item = categoryRepo.GetById(ItemId);
            var user = item.AllowedUsers.FirstOrDefault(x => x.Id == ItemId);

            if (user != null)            
                item.AllowedUsers.Remove(user);
            
            categoryRepo.Update(item);
            UnitOfWork.Commit();
        }

        public AllowedRole AddRole(AllowedRole model)
        {
            var item = categoryRepo.GetById(model.ItemId);
            var role = roleRepo.GetById(model.RoleId);

            if (item.AllowedRoles == null)
                item.AllowedRoles = new List<CmsRole>();

            if (role != null)
            {
                var allowedRole = item.AllowedRoles.FirstOrDefault(x => x.Id == model.RoleId);

                if (allowedRole == null)
                {
                    item.AllowedRoles.Add(role);

                    categoryRepo.Update(item);
                    UnitOfWork.Commit();

                    model.Name = role.GetLocalValue("Title");

                    return model;
                }
            }

            return null;
        }

        public void DeleteRole(Int32 ItemId, Int32 RoleId)
        {
            var item = categoryRepo.GetById(ItemId);
            var role = item.AllowedRoles.FirstOrDefault(x => x.Id == ItemId);

            if (role != null)
            {
                item.AllowedRoles.Remove(role);
            }

            categoryRepo.Update(item);
            UnitOfWork.Commit();
        }
    }
}
