using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using Bissoft.CouncilCMS.Core;
using Bissoft.CouncilCMS.DAL.Entities;
using System.Text;
using xTab.Tools.Extensions;
using System.Globalization;

namespace Bissoft.CouncilCMS.BLL.Services
{
    public class CmsRssService :BaseService
    {
        private IRepository<Article, int> articleRepo;
        private IRepository<Newsletter, int> newsletterRepo;
        private IRepository<Subscriber, int> subscriberRepo;
        private ContentAdminService contentService;
        public CmsRssService(string ConnectionString) : base(ConnectionString)
        {
            Initialize();
        }

        public CmsRssService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            Initialize();
        }

        private void Initialize()
        {
            articleRepo = UnitOfWork.GetIntRepository<Article>();
            newsletterRepo = UnitOfWork.GetIntRepository<Newsletter>();
            subscriberRepo = UnitOfWork.GetIntRepository<Subscriber>();
            contentService = new ContentAdminService(this.UnitOfWork);
        }


        public SyndicationFeed GetRssFeed(string baseDomain, string feedTitle, string feedDescription)
        {
            var list = articleRepo.GetList((x => x.Categories.Any(c => c.UrlName == "rss") && x.PublishDate <= DateTime.Now && x.Published == true), x => x.OrderByDescending(o => o.PublishDate), null, 0, 50);
            var feed = new SyndicationFeed(feedTitle, feedDescription, new Uri("http://" + baseDomain + "/rss/news"));
            var items = new List<SyndicationItem>();
           

            foreach (var item in list)
            {
                SyndicationItem feedItem =
                    new SyndicationItem(
                        item.TitleUk,
                        item.DescriptionUk,
                        new Uri("http://" + baseDomain + "/uk/articles/item/" + item.Id + "/" + item.UrlNameUk),
                        item.Id.ToString(),
                        item.PublishDate.Value);

                items.Add(feedItem);
            }

            feed.Items = items;
            feed.Language = "uk-UA";

            return feed;
        }

        public SyndicationFeed GetNewsletterFeed(string baseDomain, string feedTitle, string feedDescription)
        {
            var list = newsletterRepo.GetList((x => x.PublishDate <= DateTime.Now && x.Published == true), x => x.OrderByDescending(o => o.PublishDate), "", 0, 50, true, true);
            var feed = new SyndicationFeed(feedTitle, feedDescription, new Uri("http://" + baseDomain + "/rss/newsletters"));
            var items = new List<SyndicationItem>();
            
            foreach (var item in list)
            {
                SyndicationItem feedItem = new SyndicationItem(
                item.TitleUk + " №" + item.Number,
                item.DescriptionUk,
                new Uri("http://" + baseDomain + item.FileUk),
                item.Id.ToString(),
                item.PublishDate);
                items.Add(feedItem);
            }

            feed.Items = items;
            feed.Language = "uk-UA";

            return feed;
        }

        public string GetUkrNetFeed(string feedTitle, string feedDescription, string feedLogo, string siteUrl, string siteDomain)
        {
            StringBuilder rss = new StringBuilder();
            var list = articleRepo.GetList((x => x.Categories.Any(c => c.UrlName == "rss") && x.PublishDate <= DateTime.Now && x.Published == true), x => x.OrderByDescending(o => o.PublishDate), "Categories,ContentRows.ContentColumns.Contents", 0, 30).ToList();

            rss
                .Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>")
                .Append("<rss version=\"2.0\" xmlns=\"http://backend.userland.com/rss2\"  xmlns:yandex=\"http://news.yandex.ru\">")
                    .Append("<channel>")
                        .Append("<title>" + feedTitle + "</title>")
                        .Append("<link>" + siteUrl + "</link>")
                        .Append("<description>" + feedDescription + "</description>")
                        .Append("<image>")
                            .Append("<url>" + feedLogo + "</url>")
                            .Append("<title>" + feedTitle + "</title>")
                            .Append("<link>" + siteUrl + "</link>")
                        .Append("</image>");

            foreach (var item in list)
            {
                rss
                    .Append("<item>")
                        .Append("<title>").Append(item.TitleUk.ToRssText()).Append("</title>")
                        .Append("<link>").Append(siteDomain).Append("/uk/articles/item/").Append(item.Id).Append("</link>")
                        .Append("<description>").Append(item.DescriptionUk.ToRssText()).Append("</description>")
                        .Append("<pubDate>").Append(item.PublishDate.Value.ToString("ddd, dd MMM yyyy HH:mm:ss", new CultureInfo("en-US"))).Append(" +0300").Append("</pubDate>")
                        .Append(String.IsNullOrEmpty(item.Image) ? null : "<enclosure url=\"" + siteDomain + item.Image + "\" type=\"" + item.Image.MimeType() + "\"/>");

                var imgs = contentService.GetContentImages(item.ContentRows) ?? new List<string>();

                foreach (var img in imgs)
                {
                    if (!String.IsNullOrEmpty(img))
                    {
                        rss.Append("<enclosure url=\"" + siteDomain + img + "\" type=\"" + img.MimeType() + "\"/>");
                    }
                }

                rss
                    .Append("<yandex:full-text>").Append(contentService.GetContentText(item.ContentRows, false, false)[1].ToRssText()).Append("</yandex:full-text>")
                    .Append("</item>");
            }

            rss
                .Append("</channel>")
            .Append("</rss>");

            return rss.ToString();
        }

        public bool Subscribe(string email)
        {
            var entry = subscriberRepo.GetSingle(x => x.Email == email);

            if (entry != null)
                return false;

            subscriberRepo.Insert(new Subscriber() { Email = email, RegisterDate = DateTime.Now });
            UnitOfWork.Commit();

            return true;                
        }

        public bool UnSubscribe(string email)
        {
            var entry = subscriberRepo.GetSingle(x => x.Email == email);

            if (entry != null)
                return false;

            subscriberRepo.Delete(entry);
            UnitOfWork.Commit();

            return true;
        }
    }
}
