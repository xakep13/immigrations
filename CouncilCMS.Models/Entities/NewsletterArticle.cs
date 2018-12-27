using System;
using System.Collections.Generic;

namespace Bissoft.CouncilCMS.Models.Entities
{
    public class NewsletterArticle : Entity<int>
    {
        public int NewsletterId { get; set; }
        public int ArticleId { get; set; }
        public int Position { get; set; }
        public virtual Newsletter Newsletter { get; set; }
        public virtual Article Article { get; set; }
    }
}
