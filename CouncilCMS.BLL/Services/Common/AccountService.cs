using System;
using System.Collections.Generic;
using System.Linq;
using Bissoft.CouncilCMS.Core;
using Bissoft.CouncilCMS.DAL.Entities;
using Bissoft.CouncilCMS.BLL.ViewModels;
using xTab.Tools.Helpers;

namespace Bissoft.CouncilCMS.BLL.Services
{
    public class AccountService : BaseService
    {
        private IRepository<CmsUser, int> userRepo;
        private IRepository<CmsRole, int> roleRepo;

        public AccountService(String connectionString) : base(connectionString)
        {
            Initialize();
        }

        public AccountService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            Initialize();
        }

        private void Initialize()
        {
            userRepo = UnitOfWork.GetIntRepository<CmsUser>();
            roleRepo = UnitOfWork.GetIntRepository<CmsRole>();
        }

        private AuthorizeUser GetUser(CmsUser user)
        {
            var model = new AuthorizeUser();
            var premissions = new List<String>();

            if (user != null)
            {
                foreach (var role in user.Roles)
                    premissions.AddRange(role.Premissions.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList());

                model.Id = user.Id;
                model.Email = user.Email;
                model.Name = user.Name;
                model.Roles = user.Roles.Select(x => x.Name).ToArray();
                model.Premissions = premissions.ToArray();
                model.Blocked = user.Blocked || user.Deleted;

                return model;
            }
            else
            {
                return null;
            }
        }

        public AuthorizeUser GetUser(Int32 Id)
        {
            var user = userRepo.GetById(Id);

            if (user != null)
            {
                return GetUser(user);
            }

            return null;
        }

        public AuthorizeUser GetUser(string email, string password)
        {
            var user = userRepo.GetSingle(x => x.Email == email, "Roles");

            if (user != null)
            {
                var checkPassword = CryptoHelper.GetPasswordHash(user.Email, password, user.Salt);

                if (user.Password == checkPassword)
                {
                    return GetUser(user);
                }
            }

            return null;
        }

        public Boolean IsBlocked(Int32 Id)
        {
            var blocked = userRepo.GetList(x => x.Id == Id && x.Blocked).Count() > 0;

            return blocked;
        }

        public String ChangePassword(String Email, String oldPassword, String newPassord)
        {
            var user = userRepo.GetSingle(x => x.Email == Email);
            var oldPass = CryptoHelper.GetPasswordHash(user.Email, oldPassword, user.Salt);

            if (user.Password == oldPass)
            {
                var newPass = CryptoHelper.GetPasswordHash(user.Email, newPassord, user.Salt);

                user.Password = newPass;
                user.ChangePasswordOnLogin = false;

                userRepo.Update(user);

                UnitOfWork.Commit();

                return newPassord;
            }

            return null;
        }

        public String ChangePassword(Int32 id, String oldPassword, String newPassord)
        {
            var user = userRepo.GetById(id);
            var oldPass = CryptoHelper.GetPasswordHash(user.Email, oldPassword, user.Salt);

            if (user.Password == oldPass)
            {
                var newPass = CryptoHelper.GetPasswordHash(user.Email, newPassord, user.Salt);

                user.Password = newPass;
                user.ChangePasswordOnLogin = false;

                userRepo.Update(user);

                UnitOfWork.Commit();

                return newPassord;
            }

            return null;
        }

        public string[] GetAllUsersRole(Int32 Id)
        {
            var RoleList2 = userRepo.GetSingle(x=>x.Id == Id, "Roles");

            var RoleList3 = RoleList2.Roles.Select(m => m.Name);
            var RoleList = RoleList3.ToArray();
            return RoleList;
        }
      
        public bool IsUserInRoles(string email, string RolesName)
        {
            var curUserRoles = userRepo.GetSingle(x => x.Email == email).Roles;
            if (curUserRoles.Count > 0)
            {
                string[] UserRoles = RolesName.Split(',');
                foreach (var userRole in UserRoles)
                {
                    foreach (var item in UserRoles)
                    {
                        if (userRole == item)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            else
            {
                return false;
            }
        }
    }
}
