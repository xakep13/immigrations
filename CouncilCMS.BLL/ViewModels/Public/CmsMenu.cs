using System;
using System.Collections.Generic;
using Bissoft.CouncilCMS.Core.Enums;
using Bissoft.CouncilCMS.Core.Globalization;
namespace Bissoft.CouncilCMS.BLL.ViewModels
{
    public class CmsMenu
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<CmsMenuItem> Items { get; set; }
    }
}
