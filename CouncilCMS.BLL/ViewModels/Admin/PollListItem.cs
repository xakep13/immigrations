using Bissoft.CouncilCMS.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Bissoft.CouncilCMS.BLL.ViewModels.Admin
{
    public class PollListItem
    {
        public int Id { get; set; }
        public string NameRu { get; set; }
        public string NameUk { get; set; }
        public string NameEn { get; set; }
        public int VoiceCount { get; set; }

        public bool ShowPoll { get; set; }

        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public List<QuestionListItem> Questions { get; set; }
    }
}
