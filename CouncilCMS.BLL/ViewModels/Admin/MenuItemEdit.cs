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
    public class MenuItemEdit
    {
        public int Id { get; set; }
        public int MenuId { get; set; }
        public int? ParentMenuId { get; set; }

        [AllowHtml]
        public string NameRu { get; set; }
        [AllowHtml]
        public string NameUk { get; set; }
        [AllowHtml]
        public string NameEn { get; set; }

        [AllowHtml]
        public string DescriptionRu { get; set; }
        [AllowHtml]
        public string DescriptionUk { get; set; }
        [AllowHtml]
        public string DescriptionEn { get; set; }

        public string Image { get; set; }
        public string HoverImage { get; set; }
        public int Position { get; set; }
        public string Value { get; set; }
        public string ValueText { get; set; }
        public MenuItemType Type { get; set; }

        public bool ShowItem { get; set; }
        public bool ShowItemUk { get; set; }
        public bool ShowItemRu { get; set; }
        public bool ShowItemEn { get; set; }

        public List<SelectListItem> Types { get; set; }

        public List<SelectListItem> Pages { get; set; }
        public List<SelectListItem> Persons { get; set; }
        public List<SelectListItem> Enterprises { get; set; }
        public List<SelectListItem> ArticleCategories { get; set; }
        public List<SelectListItem> PersonCategories { get; set; }
        public List<SelectListItem> EnterpriseCategories { get; set; }
        public List<SelectListItem> DocCategories { get; set; }

        public List<SelectListItem> Roles { get; set; }        
        public List<AllowedRole> AllowedRoles { get; set; }
    }
}
