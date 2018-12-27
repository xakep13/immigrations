using System.Linq;
using Bissoft.CouncilCMS.Core;
using Bissoft.CouncilCMS.DAL.Entities;
using Bissoft.CouncilCMS.BLL.ViewModels;
using AutoMapper;

namespace Bissoft.CouncilCMS.BLL.Services
{
    public class SettingsService : BaseService
    {      
        private IRepository<CmsSettings, int> cmsSettingsRepo;      
        public SettingsService(string ConnectionString) : base(ConnectionString) {  }
        public SettingsService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public CmsAppSettings GetCmsSettings()
        {
            cmsSettingsRepo = cmsSettingsRepo ?? UnitOfWork.GetRepository<CmsSettings, int>();

            var settings = cmsSettingsRepo.GetList(orderBy: x => x.OrderByDescending(y => y.Id)).FirstOrDefault() ?? new CmsSettings();

            var model = Mapper.Map<CmsAppSettings>(settings);

            return model;
        }
    }
}
