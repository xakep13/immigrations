using System;
using System.Collections.Generic;

namespace Bissoft.CouncilCMS.Models.Entities
{
    public class CmsRole : Entity<int>
    {
        public string Title { get; set; }
        public string Name { get; set; }
        public string Premissions { get; set; }
        public virtual ICollection<CmsUser> Users { get; set; } = new List<CmsUser>();
    }
}
