using System;
using System.Collections.Generic;
using Bissoft.CouncilCMS.Core.Enums;
using Bissoft.CouncilCMS.Core.Globalization;
namespace Bissoft.CouncilCMS.BLL.ViewModels
{
    public class CmsNewsletterList
    {
        public int Page { get; set; }
        public int Count { get; set; }
        public List<CmsNewsletter> Newsletters { get; set; }
    }
}
