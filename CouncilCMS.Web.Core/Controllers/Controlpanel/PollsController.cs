using Bissoft.CouncilCMS.BLL;
using Bissoft.CouncilCMS.BLL.Services.Admin;
using Bissoft.CouncilCMS.BLL.ViewModels.Admin;
using Bissoft.CouncilCMS.Core.Enums;
using Bissoft.CouncilCMS.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Bissoft.CouncilCMS.Web.Areas.ControlPanel.Controllers
{
    public class PollsController : BaseCmsController
    {
        private PollAdminService pollAdminService;

        public PollsController() : base()
        {
            pollAdminService = new PollAdminService(this.connectionString);
        }


        public ActionResult List(Boolean AjaxMode = false)
        {
            if (AjaxMode)
            {
                var model = pollAdminService.GetPolls();
                return PartialView("_prtPollList", model);
            }
            else
            {
                return View();
            }

        }
        public ActionResult GetPollForm(Int32 Id = 0)
        {
            var model = pollAdminService.GetPollEdit(Id);

            return PartialView("_prtPollEdit", model);
        }
        public ActionResult GetQuestionForm(Int32 MenuId, Int32 Id = 0)
        {
            var model = pollAdminService.GetQuestionEdit(Id, MenuId);

            return PartialView("_prtQuestionEdit", model);
        }

        [HttpPost]
        public ActionResult SavePoll(PollEdit Model)
        {
            if (!ModelState.IsValid)
                return PartialView("_prtPollEdit", Model);

            var model = pollAdminService.SavePoll(Model);

            if (Model.Id > 0)
            {
                return PartialView("_prtPollListItem", model);
            }
            else
            {
                return PartialView("_prtPollList", new List<PollListItem> { model });
            }
        }
        [HttpPost]
        public ActionResult SaveQuestion(QuestionEdit Model)
        {
            if (!ModelState.IsValid)
                return PartialView("_prtQuestionEdit", Model);

            var model = pollAdminService.SaveQuestion(Model);

            if (Model.Id > 0)
            {
                return PartialView("_prtQuestionItem", model);
            }
            else
            {
                return PartialView("_prtQuestionList", new List<QuestionListItem> { model });
            }
        }


        [HttpPost]
        public ActionResult UpdatePosition(Int32 Id, Int32 OldPosition, Int32 NewPosition, Int32 PollId)
        {
            pollAdminService.UpdatePosition(Id, PollId, OldPosition, NewPosition);

            return null;
        }

        [HttpPost]
        public ActionResult RemovePoll(Int32 Id)
        {
            pollAdminService.Remove(Id);

            return null;
        }

        [HttpPost]
        public ActionResult RemoveQuestion(Int32 Id)
        {
            pollAdminService.RemoveQuestion(Id);

            return null;
        }

        public double GetPercent(int questionId, int pollId)
        {
            var percent = 0;

            if (pollAdminService.GetPollEdit(pollId).VoiсeCount != 0)
                return Convert.ToDouble( pollAdminService.GetQuestionEdit(questionId, pollId).CountClick) / Convert.ToDouble(pollAdminService.GetPollEdit(pollId).VoiсeCount) * 100;
            else
                return percent;
        }
    }
}
