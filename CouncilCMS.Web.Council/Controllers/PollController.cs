using Bissoft.CouncilCMS.BLL;
using Bissoft.CouncilCMS.BLL.Services.Public;
using Bissoft.CouncilCMS.BLL.ViewModels.Public;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bissoft.CouncilCMS.Web.Controllers
{
    public class PollController : BaseCmsController
    {
        private CmsPollService pollService;

        public PollController()
        {
            pollService = new CmsPollService(connectionString);
        }

        public ActionResult Poll()
        {
            var model = pollService.GetPolls();
            model.ForEach(m => m.IsActive = GetIsActive(m));

            return View(model);
        }

        [HttpPost]
        public JsonResult Poll(int id)
        {
            var model = pollService.GetPoll(id);
            if (model != null)
            {
                model.IsActive = GetIsActive(model);

                return Json(model);
            }

            return null;
        }

        [HttpPost]
        public PartialViewResult Vote(int pollId, int questId)
        {
            CmsPoll model = null;

            try
            {
                model = pollService.Vote(pollId, questId);

                var pollIdCookieKey = string.Format("pollId-{0}", model.Id);
                Session[pollIdCookieKey] = questId;

                model.IsActive = GetIsActive(model);
            }
            catch { }

            return PartialView("_prtPoll", model); ;
        }

        private bool GetIsActive(CmsPoll model)
        {
            var pollIdCookieKey = string.Format("pollId-{0}", model.Id);

            return DateTime.ParseExact(model.DateTo, "MM/dd/yyyy", null) >= DateTime.Now
                && (Session[pollIdCookieKey] == null);
        }
    }
};