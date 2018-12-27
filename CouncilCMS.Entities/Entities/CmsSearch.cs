using System;
using System.Collections.Generic;

namespace Bissoft.CouncilCMS.DAL.Entities
{
    public class CmsSearch : BaseEntity<int>
    {
        public int ItemId { get; set; }
        public int Type { get; set; }
        public string CleanTextRu { get; set; }
        public string CleanTextUk { get; set; }
        public string CleanTextEn { get; set; }
        public DateTime Date { get; set; }
        public bool Published { get; set; }
    }
}
