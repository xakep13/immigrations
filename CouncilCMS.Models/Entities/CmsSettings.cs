using System;
using System.Collections.Generic;

namespace Bissoft.CouncilCMS.Models.Entities
{
    public class CmsSettings : Entity<int>
    {
        public string TitleUk { get; set; }
        public string TitlePrefixUk { get; set; }

        public string TitleRu { get; set; }
        public string TitlePrefixRu { get; set; }

        public string TitleEn { get; set; }
        public string TitlePrefixREn { get; set; }

        public string LogoUk { get; set; }
        public string LogoRu { get; set; }
        public string LogoEn { get; set; }

        public string BaseDomain { get; set; }
        public string AdminDomain { get; set; }
        public string CdnDomain { get; set; }
        public string UploadDomain { get; set; }
        
        public string DefaultColorSchema { get; set; }

        public string DefaultCulture { get; set; }
        public string Cultures { get; set; }

        public string ReleaseVersion { get; set; }
        public string ProductVersion { get; set; }
        public string DesignVersion { get; set; }

        public Double DefaultLat { get; set; } 
        public Double DefaultLng { get; set; }
        public int DefaultMapZoom { get; set; }

        public string DefaultMetaKeywordsUk { get; set; }
        public string DefaultMetaDescriptionUk { get; set; }
        public string DefaultMetaKeywordsRu { get; set; }
        public string DefaultMetaDescriptionRu { get; set; }
        public string DefaultMetaKeywordsEn { get; set; }
        public string DefaultMetaDescriptionEn { get; set; }

        public string GoogleMapApiKey { get; set; }
        public string GoogleCaptchaApiKey { get; set; }

        public string OpenDataApiKey { get; set; }

        public string DisqusName { get; set; }
        
        public string GoogleAnaliticsCode { get; set; }
        public string YandexMetricsCode { get; set; }

        public string HeadSection { get; set; }
        public string BodySection { get; set; }

        public string DefaultEmail { get; set; }
        public string DefaultEmailName { get; set; }
        public string DefaultEmailHost { get; set; }
        public int DefaultEmailPort { get; set; }
        public string DefaultEmailUser { get; set; }
        public string DefaultEmailPassword { get; set; }
        public bool DefaultEmailSsl { get; set; }

        public string Favicon { get; set; }

        public int ShowMenuItemMode { get; set; }

        public DateTime? DateLastEdit { get; set; }
        public int? LastEditUserId { get; set; }
    }
}
