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
    public class CmsSettingsEdit
    {
        [AllowHtml]
        public string TitleUk { get; set; }
        [AllowHtml]
        public string SubTitleUk { get; set; }
        public string TitlePrefixUk { get; set; }

        [AllowHtml]
        public string TitleRu { get; set; }
        [AllowHtml]
        public string SubTitleRu { get; set; }
        public string TitlePrefixRu { get; set; }

        [AllowHtml]
        public string TitleEn { get; set; }
        [AllowHtml]
        public string SubTitleEn { get; set; }
        public string TitlePrefixREn { get; set; }

        public string LogoUk { get; set; }
        public string LogoRu { get; set; }
        public string LogoEn { get; set; }

        public string SubLogoUk { get; set; }
        public string SubLogoRu { get; set; }
        public string SubLogoEn { get; set; }

        public int? Type { get; set; }

        public int? MayorCategoryId { get; set; }

        public bool HideMayor { get; set; }
        [AllowHtml]
        public string MayorTitleUk { get; set; }
        [AllowHtml]
        public string MayorTitleRu { get; set; }
        [AllowHtml]
        public string MayorTitleEn { get; set; }
        public string MayorImage { get; set; }
		public string BanerImage { get; set; }
		[AllowHtml]
        public string MayorNameUk { get; set; }
        [AllowHtml]
        public string MayorNameRu { get; set; }
        [AllowHtml]
        public string MayorNameEn { get; set; }

        [AllowHtml]
        public string HotlineSubTitleUk { get; set; }
        [AllowHtml]
        public string HotlineSubTitleRu { get; set; }
        [AllowHtml]
        public string HotlineSubTitleEn { get; set; }
        [AllowHtml]
        public string HotlineTitleUk { get; set; }
        [AllowHtml]
        public string HotlineTitleRu { get; set; }
        [AllowHtml]
        public string HotlineTitleEn { get; set; }
        [AllowHtml]
        public string HotlinePhone { get; set; }
        [AllowHtml]
        public string HotlineEmail { get; set; }
        [AllowHtml]
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

        public string MailgunApiKey { get; set; }
        public string MailgunDomain { get; set; }
        public string MailgunFromEmail { get; set; }

        public string OpenDataApiKey { get; set; }

        public string DisqusName { get; set; }

        [AllowHtml]
        public string GoogleAnaliticsCode { get; set; }

        [AllowHtml]
        public string YandexMetricsCode { get; set; }

        public string HeadSection { get; set; }
        public string BodySection { get; set; }

        public string FbLink { get; set; }
        public string TwLink { get; set; }
        public string YtLink { get; set; }
        public string IgLink { get; set; }

        public string DefaultEmail { get; set; }
        public string DefaultEmailName { get; set; }
        public string DefaultEmailHost { get; set; }
        public int DefaultEmailPort { get; set; }
        public string DefaultEmailUser { get; set; }
        public string DefaultEmailPassword { get; set; }
        public bool DefaultEmailSsl { get; set; }

        public string FeedbackEmail { get; set; }

        public ShowMenuItemMode ShowMenuItemMode { get; set; }

        [AllowHtml]
        public string WeatherWidgetHtml { get; set; }

        public string Favicon { get; set; }

        public List<SelectListItem> Categories { get; set; }

        public bool IsEmailActive { get; set; }
        public bool IsPhoneActive { get; set; }

        public string FacebookAppId { get; set; }
        public string FacebookSecretKey { get; set; }
        public string FacebookPublicPageId { get; set; }

        public string FacebookUserAccessToken { get; set; }
        public string FacebookUserAccessTokenExpires { get; set; }

        [AllowHtml]
		public string OldSite { get; set; }

	}
}
