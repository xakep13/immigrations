using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Bissoft.CouncilCMS.Web
{
    public class AppControllerFactory : DefaultControllerFactory
    {
        private LocalControllerActivator localActivator;
        private string projectName;
        private string settingsName;

        public AppControllerFactory(string projectName, string settingsName = null) : base()
        {
            this.localActivator = new LocalControllerActivator();
            this.projectName = projectName;
            this.settingsName = settingsName ?? "CmsSettings";
        }

        public override IController CreateController (RequestContext requestContext, string controllerName)
        {
            controllerName = controllerName ?? requestContext.RouteData.Values["controller"].ToString();
            var controllerType = GetControllerType(requestContext, controllerName);
            var controller = localActivator.Create(requestContext, controllerType, this.projectName, this.settingsName);

            if (controller != null)
                return controller;
            else
                throw new HttpException(404, "Page Not Found");
        }
        public override void ReleaseController(IController controller)
        {
            IDisposable dispose = controller as IDisposable;

            if (dispose != null)
            {
                dispose.Dispose();
            }
        }
    }
}
