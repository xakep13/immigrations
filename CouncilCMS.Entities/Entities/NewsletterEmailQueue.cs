using System;
using System.Collections.Generic;

namespace Bissoft.CouncilCMS.DAL.Entities
{
    public class NewsletterEmailQueue : BaseEntity<int>
    {        
        public string Email { get; set; }
        public DateTime? SentOn { get; set; } 
        public int NewsletterId { get; set; }       
        public virtual Newsletter Newsletter { get; set; }
    }
}