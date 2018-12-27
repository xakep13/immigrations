using System;
using System.Collections.Generic;

namespace Bissoft.CouncilCMS.DAL.Entities
{
    public class ArticleCategoryTemplate : BaseCategoryTemplate
    {        
        public virtual ICollection<ArticleCategory> Categories { get; set; } = new List<ArticleCategory>();
    }
}
