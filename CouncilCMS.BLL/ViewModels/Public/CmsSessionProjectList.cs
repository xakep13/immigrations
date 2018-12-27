﻿using System;
using System.Collections.Generic;
using Bissoft.CouncilCMS.Core.Enums;
using Bissoft.CouncilCMS.Core.Globalization;

namespace Bissoft.CouncilCMS.BLL.ViewModels
{
    public class CmsSessionProjectList
    {
        public int SessionId { get; set; }
        public String SessionTitle { get; set; }
        public List<CmsDocListItem> Projects { get; set; }
        public int Page { get; set; }   
        public int Count { get; set; }     
    } 
}