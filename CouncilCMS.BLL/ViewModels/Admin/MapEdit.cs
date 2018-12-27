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
    public class MapEdit : ContentEdit
    {
        public string CenterLng { get; set; }
        public string CenterLat { get; set; }
        public string Height { get; set; }
        public string Zoom { get; set; }

        public List<MapPointEdit> MapPoints { get; set; }
    }

    public class MapPointEdit
    {
        public string Lng { get; set; }
        public string Lat { get; set; }
        public string Icon { get; set; }
        public string TitleRu { get; set; }
        public string TitleUk { get; set; }
        public string TitleEn { get; set; }

        [AllowHtml]
        public string DescriptionUk { get; set; }
        [AllowHtml]
        public string DescriptionRu { get; set; }
        [AllowHtml]
        public string DescriptionEn { get; set; }
    }
}
