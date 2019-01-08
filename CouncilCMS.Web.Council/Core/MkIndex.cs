using Bissoft.CouncilCMS.BLL.ViewModels.Public;
using Bissoft.CouncilCMS.Core.Globalization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bissoft.CouncilCMS.BLL.ViewModels
{
    public class MkIndex
    {
        public List<CmsArticleListItem> MainNews { get; set; }
        public List<CmsArticleListItem> MainNewsSecond { get; set; }
        public List<CmsArticleListItem> GeneralNews { get; set; }
		public List<CmsArticleListItem> ImmigrationsNews { get; set; }
		public List<CmsArticleListItem> FamaliesNews { get; set; }
		public List<CmsArticleListItem> RestoreObjects { get; set; }
		public List<CmsArticleListItem> SlidersNews { get; set; }
    }
}