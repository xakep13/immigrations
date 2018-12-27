using System;
using System.Collections.Generic;

namespace Bissoft.CouncilCMS.DAL.Entities
{
    public class ContentColumn : BaseEntity<int>
    {
        public int Position { get; set; }
        public int LgWidth { get; set; }
        public int MdWidth { get; set; }
        public int SmWidth { get; set; }
        public int XsWidth { get; set; }
        public int LgOffset { get; set; }
        public int MdOffset { get; set; }
        public int SmOffset { get; set; }
        public int XsOffset { get; set; }
        public string BackgroundColor { get; set; }
        public string BackgroundImage { get; set; }
        public string BackgroundPosition { get; set; }
        public string BackgroundRepeat { get; set; }
        public string BackgroundSize { get; set; }
        public string CssClasses { get; set; }
        public string CssStyles { get; set; }
        public string Type { get; set; }
        public int ContentRowId { get; set; }
        public virtual ContentRow ContentRow { get; set; }
        public virtual ICollection<Content> Contents { get; set; } = new List<Content>();
    }
}
