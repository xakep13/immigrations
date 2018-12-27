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
    public class EnterpriseCategoryAdminService : BaseCategoryAdminService<EnterpriseCategory, EnterpriseCategoryEdit, EnterpriseCategoryTemplate>
    {
        public EnterpriseCategoryAdminService(string ConnectionString) : base(ConnectionString, "EnterpriseCategories", "cr_enterprises_allowed") { }
        public EnterpriseCategoryAdminService(IUnitOfWork unitOfWork) : base(unitOfWork, "EnterpriseCategories", "cr_enterprises_allowed") { }
    }    
}
