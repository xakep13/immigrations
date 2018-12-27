using System;
using System.Collections.Generic;

namespace Bissoft.CouncilCMS.Models.Entities
{
    public class EnterpriseCategory : Entity<int>
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
        public int? CurrentMenuItemId { get; set; }
        public int? TemplateId { get; set; }
        public int? ParentCategoryId { get; set; }
        public virtual EnterpriseCategory ParentCategory { get; set; }
        public virtual ICollection<EnterpriseCategory> ChildCategories { get; set; } = new List<EnterpriseCategory>();
        public virtual ICollection<Enterprise> Enterprises { get; set; } = new List<Enterprise>();
        public virtual ICollection<CmsUser> AllowedUsers { get; set; } = new List<CmsUser>();
        public virtual MenuItem CurrentMenuItem { get; set; }
        public virtual EnterpriseCategoryTemplate Template { get; set; }
    }
}