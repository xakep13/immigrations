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
    public class SessionVoteEdit
    {
        public int Id { get; set; }

        public int Number { get; set; }

        public string TitleRu { get; set; }
        public string TitleUk { get; set; }
        public string TitleEn { get; set; }

        [AllowHtml]
        public string TextRu { get; set; }
        [AllowHtml]
        public string TextUk { get; set; }
        [AllowHtml]
        public string TextEn { get; set; }

        [AllowHtml]
        public string ResultRu { get; set; }
        [AllowHtml]
        public string ResultUk { get; set; }
        [AllowHtml]
        public string ResultEn { get; set; }

        public int For { get; set; }
        public int Against { get; set; }
        public int Abstained { get; set; }
        public int NotVote { get; set; }
        public int Absent { get; set; }

        public VoteResult Result { get; set; }

        public string DocTitle { get; set; }
        public int? DocId { get; set; }
        public int SessionId { get; set; }
    }
}
