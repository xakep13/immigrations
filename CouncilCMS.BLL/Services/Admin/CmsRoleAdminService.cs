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
using System.Web.Mvc;

namespace Bissoft.CouncilCMS.BLL.Services
{

    public class CmsRoleAdminService : BaseService
    {
        private IRepository<CmsRole, int> roleRepo;
        private IRepository<CmsPremission, int> premissionRepo;

        public CmsRoleAdminService(string ConnectionString) : base(ConnectionString)
        {
            Initialize();
        }

        public CmsRoleAdminService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            Initialize();
        }

        private void Initialize()
        {
            roleRepo = UnitOfWork.GetIntRepository<CmsRole>();
            premissionRepo = UnitOfWork.GetIntRepository<CmsPremission>();
        }

        public List<CmsRoleListItem> GetList()
        {
            var list = roleRepo.GetList().ToList();

            var model = list.Select(
                x => new CmsRoleListItem()
                {
                    Id = x.Id,
                    TitleUk = x.TitleUk,
                    TitleRu = x.TitleRu,
                    TitleEn = x.TitleEn,
                    Name = x.Name                   
                }).ToList();

            return model;
        }

        public CmsRoleEdit GetForm(Int32 Id)
        {
            var item = roleRepo.GetById(Id) ?? new CmsRole() { Premissions = String.Empty };
            var prems = premissionRepo.GetList(orderBy: x => x.OrderBy(o => o.Position)).ToList();
            var model = new CmsRoleEdit();

            model.Id = item.Id;
            model.TitleUk = item.TitleUk;
            model.TitleRu = item.TitleRu;
            model.TitleEn = item.TitleEn;           
            model.Name = item.Name;
            model.Premissions = 
                prems.Select(
                    x => new CheckedListItemStr()
                    {
                        Value = x.Name,
                        Name = x.GetLocalValue("Title"),
                        IsChecked = (item.Premissions.Contains(x.Name) || item.Premissions == "*")
                    }).ToList();           
            
            return model;
        }

        public CmsRoleListItem Save(CmsRoleEdit model)
        {
            var item = roleRepo.GetById(model.Id) ?? new CmsRole() { Premissions = String.Empty };

            item.Id = model.Id;
            item.TitleUk = model.TitleUk;
            item.TitleRu = model.TitleRu;
            item.TitleEn = model.TitleEn;
            item.Name = model.Name;            
            item.Premissions = model.Premissions.Count(x => !x.IsChecked) > 0 ? String.Join(";", model.Premissions.Where(x => x.IsChecked).Select(x => x.Value)) : "*";

            roleRepo.InsertOrUpdate(item);
            UnitOfWork.Commit();

            return new CmsRoleListItem()
            {
                Id = item.Id,
                Name = item.Name,
                TitleUk = item.TitleUk,
                TitleRu = item.TitleRu,
                TitleEn = item.TitleEn
            };
        }

        public void Delete(Int32 Id)
        {
            var team = roleRepo.GetById(Id);

            team.Deleted = true;

            roleRepo.Update(team);
            UnitOfWork.Commit();
        }

        public void Remove(Int32 Id)
        {
            var team = roleRepo.GetById(Id);
            
            roleRepo.Delete(team);

            UnitOfWork.Commit();
        }
    }
}
