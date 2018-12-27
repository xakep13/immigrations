using System;
using System.Collections.Generic;

namespace Bissoft.CouncilCMS.Models.Entities
{
    public class DocType : Entity<int>
    {        
        public string TitleRu { get; set; }
        public string TitleUk { get; set; }
        public string TitleEn { get; set; }
     
        public int Position { get; set; }
       
        public virtual ICollection<Doc> Docs { get; set; }
    }
}