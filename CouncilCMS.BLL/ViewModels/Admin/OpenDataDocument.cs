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
    public class OpenDataDocument
    {
        public string Id { get; set; }
        public string ApiKey { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Refresh { get; set; }
       // public string Language { get; set; }
        public List<String> Keywords { get; set; }
        public string Category { get; set; }
        public List<ResponsiblePerson> Responsible { get; set; }
        public List<OpenDataFile> Files { get; set; }
    }
    public class ResponsiblePerson
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
    public class OpenDataFile
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
