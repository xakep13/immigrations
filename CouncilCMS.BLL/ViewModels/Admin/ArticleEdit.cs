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
    public class ArticleEdit
    {
        [Required]
        public int Id { get; set; }

        [StringLength(255, ErrorMessageResourceType = typeof(Locals), ErrorMessageResourceName = "valMaxLength")]
        public string TitleUk { get; set; }

        [StringLength(255, ErrorMessageResourceType = typeof(Locals), ErrorMessageResourceName = "valMaxLength")]
        public string TitleRu { get; set; }

        [StringLength(255, ErrorMessageResourceType = typeof(Locals), ErrorMessageResourceName = "valMaxLength")]
        public string TitleEn { get; set; }

        public string DescriptionRu { get; set; }
        public string DescriptionUk { get; set; }
        public string DescriptionEn { get; set; }

        public string Image { get; set; }

        public string CreateDate { get; set; }
        public string PublishDate { get; set; }
        public string EventDate { get; set; }
        public string EditedDate { get; set; }

        public bool Published { get; set; }
        public bool FacebookSharing { get; set; }
        public String FacebookUserToken { get; set; } 
        public bool AllowComments { get; set; }
        public bool ExportToOpenData { get; set; }
        public bool ShowPublihDate { get; set; }
        public bool ShowEditDate { get; set; }
        
        public List<CheckedListItem> ArticleCategories { get; set; }
        public List<ContentRowItem> ContentRows { get; set; }

      
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

        #region OpenData

        public string OpenDataId { get; set; }

        [StringLength(255, ErrorMessageResourceType = typeof(Locals), ErrorMessageResourceName = "valMaxLength")]
        [RequiredIf("ExportToOpenData", true, ErrorMessageResourceType = typeof(Locals), ErrorMessageResourceName = "valSimpleRequired")]
        public string OpenDataTitle { get; set; }

        [RequiredIf("ExportToOpenData", true, ErrorMessageResourceType = typeof(Locals), ErrorMessageResourceName = "valSimpleRequired")]
        public string OpenDataDescription { get; set; }

        [RequiredIf("ExportToOpenData", true, ErrorMessageResourceType = typeof(Locals), ErrorMessageResourceName = "valSimpleRequired")]
        public string OpenDataKeywords { get; set; }

        [RequiredIf("ExportToOpenData", true, ErrorMessageResourceType = typeof(Locals), ErrorMessageResourceName = "valSimpleRequired")]
        public string OpenDataRefresh { get; set; }

        [RequiredIf("ExportToOpenData", true, ErrorMessageResourceType = typeof(Locals), ErrorMessageResourceName = "valSimpleRequired")]
        public string OpenDataCategory { get; set; }

        [RequiredIf("ExportToOpenData", true, ErrorMessageResourceType = typeof(Locals), ErrorMessageResourceName = "valSimpleRequired")]
        public string OpenDataName { get; set; }

        [RequiredIf("ExportToOpenData", true, ErrorMessageResourceType = typeof(Locals), ErrorMessageResourceName = "valSimpleRequired")]
        public string OpenDataEmail { get; set; }

        #endregion      
    }
}
