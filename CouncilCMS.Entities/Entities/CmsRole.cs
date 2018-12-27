using System;
using System.Collections.Generic;

namespace Bissoft.CouncilCMS.DAL.Entities
{
    public class CmsRole : BaseEntity<int>
    {
        public string TitleUk { get; set; }
        public string TitleRu { get; set; }
        public string TitleEn { get; set; }
        public string Name { get; set; }
        public string Premissions { get; set; }
        public virtual ICollection<CmsUser> Users { get; set; } = new List<CmsUser>();
        public virtual ICollection<ArticleCategory> AllowedArticleCategories { get; set; } = new List<ArticleCategory>();
        public virtual ICollection<PersonCategory> AllowedPersonCategories { get; set; } = new List<PersonCategory>();
        public virtual ICollection<EnterpriseCategory> AllowedEnterpriseCategories { get; set; } = new List<EnterpriseCategory>();
        public virtual ICollection<DocCategory> AllowedDocCategories { get; set; } = new List<DocCategory>();
        public virtual ICollection<MenuItem> AllowedMenuItems { get; set; } = new List<MenuItem>();
    }
}
