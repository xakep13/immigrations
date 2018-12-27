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
   public class PersonEdit
    {
        public int Id { get; set; }

        [StringLength(255, ErrorMessageResourceType = typeof(Locals), ErrorMessageResourceName = "valMaxLength")]
        public string TitleUk { get; set; }
        [StringLength(255, ErrorMessageResourceType = typeof(Locals), ErrorMessageResourceName = "valMaxLength")]
        public string TitleRu { get; set; }
        [StringLength(255, ErrorMessageResourceType = typeof(Locals), ErrorMessageResourceName = "valMaxLength")]
        public string TitleEn { get; set; }

        [AllowHtml]
        public string PostRu { get; set; }
        [AllowHtml]
        public string PostUk { get; set; }
        [AllowHtml]
        public string PostEn { get; set; }

        [AllowHtml]
        public string ProfessionRu { get; set; }
        [AllowHtml]
        public string ProfessionUk { get; set; }
        [AllowHtml]
        public string ProfessionEn { get; set; }

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
        public string ContactEmailsUk { get; set; }
        [AllowHtml]
        public string ContactEmailsRu { get; set; }
        [AllowHtml]
        public string ContactEmailsEn { get; set; }

        [AllowHtml]
        public string ContactPhonesUk { get; set; }
        [AllowHtml]
        public string ContactPhonesRu { get; set; }
        [AllowHtml]
        public string ContactPhonesEn { get; set; }

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

        [AllowHtml]
        public string ReceptionAddressRu { get; set; }
        [AllowHtml]
        public string ReceptionAddressUk { get; set; }
        [AllowHtml]
        public string ReceptionAddressEn { get; set; }

        [AllowHtml]
        public string ReceptionDatesRu { get; set; }
        [AllowHtml]
        public string ReceptionDatesUk { get; set; }
        [AllowHtml]
        public string ReceptionDatesEn { get; set; }

        [AllowHtml]
        public string ReceptionTimeRu { get; set; }
        [AllowHtml]
        public string ReceptionTimeUk { get; set; }
        [AllowHtml]
        public string ReceptionTimeEn { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessageResourceType = typeof(Locals), ErrorMessageResourceName = "valEmail")]
        public string NotificationEmail { get; set; }
        public string NotificationPhone { get; set; }

        public string Image { get; set; }
        public string Birthday { get; set; }        
        public string FbLink { get; set; }
        public string TwLink { get; set; }
        public string GpLink { get; set; }
        public string YtLink { get; set; }
        public string IgLink { get; set; }

        [AllowHtml]
        public string Facebook { get; set; }
        [AllowHtml]
        public string Twitter { get; set; }

        public bool Published { get; set; }
        public bool ShowPublihDate { get; set; }
        public bool ShowEditDate { get; set; }
        public bool HasContactForm { get; set; }
        public bool HasReceptionForm { get; set; }

        public string EditedDate { get; set; }
        public string PublishDate { get; set; }

        public int? SidebarMenuId { get; set; }

        public List<PersonCategoryItem> Categories { get; set; }
        public List<SelectListItem> DeputyFractions { get; set; }
        public List<SelectListItem> SidebarMenus { get; set; }
        public List<ContentRowItem> ContentRows { get; set; }
        public List<SelectListItem> CategoryList { get; set; }
        public List<SelectListItem> Users { get; set; }
        public List<AllowedUser> AllowedUsers { get; set; }


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
    public class PersonCategoryItem
    {
        public int CategoryId { get; set; }
        public int PersonId { get; set; }
        public string Title { get; set; }
        public int Position { get; set; }
    }
}
