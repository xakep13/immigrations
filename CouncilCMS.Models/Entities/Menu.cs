using System;
using System.Collections.Generic;

namespace Bissoft.CouncilCMS.Models.Entities
{
    public class Menu : Entity<int>
    {
        public string Name { get; set; }
        public string NameRu { get; set; }
        public string NameUk { get; set; }
        public string NameEn { get; set; }
        public virtual List<MenuItem> MenuItems { get; set; }
    }
}