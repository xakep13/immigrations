using System;
using System.Collections.Generic;
using Bissoft.CouncilCMS.Core.Enums;
using Bissoft.CouncilCMS.Core.Globalization;
namespace Bissoft.CouncilCMS.BLL.ViewModels
{
    public class CmsContentRow
    {
        public int Position { get; set; }
        public string CssClasses { get; set; }
        public string CssStyle { get; set; }
        public List<CmsContentColumn> Columns { get; set; }
    }
}
