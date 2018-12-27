using System;
using System.Collections.Generic;

namespace Bissoft.CouncilCMS.DAL.Entities
{
    public class PersonCategoryTemplate : BaseCategoryTemplate
    {
        public virtual ICollection<PersonCategory> Categories { get; set; } = new List<PersonCategory>();
    }
}
