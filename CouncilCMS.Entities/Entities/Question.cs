using Bissoft.CouncilCMS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bissoft.CouncilCMS.Entities.Entities
{
    public class Question : BaseEntity<int>
    {
        public string NameRu { get; set; }
        public string NameUk { get; set; }
        public string NameEn { get; set; }

        public int Position { get; set; }
        public int ClickCount { get; set; }
        public string Color { get; set; }

        public int? PollID { get; set; }
        public Poll Poll { get; set; }
    }
}
