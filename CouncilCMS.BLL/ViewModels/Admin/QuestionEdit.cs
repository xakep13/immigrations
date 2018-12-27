using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Bissoft.CouncilCMS.BLL.ViewModels.Admin
{
    public class QuestionEdit
    {
        public int Id { get; set; }
        public int PollId { get; set; }

        [AllowHtml]
        public string NameRu { get; set; }
        [AllowHtml]
        public string NameUk { get; set; }
        [AllowHtml]
        public string NameEn { get; set; }
        public int Position { get; set; }
        public int CountClick { get; set; }
        [AllowHtml]
        public string Color { get; set; }
    }
}
