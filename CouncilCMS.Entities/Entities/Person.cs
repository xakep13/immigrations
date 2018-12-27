using System;
using System.Collections.Generic;

namespace Bissoft.CouncilCMS.DAL.Entities
{
   public class Person : BaseAuditionEntity
    {
        public string PostRu { get; set; }
        public string PostUk { get; set; }
        public string PostEn { get; set; }

        public string ProfessionRu { get; set; }
        public string ProfessionUk { get; set; }
        public string ProfessionEn { get; set; }

        public string DescriptionRu { get; set; }
        public string DescriptionUk { get; set; }
        public string DescriptionEn { get; set; }

        public string KeywordsUk { get; set; }
        public string KeywordsRu { get; set; }
        public string KeywordsEn { get; set; }

        public string ContactAddressRu { get; set; }
        public string ContactAddressUk { get; set; }
        public string ContactAddressEn { get; set; }
        public string ContactEmailsUk { get; set; }
        public string ContactEmailsRu { get; set; }
        public string ContactEmailsEn { get; set; }
        public string ContactPhonesUk { get; set; }
        public string ContactPhonesRu { get; set; }
        public string ContactPhonesEn { get; set; }

        public string ReceptionAddressRu { get; set; }
        public string ReceptionAddressUk { get; set; }
        public string ReceptionAddressEn { get; set; }
        public string ReceptionDatesRu { get; set; }
        public string ReceptionDatesUk { get; set; }
        public string ReceptionDatesEn { get; set; }
        public string ReceptionTimeRu { get; set; }
        public string ReceptionTimeUk { get; set; }
        public string ReceptionTimeEn { get; set; }

        public string NotificationEmail { get; set; }
        public string NotificationPhone { get; set; }

        public string UrlNameRu { get; set; }
        public string UrlNameUk { get; set; }
        public string UrlNameEn { get; set; }

        public bool ShowSecondPage { get; set; }
        public string SecondPageTitleUk { get; set; }
        public string SecondPageTitleRu { get; set; }
        public string SecondPageTitleEn { get; set; }
        public string SecondPageTextUk { get; set; }
        public string SecondPageTextRu { get; set; }
        public string SecondPageTextEn { get; set; }

        public string MetaTitleUk { get; set; }
        public string MetaTitleRu { get; set; }
        public string MetaTitleEn { get; set; }
        public string MetaDescriptionUk { get; set; }
        public string MetaDescriptionRu { get; set; }
        public string MetaDescriptionEn { get; set; }
        public string MetaKeywordsUk { get; set; }
        public string MetaKeywordsRu { get; set; }
        public string MetaKeywordsEn { get; set; }

        public int? DeputyFractionId { get; set; }        

        public string FbLink { get; set; }
        public string TwLink { get; set; }
        public string GpLink { get; set; }
        public string YtLink { get; set; }
        public string IgLink { get; set; }
       
        public string Image { get; set; }
        public DateTime? Birthday { get; set; }
        public bool HasContactForm { get; set; }
        public bool HasReceptionForm { get; set; }
        public bool ShowPublihDate { get; set; }
        public bool ShowEditDate { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }

        public bool Saved { get; set; }
 
        public DateTime? EditedDate { get; set; }
        public int? ViewCount { get; set; }
        public int? SidebarMenuId { get; set; }
        public int? CurrentMenuItemId { get; set; }

        public virtual Enterprise DeputyFraction { get; set; }
        public virtual ICollection<PersonPersonCategory> Categories { get; set; } = new List<PersonPersonCategory>();
        public virtual ICollection<EnterprisePerson> Enterprises { get; set; } = new List<EnterprisePerson>();
        public virtual ICollection<ContentRow> ContentRows { get; set; } = new List<ContentRow>();
        public virtual ICollection<Doc> Docs { get; set; } = new List<Doc>();
        public virtual ICollection<CmsUser> AllowedUsers { get; set; } = new List<CmsUser>();
        public virtual Menu SidebarMenu { get; set; }
        public virtual MenuItem CurrentMenuItem { get; set; }       
    }
}
