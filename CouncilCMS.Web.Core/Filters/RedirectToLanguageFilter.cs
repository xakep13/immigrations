using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Mvc;

namespace Bissoft.CouncilCMS.Web.Core
{
    public class RedirectToLanguageFilter : IActionFilter
    {
        public RedirectToLanguageFilter()
        {
           
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
           
            var routeValues = filterContext.RequestContext.RouteData.Values;

            // If there is no value for culture, redirect
            if (routeValues != null && !routeValues.ContainsKey("area") && !routeValues.ContainsKey("lang"))
            {
                string culture = filterContext.RequestContext.HttpContext.Request.Cookies["lang"] != null ? filterContext.RequestContext.HttpContext.Request.Cookies["lang"].Value : Thread.CurrentThread.CurrentCulture.Name.ToLower();

                routeValues.Add("lang", culture);

                filterContext.Result = new RedirectToRouteResult(routeValues);
            }
            
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            // Do nothing
        }

        public Task<HttpResponseMessage> ExecuteActionFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            throw new NotImplementedException();
        }
    }
}
