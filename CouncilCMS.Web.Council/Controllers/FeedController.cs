using System;
using System.Web.Mvc;
using Bissoft.CouncilCMS.BLL.Services;
using Bissoft.CouncilCMS.Core.Enums;
using System.Xml;
using System.Text;
using System.ServiceModel.Syndication;
using System.Web;
using System.Linq;

namespace Bissoft.CouncilCMS.Web.Controllers
{
    public class RssController : BaseCmsController
    {
        private CmsRssService rssServce;

        public RssController()
        {
            rssServce = new CmsRssService(connectionString);
        }
        
        public ActionResult News()
        {
            var model = rssServce.GetRssFeed(CmsSettings.BaseDomain, "Дніпровська міська рада", "Розсилка новин Дніпровської міської ради");

            return new SyndicationResult(model);
        }

        public ActionResult Newsletters()
        {
            var model = rssServce.GetNewsletterFeed(CmsSettings.BaseDomain, "Дніпровська міська рада", "Розсилка електронної газети Дніпровської міської ради");

            return new SyndicationResult(model);
           
        }

        public ActionResult UkrNet()
        {
            var feed = rssServce.GetUkrNetFeed(CmsSettings.TitleUk, "Новини Запорізької міської ради", CmsSettings.BaseDomain + "/content/img/rss_logo.png", CmsSettings.BaseDomain + "/uk", CmsSettings.BaseDomain);
            
            return new XmlActionResult() { Feed = feed };
        }
    }



    public class SyndicationResult : ActionResult
    {
        private readonly SyndicationFeed feed;
        public SyndicationResult(SyndicationFeed feed)
        {
            if (feed == null)
            {
                throw new HttpException(401, "Not found");
            }

            this.feed = feed;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var settings = new XmlWriterSettings
            {
                Encoding = Encoding.UTF8,
                NewLineHandling = NewLineHandling.Entitize,
                NewLineOnAttributes = true,
                Indent = true
            };

            var response = context.HttpContext.Response;
            response.ContentType = "application/rss+xml; charset=utf-8";

            using (var writer = XmlWriter.Create(response.OutputStream, settings))
            {
                this.feed.SaveAsRss20(writer);
            }
        }
    }

    public class XmlActionResult : ActionResult
    {
        public String Feed { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.ContentType = "text/xml";
            context.HttpContext.Response.OutputStream.Write(Encoding.UTF8.GetBytes(Feed), 0, Encoding.UTF8.GetBytes(Feed).Count());
        }
    }
}