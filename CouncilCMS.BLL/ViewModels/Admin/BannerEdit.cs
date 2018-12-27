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
    public class BannerEdit
    {
        public int Id { get; set; }
        public string TextRu { get; set; }
        public string TextUk { get; set; }
        public string TextEn { get; set; }
        public string Url { get; set; }
        public string Image { get; set; }
        public string Link { get; set; }
        public bool Published { get; set; }
    }
}

