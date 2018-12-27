using System;
using System.Collections.Generic;
using Bissoft.CouncilCMS.Core.Enums;
using Bissoft.CouncilCMS.Core.Globalization;
namespace Bissoft.CouncilCMS.BLL.ViewModels
{
    public class CmsArticleList
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryUrl { get; set; }
        public int RelatedCategoryId { get; set; }
        public string RelatedCategoryName { get; set; }
        public string RelatedCategoryUrl { get; set; }
        public string CategoryDescription { get; set; }
        public string CategoryNoImage { get; set; }        
        public int TemplateId { get; set; }
        public string TemplateName { get; set; }
        public bool ShowSearchForm { get; set; }
        public int Page { get; set; }
        public int PerPage { get; set; }
        public int Count { get; set; }
        public List<CmsArticleListItem> Articles { get; set; }
        public CmsMenu CategoryMenu { get; set; }
    }
}
