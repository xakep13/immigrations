using System;
using System.Collections.Generic;
using Bissoft.CouncilCMS.Core.Enums;
using Bissoft.CouncilCMS.Core.Globalization;
namespace Bissoft.CouncilCMS.BLL.ViewModels
{
    public class CmsPage
    {
        public Int32 Id { get; set; }
        public string Title { get; set; }
        public string UrlName { get; set; }
        public string Description { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeywords { get; set; }
        public string Type { get; set; }
        public int ViewCount { get; set; }
        public string PublishDate { get; set; }
        public string LastEditDate { get; set; }
        public int? PageTemplateId { get; set; }
        public List<CmsContentRow> ContentRows { get; set; }
        public CmsMenu PageMenu { get; set; }
    }
}
