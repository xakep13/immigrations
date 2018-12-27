using System;
using System.Collections.Generic;

namespace Bissoft.CouncilCMS.DAL.Entities
{
    public class Menu : BaseEntity<int>
    {
        public string Name { get; set; }
        public string NameRu { get; set; }
        public string NameUk { get; set; }
        public string NameEn { get; set; }
        public int Index { get; set; }
        public virtual List<MenuItem> MenuItems { get; set; }
    }
}