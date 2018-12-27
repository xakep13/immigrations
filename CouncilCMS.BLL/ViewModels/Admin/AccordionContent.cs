using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Bissoft.CouncilCMS.Core;
using Bissoft.CouncilCMS.Core.Globalization;
using Bissoft.CouncilCMS.Core.Enums;
using xTab.Tools.Attributes;

namespace Bissoft.CouncilCMS.BLL.ViewModels
{
    public class AccordionContent : ContentEdit
    {
        public string MenuId { get; set; }
        public List<SelectListItem> Menus { get; set; }
    }
}
