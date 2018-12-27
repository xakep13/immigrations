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
using System.Linq.Expressions;

namespace Bissoft.CouncilCMS.BLL.Services
{
    public class DocCategoryAdminService : BaseCategoryAdminService<DocCategory, DocCategoryEdit, DocCategoryTemplate>
    {
        public DocCategoryAdminService(string ConnectionString) : 
            base(ConnectionString, "DocCategories", "cr_docs_allowed") { }
        public DocCategoryAdminService(IUnitOfWork unitOfWork) : 
            base(unitOfWork, "DocCategories", "cr_docs_allowed") { }

        private IRepository<DocCategory, int> docCatRepo;

        public void UpdatePositionNew(int parentId, int? menuId, int[] subItems)
        {
            docCatRepo = UnitOfWork.GetIntRepository<DocCategory>();
            

            Expression<Func<DocCategory, bool>> filter;
            if (parentId == 0)
            {
                filter = (x) => subItems.Contains(x.Id);


                var mainItems = docCatRepo.GetList(
                filter: filter,
                asNoTracking: false).ToList();

                for (int i = 0; i < subItems.Length; i++)
                {
                    var childItem = mainItems.First(mi => mi.Id == subItems[i]);
                    childItem.Position = i;
                }
            } 
            else
            {
                filter = (x) => x.Id == parentId;

                var parent = docCatRepo.GetList(
                filter: filter,
                asNoTracking: false).First();

                for (int i = 0; i < subItems.Length; i++)
                {
                    var childItem = parent.ChildCategories.First(mi => mi.Id == subItems[i]);
                    childItem.Position = i;
                }
            }

            docCatRepo.Context.SaveChanges();
        }
    }    
}
