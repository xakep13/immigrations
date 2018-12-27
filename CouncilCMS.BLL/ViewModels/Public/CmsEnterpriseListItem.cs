﻿using System;
using System.Collections.Generic;
using Bissoft.CouncilCMS.Core.Enums;
using Bissoft.CouncilCMS.Core.Globalization;
namespace Bissoft.CouncilCMS.BLL.ViewModels
{
    public class CmsEnterpriseListItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string UrlName { get; set; }
        public string Description { get; set; }
        public string ContactAddress { get; set; }
        public string ContactPhones { get; set; }
        public string ContactEmails { get; set; }
        public string Image { get; set; }
        public string Type { get; set; }
        public int TypePosition { get; set; }
        public int PersonCount { get; set; }
        public int ViewCount { get; set; }
    }
}