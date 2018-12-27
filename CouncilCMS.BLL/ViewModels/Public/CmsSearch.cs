using System;
using System.Collections.Generic;
using Bissoft.CouncilCMS.Core.Enums;
using Bissoft.CouncilCMS.Core.Globalization;
namespace Bissoft.CouncilCMS.BLL.ViewModels
{
    public class CmsSearchList
    {
        public int Page { get; set; }
        public int Count { get; set; }
        public string Query { get; set; }
        public List<CmsSearchItem> Results { get; set; }
    } 
}
