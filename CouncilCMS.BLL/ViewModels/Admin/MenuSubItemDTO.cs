using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bissoft.CouncilCMS.BLL.ViewModels.Admin
{
    public class MenuSubItemDTO 
    {
        public Int32[] MenuItems { get; set; }
        public Int32 ParentId { get; set; }
        public Int32? MenuId { get; set; }
    }
}
