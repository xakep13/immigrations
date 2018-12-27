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
    public class CkeContent : ContentEdit
    {
        [AllowHtml]
        public string TextRu { get; set; }
        [AllowHtml]
        public string TextUk { get; set; }
        [AllowHtml]
        public string TextEn { get; set; }
    }
}
