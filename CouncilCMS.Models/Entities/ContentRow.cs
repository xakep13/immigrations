using System;
using System.Collections.Generic;

namespace Bissoft.CouncilCMS.Models.Entities
{
    public class ContentRow : Entity<int>
    {
        public int Position { get; set; }
        public string BackgroundColor { get; set; }
        public string BackgroundImage { get; set; }
        public string BackgroundPosition { get; set; }
        public string BackgroundRepeat { get; set; }
        public string BackgroundSize { get; set; }
        public string CssClasses { get; set; }
        public string CssStyles { get; set; }
        public virtual ICollection<ContentColumn> ContentColumns { get; set; } = new List<ContentColumn>();
       
    }
}
