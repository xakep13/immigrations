using System;
using System.Collections.Generic;

namespace Bissoft.CouncilCMS.Models.Entities
{
    public class EnterprisePerson : Entity<int>
    {
        public int EnterpriseId { get; set; }
        public int PersonId { get; set; }
        public int Position { get; set; }
        public string PostUk { get; set; }
        public string PostRu { get; set; }
        public string PostEn { get; set; }
        public virtual Enterprise Enterprise { get; set; }
        public virtual Person Person { get; set; }
    }
}
