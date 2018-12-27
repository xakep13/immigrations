using System;
using System.Configuration;
using System.Web.Mvc;
using Bissoft.CouncilCMS.Core.Enums;
using Bissoft.CouncilCMS.BLL.ViewModels;
using Bissoft.CouncilCMS.BLL.Services;
using System.Collections.Generic;
using Bissoft.CouncilCMS.Web.Areas.ControlPanel.Controllers;
using System.Net;
using Bissoft.CouncilCMS.Web.Controllers;

namespace Bissoft.CouncilCMS.Web.Core.Controllers
{
    public class ErrorController : BaseCmsController
    {        
        public ActionResult NotFound()
        {
            Response.TrySkipIisCustomErrors = true;
            Response.StatusCode = (int)HttpStatusCode.NotFound;
            return View();
        }

        public ActionResult ServerError()
        {
            Response.TrySkipIisCustomErrors = true;
            Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return View();
        }
    }
}