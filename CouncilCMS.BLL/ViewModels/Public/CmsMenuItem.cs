using System;
using System.Collections.Generic;
using Bissoft.CouncilCMS.Core.Enums;
using Bissoft.CouncilCMS.Core.Globalization;
namespace Bissoft.CouncilCMS.BLL.ViewModels
{
    public class CmsMenuItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string HoverImage { get; set; }
        public string Url { get; set; }
        public int Position { get; set; }
        public MenuItemType Type { get; set; }
        public string Value { get; set; }
        public bool Current { get; set; }
        public bool Expanded { get; set; }
        public List<CmsMenuItem> Items { get; set; }
    }
}
