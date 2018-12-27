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
    public class ItemContent
    {
        public int ItemId { get; set; }
        public ContentRowType Type { get; set; }
        public List<ContentRowItem> ContetRows { get; set; }        
    }
}
