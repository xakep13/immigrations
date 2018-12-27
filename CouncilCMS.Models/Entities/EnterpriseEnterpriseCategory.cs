using System;
using System.Collections.Generic;

namespace Bissoft.CouncilCMS.Models.Entities
{
    public class EnterpriseEnterpriseCategory : Entity<int>
    {
        public int EnterpriseId { get; set; }
        public int EnterpriseCategoryId { get; set; }
        public int Position { get; set; }
        public virtual Enterprise Enterprise { get; set; }
        public virtual EnterpriseCategory EnterpriseCategory { get; set; }
    }
}
