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
   public class EnterpriseEdit
    {
        [Required]
        public int Id { get; set; }

        [StringLength(255, ErrorMessageResourceType = typeof(Locals), ErrorMessageResourceName = "valMaxLength")]
        public string TitleUk { get; set; }
        [StringLength(255, ErrorMessageResourceType = typeof(Locals), ErrorMessageResourceName = "valMaxLength")]
        public string TitleRu { get; set; }
        [StringLength(255, ErrorMessageResourceType = typeof(Locals), ErrorMessageResourceName = "valMaxLength")]
        public string TitleEn { get; set; }

        [AllowHtml]
        public string DescriptionRu { get; set; }
        [AllowHtml]
        public string DescriptionUk { get; set; }
        [AllowHtml]
        public string DescriptionEn { get; set; }

        [AllowHtml]
        public string ContactAddressRu { get; set; }
        [AllowHtml]
        public string ContactAddressUk { get; set; }
        [AllowHtml]
        public string ContactAddressEn { get; set; }

        [AllowHtml]
        public string ContactPhonesUk { get; set; }
        [AllowHtml]
        public string ContactPhonesRu { get; set; }
        [AllowHtml]
        public string ContactPhonesEn { get; set; }

        [AllowHtml]
        public string ContactEmailsRu { get; set; }
        [AllowHtml]
        public string ContactEmailsUk { get; set; }
        [AllowHtml]
        public string ContactEmailsEn { get; set; }

        [AllowHtml]
        public string ContactDatesRu { get; set; }
        [AllowHtml]
        public string ContactDatesUk { get; set; }
        [AllowHtml]
        public string ContactDatesEn { get; set; }

        [AllowHtml]
        public string ContactTimeRu { get; set; }
        [AllowHtml]
        public string ContactTimeUk { get; set; }
        [AllowHtml]
        public string ContactTimeEn { get; set; }

        [AllowHtml]
        public string ContactWebsiteRu { get; set; }
        [AllowHtml]
        public string ContactWebsiteUk { get; set; }
        [AllowHtml]
        public string ContactWebsiteEn { get; set; }

        public bool ShowSecondPage { get; set; }
        public string SecondPageTitleRu { get; set; }
        public string SecondPageTitleUk { get; set; }        
        public string SecondPageTitleEn { get; set; }
        [AllowHtml]
        public string SecondPageTextRu { get; set; }
        [AllowHtml]
        public string SecondPageTextUk { get; set; }
        [AllowHtml]
        public string SecondPageTextEn { get; set; }

        public string PersonsTitleUk { get; set; }
        public string PersonsTitleRu { get; set; }
        public string PersonsTitleEn { get; set; }
        
        [DataType(DataType.EmailAddress, ErrorMessageResourceType = typeof(Locals), ErrorMessageResourceName = "valEmail")]
        public string NotificationEmail { get; set; }
        public string NotificationPhone { get; set; }

        public string Image { get; set; }
        public string FbLink { get; set; }
        public string TwLink { get; set; }
        public string GpLink { get; set; }
        public string YtLink { get; set; }
        public string IgLink { get; set; }
     
        public bool Published { get; set; }
        public bool ShowPublihDate { get; set; }
        public bool ShowEditDate { get; set; }
        public bool HasContactForm { get; set; }
        public bool HasReceptionForm { get; set; }

        public string EditedDate { get; set; }
        public string PublishDate { get; set; }

        [AllowHtml]
        public string Facebook { get; set; }
        [AllowHtml]
        public string Twitter { get; set; }

        public int? ParentEnterpriseId { get; set; }
        public string ParentEnterpriseName { get; set; }

        public int? TypeId { get; set; }
        public int? SidebarMenuId { get; set; }

        public List<SelectListItem> CategoryList { get; set; }
        public List<EnterpriseCategoryItem> Categories { get; set; }
        public List<EnterprisePersonItem> Persons { get; set; }                
        public List<ContentRowItem> ContentRows { get; set; }
        public List<AllowedUser> AllowedUsers { get; set; }
        public List<SelectListItem> Users { get; set; }
        public List<SelectListItem> Types { get; set; }
        public List<SelectListItem> SidebarMenus { get; set; }


        #region Meta

        [StringLength(255, ErrorMessageResourceType = typeof(Locals), ErrorMessageResourceName = "valMaxLength")]
        public string MetaTitleUk { get; set; }

        [StringLength(255, ErrorMessageResourceType = typeof(Locals), ErrorMessageResourceName = "valMaxLength")]
        public string MetaTitleRu { get; set; }

        [StringLength(255, ErrorMessageResourceType = typeof(Locals), ErrorMessageResourceName = "valMaxLength")]
        public string MetaTitleEn { get; set; }

        [StringLength(255, ErrorMessageResourceType = typeof(Locals), ErrorMessageResourceName = "valMaxLength")]
        public string MetaKeywordsUk { get; set; }

        [StringLength(255, ErrorMessageResourceType = typeof(Locals), ErrorMessageResourceName = "valMaxLength")]
        public string MetaKeywordsRu { get; set; }

        [StringLength(255, ErrorMessageResourceType = typeof(Locals), ErrorMessageResourceName = "valMaxLength")]
        public string MetaKeywordsEn { get; set; }

        [StringLength(255, ErrorMessageResourceType = typeof(Locals), ErrorMessageResourceName = "valMaxLength")]
        public string MetaDescriptionUk { get; set; }

        [StringLength(255, ErrorMessageResourceType = typeof(Locals), ErrorMessageResourceName = "valMaxLength")]
        public string MetaDescriptionRu { get; set; }

        [StringLength(255, ErrorMessageResourceType = typeof(Locals), ErrorMessageResourceName = "valMaxLength")]
        public string MetaDescriptionEn { get; set; }

        #endregion
    }
    public class EnterpriseCategoryItem
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int EnterpriseId { get; set; }
        public string Title { get; set; }
        public int Position { get; set; }
    }

    public class EnterprisePersonItem
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int EnterpriseId { get; set; }
        public string Title { get; set; }
        public int Position { get; set; }
        public string PostUk { get; set; }
        public string PostRu { get; set; }
        public string PostEn { get; set; }
        public bool IsDeputyFraction { get; set; }
    }
}
