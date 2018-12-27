using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bissoft.CouncilCMS.BLL.ViewModels.Common
{
   public class CaptchaResponse
    {
        public bool Success { get; set; }

        public List<string> ErrorCodes { get; set; }
    }
}
