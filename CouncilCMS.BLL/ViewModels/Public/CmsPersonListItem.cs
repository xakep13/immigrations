using System;
using System.Collections.Generic;
using Bissoft.CouncilCMS.Core.Enums;
using Bissoft.CouncilCMS.Core.Globalization;
namespace Bissoft.CouncilCMS.BLL.ViewModels
{
    public class CmsPersonListItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Post { get; set; }
        public int? DeputyFractionId { get; set; }
        public string DeputyFractionName { get; set; }
        public string DeputyFractionUrl { get; set; }
        public string EnterprisePost { get; set; }
        public string UrlName { get; set; }
        public string Description { get; set; }
        public string ContactAddress { get; set; }
        public string ContactPhones { get; set; }
        public string ContactEmail { get; set; }
        public string Image { get; set; }
        public int ViewCount { get; set; }
    }
}
