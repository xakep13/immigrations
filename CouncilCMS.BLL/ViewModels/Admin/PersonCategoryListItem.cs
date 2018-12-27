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
    public class PersonCategoryListItem
    {
        public int Id { get; set; }
        public string TitleUk { get; set; }
        public string TitleRu { get; set; }
        public string TitleEn { get; set; }
        public int Position { get; set; }
        public int? ParentPersonCategoryId { get; set; }
        public List<PersonCategoryListItem> ChildPersonCategoryItems { get; set; }

    }
}
