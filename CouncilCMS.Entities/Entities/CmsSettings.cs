using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bissoft.CouncilCMS.DAL.Entities
{
    public class CmsSettings : BaseEntity<int>
    {
        public string TitleUk { get; set; }
        public string SubTitleUk { get; set; }
        public string TitlePrefixUk { get; set; }

        public string TitleRu { get; set; }
        public string SubTitleRu { get; set; }
        public string TitlePrefixRu { get; set; }

        public string TitleEn { get; set; }
        public string SubTitleEn { get; set; }
        public string TitlePrefixREn { get; set; }

        public string LogoUk { get; set; }
        public string LogoRu { get; set; }
        public string LogoEn { get; set; }

        public string SubLogoUk { get; set; }
        public string SubLogoRu { get; set; }
        public string SubLogoEn { get; set; }

        public int? Type { get; set; }

        public bool HideMayor { get; set; }
        public int? MayorCategoryId { get; set; }
        public string MayorTitleUk { get; set; }
        public string MayorTitleRu { get; set; }
        public string MayorTitleEn { get; set; }
        public string MayorImage { get; set; }
		public string BanerImage { get; set; }
        public string MayorNameUk { get; set; }
        public string MayorNameRu { get; set; }
        public string MayorNameEn { get; set; }

        public string HotlineTitleUk { get; set; }
        public string HotlineTitleRu { get; set; }
        public string HotlineTitleEn { get; set; }
        public string HotlineSubTitleUk { get; set; }
        public string HotlineSubTitleRu { get; set; }
        public string HotlineSubTitleEn { get; set; }
        public string HotlinePhone { get; set; }
        public string HotlineEmail { get; set; }
        public string HotlineColor { get; set; }

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
        public string ThemeName { get; set; }

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
        public string GoogleCaptchaApiSecret { get; set; }
        public string FeedburnerName { get; set; }

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

        public string FbLink { get; set; }
        public string TwLink { get; set; }
        public string YtLink { get; set; }
        public string IgLink { get; set; }

        public string FeedbackEmail { get; set; }

        public string Favicon { get; set; }

        public int ShowMenuItemMode { get; set; }

        //
        public string WeatherWidgetHtml { get; set; }
        //

        public DateTime? DateLastEdit { get; set; }

        public virtual ArticleCategory MayorCategory { get; set; }
		//public int? LastEditUserId { get; set; }

		public bool IsEmailActive { get; set; }
		public bool IsPhoneActive { get; set; }

        public string FacebookAppId { get; set; }
        public string FacebookSecretKey { get; set; }
        public string FacebookPublicPageId { get; set; }

        public string FacebookUserAccessToken { get; set; }
        public string FacebookUserAccessTokenExpires { get; set; }

        public string OldSite { get; set; }
    }
}
