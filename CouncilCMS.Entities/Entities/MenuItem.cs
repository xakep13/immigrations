using System;
using System.Collections.Generic;

namespace Bissoft.CouncilCMS.DAL.Entities
{
    public class MenuItem : BaseEntity<int>
    {
        public string NameRu { get; set; }
        public string NameUk { get; set; }
        public string NameEn { get; set; }

        public int Position { get; set; }
        public string Url { get; set; }
        public string Image { get; set; }
        public string HoverImage { get; set; }

        public string DescriptionRu { get; set; }
        public string DescriptionUk { get; set; }
        public string DescriptionEn { get; set; }

        public int Type { get; set; }
        public string Value { get; set; }
        public int ItemId { get; set; }
        public int ItemName { get; set; }

        public bool ShowItem { get; set; }
        public bool ShowItemUk { get; set; }
        public bool ShowItemRu { get; set; }
        public bool ShowItemEn { get; set; }

        public int MenuId { get; set; }
        public int? ParentMenuItemId { get; set; }

        public virtual Menu Menu { get; set; }
        public virtual MenuItem ParentMenuItem { get; set; }
        public virtual List<MenuItem> ChildMenuItems { get; set; }

        public virtual ICollection<CmsRole> AllowedRoles { get; set; } = new List<CmsRole>();
    }
}