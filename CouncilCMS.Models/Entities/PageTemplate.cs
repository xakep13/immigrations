using System;
using System.Collections.Generic;

namespace Bissoft.CouncilCMS.Models.Entities
{
    public class PageTemplate : Entity<int>
    {
        public string Name { get; set; }
        public string TitleRu { get; set; }
        public string TitleUk { get; set; }
        public string TitleEn { get; set; }
        public int Position { get; set; }
        public virtual ICollection<Page> Pages { get; set; }
    }
}
