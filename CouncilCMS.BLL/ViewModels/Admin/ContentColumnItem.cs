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
    public class ContentColumnItem
    {
        public int Id { get; set; }
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
        public string NameRu { get; set; }
        public string NameUk { get; set; }
        public int ContentRowId { get; set; }
        public List<ContentItem> Contents { get; set; }
        public List<SelectListItem> Types { get; set; }
    }
}
