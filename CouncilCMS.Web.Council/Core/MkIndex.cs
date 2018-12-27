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
        public List<CmsArticleListItem> MainNewsSecond { get; set; }
        public List<CmsArticleListItem> MainNews { get; set; }
        public List<CmsArticleListItem> AreaNews { get; set; }
		public List<CmsArticleListItem> Mayor { get; set; }
		public List<CmsPoll> Polls { get; set; }

        public CmsMenu ServiceMenu { get; set; }
        public CmsMenu ThemeMenu { get; set; }
        public CmsMenu CityMenu { get; set; }
        public CmsMenu AreaMenu { get; set; }
        public CmsMenu LinkMenu { get; set; }

        public bool EmailStatus { get; set; }
        public bool PhoneStatus { get; set; }
    }
}