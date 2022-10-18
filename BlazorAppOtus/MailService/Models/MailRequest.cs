using Microsoft.AspNetCore.Http;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailService.Models
{
    public class MailRequest
    {
        public MailRequest()
        {

        }
        public MailRequest(IEnumerable<string> to, IEnumerable<string> cc, string subject, string body,List<FileAttachment> files=null)
        {
            this.To = to?.Select(x => MailboxAddress.Parse(x)).ToList();
            this.CC = cc?.Select(x => MailboxAddress.Parse(x)).ToList();
            this.Subject = subject;
            this.Body = body;
            this.Attachments = files;
        }
        public MailRequest(IEnumerable<string> to, string subject, string body)
        {
            this.To = to?.Select(x => MailboxAddress.Parse(x)).ToList();
            this.Subject = subject;
            this.Body = body;
        }
        public MailRequest(string to, string subject, string body)
        {

            this.To = new() { MailboxAddress.Parse(to) };
            this.Subject = subject;
            this.Body = body;
        }
        public MailRequest(string to, string subject,List<FileAttachment> files)
        {

            this.To = new() { MailboxAddress.Parse(to) };
            this.Subject = subject;
            this.Attachments = files;
        }
        public MailRequest(string to, string subject,FileAttachment files)
        {

            this.To = new() { MailboxAddress.Parse(to) };
            this.Subject = subject;
            this.Attachments = new() { files };
        }
        public MailRequest(string to, string subject, string body,List<FileAttachment> files)
        {

            this.To = new() { MailboxAddress.Parse(to) };
            this.Subject = subject;
            this.Body = body;
            this.Attachments = files;
        }
        public List<MailboxAddress> To { get; set; }
        public List<MailboxAddress> CC { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Attachment { get; set; }
        public List<FileAttachment> Attachments { get; set; }
        public string From { get; set; }
        public MimeMessage Content
        {
            get
            {
                MimeMessage mimeMessage = new();
                mimeMessage.To.AddRange(To);
                mimeMessage.Subject = Subject;
                mimeMessage.Sender = MailboxAddress.Parse(From);
                BodyBuilder builder = new();

                if (Attachments != null && Attachments.Any())
                {
                    //byte[] fileBytes;
                    foreach (var file in Attachments)
                    {
                        if (file.Data.Length > 0)
                        {
                            //using (StreamWriter writer = System.IO.File.CreateText(file.pa))
                            //using (MemoryStream ms = new())
                            //{
                            //    file.CopyTo(ms);
                            //    fileBytes = ms.ToArray();
                            //}
                            builder.Attachments.Add(file.FileName, file.Data, ContentType.Parse(file.ContentType));
                        }
                    }
                }
                builder.HtmlBody = Body;
                mimeMessage.Body = builder.ToMessageBody();

                return mimeMessage;
            }
        }
    }
}
