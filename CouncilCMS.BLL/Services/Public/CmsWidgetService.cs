using AngleSharp.Parser.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Threading.Tasks;
using xTab.Tools.Extensions;
using System.Web;
using xTab.Tools.Helpers;
using Bissoft.CouncilCMS.Core;

namespace Bissoft.CouncilCMS.BLL.Services
{
    public class CmsWidgetService : BaseService
    {
        private CmsMenuService menuService;

        public CmsWidgetService(string ConnectionString) : base(ConnectionString)
        {
            Initialize();
        }

        public CmsWidgetService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            Initialize();
        }

        private void Initialize()
        {
            menuService = new CmsMenuService(this.UnitOfWork);
        }


        ////http://files.dniprorada.gov.ua/index2.php
        ////Пошук проектів документів Дніпровської міської ради
        //public List<CouncilProjectDocumentItem> CouncilProjectDocuments(String begProjPublicDate = "", string endProjPublicDate = "", string docHeader = "", Int32? docTypeCode = null)
        //{
        //    var model = new List<CouncilProjectDocumentItem>();
        //    string html = string.Empty;
        //    if (docTypeCode < 1)
        //        docTypeCode = null;
        //    string address = "http://89.251.26.216:8050/WebSolution2/wsGetPublicProjects?pDocTypeCode=" + docTypeCode + "&pBegProjPublicDate=" + begProjPublicDate + "&pEndProjPublicDate=" + endProjPublicDate + "&pDocHeader=" + HttpUtility.UrlEncode(docHeader, Encoding.GetEncoding(1251));

        //    using (WebClient client = new WebClient())
        //    {
        //        html = client.DownloadString(address);
        //    }

        //    var parser = new HtmlParser();
        //    var doc = parser.Parse(html);
        //    var table = doc.GetElementsByTagName("table").FirstOrDefault();


        //    if (table != null)
        //    {
        //        foreach (var item in table.GetElementsByTagName("tr"))
        //        {
        //            var document = new CouncilProjectDocumentItem();
        //            document.DocumentNumber = item.Children[0].TextContent;
        //            document.DatePublish = item.Children[1].InnerHtml;
        //            document.Type = item.Children[2].TextContent;
        //            document.About = item.Children[3].TextContent;
        //            var button = item.Children[4];
        //            document.DownloadUrl = new List<string>();

        //            if (button.ChildElementCount >= 1)
        //            {
        //                foreach (var item2 in button.GetElementsByTagName("input"))
        //                {
        //                    document.DownloadUrl.Add(item2.GetAttribute("onclick").Replace("location.href=", "").Replace("\'", ""));
        //                }
        //            }

        //            model.Add(document);
        //        }

        //        model.Remove(model[0]);

        //        return model;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        ////http://files.dniprorada.gov.ua/index.php
        ////Пошук прийнятих документів Дніпровської міської ради
        //public List<AcceptCouncilDocumentItem> AcceptCouncilDocuments(Int32? DocTypeCode = null, Int32? DocNumberN = null, string DocNumberC = null, String BegDocDate = null,
        //    String EndDocDate = null, string DocHeader = null)
        //{
        //    var model = new List<AcceptCouncilDocumentItem>();
        //    string html = string.Empty;
        //               if (DocTypeCode < 1)
        //        DocTypeCode = null;

        //    string address = "http://89.251.26.216:8050/WebSolution2/wsGetPublicDocuments?pDocTypeCode=" + DocTypeCode +
        //        "&pDocNumberN=" + DocNumberN +
        //        "&pDocNumberC=" + HttpUtility.UrlEncode(DocNumberC, Encoding.GetEncoding(1251))+
        //        "&pBegDocDate=" + BegDocDate +
        //        "&pEndDocDate=" + EndDocDate +
        //        "&pDocHeader=" + HttpUtility.UrlEncode(DocHeader, Encoding.GetEncoding(1251));

        //    using (WebClient client = new WebClient())
        //    {
        //        html = client.DownloadString(address);
        //    }

        //    var parser = new HtmlParser();
        //    var doc = parser.Parse(html);
        //    var table = doc.GetElementsByTagName("table").FirstOrDefault();

        //    if(table != null)
        //    {
        //        foreach (var item in table.GetElementsByTagName("tr"))
        //        {
        //            var document = new AcceptCouncilDocumentItem();

        //            document.Id = item.Children[0].TextContent;
        //            document.Number = item.Children[1].TextContent;
        //            document.DatePublish = item.Children[2].InnerHtml;
        //            document.Type = item.Children[3].TextContent;
        //            document.About = item.Children[4].TextContent;
        //            var button = item.Children[5];

        //            document.DownloadUrl = new List<string>();

        //            if (button.ChildElementCount >= 1)
        //            {
        //                foreach (var item2 in button.GetElementsByTagName("input"))
        //                {
        //                    document.DownloadUrl.Add(item2.GetAttribute("onclick").Replace("location.href=", "").Replace("\'", ""));
        //                }
        //            }

        //            model.Add(document);
        //        }

        //        model.Remove(model[0]);

        //        return model;
        //    }
        //    else
        //    {
        //        return null;
        //    }
           
        //}

        ////http://oblik.dniprorada.gov.ua/index.php
        ////Облік публічної інформації
        ////для принятых документов
        //public List<AccountingAcceptDocumentItem> AccountingAcceptDocuments(String BegDate = null, String EndDate = null)
        //{
        //    var model = new List<AccountingAcceptDocumentItem>();

        //    string html = String.Empty;
        //    if (String.IsNullOrEmpty(BegDate))
        //    {
        //        BegDate = null;
        //    }
        //    else
        //    {
        //        BegDate = DateTime.Parse(BegDate).ToString("yyyy.MM.dd");
        //    }
        //    if (String.IsNullOrEmpty(EndDate))
        //    {
        //        EndDate = null;
        //    }
        //    else
        //    {
        //        EndDate = DateTime.Parse(EndDate).ToString("yyyy.MM.dd");
        //    }

        //    string address = "http://89.251.26.216:8050/WebSolution2/wsGetRegPublicDocuments?pBegDate=" + BegDate + "&pEndDate=" + EndDate;

        //    using (WebClient client = new WebClient())
        //    {
        //        html = client.DownloadString(address);
        //    }

        //    var parser = new HtmlParser();
        //    var doc = parser.Parse(html);
        //    var table = doc.GetElementsByTagName("table").FirstOrDefault();

        //    if (table != null)
        //    {
        //        foreach (var item in table.GetElementsByTagName("tr"))
        //        {
        //            var document = new AccountingAcceptDocumentItem();

        //            document.Id = item.Children[0].TextContent;
        //            document.Name = item.Children[1].TextContent;
        //            document.DatePublish = item.Children[2].TextContent;
        //            document.Number = item.Children[3].TextContent;
        //            document.Type = item.Children[4].TextContent;
        //            document.DateReester = item.Children[5].TextContent;
        //            document.DocumentCreater = item.Children[6].TextContent;
        //            document.Branch = item.Children[7].TextContent;
        //            document.DocumentType = item.Children[8].TextContent;
        //            document.SaveForm = item.Children[9].TextContent;
        //            document.PlaceSave = "Департамент забезпечення діяльності виконавчих органів Дніпровської міської ради";

        //            var button = item.Children[11];

        //            document.DownloadUrl = new List<string>();

        //            if (button.ChildElementCount >= 1)
        //            {
        //                foreach (var item2 in button.GetElementsByTagName("input"))
        //                {
        //                    document.DownloadUrl.Add(item2.GetAttribute("onclick").Replace("location.href=", "").Replace("\'", ""));
        //                }
        //            }

        //            model.Add(document);
        //        }

        //        model.Remove(model[0]);

        //        return model;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        ////http://oblik.dniprorada.gov.ua/index2.php
        ////Облік публічної інформації
        ////для проектов
        //public List<AccountingAcceptProjectDocumentItem> AccountingAcceptProjectDocuments(String BegDate = null, String EndDate = null)
        //{
        //    var model = new List<AccountingAcceptProjectDocumentItem>();

        //    if (String.IsNullOrEmpty(BegDate))
        //    {
        //        BegDate = null;
        //    }
        //    else
        //    {
        //        BegDate = DateTime.Parse(BegDate).ToString("yyyy.MM.dd");
        //    }

        //    if (String.IsNullOrEmpty(EndDate))
        //    {
        //        EndDate = null;
        //    }
        //    else
        //    {
        //        EndDate = DateTime.Parse(EndDate).ToString("yyyy.MM.dd");
        //    }

        //    string html = String.Empty;
        //    string address = "http://89.251.26.216:8050/WebSolution2/wsGetRegPublicProjects?pBegDate=" + BegDate + "&pEndDate=" + EndDate;

        //    using (WebClient client = new WebClient())
        //    {
        //        html = client.DownloadString(address);
        //    }

        //    var parser = new HtmlParser();
        //    var doc = parser.Parse(html);
        //    var table = doc.GetElementsByTagName("table").FirstOrDefault();

        //    if (table != null)
        //    {
        //        foreach (var item in table.GetElementsByTagName("tr"))
        //        {
        //            var document = new AccountingAcceptProjectDocumentItem();

        //            document.Id = item.Children[0].TextContent;
        //            document.Name = item.Children[1].TextContent;
        //            document.Type = item.Children[2].TextContent;
        //            document.DateReester = item.Children[3].TextContent;
        //            document.DocumentCreater = item.Children[4].TextContent;
        //            document.Branch = item.Children[5].TextContent;
        //            document.DocumentType = item.Children[6].TextContent;

        //            var button = item.Children[7];

        //            document.DownloadUrl = new List<string>();

        //            if (button.ChildElementCount >= 1)
        //            {
        //                foreach (var item2 in button.GetElementsByTagName("input"))
        //                {
        //                    document.DownloadUrl.Add(item2.GetAttribute("onclick").Replace("location.href=", "").Replace("\'", ""));
        //                }
        //            }

        //            model.Add(document);
        //        }

        //        model.Remove(model[0]);

        //        return model;
        //    }
        //    else
        //    {
        //        return null;
        //    }

        //}

        //// http://plan.dniprorada.gov.ua/index2.php
        ////Возвращает найденные проекты документов в виде html-страницы для раздела сайта "Планы проведения заседаний исполкома - очередное заседание исполкома":
        //public List<ExecutiveCommitteeCurrentItem> ExecutiveCommitteeCurrent()
        //{
        //    var model = new List<ExecutiveCommitteeCurrentItem>();

        //    string html = String.Empty;

        //    string address = "http://89.251.26.216:8050/WebSolution2/wsGetNextMeetingExecutiveCommitteeDoc";

        //    using (WebClient client = new WebClient())
        //    {
        //        html = client.DownloadString(address);
        //    }

        //    var parser = new HtmlParser();
        //    var doc = parser.Parse(html);
        //    var table = doc.GetElementsByTagName("table").FirstOrDefault();


        //    foreach (var item in table.GetElementsByTagName("tr"))
        //    {
        //        var document = new ExecutiveCommitteeCurrentItem();
        //        document.Id = item.Children[0].TextContent;
        //        document.About = item.Children[1].TextContent;
        //        var button = item.Children[2];
        //        document.DownloadUrl = new List<string>();

        //        if (button.ChildElementCount >= 1)
        //        {
        //            foreach (var item2 in button.GetElementsByTagName("input"))
        //            {
        //                document.DownloadUrl.Add(item2.GetAttribute("onclick").Replace("location.href=", "").Replace("\'", ""));
        //            }
        //        }
        //        model.Add(document);
        //    }

        //    model.Remove(model[0]);

        //    return model;
        //}
        
        ////http://plan.dniprorada.gov.ua/index5.php
        ////Возвращает найденные принятые документы в виде html-страницы для раздела сайта "Планы проведения заседаний исполкома за предыдущий период":
        //public List<ExecutiveCommitteePlanItem> ExecutiveCommitteePlan()
        //{
        //    var model = new List<ExecutiveCommitteePlanItem>();

        //    string html = String.Empty;

        //    string address = "http://89.251.26.216:8050/WebSolution2/wsGetPrevMeetingExecutiveCommitteeDoc";

        //    using (WebClient client = new WebClient())
        //    {
        //        html = client.DownloadString(address);
        //    }

        //    var parser = new HtmlParser();
        //    var doc = parser.Parse(html);
        //    var table = doc.GetElementsByTagName("table").FirstOrDefault();


        //    foreach (var item in table.GetElementsByTagName("tr"))
        //    {
        //        var document = new ExecutiveCommitteePlanItem();
        //        document.Id = item.Children[0].TextContent;
        //        document.Number = item.Children[1].TextContent;
        //        document.Date = item.Children[2].TextContent;
        //        document.About = item.Children[3].TextContent;
        //        var button = item.Children[4];

        //        document.DownloadUrl = new List<string>();

        //        if (button.ChildElementCount >= 1)
        //        {
        //            foreach (var item2 in button.GetElementsByTagName("input"))
        //            {
        //                document.DownloadUrl.Add(item2.GetAttribute("onclick").Replace("location.href=", "").Replace("\'", ""));
        //            }
        //        }
        //        model.Add(document);
        //    }

        //    model.Remove(model[0]);

        //    return model;
        //}

        ////Возвращает найденные проекты документов в виде html-страницы для раздела сайта "Планы проведения сессионного заседания - очередное заседание сессии горсовета"
        ////http://plan.dniprorada.gov.ua/index.php
        //public List<CouncilSessionCurrentItem> CouncilSessionCurrent()
        //{
        //    var model = new List<CouncilSessionCurrentItem>();

        //    string html = String.Empty;

        //    string address = "http://89.251.26.216:8050/WebSolution2/wsGetNextSessionCityCouncilDoc";

        //    using (WebClient client = new WebClient())
        //    {
        //        html = client.DownloadString(address);
        //    }

        //    var parser = new HtmlParser();
        //    var doc = parser.Parse(html);
        //    var table = doc.GetElementsByTagName("table").FirstOrDefault();


        //    foreach (var item in table.GetElementsByTagName("tr"))
        //    {
        //        var document = new CouncilSessionCurrentItem();
        //        document.Id = item.Children[0].TextContent;
        //        document.About = item.Children[1].TextContent;
        //        var button = item.Children[2];
        //        document.DownloadUrl = new List<string>();

        //        if (button.ChildElementCount >= 1)
        //        {
        //            foreach (var item2 in button.GetElementsByTagName("input"))
        //            {
        //                document.DownloadUrl.Add(item2.GetAttribute("onclick").Replace("location.href=", "").Replace("\'", ""));
        //            }
        //        }
        //        model.Add(document);
        //    }

        //    model.Remove(model[0]);

        //    return model;
        //}

        //// Возвращает найденные принятые документы в виде html-страницы для раздела сайта "Планы проведения сессионного заседания - планы заседаний за предыдущий период".
        ////http://plan.dniprorada.gov.ua/index4.php
        //public List<CouncilSessionPlanItem> CouncilSessionPlan()
        //{
        //    var model = new List<CouncilSessionPlanItem>();

        //    string html = String.Empty;

        //    string address = "http://89.251.26.216:8050/WebSolution2/wsGetPrevSessionCityCouncilDoc";

        //    using (WebClient client = new WebClient())
        //    {
        //        html = client.DownloadString(address);
        //    }

        //    var parser = new HtmlParser();
        //    var doc = parser.Parse(html);
        //    var table = doc.GetElementsByTagName("table").FirstOrDefault();


        //    foreach (var item in table.GetElementsByTagName("tr"))
        //    {
        //        var document = new CouncilSessionPlanItem();
        //        document.Id = item.Children[0].TextContent;
        //        document.Number = item.Children[1].TextContent;
        //        document.Date = item.Children[2].TextContent;
        //        document.About = item.Children[3].TextContent;
        //        var button = item.Children[4];
        //        document.DownloadUrl = new List<string>();

        //        if (button.ChildElementCount >= 1)
        //        {
        //            foreach (var item2 in button.GetElementsByTagName("input"))
        //            {
        //                document.DownloadUrl.Add(item2.GetAttribute("onclick").Replace("location.href=", "").Replace("\'", ""));
        //            }
        //        }
        //        model.Add(document);
        //    }

        //    model.Remove(model[0]);

        //    return model;
        //}

        //public Tuple<byte[], string> DownloadFile(string url)
        //{
        //    var data = new byte[] { };
        //    string zipPath = HttpContext.Current.Server.MapPath("/upload/temp_doc/" + RandomHelper.RandomFileName() + ".zip");
        //    string filePath = "";
        //    string fileFormat = "";

        //    using (var client = new WebClient())
        //    {
        //        client.DownloadFile("http://89.251.26.216:8050" + url, zipPath);
        //    }

        //    using (ZipArchive archive = ZipFile.OpenRead(zipPath))
        //    {
        //        foreach (ZipArchiveEntry entry in archive.Entries)
        //        {
        //            fileFormat = Path.GetExtension(entry.FullName);
        //            filePath = HttpContext.Current.Server.MapPath("../upload/temp_doc/" + RandomHelper.RandomFileName() + fileFormat);
        //            entry.ExtractToFile(filePath);
        //        }
        //    }
        //    data = File.ReadAllBytes(filePath);
        //    File.Delete(zipPath);
        //    File.Delete(filePath);
        //    return new Tuple<byte[], string>(data, fileFormat);
        //}
    }
}
