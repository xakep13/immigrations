using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bissoft.CouncilCMS.BLL.ViewModels.Admin
{
    public class QuestionListItem
    {
        public int Id { get; set; }
        public int PollId { get; set; }

        public string TitleRu { get; set; }
        public string TitleUk { get; set; }
        public string TitleEn { get; set; }

        public int Position { get; set; }
        public int CountClick { get; set; }
        public string Color { get; set; }
    }
}
