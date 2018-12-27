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
    public class PageEdit
    {
        [Required]
        public int Id { get; set; }

        public string Type { get; set; }

        [StringLength(255, ErrorMessageResourceType = typeof(Locals), ErrorMessageResourceName = "valMaxLength")]
        public string TitleUk { get; set; }

        [StringLength(255, ErrorMessageResourceType = typeof(Locals), ErrorMessageResourceName = "valMaxLength")]
        public string TitleRu { get; set; }

        [StringLength(255, ErrorMessageResourceType = typeof(Locals), ErrorMessageResourceName = "valMaxLength")]
        public string TitleEn { get; set; }
        
        public string UrlName { get; set; }

        public string DescriptionRu { get; set; }
        public string DescriptionUk { get; set; }
        public string DescriptionEn { get; set; }

        public string EditedDate { get; set; }
        public string PublishDate { get; set; }

        public bool Published { get; set; }
        public bool ShowPublihDate { get; set; }
        public bool ShowEditDate { get; set; }
       
        public int PageTemplateId { get; set; }        
        public int? SidebarMenuId { get; set; }
        
        public List<ContentRowItem> ContentRows { get; set; }
        public List<SelectListItem> PageTemplates { get; set; }
        public List<SelectListItem> SidebarMenus { get; set; }

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
}
