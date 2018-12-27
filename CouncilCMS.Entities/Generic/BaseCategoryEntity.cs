using System;
using System.Collections.Generic;

namespace Bissoft.CouncilCMS.DAL.Entities
{
    public abstract class BaseCategoryEntity : BaseEntity<int>
    {
        public string UrlName { get; set; }
        public string TitleRu { get; set; }
        public string TitleUk { get; set; }
        public string TitleEn { get; set; }
        public string DescriptionUk { get; set; }
        public string DescriptionRu { get; set; }
        public string DescriptionEn { get; set; }
        public string MetaTitleUk { get; set; }
        public string MetaTitleRu { get; set; }
        public string MetaTitleEn { get; set; }
        public string MetaDescriptionUk { get; set; }
        public string MetaDescriptionRu { get; set; }
        public string MetaDescriptionEn { get; set; }
        public string MetaKeywordsUk { get; set; }
        public string MetaKeywordsRu { get; set; }
        public string MetaKeywordsEn { get; set; }
        public int Position { get; set; }
        public int Priority { get; set; }
        public string Image { get; set; }
        public bool ShowSearchForm { get; set; }
        public int? TemplateId { get; set; }
        public int? SidebarMenuId { get; set; }
        public int? CurrentMenuItemId { get; set; }
        public int? RelatedCategoryId { get; set; }               
        public int? ParentCategoryId { get; set; }
        public virtual Menu SidebarMenu { get; set; }
        public virtual MenuItem CurrentMenuItem { get; set; }
        public virtual ICollection<CmsUser> AllowedUsers { get; set; } = new List<CmsUser>();
        public virtual ICollection<CmsRole> AllowedRoles { get; set; } = new List<CmsRole>();        
    }
}