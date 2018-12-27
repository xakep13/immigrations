using System;
using System.Collections.Generic;

namespace Bissoft.CouncilCMS.DAL.Entities
{
    public class PersonCategory : BaseCategoryEntity
    {
        public virtual PersonCategory ParentCategory { get; set; }
        public virtual PersonCategory RelatedCategory { get; set; }
        public virtual PersonCategoryTemplate Template { get; set; }
        public virtual ICollection<PersonCategory> ChildCategories { get; set; } = new List<PersonCategory>();
        public virtual ICollection<PersonCategory> RelatedCategories { get; set; } = new List<PersonCategory>();
        public virtual ICollection<PersonPersonCategory> Persons { get; set; } = new List<PersonPersonCategory>();        
    }
}
