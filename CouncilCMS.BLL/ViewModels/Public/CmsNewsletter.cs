using System;
using System.Collections.Generic;
using Bissoft.CouncilCMS.Core.Enums;
using Bissoft.CouncilCMS.Core.Globalization;
namespace Bissoft.CouncilCMS.BLL.ViewModels
{
    public class CmsNewsletter
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Number { get; set; }
        public string Date { get; set; }
        public string File { get; set; }
    }
}
