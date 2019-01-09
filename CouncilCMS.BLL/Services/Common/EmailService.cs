using System;
using System.IO;
using System.Web;
using System.Net.Mail;
using RazorEngine;
using RazorEngine.Templating;
using System.Collections.Generic;
using System.Threading.Tasks;
using xTab.Tools.Helpers;

namespace Bissoft.CouncilCMS.BLL.Services
{
    public class EmailService
    {
        private SmtpClient smtpClient;
        public String defaultFrom;
        public String defaultName;

        public EmailService(SmtpClient smtpClient, String From, String Name)
        {
            this.smtpClient = smtpClient;
            this.defaultFrom = From;
            this.defaultName = Name;
        }

        public bool SendEmail(String to, String subject, String text, Boolean isHtml = true)
        {
            var message = new MailMessage();

            message.From = new MailAddress(defaultFrom, defaultName);
            message.To.Add(to);
            message.Subject = subject;
            message.Body = text;
            message.IsBodyHtml = isHtml;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            try
            {
                smtpClient.Send(message);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SendEmailWithModel(String to, String subject, String viewName, Object model, DynamicViewBag viewBag = null, List<string> attachments = null)
        {
            var message = new MailMessage();
            var template = File.ReadAllText(HttpContext.Current.Server.MapPath("~/Views/EmailTemplates/" + viewName + ".cshtml"));
            var text = Engine.Razor.RunCompile(template, "template" + viewName, null, model, viewBag);

            message.From = new MailAddress(defaultFrom, defaultName);
            message.To.Add(to);
            message.Subject = subject;
            message.Body = text;
            message.IsBodyHtml = true;
            message.BodyEncoding = System.Text.Encoding.UTF8;

            if (attachments != null && attachments.Count > 0)
            {
                foreach (var attach in attachments)
                {
                    message.Attachments.Add(new Attachment(HttpContext.Current.Server.MapPath(attach)));
                }
            }

            try
            {
                smtpClient.Send(message);
                return true;
            }
            catch(Exception)
			{
                return false;
            }
        }

        public bool SendEmailBatchWithModel(List<string> to, String subject, String viewName, Object model, DynamicViewBag viewBag = null, List<string> attachments = null)
        {
            var message = new MailMessage();
            var template = File.ReadAllText(HttpContext.Current.Server.MapPath("~/Views/EmailTemplates/" + viewName + ".cshtml"));
            var text = Engine.Razor.RunCompile(template, "template" + viewName, null, model, viewBag);

            message.From = new MailAddress(defaultFrom, defaultName);            
            message.Subject = subject;
            message.Body = text;
            message.IsBodyHtml = true;
            message.BodyEncoding = System.Text.Encoding.UTF8;

            if (attachments != null && attachments.Count > 0)
            {
                foreach (var attach in attachments)
                {
                    message.Attachments.Add(new Attachment(HttpContext.Current.Server.MapPath(attach)));
                }
            }

            foreach (var email in to)
            {
                message.To.Clear();
                message.To.Add(email);

                try
                {
                    smtpClient.Send(message);
                    return true;
                }
                catch(Exception)
				{
                    return false;
                }
            }

            return true;
        }

        public async Task SendEmailBatchWithModelAsync(List<string> to, String subject, String viewName, Object model, DynamicViewBag viewBag = null, List<string> attachments = null)
        {
            var message = new MailMessage();
            var template = File.ReadAllText(HttpContext.Current.Server.MapPath("~/Views/EmailTemplates/" + viewName + ".cshtml"));
            var text = Engine.Razor.RunCompile(template, "template" + RandomHelper.RandomString(6), null, model, viewBag);

            message.From = new MailAddress(defaultFrom, defaultName);
            message.Subject = subject;
            message.Body = text;
            message.IsBodyHtml = true;
            message.BodyEncoding = System.Text.Encoding.UTF8;

            if (attachments != null && attachments.Count > 0)
            {
                foreach (var attach in attachments)
                {
                    message.Attachments.Add(new Attachment(HttpContext.Current.Server.MapPath(attach)));
                }
            }

            foreach (var email in to)
            {
                message.To.Clear();
                message.To.Add(email);

                try
                {
                    await smtpClient.SendMailAsync(message);
                }
                catch(Exception) { }
            }            
        }


        //public void SendEmail(String to, String subject, String text)
        //{

        //    var message = new MailMessage();

        //    message.From = smtpClient.Host;
        //    message.To.Add(to);
        //    message.Subject = subject;
        //    message.Body = text;
        //    message.IsBodyHtml = true;
        //    message.BodyEncoding = System.Text.Encoding.UTF8;

        //    smtpClient.Send(message);
        //}
    }
}
