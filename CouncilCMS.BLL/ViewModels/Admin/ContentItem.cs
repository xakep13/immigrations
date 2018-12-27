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
    public class ContentItem
    {
        public int Id { get; set; }
        public int Position { get; set; }
        public string Type { get; set; }
        public string BodyRu { get; set; }
        public string BodyUk { get; set; }
        public string BodyEn { get; set; }
        public int ContentColumnId { get; set; }
        public int? FormId { get; set; }
    }
}
