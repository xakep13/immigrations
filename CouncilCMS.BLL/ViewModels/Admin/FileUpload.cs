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
    public class FileUpload : ContentEdit
    {
        public string NameUk { get; set; }
        public string NameRu { get; set; }
        public string NameEn { get; set; }
        public string Url { get; set; }
        public int MarginBottom { get; set; }
        public int FontSize { get; set; }
        public int FontWeight { get; set; }
        public string FontStyle { get; set; }
        public string TextAlign { get; set; }
        public bool ShowIcon { get; set; }
    }
}
