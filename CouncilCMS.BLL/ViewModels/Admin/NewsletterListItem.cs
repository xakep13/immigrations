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
    public class NewsletterListItem
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Title { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime LastEditDate { get; set; }
        public DateTime? LastBulkDate { get; set; }
        public string SaveStatus { get; set; }
        public string PublishStatus { get; set; }
        public string File { get; set; }
    }
}
