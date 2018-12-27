using System;
using System.Collections.Generic;
using Bissoft.CouncilCMS.Core.Enums;
using Bissoft.CouncilCMS.Core.Globalization;
namespace Bissoft.CouncilCMS.BLL.ViewModels
{
    public class CmsSearchItem
    {
        public string Title { get; set; }
        public string Type { get; set; }
        public string Url { get; set; }
        public DateTime Date { get; set; }
    }
}
