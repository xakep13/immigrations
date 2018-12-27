using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Bissoft.CouncilCMS.Core;
using Bissoft.CouncilCMS.Core.Enums;
using Bissoft.CouncilCMS.Core.Globalization;
using xTab.Tools.Attributes;

namespace Bissoft.CouncilCMS.BLL.ViewModels
{
    public class CmsUserList
    {
        public int Page { get; set; }
        public string Query { get; set; }        
        public int? Role { get; set; }
        public string DateRange { get; set; }
        public string SortBy { get; set; }
        public int SortDirection { get; set; }
        public int UserState { get; set; }
        public List<SelectListItem> Roles { get; set; }       
    }
}
