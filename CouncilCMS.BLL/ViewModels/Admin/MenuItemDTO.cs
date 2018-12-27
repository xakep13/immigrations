using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bissoft.CouncilCMS.BLL.ViewModels.Admin
{
    public class MenuItemDTO
    {
        public Int32 MenuId { get; set; } 
        public Int32 OldPosition { get; set; }
        public Int32 NewPosition { get; set; }
    }
}
