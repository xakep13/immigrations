using System;
using System.Collections.Generic;

namespace Bissoft.CouncilCMS.Models.Entities
{
    public class EnterpriseType : Entity<int>
    {
        public string Name { get; set; }
        public string TitleRu { get; set; }
        public string TitleUk { get; set; }
        public string TitleEn { get; set; }

        public int Position { get; set; }

        public virtual ICollection<Enterprise> Enterprises { get; set; } = new List<Enterprise>();
    }
}