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
    public class CollapseContent :ContentEdit
    {
        public string TitleRu { get; set; }
        public string TitleUk { get; set; }
        public string TitleEn { get; set; }

        [AllowHtml]
        public string TextRu { get; set; }
        [AllowHtml]
        public string TextUk { get; set; }
        [AllowHtml]
        public string TextEn { get; set; }

        public bool Expanded { get; set; }
    }
}
