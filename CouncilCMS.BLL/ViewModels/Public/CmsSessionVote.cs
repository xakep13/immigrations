using System;
using System.Collections.Generic;
using Bissoft.CouncilCMS.Core.Enums;
using Bissoft.CouncilCMS.Core.Globalization;

namespace Bissoft.CouncilCMS.BLL.ViewModels
{
    public class CmsSessionVoteList
    {
        public int SessionId { get; set; }
        public String SessionTitle { get; set; }
        public List<CmsSessionVoteListItem> Votes { get; set; }
        public int Page { get; set; }
        public int Count { get; set; }
    }

    public class CmsSessionVoteListItem
    {
        public int Id { get; set; }

        public int? DocId { get; set; }

        public int Number { get; set; }

        public int For { get; set; }
        public int Against { get; set; }
        public int Abstained { get; set; }
        public int NotVote { get; set; }
        public int Absent { get; set; }

        public VoteResult Result { get; set; }

        public string Date { get; set; }

        public string Title { get; set; }
    }

    public class CmsSessionVote
    {
        public int SessionId { get; set; }
        public String SessionTitle { get; set; }

        public int Id { get; set; }

        public int Number { get; set; }

        public string Title { get; set; }
        
        public string Text { get; set; }
      
        public string ResultText { get; set; }

        public int For { get; set; }
        public int Against { get; set; }
        public int Abstained { get; set; }
        public int NotVote { get; set; }
        public int Absent { get; set; }

        public VoteResult Result { get; set; }

        public string DocNumber { get; set; }        
        public string DocPostDate { get; set; }
        public string DocAcceptDate { get; set; }

        public string DocTitle { get; set; }
        public int? DocId { get; set; }
    }
}
