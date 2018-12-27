using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bissoft.CouncilCMS.BLL.ViewModels.Public
{
    public class CmsQuestion
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int CheckCount { get; set; }
        public int PollId { get; set; }
        public double Percent { get; set; }
        public string Color { get; set; }

    }
}
