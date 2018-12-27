using System;
using System.Collections.Generic;

namespace Bissoft.CouncilCMS.Models.Entities
{
    public class Newsletter : Entity<int>
    {        
        public string Number { get; set; }
        public string TitleRu { get; set; }
        public string TitleUk { get; set; }
        public string UrlNameRu { get; set; }
        public string UrlNameUk { get; set; }
        public string DescriptionUk { get; set; }
        public string DescriptionRu { get; set; }
        
        public DateTime CreateDate { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime LastEditDate { get; set; }
        public DateTime? LastBulkDate { get; set; }

        public int? CreateUserId { get; set; }
        public int? LastEditUserId { get; set; }
        public string FileRu { get; set; }
        public string FileUk { get; set; }

        public bool Published { get; set; }
        public bool Saved { get; set; }
        public bool Sended { get; set; }

        public virtual ICollection<NewsletterArticle> Articles { get; set; } = new List<NewsletterArticle>();
    }
}