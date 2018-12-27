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
    public class DocListItem
    {
        public int Id { get; set; }       
        public string TitleUk { get; set; }
        public string TitleRu { get; set; }
        public string TitleEn { get; set; }
        public IEnumerable<string> CategoriesUk { get; set; }
        public IEnumerable<string> CategoriesRu { get; set; }
        public IEnumerable<string> CategoriesEn { get; set; }
        public string Type { get; set; }
        public string Number { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? PublishDate { get; set; }
        public DateTime? EditedDate { get; set; }
        public DateTime? LastEditDate { get; set; }        
        public DateTime? PostDate { get; set; }
        public String CreateUser { get; set; }
        public String LastEditUser { get; set; }
        public int ViewCount { get; set; }
        public bool Deleted { get; set; }        
        public bool Published { get; set; }
    }
}
