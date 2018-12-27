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

namespace Bissoft.CouncilCMS.BLL.Services
{
    public class ArticleCategoryAdminService : BaseCategoryAdminService<ArticleCategory, ArticleCategoryEdit, ArticleCategoryTemplate>
    {       
        public ArticleCategoryAdminService(string ConnectionString) : base(ConnectionString, "ArticleCategories", "cr_articles_allowed") { }
        public ArticleCategoryAdminService(IUnitOfWork unitOfWork) : base(unitOfWork, "ArticleCategories", "cr_articles_allowed") { }        
    }
}
