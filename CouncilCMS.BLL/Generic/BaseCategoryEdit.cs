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
    public abstract class BaseCategoryEdit
    {
        public int Id { get; set; }                      
               
        [StringLength(255, ErrorMessageResourceType = typeof(Locals), ErrorMessageResourceName = "valMaxLength")]
        public string TitleUk { get; set; }        
        [StringLength(255, ErrorMessageResourceType = typeof(Locals), ErrorMessageResourceName = "valMaxLength")]
        public string TitleRu { get; set; }        
        [StringLength(255, ErrorMessageResourceType = typeof(Locals), ErrorMessageResourceName = "valMaxLength")]
        public string TitleEn { get; set; }

        public string UrlName { get; set; }

        [AllowHtml]
        public string DescriptionUk { get; set; }
        [AllowHtml]
        public string DescriptionRu { get; set; }
        [AllowHtml]
        public string DescriptionEn { get; set; }
       
        public string Image { get; set; }
        
        public int Priority { get; set; }
        public bool ShowSearchForm { get; set; }
        
        public int? SidebarMenuId { get; set; }
        public int? ParentCategoryId { get; set; }
        public int? RelatedCategoryId { get; set; }
        public int? TemplateId { get; set; }       

        public List<SelectListItem> RelatedCategories { get; set; }
        public List<SelectListItem> SidebarMenus { get; set; }
        public List<SelectListItem> Templates { get; set; }
        public List<SelectListItem> Users { get; set; }
        public List<SelectListItem> Roles { get; set; }
        public List<AllowedUser> AllowedUsers { get; set; }
        public List<AllowedRole> AllowedRoles { get; set; }


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
