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
    public class DocTypeAdminService : BaseService
    {
        private IRepository<DocType, int> docTypeRepo;

        public DocTypeAdminService(string ConnectionString) : base(ConnectionString)
        {
            Initialize();
        }

        public DocTypeAdminService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            Initialize();
        }

        private void Initialize()
        {
            docTypeRepo = UnitOfWork.GetRepository<DocType, int>();
        }
        public List<DocTypeListItem> GetList()
        {
            var list = docTypeRepo.GetList(notDeleted: false, orderBy: x => x.OrderBy(o => o.Position)).ToList();

            var model = list.Select(x => new DocTypeListItem()
            {
                Id = x.Id,
                Title = x.GetLocalValue("Title"),
                Position = x.Position
            }).ToList();

            return model;
        }
        public DocTypeEdit GetEdit(Int32 Id)
        {
            var item = docTypeRepo.GetById(Id) ?? new DocType() { Id = 0 };
            DocTypeEdit model = new DocTypeEdit();

            model.Id = item.Id;
            model.TitleUk = item.TitleUk;
            model.TitleRu = item.TitleRu;
            model.TitleEn = item.TitleEn;

            return model;
        }
        public DocTypeListItem SaveItem(DocTypeEdit model)
        {
            var item = docTypeRepo.GetById(model.Id) ?? new DocType();

            item.Id = model.Id;
          
            item.TitleRu = model.TitleRu;
            item.TitleUk = model.TitleUk;
            item.TitleEn = model.TitleEn;

            item.Position = model.Id > 0 ? item.Position : GetMaxPosition() + 1;

            
            if (item.Id > 0)
                docTypeRepo.Update(item);
            else
                docTypeRepo.Insert(item);

            UnitOfWork.Commit();

            var result = new DocTypeListItem()
            {
                Id = item.Id,
                Position = item.Position,
                Title = item.GetLocalValue("Title")
            };


            return result;
        }

        public void Delete(int Id)
        {
            var item = docTypeRepo.GetById(Id);

            item.Deleted = true;
            item.Docs = new List<Doc>();

            docTypeRepo.Update(item);

            UnitOfWork.Commit();
        }

        private Int32 GetMaxPosition()
        {
            var max = 0;
            try
            {
               max = docTypeRepo.DbSet.Select(x => x.Position).DefaultIfEmpty(0).Max();
            }
            catch { }
            
            return max;
        } 

        public void UpdatePosition(Int32 id, int? parentId, Int32 oldPosition, Int32 newPosition)
        {
            var result = 0;

            if (oldPosition > newPosition)
            {
                result = docTypeRepo
                    .Context
                    .ExecuteSqlCommand("UPDATE DocTypes SET Position = Position + 1 WHERE Position >= {0} AND Position < {1}", false, null, newPosition, oldPosition);
            }
            else if (oldPosition < newPosition)
            {
                result = docTypeRepo
                   .Context
                   .ExecuteSqlCommand("UPDATE DocTypes SET Position = Position - 1 WHERE Position <= {0} AND Position > {1}", false, null, newPosition, oldPosition);
            }

            result = docTypeRepo
                   .Context
                   .ExecuteSqlCommand("UPDATE DocTypes SET Position = {0} WHERE Id = {1}", false, null, newPosition, id);
        }
    }
}
