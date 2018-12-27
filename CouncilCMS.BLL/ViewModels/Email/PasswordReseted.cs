using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bissoft.CouncilCMS.BLL.ViewModels
{
    public class PasswordReseted
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime ResetDate { get; set; }
        public string TempPassword { get; set; }
        public string Link { get; set; }
    }
}
