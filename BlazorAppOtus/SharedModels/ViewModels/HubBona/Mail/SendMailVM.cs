using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.ViewModels.HubBona.Mail
{
    public class SendMailVM
    {
        public SendMailVM()
        {

        }
        public SendMailVM(string to, string subject, string body)
        {
            this.ToEmail = to;
            this.Subject = subject;
            this.Body = body;
        }
        public SendMailVM(string to, IEnumerable<string> cc, string subject, string body)
        {
            this.ToEmail = to;
            this.CC = cc;
            this.Subject = subject;
            this.Body = body;
        }
        public SendMailVM(string to, string cc, string subject, string body)
        {
            this.ToEmail = to;
            this.CCEmail = cc;
            this.Subject = subject;
            this.Body = body;
        }
        public SendMailVM(IEnumerable<string> to, string subject, string body)
        {
            this.To = to;
            this.Subject = subject;
            this.Body = body;
        }
        public SendMailVM(IEnumerable<string> to,string cc, string subject, string body)
        {
            this.To = to;
            this.CCEmail = cc;
            this.Subject = subject;
            this.Body = body;
        }
        public SendMailVM(IEnumerable<string> to, IEnumerable<string> cc, string subject, string body)
        {
            this.To = to;
            this.CC = cc;
            this.Subject = subject;
            this.Body = body;
        }
        //public SendMailVM(IEnumerable<string> to, string subject, string body, List<FileAttachment> files)
        //{
        //    this.To = to;
        //    this.Subject = subject;
        //    this.Body = body;
        //    this.Attachments = files;
        //}
        public string ToEmail { get; set; }
        public IEnumerable<string> To { get; set; }
        public string CCEmail { get; set; }
        public IEnumerable<string> CC { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Attachment { get; set; }
        //public List<FileAttachment> Attachments { get; set; }
    }
}
