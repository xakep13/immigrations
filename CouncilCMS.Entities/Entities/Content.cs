using System;
using System.Collections.Generic;

namespace Bissoft.CouncilCMS.DAL.Entities
{
    public class Content : BaseEntity<int>
    {
        public int Position { get; set; }
        public int Type { get; set; }
        public string BodyRu { get; set; }
        public string BodyUk { get; set; }
        public string BodyEn { get; set; }
        public int ContentColumnId { get; set; }       
        public virtual ContentColumn ContentColumn { get; set; }
    }
}