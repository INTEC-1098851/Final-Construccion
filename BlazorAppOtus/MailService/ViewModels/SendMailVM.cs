using MailService.Models;
using Microsoft.AspNetCore.Http;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailService.ViewModels
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
        public SendMailVM(IEnumerable<string> to, string subject, string body)
        {
            this.To = to;
            this.Subject = subject;
            this.Body = body;
        }
        public SendMailVM(IEnumerable<string> to, string subject, string body,List<FileAttachment> files)
        {
            this.To = to;
            this.Subject = subject;
            this.Body = body;
            this.Attachments = files;
        }
        public string ToEmail { get; set; }
        public IEnumerable<string> To { get; set; }
        public string CCEmail { get; set; }
        public IEnumerable<string> CC { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Attachment { get; set; }
        public List<FileAttachment> Attachments { get; set; }

        // Implicit Operator to Map from Entity to entity
        public static implicit operator MailRequest(SendMailVM vm)
        =>new()
            {
                Subject = vm.Subject,
                Body = vm.Body,
                Attachment = vm.Attachment,
                Attachments = vm.Attachments,
                To = vm.To?.Select(x => MailboxAddress.Parse(x)).ToList(),
                CC = vm.CC?.Select(x => MailboxAddress.Parse(x)).ToList(),
            };
    }
}
