using MailService.Models;
using MailService.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailService.Services
{
    public class MailSharedService : IMailSharedService
    {
        private readonly IMailService _mailService;
        private readonly IMailTemplateService _mailTemplateService;
        public MailSharedService(IMailService mailService, IMailTemplateService mailTemplateService)
        {
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
            _mailTemplateService = mailTemplateService ?? throw new ArgumentNullException(nameof(mailTemplateService));
        }
        #region Mail Service
        public async Task<string> BuilEmail(MailTemplate template, object parameters)
        => await _mailService.BuilEmail(template, parameters);
        public async Task SendEmail(MailRequest mailRequest)
        => await _mailService.SendEmail(mailRequest);

        public async Task SendEmailAsync(MailRequest mailRequest)
        => await _mailService.SendEmailAsync(mailRequest);
        #endregion

        #region Mail Template Service
        public async Task<IEnumerable<MailTemplate>> GetAllTemplatesAsync()
        => await _mailTemplateService.GetAllAsync();

        public async Task<IEnumerable<MailTemplate>> GetAllTemplatesAsync(MailTemplate entity)
        => await _mailTemplateService.GetAllAsync(entity);

        public async Task<MailTemplate> GetOneTemplateAsync(MailTemplate entity)
        => await _mailTemplateService.GetOneAsync(entity);
        public async Task<bool> ExistsTemplateAsync(MailTemplate entity)
        => await _mailTemplateService.ExistsAsync(entity);
        public async Task<MailTemplate> InsertTemplateAsync(MailTemplate entity)
        => await _mailTemplateService.InsertAsync(entity);

        public async Task<MailTemplate> UpdateTemplateAsync(MailTemplate entity)
        => await _mailTemplateService.UpdateAsync(entity);
        public async Task<bool> ChangeStatusTemplateAsync(MailTemplate entity)
        => await _mailTemplateService.ChangeStatusAsync(entity);

        public async Task<bool> DeleteTemplateAsync(MailTemplate entity)
        => await _mailTemplateService.DeleteAsync(entity);
        #endregion





    }
}
