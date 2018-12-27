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
    public class HtmlContent : ContentEdit
    {
        [AllowHtml]
        public string BodyRu { get; set; }
        [AllowHtml]
        public string BodyUk { get; set; }
        [AllowHtml]
        public string BodyEn { get; set; }
    }
}
