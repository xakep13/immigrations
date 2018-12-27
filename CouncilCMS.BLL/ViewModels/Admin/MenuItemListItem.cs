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
   public class MenuItemListItem
    {
        public int Id { get; set; }
        public int MenuId { get; set; }
        public int? ParentMenuId { get; set; }
        public string TitleRu { get; set; }
        public string TitleUk { get; set; }
        public string TitleEn { get; set; }
        public string Image { get; set; }
        public string Url { get; set; }
        public string Type { get; set; }
        public int Position { get; set; }  
        public bool ShowItem { get; set; }
        public bool ShowItemUk { get; set; }
        public bool ShowItemRu { get; set; }
        public bool ShowItemEn { get; set; }
        public bool Allowed { get; set; }
        public bool ParentAllowed { get; set; }
        public bool HasChilds { get; set; }
        public List<MenuItemListItem> ChildMenuItems { get; set; }
    }
}
