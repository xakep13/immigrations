using System;
using System.Web.Mvc;
using Bissoft.CouncilCMS.BLL.Services;
using Bissoft.CouncilCMS.Core.Enums;
using Bissoft.CouncilCMS.BLL.ViewModels;
using System.Threading.Tasks;
using xTab.Tools.Helpers;
using System.Linq;
using Bissoft.CouncilCMS.BLL;
using System.Web;
using xTab.Tools.Extensions;

namespace Bissoft.CouncilCMS.Web.Controllers
{
    public class ErrorController : BaseCmsController
    {
   
   
        public ActionResult NotFound()
        {
            return View();            
        }

        public ActionResult ServerError()
        {
            return View();
        }
    }
}