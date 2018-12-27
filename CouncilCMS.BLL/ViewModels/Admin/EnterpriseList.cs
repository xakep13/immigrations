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
   public class EnterpriseList
    {
        public int Page { get; set; }
        public string Query { get; set; }
        public string DateRange { get; set; }
        public int DateType { get; set; }
        public int? CategoryId { get; set; }
        public string SortBy { get; set; }
        public int SortDirection { get; set; }
        public int ItemState { get; set; }
        public List<SelectListItem> Categories { get; set; }

        public int? User { get; set; }
        public int UserAction { get; set; }
        public List<SelectListItem> Users { get; set; }
    }
}
