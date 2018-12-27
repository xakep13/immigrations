using Bissoft.CouncilCMS.BLL.ViewModels.Public;
using Bissoft.CouncilCMS.Core;
using Bissoft.CouncilCMS.Core.Enums;
using Bissoft.CouncilCMS.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xTab.Tools.Extensions;
using xTab.Tools.Helpers;

namespace Bissoft.CouncilCMS.BLL.Services.Public
{
    public class CmsPollService : BaseService
    {
        private IRepository<Poll, int> pollRepo;
        private IRepository<Question, int> questionRepo;

        public CmsPollService(string ConnectionString) : base(ConnectionString)
        {
            Initialize();
        }

        public CmsPollService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            Initialize();
        }

        private void Initialize()
        {
            pollRepo = UnitOfWork.GetIntRepository<Poll>();
            questionRepo = UnitOfWork.GetIntRepository<Question>();
        }


        public List<CmsPoll> GetPolls()
        {
            var list = pollRepo.GetList(notDeleted: false).ToList();
            var questions = questionRepo.GetList(notDeleted: false).ToList();

            var model = new List<CmsPoll>();

            foreach (var poll in list)
            {
                model.Add(new CmsPoll()
                {
                    Id = poll.Id,
                    Title = poll.GetLocalValue("Title"),
                    CmsQuestions = GetQuestions(poll.Questions.ToList()),
                    DateFrom = poll.DateFrom,
                    DateTo = poll.DateTo,
                    VoiсeCount = poll.VoiсeCount,
                });
            }

            return model;
        }

        public CmsPoll GetPoll(Int32 id)
        {
            var poll = pollRepo.GetById(id);
            var model = new CmsPoll();

            model.Id = poll.Id;
            model.Title = poll.GetLocalValue("Title");
            model.CmsQuestions = GetQuestions(poll.Questions.ToList());
            model.DateFrom = poll.DateFrom;
            model.DateTo = poll.DateTo;
            model.VoiсeCount = poll.VoiсeCount;

            return model;
        }

        public CmsQuestion GetQuestion(int questionId, int pollId)
        {
            var model = new CmsQuestion();
            var item = questionRepo.GetById(questionId);

            model.Id = item.Id;
            model.PollId = (int)item.PollID;
            model.CheckCount = item.ClickCount;
            model.Title = item.GetLocalValue("Name");
            model.Color = item.Color;

            model.Percent = item.Poll.VoiсeCount == 0 ? 0.0 : ((double)item.ClickCount / item.Poll.VoiсeCount) * 100;


            return model;
        }

        public List<CmsQuestion> GetQuestions(List<Question> list)
        {
            var model = new List<CmsQuestion>();

            if (list != null)
            {
                foreach (var item in list)
                {
                    var questions = new CmsQuestion()
                    {
                        Id = item.Id,
                        PollId = (int)item.PollID,
                        CheckCount = item.ClickCount,
                        Title = item.GetLocalValue("Name"),
                        Color = item.Color,
                        Percent = item.Poll.VoiсeCount == 0 ? 0.0 : ((double)item.ClickCount / item.Poll.VoiсeCount) * 100
                    };
                    model.Add(questions);
                }
            }
            return model;
        }

        public CmsPoll SavePoll(CmsPoll model)
        {
            var item = pollRepo.GetById(model.Id) ?? new Poll();

            item.VoiсeCount = model.VoiсeCount;

            pollRepo.InsertOrUpdate(item);
            UnitOfWork.Commit();

            var result = new CmsPoll()
            {
                Id = item.Id,
                VoiсeCount = item.VoiсeCount
            };

            return result;
        }

        public CmsQuestion SaveQuestion(CmsQuestion model)
        {
            var item = questionRepo.GetById(model.Id);

            item.ClickCount = model.CheckCount;

            questionRepo.InsertOrUpdate(item);
            UnitOfWork.Commit();

            var result = new CmsQuestion()
            {

                PollId = (int)item.PollID,
                CheckCount = item.ClickCount,
                Id = item.Id,
                Color = item.Color
            };

            return result;
        }

        public CmsPoll Vote(int pollId, int questId)
        {
            var poll = pollRepo.GetById(pollId);
            var question = questionRepo.GetById(questId);

            poll.VoiсeCount++;
            question.ClickCount++;

            pollRepo.Update(poll);
            questionRepo.Update(question);

            UnitOfWork.Commit();

            var result = GetPoll(poll.Id);

            return result;
        }
    }
}
