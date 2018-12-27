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
using System.Web;

namespace Bissoft.CouncilCMS.BLL.Services
{
    public class CmsSettingsAdminService : BaseService
    {
        private IRepository<CmsSettings, int> coreSettingsRepo;

        public CmsSettingsAdminService(string ConnectionString) : base(ConnectionString)
        {
            Initialize();
        }

        public CmsSettingsAdminService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            Initialize();
        }

        private void Initialize()
        {
            coreSettingsRepo = UnitOfWork.GetRepository<CmsSettings, int>();
        }

        public CmsSettingsEdit GetForEdit()
        {
        
            var item = coreSettingsRepo.GetList().FirstOrDefault() ?? new CmsSettings();

            var model = Mapper.Map<CmsSettingsEdit>(item);

            return model;
        }

        public Int32 Save(CmsSettingsEdit item)
        {
            var itemToSave = coreSettingsRepo.GetList().FirstOrDefault() ?? new CmsSettings();

            Mapper.Map<CmsSettingsEdit, CmsSettings>(item, itemToSave);
            
            itemToSave.DateLastEdit = DateTime.Now;

            if (itemToSave.Id > 0)
                coreSettingsRepo.Update(itemToSave);
            else
                coreSettingsRepo.Insert(itemToSave);
            
            UnitOfWork.Commit();

            HttpContext.Current.Application["CmsSettings"] = Mapper.Map<CmsAppSettings>(itemToSave);

            return itemToSave.Id;
        }
    }
}
