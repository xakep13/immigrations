using System;
using System.Collections.Generic;
using Bissoft.CouncilCMS.Core.Enums;

namespace Bissoft.CouncilCMS.DAL.Entities
{
    public class SessionVote : BaseEntity<int>
    {
        public int Number { get; set; }

        public string TitleRu { get; set; }
        public string TitleUk { get; set; }
        public string TitleEn { get; set; }

        public string TextRu { get; set; }
        public string TextUk { get; set; }
        public string TextEn { get; set; }

        public string ResultRu { get; set; }
        public string ResultUk { get; set; }
        public string ResultEn { get; set; }

        public int For { get; set; }
        public int Against { get; set; }
        public int Abstained { get; set; }
        public int NotVote { get; set; }
        public int Absent { get; set; }
        
        public int Result { get; set; }

        public int? DocId { get; set; }
        public int SessionId { get; set; }

        public virtual Session Session { get; set; }
        public virtual Doc Doc { get; set; }
    }
}