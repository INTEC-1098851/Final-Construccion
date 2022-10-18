using MailService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailService.Services.Interfaces
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
        Task SendEmail(MailRequest mailRequest);
        //Task<string> BuilEmail(MailTemplate template,string[] parameters);
        Task<string> BuilEmail(MailTemplate template,object parameters);
    }
}
