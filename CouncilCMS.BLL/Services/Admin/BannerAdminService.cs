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
    public class BannerAdminService : BaseService
    {
        private IRepository<Banner, int> bannerRepo;

        public BannerAdminService(string ConnectionString) : base(ConnectionString)
        {
            Initialize();
        }

        public BannerAdminService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            Initialize();
        }

        private void Initialize()
        {
            bannerRepo = UnitOfWork.GetIntRepository<Banner>();
        }

        public List<BannerListItem> GetList()
        {
            var model = bannerRepo.GetList(orderBy: x => x.OrderBy(o => o.Deleted).ThenByDescending(o => o.Position), notDeleted: false).ToList()
                        .Select(x => new BannerListItem()
                        {
                            Id = x.Id,
                            Text = x.GetLocalValue("Text"),
                            Url = x.Url,
                            Image = x.Image,
                            Published = x.Published,
                            Position = x.Position,
                        }).ToList();
           
           
            return model;
        }
        public BannerEdit GetForm(Int32 Id)
        {
            var item = bannerRepo.GetById(Id) ?? new Banner() {  };
            BannerEdit model = new BannerEdit();

            model.Id = item.Id;
            model.TextRu = item.TextRu;
            model.TextUk = item.TextUk;
            model.TextEn = item.TextEn;
            model.Url = item.Url;
            model.Image = item.Image;
            model.Link = item.Link;
            model.Published = item.Published;

            return model;
        }
        public BannerEdit SaveItem(BannerEdit model)
        {
            var item = bannerRepo.GetById(model.Id) ?? new Banner();

            item.TextRu = model.TextRu;
            item.TextUk = model.TextUk;
            item.TextEn = model.TextEn;
            item.Url = model.Url;
            item.Image = model.Image;
            item.Link = model.Link;
            item.Published = model.Published;
            item.Position = model.Id > 0 ? item.Position : GetMaxPosition() + 1;

            bannerRepo.InsertOrUpdate(item);
            UnitOfWork.Commit();

            return model;
        }

        public void Delete(int Id)
        {
            var item = bannerRepo.GetById(Id);

            item.Deleted = true;

            bannerRepo.Insert(item);

            UnitOfWork.Commit();
        }

        private Int32 GetMaxPosition()
        {
            var max = 0;

            try
            {
               max = bannerRepo.DbSet.Select(x => x.Position).DefaultIfEmpty(0).Max();
            }
            catch { }
            
            return max;
        }

     
        public void UpdatePosition(Int32 id, Int32 oldPosition, Int32 newPosition)
        {
            var result = 0;

            if (oldPosition > newPosition)
            {
                result = bannerRepo
                    .Context
                    .ExecuteSqlCommand("UPDATE Banners SET Position = Position + 1 WHERE Position >= {0} AND Position < {1}", false, null, newPosition, oldPosition);
            }
            else if (oldPosition < newPosition)
            {
                result = bannerRepo
                   .Context
                   .ExecuteSqlCommand("UPDATE Banners SET Position = Position - 1 WHERE Position <= {0} AND Position > {1}", false, null, newPosition, oldPosition);
            }

            result = bannerRepo
                   .Context
                   .ExecuteSqlCommand("UPDATE Banners SET Position = {0} WHERE Id = {1}", false, null, newPosition, id);
        }
    }
}
