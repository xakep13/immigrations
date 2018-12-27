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
    public class DocEdit
    {
        public int Id { get; set; }

        public string Number { get; set; }

        [Required(ErrorMessageResourceType = typeof(Locals), ErrorMessageResourceName = "valSimpleRequired")]
        public string TitleUk { get; set; }
        [Required(ErrorMessageResourceType = typeof(Locals), ErrorMessageResourceName = "valSimpleRequired")]
        public string TitleRu { get; set; }
        [Required(ErrorMessageResourceType = typeof(Locals), ErrorMessageResourceName = "valSimpleRequired")]
        public string TitleEn { get; set; }

        public string MetaTitleUk { get; set; }
        public string MetaTitleRu { get; set; }
        public string MetaTitleEn { get; set; }
        public string MetaKeywordsUk { get; set; }
        public string MetaKeywordsRu { get; set; }
        public string MetaKeywordsEn { get; set; }
        public string MetaDescriptionUk { get; set; }
        public string MetaDescriptionRu { get; set; }
        public string MetaDescriptionEn { get; set; }

        [AllowHtml]
        public string Text { get; set; }

        public string KeywordsUk { get; set; }
        public string KeywordsRu { get; set; }
        public string KeywordsEn { get; set; }

        public int TypeId { get; set; }

        public string CreateDate { get; set; }
        public string PublishDate { get; set; }

        public string PostDate { get; set; }
        public string AcceptDate { get; set; }

        public bool Published { get; set; }
        public bool ShowPublihDate { get; set; }
        public bool ShowEditDate { get; set; } 

        public List<CheckedListItem> Categories { get; set; }
        public List<SelectListItem> Types { get; set; }

        public List<DocPerson> DocPersons { get; set; }
        public List<DocEnterprise> DocEnterprises { get; set; }
        public List<DocFileUpload> Files { get; set; }
        public List<SelectListItem> Persons { get; set; }
        public List<SelectListItem> Enterprises { get; set; }
    }

    public class DocPerson
    {
        public int PersonId { get; set; }
        public int DocId { get; set; }
        public string Title { get; set; }
    }

    public class DocEnterprise
    {
        public int EnterpriseId { get; set; }
        public int DocId { get; set; }
        public string Title { get; set; }
    }

    public class DocFileUpload
    {
        public int Id { get; set; }

        public string Extension { get; set; }

        public int Size { get; set; }

        public string File { get; set; }

        public int DocId { get; set; }

        public string TitleUk { get; set; }

        public string TitleRu { get; set; }

        public string TitleEn { get; set; }
    }
}
