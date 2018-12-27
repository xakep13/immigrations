using System;
using System.Collections.Generic;
using Bissoft.CouncilCMS.Core.Enums;
using Bissoft.CouncilCMS.Core.Globalization;

namespace Bissoft.CouncilCMS.BLL.ViewModels
{
    public class CmsSessionAgendaList
    {
        public int SessionId { get; set; }
        public String SessionTitle { get; set; }
        public List<CmsSessionAgendaListItem> Agenda { get; set; }
        public string AgendaFile { get; set; }
        public int? AdditionalAgendaId { get; set; }
        public string AdditionalAgendaTitle { get; set; }
    }

    public class CmsSessionAgendaListItem
    {
        public int Id { get; set; }

        public int? DocId { get; set; }

        public int Number { get; set; }        

        public string Title { get; set; }
    }
}
