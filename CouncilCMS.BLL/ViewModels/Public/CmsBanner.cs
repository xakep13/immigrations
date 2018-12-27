using System;
using System.Collections.Generic;
using Bissoft.CouncilCMS.Core.Enums;
using Bissoft.CouncilCMS.Core.Globalization;
namespace Bissoft.CouncilCMS.BLL.ViewModels
{
    public class CmsBanner
    {
        public int Id { get; set; }
        public int Position { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
        public string Url { get; set; }
    }
}
