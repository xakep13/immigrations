using System;
using System.Configuration;
using System.Web.Mvc;
using Bissoft.CouncilCMS.Core.Enums;
using Bissoft.CouncilCMS.BLL.ViewModels;
using Bissoft.CouncilCMS.BLL.Services;
using System.Collections.Generic;
using Bissoft.CouncilCMS.Web.Controllers;
using Bissoft.CouncilCMS.Web.Filters;

namespace Bissoft.CouncilCMS.Web.Areas.ControlPanel.Controllers
{
    [CmsAuthorize]
    public class SessionsController : BaseCmsController
    {
        private SessionAdminService sessionService;

        public SessionsController() : base()
        {
            sessionService = new SessionAdminService(this.ConnectionString);
        }


        public ActionResult List(string dateRange = null, int dateType = 0, int state = 1, int page = 1, String sortBy = "SessionDate", int sortDirection = 1, int? user = null, int userAction = 0, Boolean AjaxMode = false)
        {
            if (User.HasPremissions("sessions"))
            {
                if (AjaxMode)
                {
                    var count = 0;
                    var model = sessionService.GetList(
                        page: page,
                        dateRange: dateRange,
                        dateType: dateType,
                        sortBy: sortBy,
                        sortDir: sortDirection,
                        user: user,
                        userAction: userAction,
                        itemState: (CmsItemState)state,
                        count: out count);

                    ViewBag.Count = count;
                    ViewBag.Page = page;
                    ViewBag.PerPage = 20;

                    return PartialView("_prtSessionList", model);
                }
                else
                {
                    var model = sessionService.List(
                        page: page,
                        dateRange: dateRange,
                        dateType: dateType,
                        sortBy: sortBy,
                        sortDir: sortDirection,
                        user: user,
                        userAction: userAction,
                        itemState: (CmsItemState)state);

                    return View(model);
                }
            }
            else
            {
                return new HttpUnauthorizedResult();
            }
        }

        public ActionResult GetForm(Int32 Id = 0)
        {        
            var model = sessionService.GetForEdit(Id);

            return PartialView("_prtSessionEdit", model);
        }        

        [HttpPost]
        public ActionResult Save(SessionEdit model)
        {
            sessionService.Save(model);

            return PartialView("_prtSessionEditComplete", model);
        }

        public ActionResult Delete(Int32 Id)
        {
            sessionService.Delete(Id);

            return null;
        }

        public ActionResult Restore(Int32 Id)
        {
            sessionService.Restore(Id);

            return null;
        }


        public ActionResult AddProject(Int32 DocId, Int32 SessionId)
        {
            var model = sessionService.AddProject(DocId, SessionId);

            return PartialView("_prtSessionProjectListItem", model);
        }

        public ActionResult MoveProjectToAgenda(Int32 DocId, Int32 SessionId)
        {
            var model = sessionService.MoveProjectToAgenda(DocId, SessionId);

            return PartialView("_prtSessionAgendaListItem", model);
        }

        public ActionResult GetAgendaForm(Int32 Id, Int32 SessionId = 0)
        {
            var model = sessionService.GetAgendaItemForm(Id, SessionId);
            
            return PartialView("_prtSessionAgendaEdit", model);
        }

        public ActionResult SaveAgenda(SessionAgendaItem model)
        {
            var result = sessionService.SaveAgendaItem(model);

            return PartialView("_prtSessionAgendaListItem", model);
        }

        public ActionResult DeleteProject(Int32 DocId, Int32 SessionId)
        {
            sessionService.DeleteProject(DocId, SessionId);
            return null;
        }

        public ActionResult DeleteAgenda(Int32 Id)
        {
            sessionService.DeleteAgendaItem(Id);
            return null;
        }

        public ActionResult UpdateAgendaPosition(int id, int sessionId, int oldPosition, int newPosition)
        {
            sessionService.UpdateAgendaPosition(id, sessionId, oldPosition, newPosition);

            return null;
        }


        public ActionResult GetVoteForm(Int32 Id, Int32 SessionId = 0)
        {
            var model = sessionService.GetVoteForm(Id, SessionId);

            return PartialView("_prtSessionVoteEdit", model);
        }

        public ActionResult SaveVote(SessionVoteEdit model)
        {
            var result = sessionService.SaveVote(model);

            return PartialView("_prtSessionVoteListItem", result);
        }

        public ActionResult DeleteVote(Int32 Id)
        {
            sessionService.DeleteVote(Id);

            return null;
        }

        public ActionResult UpdateVotePosition(int id, int sessionId, int oldPosition, int newPosition)
        {
            sessionService.UpdateVotePosition(id, sessionId, oldPosition, newPosition);

            return null;
        }
    }
}