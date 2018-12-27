using System;
using System.Collections.Generic;
using Bissoft.CouncilCMS.Core.Enums;
using Bissoft.CouncilCMS.Core.Globalization;
namespace Bissoft.CouncilCMS.BLL.ViewModels
{
    public class CmsArticleListItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string UrlName { get; set; }
        public string Description { get; set; }
        public string CategoryTitle { get; set; }
        public string PublishDate { get; set; }
        public DateTime? PublishDateTime { get; set; }
        public DateTime? EventDate { get; set; }
        public string Image { get; set; }
        public int ViewCount { get; set; }
        public bool IsAnons { get; set; }

    }
}
