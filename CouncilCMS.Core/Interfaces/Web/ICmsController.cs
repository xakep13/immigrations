using System;
using System.Web.Mvc;

namespace Bissoft.CouncilCMS.Core.Interfaces
{
    public interface ICmsController : IController
    {
        string RenderToString(String ViewName, Object Model);        
        string ConnectionString { get; }
    }
}
