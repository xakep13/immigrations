using System.Web.Mvc;

namespace Bissoft.CouncilCMS.Web.Areas.ControlPanel
{
    public class ControlPanelAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "ControlPanel";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            
            context.MapRoute(
                 name: "FacebookGetExternalAccessToken",
                 url: "{lang}/ControlPanel/FacebookLogin/GetExternalAccessToken/{code}",
                 defaults: new
                 {
                     controller = "FacebookLogin",
                     action = "GetExternalAccessToken",
                     code = UrlParameter.Optional
                 },
                 namespaces: new string[] {
                        "Bissoft.CouncilCMS.Web.Areas.ControlPanel.Controllers"
                 }
            );

            context.MapRoute(
                "ControlPanelLocal",
                "{lang}/ControlPanel/{controller}/{action}/{id}",
                new { action = "List", controller = "Pages", lang = "uk", id = UrlParameter.Optional },
                new { lang = @"uk|ru|en" },
                new string[] { "Bissoft.CouncilCMS.Web.Areas.ControlPanel.Controllers" }
            );

            context.MapRoute(
                "ControlPanelDefault",
                "ControlPanel/{controller}/{action}/{id}",
                new { action = "List", controller = "Pages", id = UrlParameter.Optional },
                new string[] { "Bissoft.CouncilCMS.Web.Areas.ControlPanel.Controllers" }
            );
        }
    }
}