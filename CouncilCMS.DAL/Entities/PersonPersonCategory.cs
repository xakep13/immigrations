using System;
using System.Collections.Generic;

namespace Bissoft.CouncilCMS.DAL.Entities
{
    public class PersonPersonCategory : BaseEntity<int>
    {
        public int PersonId { get; set; }
        public int PersonCategoryId { get; set; }
        public int Position { get; set; }
        public bool ShowOnPersonPage { get; set; }
        public virtual Person Person { get; set; }
        public virtual PersonCategory PersonCategory { get; set; }
    }
}
