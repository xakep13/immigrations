using Bissoft.CouncilCMS.BLL.ViewModels.Admin;
using Bissoft.CouncilCMS.Core;
using Bissoft.CouncilCMS.Core.Enums;
using Bissoft.CouncilCMS.DAL.Entities;
using Bissoft.CouncilCMS.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using xTab.Tools.Extensions;
using xTab.Tools.Helpers;

namespace Bissoft.CouncilCMS.BLL.Services.Admin
{
    public class PollAdminService : BaseService
    {
        private IRepository<Poll, int> pollRepo;
        private IRepository<Question, int> questionRepo;
        private IRepository<CmsUser, int> userRepo;
        private ContentAdminService contentAdminService;
        private SelectListService selectListService;
        private CmsSearchAdminService cmsSearchService;

        public PollAdminService(string ConnectionString) : base(ConnectionString)
        {
            Initialize();
        }

        public PollAdminService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            Initialize();
        }

        private void Initialize()
        {
            pollRepo = UnitOfWork.GetIntRepository<Poll>();
            questionRepo = UnitOfWork.GetIntRepository<Question>();
            userRepo = UnitOfWork.GetIntRepository<CmsUser>();
            selectListService = new SelectListService(this.UnitOfWork);
            contentAdminService = new ContentAdminService(this.UnitOfWork);
            cmsSearchService = new CmsSearchAdminService(this.UnitOfWork);
        }

        public List<PollListItem> GetPolls()
        {
            var list = pollRepo.GetList(notDeleted: false).ToList();
            var questions = questionRepo.GetList(notDeleted: false).ToList();

            var model = new List<PollListItem>();

            foreach (var poll in list)
            {
                model.Add(new PollListItem()
                {
                    Id = poll.Id,

                    DateFrom = poll.DateFrom,
                    DateTo = poll.DateTo,
                    

                    VoiceCount = poll.VoiсeCount,
                    ShowPoll = poll.Published,

                    NameRu = poll.TitleRu,
                    NameUk = poll.TitleUk,
                    NameEn = poll.TitleEn,

                    Questions = questions
                        .Where(x => x.PollID == poll.Id)
                        .OrderBy(x => x.Position)
                        .Select(x => FillMenuItem(x, questions))
                        .ToList()
                });
            }

            return model;
        }
        private QuestionListItem FillMenuItem(Question item, List<Question> list)
        {
            var model = new QuestionListItem()
            {
                TitleRu = item.NameRu,
                TitleUk = item.NameUk,
                TitleEn = item.NameEn,
                Color = item.Color,
                CountClick = item.ClickCount,
                Position = item.Position,
                PollId = (int)item.PollID,
                Id = item.Id,

            };

            return model;
        }

        public PollEdit GetPollEdit(Int32 Id)
        {
            var model = new PollEdit();

            var item = pollRepo.GetById(Id) ?? new Poll() { Id = 0 };

            model.Id = item.Id;

            model.NameRu = item.TitleRu;
            model.NameUk = item.TitleUk;
            model.NameEn = item.TitleEn;

            model.DateFrom = item.DateFrom;
            model.DateTo = item.DateTo;

            model.VoiсeCount = item.VoiсeCount;
            model.ShowPoll = item.Published;

            return model;
        }

        public QuestionEdit GetQuestionEdit(Int32 id, Int32 pollId)
        {
            var model = new QuestionEdit();
            var item = questionRepo.GetById(id) ?? new Question() { Id = 0, PollID = pollId };

            model.Id = item.Id;
            model.PollId = (int)item.PollID;

            model.Position = item.Position;
            model.NameRu = item.NameRu;
            model.NameUk = item.NameUk;
            model.NameEn = item.NameEn;
            model.CountClick = item.ClickCount ;
            model.Color = item.Color;


            return model;
        }

        public PollListItem SavePoll(PollEdit model, int? count = null)
        {
            var item = pollRepo.GetById(model.Id) ?? new Poll();

            item.TitleUk = String.IsNullOrEmpty(model.NameUk) ? "Без назви" : model.NameUk;
            item.TitleRu = String.IsNullOrEmpty(model.NameRu) ? "Без названия" : model.NameRu;
            item.TitleEn = String.IsNullOrEmpty(model.NameEn) ? "No name" : model.NameEn;

        if(count != null)
            {
                item.VoiсeCount =(int)count;
            }

            item.DateFrom = model.DateFrom;
            item.DateTo = model.DateTo;

            item.Published = model.ShowPoll;

            pollRepo.InsertOrUpdate(item);
            UnitOfWork.Commit();

            var result = new PollListItem()
            {
                Id = item.Id,
                NameRu = item.TitleRu,
                NameUk = item.TitleUk,
                NameEn = item.TitleEn
            };

            return result;
        }
        public PollListItem CreatePoll(int pollItemId)
        {
            var item = pollRepo.GetById(pollItemId);

            if (item != null)
            {
                var poll = new Poll()
                {
                    Id = -1,
                    TitleUk = item.TitleUk,
                    TitleRu = item.TitleRu,
                    TitleEn = item.TitleEn,
                    DateFrom = item.DateFrom,
                    DateTo = item.DateTo,
                    VoiсeCount = item.VoiсeCount,
                    Published = item.Published
                    
                };

                pollRepo.Insert(poll);
                UnitOfWork.Commit();

                var items = item.Questions;
            }

            return null;
        }

        public QuestionListItem SaveQuestion(QuestionEdit model)
        {
            var item = questionRepo.GetById(model.Id) ?? new Question() { Id = 0 };

         

            item.NameUk = String.IsNullOrEmpty(model.NameUk) ? "Без назви" : model.NameUk;
            item.NameRu = String.IsNullOrEmpty(model.NameRu) ? "Без названия" : model.NameRu;
            item.NameEn = String.IsNullOrEmpty(model.NameEn) ? "No name" : model.NameEn;

            item.Position = model.Id > 0 ? item.Position : (GetMaxPosition(model.PollId) + 1);

            item.PollID = model.PollId;
            item.Color = model.Color;
           

            questionRepo.InsertOrUpdate(item);
            UnitOfWork.Commit();

            var result = new QuestionListItem()
            {
                TitleRu = item.NameRu,
                TitleUk = item.NameUk,
                TitleEn = item.NameEn,

                Position = item.Position,
                PollId = (int)item.PollID,

                Id = item.Id,

            };

            return result;
        }

        public void UpdatePosition(Int32 id, int menuId, Int32 oldPosition, Int32 newPosition)
        {
            var result = 0;

            if (oldPosition > newPosition)
            {
                result = questionRepo
                    .Context
                    .ExecuteSqlCommand("UPDATE Questions SET Position = Position + 1 WHERE Position >= {0} AND Position < {1} AND PollId = {2} ", false, null, newPosition, oldPosition, menuId);
            }
            else if (oldPosition < newPosition)
            {
                result = questionRepo
                   .Context
                   .ExecuteSqlCommand("UPDATE Questions SET Position = Position - 1 WHERE Position <= {0} AND Position > {1} AND PollId = {2} ", false, null, newPosition, oldPosition, menuId);
            }

            result = questionRepo
                   .Context
                   .ExecuteSqlCommand("UPDATE Questions SET Position = {0} WHERE Id = {1}", false, null, newPosition, id);
        }

        public void Delete(Int32 Id)
        {
            var menu = pollRepo.GetById(Id);

            menu.Deleted = true;
            pollRepo.Update(menu);

            UnitOfWork.Commit();
        }

        public void Remove(Int32 Id)
        {
            var menu = pollRepo.GetById(Id);

            pollRepo.Delete(menu);

            UnitOfWork.Commit();
        }

        public void DeleteQuestion(Int32 Id)
        {
            var menuItem = questionRepo.GetById(Id);

            menuItem.Deleted = true;
            questionRepo.Update(menuItem);

            UnitOfWork.Commit();
        }

        public void RemoveQuestion(Int32 Id)
        {
            var menuItem = questionRepo.GetById(Id);

            questionRepo.Delete(menuItem);

            UnitOfWork.Commit();

            questionRepo
                   .Context
                   .ExecuteSqlCommand("UPDATE Questions SET Position = Position - 1 WHERE Position > {0} AND PollId = {1} ", false, null, menuItem.Position, menuItem.PollID);
        }




        private Int32 GetMaxPosition(Int32 PollId)
        {
            var max = questionRepo.GetList().Where(x => x.PollID == PollId).Select(x => x.Position).DefaultIfEmpty(0).Max();

            return max;
        }
    }
}
