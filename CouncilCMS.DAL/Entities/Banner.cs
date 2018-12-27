using System;
using System.Collections.Generic;

namespace Bissoft.CouncilCMS.DAL.Entities
{
    public class Banner : BaseEntity<int>
    {       
        public string TextRu { get; set; }
        public string TextUk { get; set; }
        public string TextEn { get; set; }
        public string Url { get; set; }
        public string Image { get; set; }
        public string Link { get; set; }
        public bool Published { get; set; } 
        public int Position { get; set; }
    }
}