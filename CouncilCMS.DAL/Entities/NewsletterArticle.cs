using System;
using System.Collections.Generic;

namespace Bissoft.CouncilCMS.DAL.Entities
{
    public class NewsletterArticle : BaseEntity<int>
    {
        public int NewsletterId { get; set; }
        public int ArticleId { get; set; }
        public int Position { get; set; }
        public virtual Newsletter Newsletter { get; set; }
        public virtual Article Article { get; set; }
    }
}
