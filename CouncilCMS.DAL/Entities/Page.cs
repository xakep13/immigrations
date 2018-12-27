using System;
using System.Collections.Generic;

namespace Bissoft.CouncilCMS.DAL.Entities
{
    public class Page : BaseEntity<int>
    {
        public string UrlName { get; set; }
        public string TitleRu { get; set; }
        public string TitleUk { get; set; }
        public string TitleEn { get; set; }

        public string KeywordsUk { get; set; }
        public string KeywordsRu { get; set; }
        public string KeywordsEn { get; set; }

        public string MetaTitleUk { get; set; }
        public string MetaTitleRu { get; set; }
        public string MetaTitleEn { get; set; }
        public string MetaDescriptionUk { get; set; }
        public string MetaDescriptionRu { get; set; }
        public string MetaDescriptionEn { get; set; }
        public string MetaKeywordsUk { get; set; }
        public string MetaKeywordsRu { get; set; }
        public string MetaKeywordsEn { get; set; }
        public string Type { get; set; }
        public string DescriptionRu { get; set; }
        public string DescriptionUk { get; set; }
        public string DescriptionEn { get; set; }
        public string CleanTextRu { get; set; }
        public string CleanTextUk { get; set; }
        public string CleanTextEn { get; set; }
        public int ViewCount { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastEditDate { get; set; }
        public DateTime? PublishDate { get; set; }
        public DateTime? EditedDate { get; set; }
        public int? CreateUserId { get; set; }
        public int? LastEditUserId { get; set; }
        public int? PageTemplateId { get; set; }
        public int? SidebarMenuId { get; set; }
        public int? CurrentMenuItemId { get; set; }
        public bool Saved { get; set; }
        public bool Published { get; set; }
        public bool ShowPublihDate { get; set; }
        public bool ShowEditDate { get; set; }
        public virtual PageTemplate Template { get; set; }
        public virtual Menu SidebarMenu { get; set; }
        public virtual MenuItem CurrentMenuItem { get; set; }
        public virtual List<ContentRow> ContentRows { get; set; }
        public virtual ICollection<CmsUser> AllowedUsers { get; set; } = new List<CmsUser>();
        
    }
}