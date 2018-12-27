using System;
using System.Collections.Generic;

namespace Bissoft.CouncilCMS.DAL.Entities
{
    public class EnterpriseCategoryTemplate : BaseCategoryTemplate
    {
        public virtual ICollection<EnterpriseCategory> Categories { get; set; } = new List<EnterpriseCategory>();
    }
}
