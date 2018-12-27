using System;
using System.Collections.Generic;

namespace Bissoft.CouncilCMS.DAL.Entities
{
    public class Doc : BaseEntity<int>
    {
        public string Number { get; set; }

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

        public string Text { get; set; }
        public string CleanText { get; set; }

        public string KeywordsUk { get; set; }
        public string KeywordsRu { get; set; }
        public string KeywordsEn { get; set; }

        public bool ShowPublihDate { get; set; }
        public bool ShowEditDate { get; set; }

        public DateTime? AcceptDate { get; set; }
        public DateTime? PostDate { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime? PublishDate { get; set; }
        public DateTime? LastEditDate { get; set; }
        public DateTime? EditedDate { get; set; }

        public int? CreateUserId { get; set; }
        public int? LastEditUserId { get; set; }

        public bool Published { get; set; }
        public bool Saved { get; set; }
        public int ViewCount { get; set; }

        public int DocTypeId { get; set; }
       
        public virtual DocType DocType { get; set; }     
        public virtual ICollection<DocCategory> Categories { get; set; } = new List<DocCategory>();
        public virtual ICollection<DocFile> Files { get; set; } = new List<DocFile>();
        public virtual ICollection<Person> Persons { get; set; } = new List<Person>();
        public virtual ICollection<Enterprise> Enterprises { get; set; } = new List<Enterprise>();
        public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();
        public virtual ICollection<SessionAgenda> SessionAgendas { get; set; } = new List<SessionAgenda>();
        public virtual ICollection<SessionVote> SessionVotes { get; set; } = new List<SessionVote>();
    }
}