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

namespace Bissoft.CouncilCMS.BLL.Services
{
    public class CmsUserAdminService :BaseService
    {        
        private IRepository<CmsUser,int> userRepo;
        private IRepository<CmsRole,int> roleRepo;
        private SelectListService selectListService;
        public CmsUserAdminService(string ConnectionString) : base(ConnectionString)
        {
            Initialize();
        }

        public CmsUserAdminService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            Initialize();
        }

        private void Initialize()
        {
            userRepo = UnitOfWork.GetIntRepository<CmsUser>();
            roleRepo = UnitOfWork.GetIntRepository<CmsRole>();
            selectListService = new SelectListService(this.UnitOfWork);
        }

        public CmsUserList List(string query = null, CmsUserState userState = CmsUserState.All, string dateRange = null, int? role = null, int page = 1, string sortBy = "PublishDate", int sortDir = 1)
        {
            var model = new CmsUserList()
            {
                Page = page,
                Query = query,
                DateRange = dateRange,
                UserState = (int)userState,
                SortBy = sortBy,
                SortDirection = sortDir,
                Role = role,                
                Roles = selectListService.GetCmsRoleSelectList(String.Empty),                
            };

            return model;
        }
        public List<CmsUserListItem> GetList(out int count, string query = null, CmsUserState userState = CmsUserState.All, string dateRange = null, int? role = null, int page = 1, int perPage = 20, string sortBy = "PublishDate", int sortDir = 1)
        {
            var predicate = PredicateBuilder.True<CmsUser>();

            if (!String.IsNullOrEmpty(query))
                predicate = predicate.And(x => x.Name.Contains(query) || x.Email.Contains(query));
          
            if (role > 0)
                predicate = predicate.And(x => x.Roles.Any(c => c.Id == role));

            if (!String.IsNullOrEmpty(dateRange))
            {
                var dates = dateRange.Split(new string[] { " - " }, StringSplitOptions.RemoveEmptyEntries);

                if (dates.Count() == 2)
                {
                    var fromDate = DateTimeHelper.ParseDateNullable(dates[0]);
                    var toDate = DateTimeHelper.ParseDateNullable(dates[1]);

                    if (fromDate != null && toDate != null)
                    {
                        fromDate = fromDate.Value.Date;
                        toDate = toDate.Value.Date.AddDays(1).AddSeconds(-1);

                        predicate = predicate.And(x => x.RegisterDate >= fromDate && x.RegisterDate <= toDate);
                    }
                }
            }

            switch (userState)
            {
                case CmsUserState.Active:
                    predicate = predicate.And(x => !x.Blocked && !x.Deleted);
                    break;
                case CmsUserState.Blocked:
                    predicate = predicate.And(x => x.Blocked && !x.Deleted);
                    break;
                case CmsUserState.Deleted:
                    predicate = predicate.And(x => x.Deleted);
                    break;
            }

            var list = userRepo.GetList(out count, predicate, x => x.OrderByColumn(sortBy, sortDir > 0), "Roles", (page - 1) * perPage, perPage, true, false);

            var model = list.Select(x => new CmsUserListItem()
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                Mobile = x.Phone.ToString(),
                Blocked = x.Blocked,
                Deleted = x.Deleted,
                Roles = String.Join(" ", x.Roles.Select(r => r.TitleUk)),
                CreateDate = x.RegisterDate
            }).ToList();

            return model;
        }

        public CmsUserEdit GetForm(Int32 id)
        {
            var item = userRepo.GetById(id) ?? new CmsUser() { Roles = new List<CmsRole>() };

            var model = new CmsUserEdit()
            {
                Id = item.Id,
                Name = item.Name,
                Avatar = item.Avatar,
                Gender = (Gender?)item.Gender,
                Email = item.Email,
                Phone = item.Phone.HasValue ? item.Phone.Value.ToString() : null,
                ChangePasswordOnLogin = item.Id <= 0,
                Roles = item.Roles != null ? item.Roles.Select(x => x.Id).ToList() : null,
                RoleList = selectListService.GetCmsRoleSelectList()
            };

            if (item.AllowedArticleCategories != null && item.AllowedArticleCategories.Count > 0)
                model.AllowedArticleCategories = item.AllowedArticleCategories.Select(x => new CmsUserAllowedContent() { Id = x.Id, Title = x.TitleUk }).ToList();

            if (item.AllowedEnterpriseCategories != null && item.AllowedEnterpriseCategories.Count > 0)
                model.AllowedEnterpriseCategories = item.AllowedEnterpriseCategories.Select(x => new CmsUserAllowedContent() { Id = x.Id, Title = x.TitleUk }).ToList();

            if (item.AllowedPersonCategories != null && item.AllowedPersonCategories.Count > 0)
                model.AllowedPersonCategories = item.AllowedPersonCategories.Select(x => new CmsUserAllowedContent() { Id = x.Id, Title = x.TitleUk }).ToList();

            if (item.AllowedDocCategories != null && item.AllowedDocCategories.Count > 0)
                model.AllowedDocCategories = item.AllowedDocCategories.Select(x => new CmsUserAllowedContent() { Id = x.Id, Title = x.TitleUk }).ToList();

            if (item.AllowedPages != null && item.AllowedPages.Count > 0)
                model.AllowedPages = item.AllowedPages.Select(x => new CmsUserAllowedContent() { Id = x.Id, Title = x.TitleUk }).ToList();

            if (item.AllowedPersons != null && item.AllowedPersons.Count > 0)
                model.AllowedPersons = item.AllowedPersons.Select(x => new CmsUserAllowedContent() { Id = x.Id, Title = x.TitleUk }).ToList();

            if (item.AllowedEnterprises != null && item.AllowedEnterprises.Count > 0)
                model.AllowedEnterprises = item.AllowedEnterprises.Select(x => new CmsUserAllowedContent() { Id = x.Id, Title = x.TitleUk }).ToList();

            return model;

        }

        public RegisteredUser SaveUser(CmsUserEdit model)
        {

            var item =
                userRepo.GetById(model.Id) ??
                new CmsUser()
                {
                    Roles = new List<CmsRole>(),
                    RegisterDate = DateTime.Now,
                    ChangePasswordOnLogin = true,
                    Salt = GetRandomSalt(),
                };

            item.Name = model.Name;
            item.Email = model.Email;
            item.Phone = (model.Phone ?? String.Empty).RegexReplace(ObjectExtensions.RegExprs.NotDigits, String.Empty).ToNullableInt();
            item.Avatar = model.Avatar;
            item.Gender = (int?)model.Gender;
            
            var emailModel = new RegisteredUser()
            {
                Email = model.Email,
                Name = model.Name,
                RegisterDate = item.RegisterDate
            };

            if (model.Id == 0)
            {
                var tempPassword = RandomHelper.RandomString(6);
                var password = CryptoHelper.GetPasswordHash(model.Email, tempPassword, item.Salt);

                item.Password = password;
                emailModel.TempPassword = tempPassword;
            }

            if (item.Roles == null)
                item.Roles = new List<CmsRole>();

            if (model.Roles == null)
                model.Roles = new List<int>();

            foreach (var role in item.Roles.ToList())
            {
                if (!model.Roles.Any(x => x == role.Id))
                {
                    item.Roles.Remove(role);
                }
            }

            foreach (var role in model.Roles)
            {
                if (!item.Roles.Any(x => x.Id == role))
                {
                    var roleToAdd = roleRepo.GetById(role);
                    item.Roles.Add(roleToAdd);
                }
            }

            userRepo.InsertOrUpdate(item);
            UnitOfWork.Commit();
            
            return emailModel;
        }

        public RegisteredUser ResetPassword(int Id)
        {
            var item = userRepo.GetById(Id);

            if (item != null)
            {
                var tempPassword = RandomHelper.RandomString(6);

                var emailModel = new RegisteredUser()
                {
                    Email = item.Email,
                    Name = item.Name,
                    TempPassword = tempPassword,
                    RegisterDate = DateTime.Now
                };

                item.Password = CryptoHelper.GetPasswordHash(item.Email, tempPassword, item.Salt);
                item.ChangePasswordOnLogin = true;
                userRepo.Update(item);
                UnitOfWork.Commit();

                return emailModel;
            };

            return null;
        }

        public void Block(Int32 Id)
        {
            var user = userRepo.GetById(Id);

            user.Blocked = true;
            userRepo.Update(user);

            UnitOfWork.Commit();
        }

        public void Unblock(Int32 Id)
        {
            var user = userRepo.GetById(Id);

            user.Blocked = false;
            userRepo.Update(user);

            UnitOfWork.Commit();
        }

        public void Delete(Int32 Id)
        {
            var user = userRepo.GetById(Id);

            user.Deleted = true;
            userRepo.Update(user);

            UnitOfWork.Commit();
        }

        public void Restore(Int32 Id)
        {
            var user = userRepo.GetById(Id);

            user.Deleted = false;
            userRepo.Update(user);

            UnitOfWork.Commit();
        }

        public void Remove(Int32 Id)
        {            
            userRepo.Delete(Id);

            UnitOfWork.Commit();
        }

        public String GetRandomSalt()
        {
            var salt = RandomHelper.RandomString(10);

            while (userRepo.GetSingle(x => x.Salt == salt, null, true) != null)
            {
                salt = RandomHelper.RandomString(10);
            }

            return salt;
        }

        public bool EmailFree(int id, string email)
        {
            var user = userRepo.GetSingle(x => x.Id != id && x.Email == email, asNoTracking:true);

            return user == null;
        }

		public bool IsAdmin(int userId)
		{
			CmsUser user = userRepo.GetById(userId);
			if(roleRepo.GetSingle(x => x.Name == "admin").Users.Contains(user))
				return true;
			return false;
		}
    }
}
