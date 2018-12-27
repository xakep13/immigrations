using System;
using System.Linq;
using Bissoft.CouncilCMS.Core;
using Bissoft.CouncilCMS.DAL.Entities;
using Bissoft.CouncilCMS.BLL.ViewModels;
using xTab.Tools.Helpers;
using LemmaSharp;
using Bissoft.CouncilCMS.Core.Enums;
using System.Collections.Generic;

namespace Bissoft.CouncilCMS.BLL.Services
{
    public class CmsSessionService : BaseService
    {
        private IRepository<SessionAgenda, int> sessionAgendaRepo;
        private IRepository<Session, int> sessionRepo;
        private IRepository<SessionVote, int> sessionVoteRepo;
        private IRepository<Doc, int> docRepo;
        private CmsDocService docService;

        public CmsSessionService(string ConnectionString) : base(ConnectionString)
        {
            Initialize();
        }

        public CmsSessionService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            Initialize();
        }

        public void Initialize()
        {
            sessionAgendaRepo = UnitOfWork.GetIntRepository<SessionAgenda>();
            sessionRepo = UnitOfWork.GetIntRepository<Session>();
            sessionVoteRepo = UnitOfWork.GetIntRepository<SessionVote>();
            docRepo = UnitOfWork.GetIntRepository<Doc>();
            docService = new CmsDocService(this.UnitOfWork);
        }

        public CmsSessionList List(int page = 1, bool ended = true)
        {
            var count = 0;
            var now = DateTime.Now;
            var list = sessionRepo.GetList(out count, x => x.Ended == ended && x.Published && x.PublishDate <= now, x => x.OrderByDescending(o => o.SessionDate), null, (page - 1) * 20, 20).ToList().Select(x => new CmsSession() { Id = x.Id, Title = x.GetLocalValue("Title") }).ToList();

            var model = new CmsSessionList()
            {
                Count = count,
                Page = page,
                Sessions = list,
                SessionPage = "resolutions"
            };

            return model;
        }

        public CmsSessionProjectList Projects(int id, int page = 1)
        {
            var session = sessionRepo.GetById(id);
            var now = DateTime.Now;
            var predicate = PredicateBuilder.True<Doc>();
            var count = 0;

            if (session == null)
            {
                session = sessionRepo.GetList(out count, x => x.Published && x.PublishDate <= now, x => x.OrderByDescending(o => o.SessionDate)).FirstOrDefault();

                if (session == null)
                    return null;
            }

            if (session.Ended)
                predicate = predicate.And(x => !x.SessionAgendas.Any(s => s.SessionId == id));

            predicate = predicate.And(x => x.Sessions.Any(s => s.Id == id) && x.Published && x.PublishDate <= now);

            var list = 
                docRepo.GetList(out count, predicate, x => x.OrderByDescending(o => o.AcceptDate), "Sessions,SessionAgendas,DocType", (page - 1) * 30, 30).Select(x =>
                    new
                    {
                        Id = x.Id,
                        Number = x.Number,
                        TitleRu = x.TitleRu,
                        TitleUk = x.TitleUk,
                        TitleEn = x.TitleEn,
                        TypeRu = x.DocType.TitleRu,
                        TypeEn = x.DocType.TitleEn,
                        TypeUk = x.DocType.TitleUk,
                        PublishDate = x.PublishDate,
                        PostDate = x.PostDate,
                        AcceptDate = x.AcceptDate,
                        ViewCount = x.ViewCount,
                    }).ToList().Select(x => new CmsDocListItem()
                    {
                        Id = x.Id,
                        Title = x.GetLocalValue("Title"),
                        Type = x.GetLocalValue("Type"),
                        Number = x.Number,
                        PostDate = x.PostDate,
                        AcceptDate = x.AcceptDate,
                        PublishDate = DateTimeHelper.NullableDateTimeString(x.PublishDate, Format: "dd MMMM yyyy HH:mm"),
                        ViewCount = x.ViewCount,
                    }).ToList(); ;

            var model = new CmsSessionProjectList()
            {
                SessionId = session.Id,
                SessionTitle = session.GetLocalValue("Title"),
                Page = page,
                Projects = list,
                Count = count
            };

            return model;                    
        }

        public CmsDoc Project(int id, int docId)
        {
            var session = sessionRepo.GetById(id);
            var model = docService.Doc(docId);

            if (model != null)
            {
                model.SessionId = session.Id;
                model.SessionTitle = session.GetLocalValue("Title");

                return model;
            }

            return null;
        }

        public CmsDoc Resolution(int id, int docId)
        {
            var session = sessionRepo.GetById(id);
            var model = docService.Doc(docId);

            if (session == null)
                return null;

            if (model != null)
            {
                model.SessionId = session.Id;
                model.SessionTitle = session.GetLocalValue("Title");                

                return model;
            }

            return null;
        }

        public CmsSessionResolutionList Resolutons(int id, int page = 1)
        {
            var session = sessionRepo.GetById(id);
            var now = DateTime.Now;
            var predicate = PredicateBuilder.True<SessionVote>();
            var count = 0;

            if (session == null)
                return null;

            predicate = predicate.And(x => x.SessionId == id && x.DocId > 0 && x.Doc.Published && x.Doc.PublishDate <= now);

            var list =
                sessionVoteRepo.GetList(out count, predicate, x => x.OrderBy(o => o.Number), "Doc", (page - 1) * 30, 30).Select(x =>
                    new
                    {
                        Id = x.Doc.Id,
                        Number = x.Doc.Number,
                        TitleRu = x.Doc.TitleRu,
                        TitleUk = x.Doc.TitleUk,
                        TitleEn = x.Doc.TitleEn,
                        
                        PublishDate = x.Doc.PublishDate,
                        PostDate = x.Doc.PostDate,
                        For = x.For,
                        Against = x.Against,
                        Abstained = x.Abstained,
                        NotVote = x.NotVote,
                        Result = x.Result,
                        AcceptDate = x.Doc.AcceptDate,                                                
                    }).ToList().Select(x => new CmsDocListItem()
                    {
                        Id = x.Id,
                        Title = x.GetLocalValue("Title"),
                        
                        Number = x.Number,
                        PostDate = x.PostDate,
                        AcceptDate = x.AcceptDate,
                        For = x.For,
                        Against = x.Against,
                        Abstained = x.Abstained,
                        NotVote = x.NotVote,
                        Result = (VoteResult)x.Result,
                        PublishDate = DateTimeHelper.NullableDateTimeString(x.PublishDate, Format: "dd MMMM yyyy HH:mm"),                       
                    }).ToList(); ;

            var model = new CmsSessionResolutionList()
            {
                SessionId = session.Id,
                SessionTitle = session.GetLocalValue("Title"),
                Page = page,
                Resolutons = list,
                Count = count
            };

            return model;
        }

        public CmsSessionVoteList Votes(int id, int page = 1)
        {
            var session = sessionRepo.GetById(id);            
            var predicate = PredicateBuilder.True<SessionVote>();
            var now = DateTime.Now;
            var count = 0;

            if (session == null)
                return null;

            predicate = predicate.And(x => x.SessionId == id);

            var list =
                sessionVoteRepo.GetList(out count, predicate, x => x.OrderBy(o => o.Number), "Doc", (page - 1) * 30, 30).Select(x =>
                    new
                    {
                        Id = x.Id,
                        Number = x.Number,
                        TitleRu = x.TitleRu,
                        TitleUk = x.TitleUk,
                        TitleEn = x.TitleEn, 
                        For = x.For,
                        Against = x.Against,
                        Abstained = x.Abstained,
                        NotVote = x.NotVote, 
                        Result = x.Result,                       
                    }).ToList().Select(x => new CmsSessionVoteListItem()
                    {
                        Id = x.Id,
                        Title = x.GetLocalValue("Title"),
                        Number = x.Number,
                        For = x.For,
                        Against = x.Against,
                        Abstained = x.Abstained,
                        NotVote = x.NotVote,
                        Result = (VoteResult)x.Result,
                        Date = DateTimeHelper.NullableDateTimeString(session.SessionDate, null, false, "dd MMMM yyyy")
                    }).ToList(); ;

            var model = new CmsSessionVoteList()
            {
                SessionId = session.Id,
                SessionTitle = session.GetLocalValue("Title"),
                Page = page,
                Votes = list,
                Count = count
            };

            return model;
        }

        public CmsSessionVote Vote(int id, int voteId)
        {
            var session = sessionRepo.GetById(id);
            var vote = sessionVoteRepo.GetById(voteId);

            if (session == null)
                return null;

            if (session != null && vote != null)
            {
                var model = new CmsSessionVote()
                {
                    SessionId = session.Id,
                    SessionTitle = session.GetLocalValue("Title"),
                    Title = vote.GetLocalValue("Title"),
                    DocId = vote.DocId,
                    DocTitle = vote.Doc != null ? vote.Doc.GetLocalValue("Title") : null,
                    DocNumber = vote.Doc != null ? vote.Doc.Number : null,
                    DocAcceptDate = vote.Doc != null ? DateTimeHelper.NullableDateTimeString(vote.Doc.AcceptDate, Format: "dd MMMM yyyy") : null,
                    DocPostDate = vote.Doc != null ? DateTimeHelper.NullableDateTimeString(vote.Doc.PostDate, Format: "dd MMMM yyyy") : null,
                    Number = vote.Number,
                    Text = vote.GetLocalValue("Text"),
                    ResultText = vote.GetLocalValue("Result"),
                    Result = (VoteResult)vote.Result,
                    Id = voteId,
                    For = vote.For,
                    Against = vote.Against,
                    Abstained = vote.Abstained,
                    NotVote = vote.NotVote,
                    Absent = vote.Absent
                };

                return model;
            }

            return null;            
        }

        public CmsSessionAgendaList Agenda(int id)
        {
            var session = sessionRepo.GetById(id);
            var predicate = PredicateBuilder.True<SessionAgenda>();
            var now = DateTime.Now;

            if (session == null)
                return null;

            predicate = predicate.And(x => x.SessionId == id);

            var list =
                sessionAgendaRepo.GetList(predicate, x => x.OrderBy(o => o.Number), "Doc").Select(x =>
                    new
                    {
                        Id = x.Id,
                        Number = x.Number,
                        TitleRu = x.TitleRu,
                        TitleUk = x.TitleUk,
                        TitleEn = x.TitleEn,  
                        DocId = x.DocId, 
                        DocTitleUk = x.Doc != null ? x.Doc.TitleUk : null,
                        DocTitleRu = x.Doc != null ? x.Doc.TitleRu : null,
                        DocTitleEn = x.Doc != null ? x.Doc.TitleEn : null,
                    }).ToList().Select(x => new CmsSessionAgendaListItem()
                    {
                        Id = x.Id,
                        DocId = x.DocId,
                        Title = x.DocId > 0 ? x.GetLocalValue("DocTitle") : x.GetLocalValue("Title"),
                        Number = x.Number,                       
                    }).ToList(); 

            var model = new CmsSessionAgendaList()
            {
                SessionId = session.Id,
                SessionTitle = session.GetLocalValue("Title"),
                AdditionalAgendaId = session.AgendaAdditionId,
                AdditionalAgendaTitle = session.AgendaAddition != null ? session.AgendaAddition.GetLocalValue("Title") : null,
                Agenda = list,
                AgendaFile = session.AgendaFile
            };

            return model;
        }

        public CmsDoc Decree(int id)
        {
            var session = sessionRepo.GetById(id);

            if (session == null)
                return null;

            var model = docService.Doc(session.DecreeId ?? 0);

            if (model != null)
            {
                model.SessionId = session.Id;
                model.SessionTitle = session.GetLocalValue("Title");

                return model;
            }
            
            return null;
        }

        public CmsDoc Protocol(int id)
        {
            var session = sessionRepo.GetById(id);

            if (session == null)
                return null;

            var model = docService.Doc(session.ProtocolId ?? 0);
            
            if (model != null)
            {
                model.SessionId = session.Id;
                model.SessionTitle = session.GetLocalValue("Title");

                return model;
            }

            return null;
        }

        public CmsDoc Reglament(int id)
        {
            var session = sessionRepo.GetById(id);

            if (session == null)
                return null;

            var model = docService.Doc(session.ReglamentId ?? 0);
            
            if (model != null)
            {
                model.SessionId = session.Id;
                model.SessionTitle = session.GetLocalValue("Title");

                return model;
            }

            return null;
        }

        public CmsDoc AdditionalAgenda(int id)
        {
            var session = sessionRepo.GetById(id);

            if (session == null)
                return null;

            var model = docService.Doc(session.AgendaAdditionId ?? 0);

            

            if (model != null)
            {
                model.SessionId = session.Id;
                model.SessionTitle = session.GetLocalValue("Title");

                return model;
            }

            return null;
        }

        public CmsSessionMedia Media(int id)
        {
            var session = sessionRepo.GetById(id);

            if (session == null)
                return null;

            var model = new CmsSessionMedia()
            {
                SessionId = session.Id,
                SessionTitle = session.GetLocalValue("Title"),
                Audio = session.EmbedAudio,
                Video = session.EmbedVideo
            };
          
            return model;
        }

        public int GetCurrentSessionId()
        {
            var now = DateTime.Now;
            var session = sessionRepo.GetList(x => !x.Ended && x.Published && x.PublishDate <= now, x => x.OrderByDescending(o => o.SessionDate)).FirstOrDefault();

            if (session != null)
                return session.Id;
            else
                return 0;
        }
    }
}
