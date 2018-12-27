using System;
using System.Collections.Generic;

namespace Bissoft.CouncilCMS.DAL.Entities
{
    public class PersonCategoryTemplate : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Preview { get; set; }
        public string TitleRu { get; set; }
        public string TitleUk { get; set; }
        public string TitleEn { get; set; }
        public int Position { get; set; }
        public virtual ICollection<PersonCategory> Categories { get; set; } = new List<PersonCategory>();
    }
}
