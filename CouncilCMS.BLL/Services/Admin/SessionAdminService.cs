using System;
using System.Collections.Generic;
using System.Linq;
using Bissoft.CouncilCMS.Core;
using Bissoft.CouncilCMS.Core.Enums;
using Bissoft.CouncilCMS.Core.Globalization;
using Bissoft.CouncilCMS.DAL.Entities;
using Bissoft.CouncilCMS.BLL.ViewModels;
using xTab.Tools.Extensions;
using xTab.Tools.Helpers;
using LemmaSharp;
using System.Web;
using Bissoft.CouncilCMS.Web.Core;

namespace Bissoft.CouncilCMS.BLL.Services
{
    public class SessionAdminService : BaseService
    {
        private IRepository<SessionAgenda, int> sessionAgendaRepo;
        private IRepository<Session, int> sessionRepo;
        private IRepository<SessionVote, int> sessionVoteRepo;
        private IRepository<Doc, int> docRepo;
        private SelectListService selectListService;

        public SessionAdminService(string ConnectionString) : base(ConnectionString)
        {
            Initialize();
        }

        public SessionAdminService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            Initialize();
        }

        private void Initialize()
        {
            sessionAgendaRepo = UnitOfWork.GetIntRepository<SessionAgenda>();
            sessionRepo = UnitOfWork.GetIntRepository<Session>();
            sessionVoteRepo = UnitOfWork.GetIntRepository<SessionVote>();
            docRepo = UnitOfWork.GetIntRepository<Doc>();
            selectListService = new SelectListService(this.UnitOfWork);
        }

        public SessionList List(string dateRange = null, int dateType = 0, CmsItemState itemState = CmsItemState.All, int page = 1, string sortBy = "SessionDate", int sortDir = 1, int? user = null, int userAction = 0)
        {
            var model = new SessionList()
            {
                Page = page,
                State = (int)itemState,
                DateRange = dateRange,
                DateType = dateType,
                SortBy = sortBy,
                SortDirection = sortDir,
                User = user,
                UserAction = userAction,
                Users = selectListService.GetCmsUserSelectList(String.Empty)
            };

            return model;
        }
        public List<SessionListItem> GetList(out int count, int dateType = 0, string dateRange = null, CmsItemState itemState = CmsItemState.All, int page = 1, int perPage = 20, string sortBy = "SessionDate", int sortDir = 1, int? user = null, int userAction = 0)
        {
            var predicate = CreateStatePredicate<Session>(itemState);

            if (!String.IsNullOrEmpty(dateRange))            
                predicate = DateRangeFilter(predicate, dateRange, dateType);

            if (user > 0)
                predicate = UserFilter(predicate, user, userAction);
          
            var list = sessionRepo.GetList(out count, predicate, x => x.OrderByColumn(sortBy, sortDir > 0), "CreateUser,LastEditUser", (page - 1) * perPage, perPage, true, false);

            var model = list.Select(x => new SessionListItem()
            {
                Id = x.Id,
                TitleRu = x.TitleRu,
                TitleUk = x.TitleUk,
                TitleEn = x.TitleEn,
                CreateDate = x.CreateDate,
                LastEditDate = x.LastEditDate,
                PublishDate = x.PublishDate,
                SessionDate = x.SessionDate,
                Ended = x.Ended,
                Deleted = x.Deleted,
                Published = x.Published,
                CreateUser = x.CreateUserId > 0 ? x.CreateUser.Name : null,
                LastEditUser = x.LastEditUserId > 0 ? x.LastEditUser.Name : null
            }).ToList();

            return model;
        }

        public SessionEdit GetForEdit(Int32 Id)
        {
            var entry = sessionRepo.GetById(Id) ?? new Session()
            {
                Id = 0,
                TitleUk = "Без назви",
                TitleRu = "Без названия",
                TitleEn = "No title",
                CreateDate = DateTime.Now,
                PublishDate = DateTime.Now,
                CreateUserId = (HttpContext.Current.User as CmsPrincipal).Identity.UserId
            };

            if (Id == 0)
            {
                sessionRepo.Insert(entry);
                UnitOfWork.Commit();
            }

            var model = new SessionEdit()
            {
                Id = entry.Id,

                TitleRu = entry.TitleRu,
                TitleUk = entry.TitleUk,
                TitleEn = entry.TitleEn,

                MetaTitleRu = entry.MetaTitleRu,
                MetaTitleUk = entry.MetaTitleUk,
                MetaTitleEn = entry.MetaTitleEn,
                MetaKeywordsRu = entry.MetaKeywordsRu,
                MetaKeywordsUk = entry.MetaKeywordsUk,
                MetaKeywordsEn = entry.MetaKeywordsEn,
                MetaDescriptionRu = entry.MetaDescriptionRu,
                MetaDescriptionUk = entry.MetaDescriptionUk,
                MetaDescriptionEn = entry.MetaDescriptionEn,

                EmbedAudio = entry.EmbedAudio,
                EmbedVideo = entry.EmbedVideo,

                Published = entry.Published,
                Ended = entry.Ended,

                PublishDate = DateTimeHelper.NullableDateTimeString(entry.PublishDate, null, false, "dd.MM.yyyy HH:mm"),
                SessionDate = DateTimeHelper.NullableDateTimeString(entry.SessionDate, null, false, "dd.MM.yyyy HH:mm"),
                AgendaFile = entry.AgendaFile,
                DecreeId = entry.DecreeId,
                ReglamentId = entry.ReglamentId,
                ProtocolId = entry.ProtocolId,
                AgendaAdditionId = entry.AgendaAdditionId,

                AgendaAdditionTitle = entry.AgendaAddition != null ? (entry.AgendaAddition.GetLocalValue("Title") + " [id: " + entry.AgendaAddition.Id + "]") : null,
                ReglamentTitle = entry.Reglament != null ? (entry.Reglament.GetLocalValue("Title") + " [id: " + entry.Reglament.Id + "]") : null,
                ProtocolTitle = entry.Protocol != null ? (entry.Protocol.GetLocalValue("Title") + " [id: " + entry.Protocol.Id + "]") : null,
                DecreeTitle = entry.Decree != null ? (entry.Decree.GetLocalValue("Title") + " [id: " + entry.Decree.Id + "]") : null,

                Projects = entry.Projects != null ? entry.Projects.Select(x => new SessionProjectItem() { DocId = x.Id, SessionId = entry.Id, DocTitle = String.Format("Проект №{0}: {1}", x.Number, x.GetLocalValue("Title")), IsAgenda = entry.Agenda == null ? false : entry.Agenda.Any(a => a.DocId == x.Id) }).ToList() : null,
                Agenda = entry.Agenda != null ? entry.Agenda.Select(x => new SessionAgendaItem() { Id = x.Id, SessionId = entry.Id, Number = x.Number, TitleUk = x.TitleUk, TitleRu = x.TitleRu, TitleEn = x.TitleEn, DocId = x.DocId, DocTitle = x.Doc != null ? (String.Format("Проект №{0}: {1}", x.Doc.Number, x.Doc.GetLocalValue("Title"))) : null }).OrderBy(x => x.Number).ToList() : null,
                Votes = entry.Votes != null ? entry.Votes.Select(x => new SessionVoteListItem() { Id = x.Id, Number = x.Number, TitleUk = x.TitleUk, TitleRu = x.TitleRu, TitleEn = x.TitleEn, For = x.For, Against = x.Against, Abstained = x.Abstained, NotVote = x.NotVote, Absent = x.Absent, Result = (VoteResult)x.Result }).OrderBy(x => x.Number).ToList() : null,
            };

            return model;
        }
        public SessionEdit Save(SessionEdit model)
        {
            var entry = sessionRepo.GetById(model.Id);

            entry.Id = model.Id;

            entry.TitleRu = model.TitleRu;
            entry.TitleUk = model.TitleUk;
            entry.TitleEn = model.TitleEn;

            entry.MetaTitleRu = model.MetaTitleRu;
            entry.MetaTitleUk = model.MetaTitleUk;
            entry.MetaTitleEn = model.MetaTitleEn;
            entry.MetaKeywordsRu = model.MetaKeywordsRu;
            entry.MetaKeywordsUk = model.MetaKeywordsUk;
            entry.MetaKeywordsEn = model.MetaKeywordsEn;
            entry.MetaDescriptionRu = model.MetaDescriptionRu;
            entry.MetaDescriptionUk = model.MetaDescriptionUk;
            entry.MetaDescriptionEn = model.MetaDescriptionEn;

            entry.EmbedAudio = model.EmbedAudio;
            entry.EmbedVideo = model.EmbedVideo;

            entry.Published = model.Published;
            entry.Ended = model.Ended;

            entry.LastEditDate = DateTime.Now;
            entry.PublishDate = DateTimeHelper.ParseDateNullable(model.PublishDate, null, DateTimeHelper.DefaultTimeOfDay.None);
            entry.SessionDate = DateTimeHelper.ParseDateNullable(model.SessionDate, null, DateTimeHelper.DefaultTimeOfDay.None);
            entry.AgendaFile = model.AgendaFile;
            entry.DecreeId = model.DecreeId;
            entry.AgendaAdditionId = model.AgendaAdditionId;
            entry.ProtocolId = model.ProtocolId;
            entry.ReglamentId = model.ReglamentId;
            entry.LastEditUserId = (HttpContext.Current.User as CmsPrincipal).Identity.UserId;

            sessionRepo.Update(entry);
            UnitOfWork.Commit();

            return model;
        }


        public void Delete(Int32 Id)
        {
            var entry = sessionRepo.GetById(Id);

            entry.Deleted = true;

            sessionRepo.Update(entry);

            UnitOfWork.Commit();
        }

        public void Restore(Int32 Id)
        {
            var entry = sessionRepo.GetById(Id);

            entry.Deleted = false;

            sessionRepo.Update(entry);

            UnitOfWork.Commit();
        }

        public void Remove(Int32 Id)
        {
            var entry = sessionRepo.GetById(Id);

            sessionRepo.Delete(entry);

            UnitOfWork.Commit();
        }


        public SessionProjectItem AddProject(Int32 DocId, Int32 SessionId)
        {
            var doc = docRepo.GetById(DocId);
            var ses = sessionRepo.GetById(SessionId);

            ses.Projects = ses.Projects ?? new List<Doc>();

            if (!ses.Projects.Any(x => x.Id == DocId))
            {
                ses.Projects.Add(doc);
                sessionRepo.Update(ses);
                UnitOfWork.Commit();

                var model = new SessionProjectItem()
                {
                    DocId = DocId,
                    DocTitle = String.Format("Проект №{0}: {1}", doc.Number, doc.GetLocalValue("Title")),
                    SessionId = SessionId
                };

                return model;
            }

            return null;
        }      

        public void DeleteProject(Int32 DocId, Int32 SessionId)
        {
            var doc = docRepo.GetById(DocId);
            var ses = sessionRepo.GetById(SessionId);

            ses.Projects = ses.Projects ?? new List<Doc>();

            if (ses.Projects.Any(x => x.Id == DocId))
            {
                ses.Projects.Remove(doc);
                sessionRepo.Update(ses);
                UnitOfWork.Commit();               
            }
        }

        public SessionAgendaItem MoveProjectToAgenda(Int32 DocId, Int32 SessionId)
        {
            var ses = sessionRepo.GetById(SessionId);
            var doc = docRepo.GetById(DocId);

            ses.Agenda = ses.Agenda ?? new List<SessionAgenda>();

            if (!ses.Agenda.Any(x => x.DocId == DocId))
            {
                var entry = new SessionAgenda()
                {
                    Id = 0,
                    SessionId = SessionId,
                    DocId = DocId,
                    Number = (sessionAgendaRepo.DbSet.Where(x => x.SessionId == SessionId).Select(x => (int?)x.Number).DefaultIfEmpty(0).Max() ?? 0) + 1,
                };

                sessionAgendaRepo.Insert(entry);
                UnitOfWork.Commit();

                var model = new SessionAgendaItem()
                {
                    Id = entry.Id,
                    DocId = entry.DocId,
                    DocTitle = String.Format("Проект №{0}: {1}", doc.Number, doc.GetLocalValue("Title")),
                    SessionId = entry.SessionId,
                    Number = entry.Number,                  
                };

                return model;
            }                     

            return null;
        }

        public SessionAgendaItem GetAgendaItemForm(Int32 Id = 0, Int32 SessionId = 0)
        {
            var entry = sessionAgendaRepo.GetById(Id) ?? new SessionAgenda() { SessionId = SessionId };

            var model = new SessionAgendaItem()
            {
                Id = entry.Id,                
                SessionId = entry.SessionId,
                TitleUk = entry.TitleUk,
                TitleRu = entry.TitleRu,
                TitleEn = entry.TitleEn
            };

            return model;
        }

        public SessionAgendaItem SaveAgendaItem(SessionAgendaItem model)
        {
            var entry =
                sessionAgendaRepo.GetById(model.Id) ??
                new SessionAgenda()
                {
                    Id = 0,
                    SessionId = model.SessionId,
                    Number = (sessionAgendaRepo.DbSet.Where(x => x.SessionId == model.SessionId).Select(x => (int?)x.Number).DefaultIfEmpty(0).Max() ?? 0) + 1,
                };

            entry.TitleUk = model.TitleUk;
            entry.TitleRu = model.TitleRu;
            entry.TitleEn = model.TitleEn;
           
            sessionAgendaRepo.InsertOrUpdate(entry);
            UnitOfWork.Commit();

            model.Id = entry.Id;
            model.Number = entry.Number;

            return model;
        }

        public void DeleteAgendaItem(Int32 Id)
        {
            var entry = sessionAgendaRepo.GetById(Id);

            if (entry != null)
            {
                sessionAgendaRepo.Delete(entry);
                UnitOfWork.Commit();

                UnitOfWork
                    .Context
                    .ExecuteSqlCommand("UPDATE SessionAgendas SET Number = Number - 1 WHERE Number > {0} AND SessionId = " + entry.SessionId, false, null, entry.Number);
            }
        }

        public void UpdateAgendaPosition(int id, int sessionId, int oldPosition, int newPosition)
        {
            var result = 0;

            if (oldPosition > newPosition)
            {
                result = UnitOfWork
                    .Context
                    .ExecuteSqlCommand("UPDATE SessionAgendas SET Number = Number + 1 WHERE Number >= {0} AND Number < {1} AND SessionId = " + sessionId, false, null, newPosition, oldPosition);
            }
            else if (oldPosition < newPosition)
            {
                result = UnitOfWork
                   .Context
                   .ExecuteSqlCommand("UPDATE SessionAgendas SET Number = Number - 1 WHERE Number <= {0} AND Number > {1} AND SessionId = " + sessionId, false, null, newPosition, oldPosition);
            }

            result = UnitOfWork
                   .Context
                   .ExecuteSqlCommand("UPDATE SessionAgendas SET Number = {0} WHERE Id = {1}", false, null, newPosition, id);
        }


        public SessionVoteEdit GetVoteForm(Int32 Id, Int32 SessionId = 0)
        {
            var entry = sessionVoteRepo.GetById(Id) ?? new SessionVote() { SessionId = SessionId };

            var model = new SessionVoteEdit()
            {
                Id = entry.Id,
                DocId = entry.DocId,
                DocTitle = entry.Doc != null ? (entry.Doc.GetLocalValue("Title") + " [id: " + entry.Doc.Id + "]") : null,
                SessionId = entry.SessionId,
                TitleUk = entry.TitleUk,
                TitleRu = entry.TitleRu,
                TitleEn = entry.TitleEn,
                TextUk = entry.TextUk,
                TextRu = entry.TextRu,
                TextEn = entry.TextEn,
                ResultUk = entry.ResultUk,
                ResultRu = entry.ResultRu,
                ResultEn = entry.ResultEn,
                Result = (VoteResult)entry.Result,
                For = entry.For,
                Against = entry.Against,
                Abstained = entry.Abstained,
                Absent = entry.Absent,
                NotVote = entry.NotVote,                
            };

            return model;
        }

        public SessionVoteListItem SaveVote(SessionVoteEdit model)
        {
            var entry =
               sessionVoteRepo.GetById(model.Id) ??
               new SessionVote()
               {
                   SessionId = model.SessionId,                   
                   Number = (sessionVoteRepo.DbSet.Where(x => x.SessionId == model.SessionId).Select(x => (int?)x.Number).DefaultIfEmpty(0).Max() ?? 0) + 1,
               };

            entry.TitleUk = model.TitleUk;
            entry.TitleRu = model.TitleRu;
            entry.TitleEn = model.TitleEn;
            entry.TextUk = model.TextUk;
            entry.TextRu = model.TextRu;
            entry.TextEn = model.TextEn;
            entry.ResultUk = model.ResultUk;
            entry.ResultRu = model.ResultRu;
            entry.ResultEn = model.ResultEn;
            entry.Result = (int)model.Result;

            entry.For = model.For;
            entry.Against = model.Against;
            entry.Absent = model.Absent;
            entry.Abstained = model.Abstained;
            entry.NotVote = model.NotVote;

            entry.DocId = model.DocId;

            sessionVoteRepo.InsertOrUpdate(entry);
            UnitOfWork.Commit();

            model.Id = entry.Id;
            model.Number = entry.Number;

            var result = new SessionVoteListItem()
            {
                Id = entry.Id,
                Number = entry.Number,
                TitleUk = entry.TitleUk,
                TitleRu = entry.TitleRu,
                TitleEn = entry.TitleEn,
                For = entry.For,
                Against = entry.Against,
                Abstained = entry.Abstained,
                NotVote = entry.NotVote,
                Absent = entry.Absent,
                Result = (VoteResult)entry.Result
            };

            return result;
        }

        public void DeleteVote(Int32 Id)
        {
            var entry = sessionVoteRepo.GetById(Id);

            if (entry != null)
            {
                sessionVoteRepo.Delete(entry);
                UnitOfWork.Commit();

                UnitOfWork
                    .Context
                    .ExecuteSqlCommand("UPDATE SessionVotes SET Number = Number - 1 WHERE Number > {0} AND SessionId = " + entry.SessionId, false, null, entry.Number);
            }
        }

        public void UpdateVotePosition(int id, int sessionId, int oldPosition, int newPosition)
        {
            var result = 0;

            if (oldPosition > newPosition)
            {
                result = UnitOfWork
                    .Context
                    .ExecuteSqlCommand("UPDATE SessionVotes SET Number = Number + 1 WHERE Number >= {0} AND Number < {1} AND SessionId = " + sessionId, false, null, newPosition, oldPosition);
            }
            else if (oldPosition < newPosition)
            {
                result = UnitOfWork
                   .Context
                   .ExecuteSqlCommand("UPDATE SessionVotes SET Number = Number - 1 WHERE Number <= {0} AND Number > {1} AND SessionId = " + sessionId, false, null, newPosition, oldPosition);
            }

            result = UnitOfWork
                   .Context
                   .ExecuteSqlCommand("UPDATE SessionVotes SET Number = {0} WHERE Id = {1}", false, null, newPosition, id);
        }
    }
}