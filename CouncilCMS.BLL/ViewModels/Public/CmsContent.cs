using System;
using System.Collections.Generic;
using Bissoft.CouncilCMS.Core.Enums;
using Bissoft.CouncilCMS.Core.Globalization;
namespace Bissoft.CouncilCMS.BLL.ViewModels
{
    public class CmsContent
    {
        public int Id { get; set; }
        public int Position { get; set; }
        public string Body { get; set; }
        public ContentType Type { get; set; }
    }
}
