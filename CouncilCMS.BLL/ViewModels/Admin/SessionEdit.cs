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
    public class SessionEdit
    {
        public int Id { get; set; }        

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

        public bool Ended { get; set; }

        public int? DecreeId { get; set; }
        public int? ReglamentId { get; set; }
        public int? AgendaAdditionId { get; set; }
        public int? ProtocolId { get; set; }

        public string DecreeTitle { get; set; }
        public string ReglamentTitle { get; set; }
        public string AgendaAdditionTitle { get; set; }
        public string ProtocolTitle { get; set; }

        public string AgendaFile { get; set; }

        [AllowHtml]
        public String EmbedVideo { get; set; }

        [AllowHtml]
        public String EmbedAudio { get; set; }

        public bool Published { get; set; }
        
        public string PublishDate { get; set; }        
        public string SessionDate { get; set; }

        public List<SessionVoteListItem> Votes { get; set; }
        public List<SessionAgendaItem> Agenda { get; set; }
        public List<SessionProjectItem> Projects { get; set; }
    }

    public class SessionProjectItem
    {
        public int Id { get; set; }
        public string DocTitle { get; set; }
        public int DocId { get; set; }
        public int SessionId { get; set; }
        public bool IsAgenda { get; set; }
    }

    public class SessionAgendaItem
    {
        public int Id { get; set; }

        public int Number { get; set; }

        public string TitleRu { get; set; }
        public string TitleUk { get; set; }
        public string TitleEn { get; set; }

        public int Status { get; set; }

        public string DocTitle { get; set; }
        public int? DocId { get; set; }
        public int SessionId { get; set; }
    }    
}
