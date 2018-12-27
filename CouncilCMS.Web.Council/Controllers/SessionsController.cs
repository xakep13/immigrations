using System;
using System.Web.Mvc;
using Bissoft.CouncilCMS.BLL.Services;
using Bissoft.CouncilCMS.Core.Enums;

namespace Bissoft.CouncilCMS.Web.Controllers
{
    public class SessionsController : BaseCmsController
    {
        CmsSessionService sessionService;

        public SessionsController()
        {
            sessionService = new CmsSessionService(connectionString);
        }

        public ActionResult List(string sessionId = "prev", int page = 1)
        {            
            var model = sessionService.List(page);

            ViewBag.PageType = MenuItemType.CouncilSession;
            ViewBag.PageValue = sessionId == "current" ? "cur" : "prev";            
            ViewBag.SessionId = sessionId;

            return View(model);
        }

        public ActionResult Projects(string sessionId = "current", int page = 1)
        {
            var id = sessionId == "current" ? sessionService.GetCurrentSessionId() : Int32.Parse(sessionId);
            var model = sessionService.Projects(id, page);

            ViewBag.PageType = MenuItemType.CouncilSession;
            ViewBag.PageValue = sessionId == "current" ? "cur" : "prev";
            ViewBag.SessionMenu = "projects";
            ViewBag.SessionId = sessionId;

            return View(model);
        }

        public ActionResult Project(string sessionId = "current", int page = 1)
        {
            var id = sessionId == "current" ? sessionService.GetCurrentSessionId() : Int32.Parse(sessionId);
            var model = sessionService.Project(id, page);

            ViewBag.PageType = MenuItemType.CouncilSession;
            ViewBag.PageValue = sessionId == "current" ? "cur" : "prev";
            ViewBag.SessionMenu = "projects";
            ViewBag.SessionId = sessionId;

            return View(model);
        }

        public ActionResult Agenda(string sessionId = "cuurent")
        {
            var id = sessionId == "current" ? sessionService.GetCurrentSessionId() : Int32.Parse(sessionId);
            var model = sessionService.Agenda(id);

            ViewBag.PageType = MenuItemType.CouncilSession;
            ViewBag.PageValue = sessionId == "current" ? "cur" : "prev";
            ViewBag.SessionMenu = "agenda";
            ViewBag.SessionId = sessionId;

            return View(model);
        }

        public ActionResult Decree(string sessionId = "cuurent")
        {
            var id = sessionId == "current" ? sessionService.GetCurrentSessionId() : Int32.Parse(sessionId);
            var model = sessionService.Decree(id);

            ViewBag.PageType = MenuItemType.CouncilSession;
            ViewBag.PageValue = sessionId == "current" ? "cur" : "prev";
            ViewBag.SessionMenu = "decree";
            ViewBag.SessionId = sessionId;

            return View(model);
        }

        public ActionResult Reglament(string sessionId = "cuurent")
        {
            var id = sessionId == "current" ? sessionService.GetCurrentSessionId() : Int32.Parse(sessionId);
            var model = sessionService.Reglament(id);

            ViewBag.PageType = MenuItemType.CouncilSession;
            ViewBag.PageValue = sessionId == "current" ? "cur" : "prev";
            ViewBag.SessionMenu = "reglament";
            ViewBag.SessionId = sessionId;

            return View(model);
        }

        public ActionResult Protocol(string sessionId = "cuurent")
        {
            var id = sessionId == "current" ? sessionService.GetCurrentSessionId() : Int32.Parse(sessionId);
            var model = sessionService.Protocol(id);

            ViewBag.PageType = MenuItemType.CouncilSession;
            ViewBag.PageValue = sessionId == "current" ? "cur" : "prev";
            ViewBag.SessionMenu = "protocol";
            ViewBag.SessionId = sessionId;

            return View(model);
        }

        public ActionResult AdditionalAgenda(string sessionId = "cuurent")
        {
            var id = sessionId == "current" ? sessionService.GetCurrentSessionId() : Int32.Parse(sessionId);
            var model = sessionService.AdditionalAgenda(id);

            ViewBag.PageType = MenuItemType.CouncilSession;
            ViewBag.PageValue = sessionId == "current" ? "cur" : "prev";
            ViewBag.SessionMenu = "agenda";
            ViewBag.SessionId = sessionId;

            return View(model);
        }

        public ActionResult Votes(string sessionId = "cuurent", int page = 1)
        {
            var id = sessionId == "current" ? sessionService.GetCurrentSessionId() : Int32.Parse(sessionId);
            var model = sessionService.Votes(id, page);

            ViewBag.PageType = MenuItemType.CouncilSession;
            ViewBag.PageValue = sessionId == "current" ? "cur" : "prev";
            ViewBag.SessionMenu = "votes";
            ViewBag.SessionId = sessionId;

            return View(model);
        }

        public ActionResult Vote(string sessionId = "cuurent", int page = 1)
        {
            var id = sessionId == "current" ? sessionService.GetCurrentSessionId() : Int32.Parse(sessionId);
            var model = sessionService.Vote(id, page);

            ViewBag.PageType = MenuItemType.CouncilSession;
            ViewBag.PageValue = sessionId == "current" ? "cur" : "prev";
            ViewBag.SessionMenu = "votes";
            ViewBag.SessionId = sessionId;

            return View(model);
        }

        public ActionResult Resolutions(string sessionId = "cuurent", int page = 1)
        {
            var id = sessionId == "current" ? sessionService.GetCurrentSessionId() : Int32.Parse(sessionId);
            var model = sessionService.Resolutons(id, page);

            ViewBag.PageType = MenuItemType.CouncilSession;
            ViewBag.PageValue = sessionId == "current" ? "cur" : "prev";
            ViewBag.SessionMenu = "resolutions";
            ViewBag.SessionId = sessionId;

            return View(model);
        }

        public ActionResult Resolution(string sessionId = "cuurent", int page = 1)
        {
            var id = sessionId == "current" ? sessionService.GetCurrentSessionId() : Int32.Parse(sessionId);
            var model = sessionService.Resolution(id, page);

            ViewBag.PageType = MenuItemType.CouncilSession;
            ViewBag.PageValue = sessionId == "current" ? "cur" : "prev";
            ViewBag.SessionMenu = "resolutions";
            ViewBag.SessionId = sessionId;

            return View(model);
        }

        public ActionResult Media(string sessionId = "cuurent")
        {
            var id = sessionId == "current" ? sessionService.GetCurrentSessionId() : Int32.Parse(sessionId);
            var model = sessionService.Media(id);

            ViewBag.PageType = MenuItemType.CouncilSession;
            ViewBag.PageValue = sessionId == "current" ? "cur" : "prev";
            ViewBag.SessionMenu = "media";
            ViewBag.SessionId = sessionId;

            return View(model);
        }

        public ActionResult SessionsFilter(String page = "projects", int? current = null)
        {
            var model = sessionService.List();

            ViewBag.SessionPage = page;
            ViewBag.Id = current;

            return PartialView("_prtSessionFilter", model);
        }
    }
}