using System;
using System.Collections.Generic;
using Bissoft.CouncilCMS.Core.Enums;

namespace Bissoft.CouncilCMS.Models.Entities
{
    public class Session : Entity<int>
    {
        public string TitleRu { get; set; }
        public string TitleUk { get; set; }
        public string TitleEn { get; set; }

        public string MetaTitleUk { get; set; }
        public string MetaTitleRu { get; set; }
        public string MetaTitleEn { get; set; }
        public string MetaDescriptionUk { get; set; }
        public string MetaDescriptionRu { get; set; }
        public string MetaDescriptionEn { get; set; }
        public string MetaKeywordsUk { get; set; }
        public string MetaKeywordsRu { get; set; }
        public string MetaKeywordsEn { get; set; }
    }
}