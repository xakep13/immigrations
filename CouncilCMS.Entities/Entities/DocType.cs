using System;
using System.Collections.Generic;

namespace Bissoft.CouncilCMS.DAL.Entities
{
    public class DocType : BaseEntity<int>
    {        
        public string TitleRu { get; set; }
        public string TitleUk { get; set; }
        public string TitleEn { get; set; }
     
        public int Position { get; set; }
       
        public virtual ICollection<Doc> Docs { get; set; }
    }
}