using System;
using System.Collections.Generic;
using Bissoft.CouncilCMS.Core.Enums;
using Bissoft.CouncilCMS.Core.Globalization;

namespace Bissoft.CouncilCMS.BLL.ViewModels
{
    public class CmsSessionList
    {
        public int Count { get; set; }
        public int Page { get; set; }

        public string SessionPage { get; set; }
        public List<CmsSession> Sessions { get; set; }        
    }

    public class CmsSession
    {
        public int Id { get; set; }    
        public string Title { get; set; }        
    }
}
