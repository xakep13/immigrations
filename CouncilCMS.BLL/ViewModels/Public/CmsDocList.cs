﻿using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Bissoft.CouncilCMS.Core;
using Bissoft.CouncilCMS.Core.Globalization;
using Bissoft.CouncilCMS.BLL.ViewModels.Public;

namespace Bissoft.CouncilCMS.BLL.ViewModels
{
    public class CmsDocList
    {        
        [MaxLength(100, ErrorMessageResourceType = typeof(Locals), ErrorMessageResourceName = "valMinLength")]
        public string QueryTitle { get; set; }

        [MinLength(3, ErrorMessageResourceType = typeof(Locals), ErrorMessageResourceName = "valMinLength")]
        [MaxLength(200, ErrorMessageResourceType = typeof(Locals), ErrorMessageResourceName = "valMinLength")]
        public string QueryText { get; set; }

        public string Date { get; set; }
        public int? Type { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryUrl { get; set; }
        public int RelatedCategoryId { get; set; }
        public string RelatedCategoryName { get; set; }
        public string RelatedCategoryUrl { get; set; }
        public string CategoryDescription { get; set; }
        public string PersonName { get; set; }
        public string EnterpriseName { get; set; }
        public int TemplateId { get; set; }
        public string TemplateName { get; set; }
        public int Page { get; set; }
        public int PerPage { get; set; }
        public int Count { get; set; }
        public int? PersonId { get; set; }
        public int? EnterpriseId { get; set; }
        public bool ShowSearchForm { get; set; }

        public string Order { get; set; }

        public List<CmsDocListItem> Docs { get; set; }
        public CmsMenu CategoryMenu { get; set; }
        public List<SelectListItem> Types { get; set; }

        // added by vadym
        public bool HasChildren { get; set; }
        public bool HasDocuments { get; set; }
    }
}
