using System;
using System.Collections.Generic;

namespace Bissoft.CouncilCMS.DAL.Entities
{
    public class EnterpriseCategory : BaseCategoryEntity
    {       
        public virtual EnterpriseCategory ParentCategory { get; set; }
        public virtual EnterpriseCategory RelatedCategory { get; set; }
        public virtual EnterpriseCategoryTemplate Template { get; set; }
        public virtual ICollection<EnterpriseCategory> ChildCategories { get; set; } = new List<EnterpriseCategory>();
        public virtual ICollection<EnterpriseCategory> RelatedCategories { get; set; } = new List<EnterpriseCategory>();
        public virtual ICollection<EnterpriseEnterpriseCategory> Enterprises { get; set; } = new List<EnterpriseEnterpriseCategory>();                
    }
}