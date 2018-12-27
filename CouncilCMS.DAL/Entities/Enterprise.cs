using System;
using System.Collections.Generic;

namespace Bissoft.CouncilCMS.DAL.Entities
{
    public class Enterprise : BaseEntity<int>
    {
        public string TitleRu { get; set; }
        public string TitleUk { get; set; }
        public string TitleEn { get; set; }

        public string DescriptionRu { get; set; }
        public string DescriptionUk { get; set; }
        public string DescriptionEn { get; set; }

        public string KeywordsUk { get; set; }
        public string KeywordsRu { get; set; }
        public string KeywordsEn { get; set; }

        public string ContactAddressRu { get; set; }
        public string ContactAddressUk { get; set; }
        public string ContactAddressEn { get; set; }
        public string ContactDatesRu { get; set; }
        public string ContactDatesUk { get; set; }
        public string ContactDatesEn { get; set; }
        public string ContactTimeRu { get; set; }
        public string ContactTimeUk { get; set; }
        public string ContactTimeEn { get; set; }
        public string ContactEmailsUk { get; set; }
        public string ContactEmailsRu { get; set; }
        public string ContactEmailsEn { get; set; }
        public string ContactPhonesUk { get; set; }
        public string ContactPhonesRu { get; set; }
        public string ContactPhonesEn { get; set; }
        public string ContactWebsiteUk { get; set; }
        public string ContactWebsiteRu { get; set; }
        public string ContactWebsiteEn { get; set; }

        public bool ShowSecondPage { get; set; }
        public string SecondPageTitleUk { get; set; }
        public string SecondPageTitleRu { get; set; }
        public string SecondPageTitleEn { get; set; }
        public string SecondPageTextUk { get; set; }
        public string SecondPageTextRu { get; set; }
        public string SecondPageTextEn { get; set; }

        public string NotificationEmail { get; set; }
        public string NotificationPhone { get; set; }

        public string UrlNameRu { get; set; }
        public string UrlNameUk { get; set; }
        public string UrlNameEn { get; set; }
       
        public string MetaTitleUk { get; set; }
        public string MetaTitleRu { get; set; }
        public string MetaTitleEn { get; set; }
        public string MetaDescriptionUk { get; set; }
        public string MetaDescriptionRu { get; set; }
        public string MetaDescriptionEn { get; set; }
        public string MetaKeywordsUk { get; set; }
        public string MetaKeywordsRu { get; set; }
        public string MetaKeywordsEn { get; set; }

        public string PersonsTitleUk { get; set; }
        public string PersonsTitleRu { get; set; }
        public string PersonsTitleEn { get; set; }

        public string FbLink { get; set; }
        public string TwLink { get; set; }
        public string GpLink { get; set; }
        public string YtLink { get; set; }
        public string IgLink { get; set; }
       
        public string Image { get; set; }
        public bool HasContactForm { get; set; }
        public bool ShowPublihDate { get; set; }
        public bool ShowEditDate { get; set; }

        public string Facebook { get; set; }
        public string Twitter { get; set; }
        
        public bool Published { get; set; }
        public bool Saved { get; set; }

        public DateTime? PublishDate { get; set; }
        public DateTime? EditedDate { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastEditDate { get; set; }

        public int ViewCount { get; set; }

        public int? TypeId { get; set; }
        public int? ParentEnterpriseId { get; set; }
        public int? SidebarMenuId { get; set; }
        public int? CreateUserId { get; set; }
        public int? LastEditUserId { get; set; }
        public int? CurrentMenuItemId { get; set; }

        public virtual Enterprise ParentEnterprise { get; set; }
        public virtual EnterpriseType Type { get; set; }
        public virtual ICollection<Enterprise> ChildEnterprises { get; set; } = new List<Enterprise>();
        public virtual ICollection<EnterpriseEnterpriseCategory> Categories { get; set; } = new List<EnterpriseEnterpriseCategory>();
        public virtual ICollection<EnterprisePerson> Persons { get; set; } = new List<EnterprisePerson>();
        public virtual ICollection<ContentRow> ContentRows { get; set; } = new List<ContentRow>();
        public virtual ICollection<Doc> Docs { get; set; } = new List<Doc>();
        public virtual ICollection<CmsUser> AllowedUsers { get; set; } = new List<CmsUser>();
        public virtual Menu SidebarMenu { get; set; }
        public virtual MenuItem CurrentMenuItem { get; set; }
    }
}
