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
    public abstract class ContentEdit
    {
        public int Id { get; set; }
        public int Position { get; set; }
        public ContentType Type { get; set; }
        public int ContentColumnId { get; set; }
        public bool IsNew { get; set; }
        public int? FormId { get; set; }
        public List<SelectListItem> Forms { get; set; }       
        
    }
}
