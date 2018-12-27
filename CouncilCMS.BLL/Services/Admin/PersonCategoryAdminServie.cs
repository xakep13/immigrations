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
    public class PersonCategoryAdminService : BaseCategoryAdminService<PersonCategory, PersonCategoryEdit, PersonCategoryTemplate>
    {
        public PersonCategoryAdminService(string ConnectionString) : base(ConnectionString, "PersonCategories", "cr_persons_allowed") { }
        public PersonCategoryAdminService(IUnitOfWork unitOfWork) : base(unitOfWork, "PersonCategories", "cr_persons_allowed") { }
    }
}
