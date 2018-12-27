using System.Collections.Generic;
using System.Linq;
using Bissoft.CouncilCMS.Core;
using Bissoft.CouncilCMS.DAL.Entities;
using Bissoft.CouncilCMS.BLL.ViewModels;
using xTab.Tools.Helpers;
using System;

namespace Bissoft.CouncilCMS.BLL.Services
{
    public class CmsNewsletterService : BaseService
    {
        private IRepository<Newsletter, int> nlRepo;
        public CmsNewsletterService(string ConnectionString) : base(ConnectionString)
        {
            Initialize();
        }

        public CmsNewsletterService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            Initialize();
        }

        private void Initialize()
        {
            nlRepo = UnitOfWork.GetIntRepository<Newsletter>();
        }

        public NewsletterPreview GetPreview(Int32 Id)
        {
            var item = nlRepo.GetById(Id) ?? new Newsletter()
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
            model.File = item.FileUk;
            model.CreateDate = DateTimeHelper.DateString(item.CreateDate, DateTime.Now);
            model.PublishDate = DateTimeHelper.DateString(item.PublishDate, DateTime.Now);

            model.Articles = item.Articles.OrderBy(x => x.Position).Select(x => new NewsletterArticleItem()
            {
                Id = x.ArticleId,
                NewsletterId = x.NewsletterId,
                Title = x.Article.GetLocalValue("Title"),
                Description = x.Article.GetLocalValue("Description"),
                Image = x.Article.Image,
                                
                Date = DateTimeHelper.NullableDateTimeString(x.Article.PublishDate),
                Position = x.Position
            }).ToList();

            return model;
        }
    }
}
