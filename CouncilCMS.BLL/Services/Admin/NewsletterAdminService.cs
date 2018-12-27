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

namespace Bissoft.CouncilCMS.BLL.Services
{
    public class NewsletterAdminService : BaseService
    {
        private IRepository<Newsletter, int> newsletterRepo;
        private IRepository<Article, int> articleRepo;
        private IRepository<Subscriber, int> subscriberRepo;
        private IRepository<NewsletterEmailQueue, int> queueRepo;
        private ContentAdminService contentAdminService;        

        public NewsletterAdminService(string ConnectionString) : base(ConnectionString)
        {
            Initialize();
        }

        public NewsletterAdminService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            Initialize();
        }

        private void Initialize()
        {
            newsletterRepo = UnitOfWork.GetRepository<Newsletter, int>();
            articleRepo = UnitOfWork.GetRepository<Article, int>();
            subscriberRepo = UnitOfWork.GetRepository<Subscriber, int>();
            queueRepo = UnitOfWork.GetRepository<NewsletterEmailQueue, int>();
            contentAdminService = new ContentAdminService(this.UnitOfWork);
        }

        public NewsletterList List(int page = 1, string query = null, string from = null, string to = null)
        {
            var model = new NewsletterList()
            {
                Query = query,
                Page = page,
                From = from,
                To = to
            };

            return model;
        }
        public List<NewsletterListItem> GetList(out int count, int page = 1, int perPage = 50, string query = null, string to = null, string from = null)
        {
            var predicate = PredicateBuilder.True<Newsletter>();

            if (!String.IsNullOrEmpty(query))
                predicate = predicate.And(x => x.TitleRu.Contains(query) || x.TitleUk.Contains(query));

            if (!String.IsNullOrEmpty(from))
            {
                var fromDate = DateTimeHelper.ParseDate(from, DateTime.Now);
                predicate = predicate.And(x => x.CreateDate >= fromDate);
            }

            if (!String.IsNullOrEmpty(to))
            {
                var toDate = DateTimeHelper.ParseDate(to, DateTime.Now);
                predicate = predicate.And(x => x.CreateDate <= toDate);
            }

            var list = newsletterRepo.GetList(out count, predicate, x => x.OrderBy(o => o.Id), null, (page - 1) * perPage, perPage, true, false);

            var model = list.Select(x =>
                new
                {
                    Id = x.Id,
                    TitleRu = x.TitleRu,
                    TitleUk = x.TitleUk,
                    Saved = x.Saved,
                    Number = x.Number,
                    FileUk = x.FileUk,
                    FileRu = x.FileRu,
                    CreateDate = x.CreateDate,
                    LastEditDate = x.LastEditDate,
                    PublishDate = x.PublishDate,
                    LastBulkDate = x.LastBulkDate,
                    Published = x.Published
                }).ToList()
                .Select(x => new NewsletterListItem()
                {
                    Id = x.Id,
                    Title = x.GetLocalValue("Title"),
                    File = x.GetLocalValue("File"),
                    Number = x.Number,
                    CreateDate = x.CreateDate,
                    LastEditDate = x.LastEditDate,
                    PublishDate = x.PublishDate,
                    LastBulkDate = x.LastBulkDate,                   
                }).ToList();

            return model;
        }

        public NewsletterEdit GetForEdit(Int32 Id)
        {

            var item = newsletterRepo.GetById(Id) ?? new Newsletter()
            {
                Id = 0,
                TitleUk = "Електронна газета",
                TitleRu = "Электронная газета",
                Number = "00",
                Articles = new List<DAL.Entities.NewsletterArticle>(),
                CreateDate = DateTime.Now,
                PublishDate = DateTime.Now,
                LastEditDate = DateTime.Now,
            };

            if (Id == 0)
            {
                newsletterRepo.Insert(item);
                UnitOfWork.Commit();
            }

            var model = new NewsletterEdit();

            model.Id = item.Id;
            model.TitleRu = item.TitleRu;
            model.TitleUk = item.TitleUk;
            model.DescriptionRu = item.DescriptionRu;
            model.DescriptionUk = item.DescriptionUk;
            model.FileRu = item.FileRu;
            model.FileUk = item.FileUk;
            model.Number = item.Number;
            model.CreateDate = DateTimeHelper.DateString(item.CreateDate, DateTime.Now);
            model.PublishDate = DateTimeHelper.DateString(item.PublishDate, DateTime.Now);
            model.Published = item.Published;

            model.Articles = item.Articles.OrderBy(x => x.Position).Select(x => new NewsletterArticleItem()
            {
                Id = x.ArticleId,
                NewsletterId = x.NewsletterId,
                Title = x.Article.GetLocalValue("Title"),
                Html = GetArticleHtml(x.Article),
                Image = x.Article.Image,
                Date = DateTimeHelper.NullableDateTimeString(x.Article.PublishDate),
                Position = x.Position
            }).ToList();

            return model;
        }

        public NewsletterPreview GetPreview(Int32 Id)
        {
            var item = newsletterRepo.GetById(Id) ?? new Newsletter()
            {
                Id = 0,
                Articles = new List<DAL.Entities.NewsletterArticle>(),
                CreateDate = DateTime.Now,
                PublishDate = DateTime.Now,
                LastEditDate = DateTime.Now
            };


            var model = new NewsletterPreview();

            model.Id = item.Id;

            model.TitleUk = item.TitleUk;

            model.DescriptionUk = item.DescriptionUk;
            model.Number = item.Number;
            
            model.CreateDate = DateTimeHelper.DateString(item.CreateDate, DateTime.Now);
            model.PublishDate = DateTimeHelper.DateString(item.PublishDate, DateTime.Now);

            model.Articles = item.Articles.OrderBy(x => x.Position).Select(x => new NewsletterArticleItem()
            {
                Id = x.ArticleId,
                NewsletterId = x.NewsletterId,
                Title = x.Article.GetLocalValue("Title"),
                Image = x.Article.Image,
                Html = GetArticleHtml(x.Article),
                Date = DateTimeHelper.NullableDateTimeString(x.Article.PublishDate),
                Position = x.Position
            }).ToList();

            return model;
        }

        public NewsletterArticleItem AddArticleToNewsletter(int articleId, int newsletterId)
        {
            var newsletter = newsletterRepo.GetById(newsletterId);
            var article = articleRepo.GetById(articleId);

            var position = (newsletter.Articles.Select(x => x.Position).DefaultIfEmpty(0).Max() + 1);

            var newsletterArticle = new NewsletterArticle() { ArticleId = articleId, NewsletterId = newsletterId, Position = position };

            newsletter.Articles.Add(newsletterArticle);

            newsletterRepo.Update(newsletter);
            UnitOfWork.Commit();

            var model = new NewsletterArticleItem()
            {
                Id = article.Id,
                NewsletterId = newsletter.Id,
                Title = article.GetLocalValue("Title"),
                Image = article.Image,
                Html = GetArticleHtml(article),
                Date = DateTimeHelper.NullableDateTimeString(article.PublishDate),
                Position = position
            };

            return model;
        }

        public NewsletterEdit Save(NewsletterEdit model)
        {
            var item = newsletterRepo.GetById(model.Id);

            item.Id = model.Id;
            item.TitleRu = model.TitleRu;
            item.TitleUk = model.TitleUk;
            item.DescriptionRu = model.DescriptionRu;
            item.DescriptionUk = model.DescriptionUk;
            item.UrlNameRu = model.TitleRu.Translit();
            item.UrlNameUk = model.TitleUk.Translit();
            item.Number = model.Number;
            item.Saved = true;
            item.Published = model.Published;
            item.LastEditDate = DateTime.Now;
            item.PublishDate = DateTimeHelper.ParseDate(model.PublishDate, DateTime.Now, DateTimeHelper.DefaultTimeOfDay.None);

            UnitOfWork.Commit();

            return model;
        }

        public String GetArticleHtml(Article article)
        {
            if (article != null)
            {
                var result = String.Empty;

                foreach (var row in article.ContentRows)
                {
                    foreach (var col in row.ContentColumns)
                    {
                        foreach (var content in col.Contents)
                        {
                            switch (content.Type)
                            {
                                case (int)ContentType.Ckeditor:
                                    result += content.BodyUk;
                                    break;
                                case (int)ContentType.Image:
                                    result += content.BodyUk;
                                    break;
                                case (int)ContentType.Gallery:
                                    result += content.BodyUk;
                                    break;
                                case (int)ContentType.Table:
                                    result += content.BodyUk;
                                    break;
                                case (int)ContentType.TextBlock:
                                    result += content.BodyUk;
                                    break;
                            }
                        }
                    }
                }

                return result;
            }

            return null;
        }

        public void SaveFile(Int32 id, String filePath)
        {
            var newsletter = newsletterRepo.GetById(id);

            newsletter.FileUk = filePath;
            newsletter.FileRu = filePath;

            newsletterRepo.Update(newsletter);
            UnitOfWork.Commit();
        }

        public void CreateBulk(Int32 id)
        {
            var newsletter = newsletterRepo.GetById(id);

            newsletter.Sended = true;
            newsletter.LastBulkDate = DateTime.Now;

            newsletterRepo.Update(newsletter);
            UnitOfWork.Commit();
        }

        public void UpdatePosition(Int32 articleId, int? newsletterId, Int32 oldPosition, Int32 newPosition)
        {
            var result = 0;

            if (oldPosition > newPosition)
            {
                result = newsletterRepo
                    .Context
                    .ExecuteSqlCommand("UPDATE NewsletterArticles SET Position = Position + 1 WHERE Position >= {0} AND Position < {1} AND NewsletterId = {2}", false, null, newPosition, oldPosition, newsletterId);
            }
            else if (oldPosition < newPosition)
            {
                result = newsletterRepo
                   .Context
                   .ExecuteSqlCommand("UPDATE NewsletterArticles SET Position = Position - 1 WHERE Position <= {0} AND Position > {1}  AND NewsletterId = {2}", false, null, newPosition, oldPosition, newsletterId);
            }

            result = newsletterRepo
                   .Context
                   .ExecuteSqlCommand("UPDATE NewsletterArticles SET Position = {0} WHERE ArticleId = {1} AND NewsletterId = {2}", false, null, newPosition, articleId, newsletterId);
        }

        public void RemoveArtcileFromNewsletter(int articleId, int newsletterId)
        {
            var newsletter = newsletterRepo.GetById(newsletterId);
            var article = articleRepo.GetById(articleId);
            var item = newsletter.Articles.Where(x => x.ArticleId == articleId).FirstOrDefault();

            UnitOfWork.DefineAsDeleted<NewsletterArticle, int>(item);
          
            UnitOfWork.Commit();

            newsletterRepo
                .Context
                .ExecuteSqlCommand("UPDATE NewsletterArticles SET Position = Position - 1 WHERE Position > {0} AND NewsletterId = {1}", false, null, item.Position, item.NewsletterId);
        }

        public void Delete(Int32 Id)
        {
            var item = newsletterRepo.GetById(Id);

            item.Deleted = true;

            newsletterRepo.Update(item);

            UnitOfWork.Commit();
        }

        public void Remove(Int32 Id)
        {
            var item = newsletterRepo.GetById(Id);

            newsletterRepo.Delete(item);

            UnitOfWork.Commit();
        }

        public void Send(int id)
        {
            var entry = newsletterRepo.GetById(id);

            if (entry != null)
            {
                entry.Sended = true;

                var emails = subscriberRepo.GetList().ToList();

                foreach (var emailBatch in emails.Batch(100))
                {
                    foreach (var email in emailBatch)
                    {
                        var queue = new NewsletterEmailQueue() { Email = email.Email, NewsletterId = id };
                        queueRepo.Insert(queue);
                    }

                    UnitOfWork.Commit();                   
                }
            }
        }
    }
}