using System;
using System.Collections.Generic;
using Bissoft.CouncilCMS.Core.Enums;

namespace Bissoft.CouncilCMS.DAL.Entities
{
    public class SessionAgenda : BaseEntity<int>
    {
        public int Number { get; set; }

        public string TitleRu { get; set; }
        public string TitleUk { get; set; }
        public string TitleEn { get; set; }

        public int Status { get; set; }

        public int? DocId { get; set; }
        public int SessionId { get; set; }

        public virtual Session Session { get; set; }
        public virtual Doc Doc { get; set; }
    }
}