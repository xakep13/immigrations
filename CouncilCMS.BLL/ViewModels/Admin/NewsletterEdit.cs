using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Bissoft.CouncilCMS.Core;
using Bissoft.CouncilCMS.Core.Enums;
using Bissoft.CouncilCMS.Core.Globalization;
using xTab.Tools.Attributes;

namespace Bissoft.CouncilCMS.BLL.ViewModels
{
    public class NewsletterEdit
    {
        public int Id { get; set; }
        public string TitleRu { get; set; }
        public string TitleUk { get; set; }
        public string DescriptionRu { get; set; }
        public string DescriptionUk { get; set; }
       
        public string Number { get; set; }

        public string CreateDate { get; set; }
        public string PublishDate { get; set; }
        
        public bool Published { get; set; }     
        public string FileRu { get; set; }
        public string FileUk { get; set; }

        public List<NewsletterArticleItem> Articles { get; set; }
    }

    public class NewsletterArticleItem
    {
        public int Id { get; set; }
        public int NewsletterId { get; set; }
        public int Position { get; set; }
        public string Image { get; set; }
        public string Html { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
    }
}
