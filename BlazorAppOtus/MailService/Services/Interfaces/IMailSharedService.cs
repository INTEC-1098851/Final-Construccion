using MailService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailService.Services.Interfaces
{
    public interface IMailSharedService
    {
        Task SendEmailAsync(MailRequest mailRequest);
        Task SendEmail(MailRequest mailRequest);
        Task<string> BuilEmail(MailTemplate template, object parameters);
        Task<MailTemplate> GetOneTemplateAsync(MailTemplate entity);
        Task<bool> ExistsTemplateAsync(MailTemplate entity);
        Task<IEnumerable<MailTemplate>> GetAllTemplatesAsync();
        Task<IEnumerable<MailTemplate>> GetAllTemplatesAsync(MailTemplate entity);
        Task<MailTemplate> InsertTemplateAsync(MailTemplate entity);
        Task<MailTemplate> UpdateTemplateAsync(MailTemplate entity);
        Task<bool> DeleteTemplateAsync(MailTemplate entity);
        Task<bool> ChangeStatusTemplateAsync(MailTemplate entity);
    }
}
