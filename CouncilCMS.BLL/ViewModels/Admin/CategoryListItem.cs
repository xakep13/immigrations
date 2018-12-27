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
    public class CategoryListItem
    {
        public int Id { get; set; }
        public string TitleUk { get; set; }
        public string TitleRu { get; set; }
        public string TitleEn { get; set; }
        public string UrlName { get; set; }
        public int Position { get; set; }
        public int Priority { get; set; }
        public string Template { get; set; }
        public string Image { get; set; }
        public string Type { get; set; }
        public bool HasChilds { get; set; }
        public bool Allowed { get; set; }
        public int? ParentCategoryId { get; set; }
        public List<CategoryListItem> ChildCategoryItems { get; set; }
        public CategoryListItem Parent { get; set; }

        // added from vadym
        public bool HasDocuments { get; set; }
    }
}
