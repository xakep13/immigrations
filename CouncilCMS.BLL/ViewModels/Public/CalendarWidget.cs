using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bissoft.CouncilCMS.BLL.ViewModels
{
    public class CalendarWidget
    {
        public DateTime FirstDate { get; set; }
        public DateTime LastDate { get; set; }
        public List<DateTime> Events { get; set; }
    }
}
