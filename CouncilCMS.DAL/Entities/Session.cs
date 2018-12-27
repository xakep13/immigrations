using System;
using System.Collections.Generic;
using Bissoft.CouncilCMS.Core.Enums;

namespace Bissoft.CouncilCMS.DAL.Entities
{
    public class Session : BaseEntity<int>
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

        public bool Ended { get; set; }

        public string AgendaFile { get; set; }
        
        public int? DecreeId { get; set; }
        public int? ReglamentId { get; set; }        
        public int? AgendaAdditionId { get; set; }
        public int? ProtocolId { get; set; }  
        
        public String EmbedVideo { get; set; }
        public String EmbedAudio { get; set; }

        public bool Published { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime? PublishDate { get; set; }        
        public DateTime? LastEditDate { get; set; }
        public DateTime? SessionDate { get; set; }

        public virtual Doc Decree { get; set; }
        public virtual Doc Reglament { get; set; }
        public virtual Doc AgendaAddition { get; set; }
        public virtual Doc Protocol { get; set; }

        public virtual ICollection<Doc> Projects { get; set; } = new List<Doc>();
        public virtual ICollection<SessionAgenda> Agenda { get; set; } = new List<SessionAgenda>();
        public virtual ICollection<SessionVote> Votes { get; set; } = new List<SessionVote>();         
    }
}