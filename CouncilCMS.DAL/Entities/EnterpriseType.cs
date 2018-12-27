﻿using System;
using System.Collections.Generic;

namespace Bissoft.CouncilCMS.DAL.Entities
{
    public class EnterpriseType : BaseEntity<int>
    {
        public string Name { get; set; }
        public string TitleRu { get; set; }
        public string TitleUk { get; set; }
        public string TitleEn { get; set; }

        public int Position { get; set; }

        public virtual ICollection<Enterprise> Enterprises { get; set; } = new List<Enterprise>();
    }
}