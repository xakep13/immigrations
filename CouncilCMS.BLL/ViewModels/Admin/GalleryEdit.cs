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
    public class GalleryEdit : ContentEdit
    {
        public string NameRu { get; set; }
        public string NameUk { get; set; }
        public string NameEn { get; set; }
        [AllowHtml]
        public string DescriptionRu { get; set; }
        [AllowHtml]
        public string DescriptionUk { get; set; }
        [AllowHtml]
        public string DescriptionEn { get; set; }

        public string Ratio { get; set; }

        public List<GalleryItemEdit> GalleryItems { get; set; }
    }
    public class GalleryItemEdit
    {
        public string NameRu { get; set; }
        public string NameUk { get; set; }
        public string NameEn { get; set; }
        public string Image { get; set; }
    }
}
