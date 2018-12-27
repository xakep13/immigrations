using System;
using System.Collections.Generic;
using Bissoft.CouncilCMS.Core.Enums;
using Bissoft.CouncilCMS.Core.Globalization;
namespace Bissoft.CouncilCMS.BLL.ViewModels
{
    public class CmsEnterprise
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryUrlName { get; set; }

        public string ContactAddress { get; set; }
        public string ContactEmails { get; set; }
        public string ContactPhones { get; set; }
        public string ContactDates { get; set; }
        public string ContactTimes { get; set; }
        public string ContactWebsite { get; set; }

        public string FbLink { get; set; }
        public string TwLink { get; set; }
        public string GpLink { get; set; }
        public string YtLink { get; set; }
        public string IgLink { get; set; }

        public string MetaTitle { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }

        public string Image { get; set; }

        public bool ShowReceptionForm { get; set; }

        public string PublishDate { get; set; }
        public string LastEditDate { get; set; }
        public int ViewCount { get; set; }
        public bool ShowSecondPage { get; set; }
        public string SecondPageTitle { get; set; }
        public string SecondPageText { get; set; }
        public List<CmsContentRow> ContentRows { get; set; }

        public int? ParentId { get; set; }
        public string ParentName { get; set; }
        public string ParentUrlName { get; set; }

        public string PersonsTitle { get; set; }

        public CmsEnterpriseReception Reception { get; set; }

        public List<CmsEnterpriseListItem> ChildEnterprises { get; set; }
        public List<CmsPersonListItem> Persons   { get; set; }
        public List<CmsDocCategoryListItem> DocCategories { get; set; }
        public CmsMenu PersonMenu { get; set; }
    }
}
