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
    public class NewsletterPreview
    {
        public int Id { get; set; }
        
        [Required(ErrorMessageResourceType = typeof(Locals), ErrorMessageResourceName = "valSimpleRequired")]
        public string TitleUk { get; set; }
        public string DescriptionUk { get; set; }
      
        public string CreateDate { get; set; }
        public string PublishDate { get; set; }
        
        public bool Published { get; set; }     
        public string File { get; set; }
        public string Number { get; set; }
        public List<NewsletterArticleItem> Articles { get; set; }
    }
}
