using System;
using System.Collections.Generic;
using Bissoft.CouncilCMS.Core.Enums;
using Bissoft.CouncilCMS.Core.Globalization;

namespace Bissoft.CouncilCMS.BLL.ViewModels
{
    public class CmsSessionMedia
    {
        public int SessionId { get; set; }
        public String SessionTitle { get; set; }
        public string Video { get; set; }
        public string Audio { get; set; }        
    }
}
