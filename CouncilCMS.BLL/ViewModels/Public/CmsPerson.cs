using System;
using System.Collections.Generic;
using Bissoft.CouncilCMS.Core.Enums;
using Bissoft.CouncilCMS.Core.Globalization;
namespace Bissoft.CouncilCMS.BLL.ViewModels
{
    public class CmsPerson
    {
        public int Id { get; set; }    
        public string Url { get; set; }
        public string Title { get; set; }
        public string Post { get; set; }
        public string Profession { get; set; }
        public string Description { get; set; }
        public string ContactAddress { get; set; }
        public string ContactEmails { get; set; }
        public string ContactPhones { get; set; }
        public string ReceptionAddress { get; set; }
        public string ReceptionDates { get; set; }
        public string ReceptionTime { get; set; }
        public string CurrentCategoryTitle { get; set; }
        public string CurrentCategoryUrl { get; set; }
        public int CurrentCategoryId { get; set; }

        public string FbLink { get; set; }
        public string TwLink { get; set; }
        public string GpLink { get; set; }
        public string YtLink { get; set; }
        public string IgLink { get; set; }

        public string MetaTitle { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }

        public string Image { get; set; }
        public string Bithday { get; set; }

        public string Email { get; set; }
        public bool ShowContactForm { get; set; }
        public bool ShowReceptionForm { get; set; }

        public int? DeputyFractionId { get; set; }
        public string DeputyFractionName { get; set; }
        public string DeputyFractionUrl { get; set; }

        public bool ShowSecondPage { get; set; }
        public string SecondPageTitle { get; set; }
        public string SecondPageText { get; set; }

        public string PublishDate { get; set; }
        public string LastEditDate { get; set; }

        public int ViewCount { get; set; }

        public List<CmsContentRow> ContentRows { get; set; }
        public CmsMenu PersonMenu { get; set; }
        public CmsPersonReception Reception { get; set; }
        public List<CmsDocCategoryListItem> DocCategories { get; set; }

        public List<CmsEnterpriseListItem> Enterprises;
    }
}
