using System;
using System.Collections.Generic;

namespace Bissoft.CouncilCMS.DAL.Entities
{
    public class CmsPremission : BaseEntity<int>
    {
        public string TitleUk { get; set; }
        public string TitleRu { get; set; }
        public string TitleEn { get; set; }
        public string Name { get; set; }
        public int Position { get; set; }       
    }
}
