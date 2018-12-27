using System;
using System.Collections.Generic;
using Bissoft.CouncilCMS.Core.Enums;
using Bissoft.CouncilCMS.Core.Globalization;

namespace Bissoft.CouncilCMS.BLL.ViewModels
{
    public class CmsIndex
    {
        public List<CmsArticleListItem> MainNews { get; set; }
        public List<CmsArticleListItem> MainNewsSecond { get; set; }
        public List<CmsArticleListItem> LastNews { get; set; }
        public List<CmsArticleListItem> BillboardNews { get; set; }
        public List<CmsArticleListItem> PhotoreportNews { get; set; }
        public List<CmsArticleListItem> VideoreportNews { get; set; }
        public List<CmsDocListItem> Documents { get; set; }
        public CmsPersonListItem Major { get; set; }
        public CmsMenu ThemeMenu { get; set; }
        public CmsMenu ServiceMenu { get; set; }
        public CmsBanner Banner { get; set; }
    }
}
