using System.Collections.Generic;
using System.Linq;
using Bissoft.CouncilCMS.Core;
using Bissoft.CouncilCMS.DAL.Entities;
using Bissoft.CouncilCMS.BLL.ViewModels;
using xTab.Tools.Helpers;


namespace Bissoft.CouncilCMS.BLL.Services
{
    public class CmsBannerService : BaseService
    {
        private IRepository<Banner, int> bannerRepo;
        public CmsBannerService(string ConnectionString) : base(ConnectionString)
        {
            Initialize();
        }

        public CmsBannerService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            Initialize();
        }

        private void Initialize()
        {
            bannerRepo = UnitOfWork.GetIntRepository<Banner>();
        }

        public List<CmsBanner> Banners()
        {
            var model = bannerRepo
                .GetList(x => x.Published, x => x.OrderBy(o => o.Position)).ToList()
                .Select(x => new CmsBanner() { Id = x.Id, Text = x.GetLocalValue("Text"), Url = x.Url, Image = x.Image })
                .ToList();
            
            return model;
        }
    }
}
