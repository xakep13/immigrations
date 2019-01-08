using Bissoft.CouncilCMS.BLL.ViewModels;
using Bissoft.CouncilCMS.Core;
using Bissoft.CouncilCMS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bissoft.CouncilCMS.BLL.Services
{
	public class DamagedHousingCategoryAdminService : BaseCategoryAdminService<DamagedHousingCategory, DamagedHousingCategoryEdit , DamagedHousingCategoryTemplate>
	{
		public DamagedHousingCategoryAdminService(string ConnectionString) : base(ConnectionString, "DamagedHousingCategories", "cr_damagedhousing_allowed") { }
		public DamagedHousingCategoryAdminService(IUnitOfWork unitOfWork) : base(unitOfWork, "DamagedHousingCategories", "cr_damagedhousing_allowed") { }
	}
}
