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
    public class CmsLogin
    {
        [Required(ErrorMessageResourceType = typeof(Locals), ErrorMessageResourceName = "valSimpleRequired")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(Locals), ErrorMessageResourceName = "valSimpleRequired")]
        public string Password { get; set; }

    }
}
