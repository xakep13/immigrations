using System;
using System.Collections.Generic;
using Bissoft.CouncilCMS.Core.Enums;
using Bissoft.CouncilCMS.Core.Globalization;

namespace Bissoft.CouncilCMS.BLL.ViewModels
{
    public class CmsArticle
    {
        public int Id { get; set; }    
        public string Url { get; set; }
        public string Title { get; set; }
        public string CategoryUrl { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public int RelatedCategoryId { get; set; }
        public string RelatedCategoryUrl { get; set; }
        public string RelatedCategoryName { get; set; }
        public string Description { get; set; }
        public string MetaTitle { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public string Image { get; set; }
        public string PublishDate { get; set; }
        public string LastEditDate { get; set; }
        public string EventDate { get; set; }
        public bool ShowComments { get; set; }
        public int ViewCount { get; set; }
        public List<CmsContentRow> ContentRows { get; set; }
        public CmsMenu ArticleMenu { get; set; }
        public bool userIsAdmin { get; set; }
    }
}
