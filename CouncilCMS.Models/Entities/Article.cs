using System;
using System.Collections.Generic;

namespace Bissoft.CouncilCMS.Models.Entities
{
    public class Article : Entity<int>
    {        
        public string TitleRu { get; set; }
        public string TitleUk { get; set; }
        public string TitleEn { get; set; }
        public string UrlNameRu { get; set; }
        public string UrlNameUk { get; set; }
        public string UrlNameEn { get; set; }
        public string DescriptionUk { get; set; }
        public string DescriptionRu { get; set; }
        public string DescriptionEn { get; set; }
        public string KeywordsUk { get; set; }
        public string KeywordsRu { get; set; }
        public string KeywordsEn { get; set; }
        public string MetaTitleUk { get; set; }
        public string MetaTitleRu { get; set; }
        public string MetaTitleEn { get; set; }
        public string MetaDescriptionUk { get; set; }
        public string MetaDescriptionRu { get; set; }
        public string MetaDescriptionEn { get; set; }
        public string MetaKeywordsUk { get; set; }
        public string MetaKeywordsRu { get; set; }
        public string MetaKeywordsEn { get; set; }
        
        public bool ShowPublihDate { get; set; }
        public bool ShowEditDate { get; set; }
        public bool AllowComments { get; set; }
        public bool ExportToOpenData { get; set; }
        public string OpenDataId { get; set; }
        public string OpenDataTitle { get; set; }
        public string OpenDataDescription { get; set; }
        public string OpenDataKeywords { get; set; }
        public string OpenDataRefresh { get; set; }
        public string OpenDataCategory { get; set; }
        public string OpenDataName { get; set; }
        public string OpenDataEmail { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? PublishDate { get; set; }
        public DateTime? LastEditDate { get; set; }
        public DateTime? EventDate { get; set; }
        public DateTime? EditedDate { get; set; }

        public int? CreateUserId { get; set; }
        public int? LastEditUserId { get; set; }
        public bool Published { get; set; }   
        public bool Saved { get; set; }   
        public int ViewCount { get; set; }
        public string Image { get; set; }
        public string ImageSource { get; set; }
        public virtual List<ArticleCategory> ArticleCategories { get; set; }
        public virtual List<NewsletterArticle> Newsletters { get; set; }
        public virtual List<ContentRow> ContentRows { get; set; }
    }
}