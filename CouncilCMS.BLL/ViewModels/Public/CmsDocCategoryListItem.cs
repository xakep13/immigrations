using System;
using System.Collections.Generic;
using Bissoft.CouncilCMS.Core.Enums;
using Bissoft.CouncilCMS.Core.Globalization;
namespace Bissoft.CouncilCMS.BLL.ViewModels
{
    public class CmsDocCategoryListItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public int Count { get; set; }
        public int? ParentId { get; set; }     
    }    
}
