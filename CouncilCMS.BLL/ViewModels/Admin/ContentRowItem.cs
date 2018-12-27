using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Bissoft.CouncilCMS.Core;
using Bissoft.CouncilCMS.Core.Enums;
using Bissoft.CouncilCMS.Core.Globalization;
using xTab.Tools.Attributes;

namespace Bissoft.CouncilCMS.BLL.ViewModels
{
    public class ContentRowItem
    {
        public int Id { get; set; }
        public int Position { get; set; }
        public int ItemId { get; set; }
        public string BackgroundColor { get; set; }
        public string BackgroundImage { get; set; }
        public string BackgroundPosition { get; set; }
        public string BackgroundRepeat { get; set; }
        public string BackgroundSize { get; set; }
        public string CssClasses { get; set; }
        public string CssStyles { get; set; }
        public List<ContentColumnItem> ContentColumns { get; set; }
    }
}
