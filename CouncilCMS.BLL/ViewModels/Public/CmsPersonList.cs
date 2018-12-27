using System;
using System.Collections.Generic;
using Bissoft.CouncilCMS.Core.Enums;
using Bissoft.CouncilCMS.Core.Globalization;
namespace Bissoft.CouncilCMS.BLL.ViewModels
{
    public class CmsPersonList
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryUrl { get; set; }
        public string CategoryTemplate { get; set; }
        public string CategoryDescription { get; set; }
        public List<CmsPersonListItem> Persons { get; set; }
        public bool ShowSearchForm { get; set; }
        public CmsMenu CategoryMenu { get; set; }
    }
}
