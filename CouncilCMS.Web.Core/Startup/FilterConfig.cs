using Bissoft.CouncilCMS.Web.Core;
using System.Web;
using System.Web.Mvc;

namespace Bissoft.CouncilCMS.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new RedirectToLanguageFilter());
            filters.Add(new HandleErrorAttribute());
        }
    }
}
