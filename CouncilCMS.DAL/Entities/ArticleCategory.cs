using System;
using System.Collections.Generic;

namespace Bissoft.CouncilCMS.DAL.Entities
{
    public class ArticleCategory : BaseCategoryEntity
    {               
        public bool AllowOpenDataExport { get; set; }
      
        public virtual ArticleCategory ParentCategory { get; set; }
        public virtual ICollection<ArticleCategory> ChildCategories { get; set; } = new List<ArticleCategory>();
        public virtual ICollection<Article> Articles { get; set; } = new List<Article>();        
        public virtual ArticleCategory RelatedCategory { get; set; }       
        public virtual ArticleCategoryTemplate Template { get; set; }
    }
}