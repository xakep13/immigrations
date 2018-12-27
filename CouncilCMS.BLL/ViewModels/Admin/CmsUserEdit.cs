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
    public class CmsUserEdit
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string TempPassword { get; set; }

        [EmailAddress(ErrorMessageResourceType = typeof(Locals), ErrorMessageResourceName = "valEmail")]
        [Required(ErrorMessageResourceType = typeof(Locals), ErrorMessageResourceName = "valSimpleRequired")]
        [Remote("EmailFree", "Users", "ControlPanel", AdditionalFields = "Id", ErrorMessageResourceType = typeof(Locals), ErrorMessageResourceName = "valSameEmail")]
        public string Email { get; set; }
        public string Phone { get; set; }

        [Required(ErrorMessageResourceType = typeof(Locals), ErrorMessageResourceName = "valSimpleRequired")]
        public string Name { get; set; }
        public string Avatar { get; set; }
        public Gender? Gender { get; set; }         
        public bool ChangePasswordOnLogin { get; set; }
        public List<int> Roles { get; set; }
        public List<SelectListItem> RoleList { get; set; }
        public List<CmsUserAllowedContent> AllowedPages { get; set; } = new List<CmsUserAllowedContent>();
        public List<CmsUserAllowedContent> AllowedPersons { get; set; } = new List<CmsUserAllowedContent>();
        public List<CmsUserAllowedContent> AllowedEnterprises { get; set; } = new List<CmsUserAllowedContent>();
        public List<CmsUserAllowedContent> AllowedArticleCategories { get; set; } = new List<CmsUserAllowedContent>();
        public List<CmsUserAllowedContent> AllowedPersonCategories { get; set; } = new List<CmsUserAllowedContent>();
        public List<CmsUserAllowedContent> AllowedEnterpriseCategories { get; set; } = new List<CmsUserAllowedContent>();
        public List<CmsUserAllowedContent> AllowedDocCategories { get; set; } = new List<CmsUserAllowedContent>();
    }

    public class CmsUserAllowedContent
    {
        public int Id { get; set; }
        public string Title { get; set; }                
    }
}
