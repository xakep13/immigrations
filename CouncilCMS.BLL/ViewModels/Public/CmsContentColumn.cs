using System;
using System.Collections.Generic;
using Bissoft.CouncilCMS.Core.Enums;
using Bissoft.CouncilCMS.Core.Globalization;
namespace Bissoft.CouncilCMS.BLL.ViewModels
{
    public class CmsContentColumn
    {
        public int Position { get; set; }
        public string CssClass { get; set; }
        public string CssStyle { get; set; }  
        public List<CmsContent> Contents { get; set; }
    }
}
