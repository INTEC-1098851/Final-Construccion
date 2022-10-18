using MailService.Models;
using MailService.Repository;
using MailService.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailService.Services
{
    public class MailTemplateService:IMailTemplateService
    {
        private readonly MailTemplateRepository _templateRepository;
        public MailTemplateService(MailTemplateRepository templateRepository)
        {
            _templateRepository = templateRepository ?? throw new ArgumentNullException(nameof(templateRepository));
        }
        private async Task<MailTemplate> EnforceMailTemplateExistenceAsync(MailTemplate entity)
        {
            MailTemplate remoteMailTemplate = null;
            if(!string.IsNullOrEmpty(entity.TemplateCode))
                remoteMailTemplate = await _templateRepository.GetOneAsync(entity.TemplateCode); 
            else if(entity.Id>0)
                remoteMailTemplate = await _templateRepository.GetOneAsync(entity.Id);
            //if (remoteMailTemplate == null)
            //{
            //    if (entity.Id.HasValue)
            //        throw new MailTemplateNotFoundException(entity.Id.Value);
            //    throw new MailTemplateNotFoundException(entity);
            //}
            return remoteMailTemplate;
        }
        public async Task<IEnumerable<MailTemplate>> GetAllAsync() { 
            var templates = await _templateRepository.GetAllAsync();
            return templates.AsEnumerable();
        }
        public async Task<IEnumerable<MailTemplate>> GetAllAsync(MailTemplate entity)
        {
            var templates = await _templateRepository.GetAllAsync();
            return templates.AsEnumerable();
        }
        
        public async Task<MailTemplate> GetOneAsync(MailTemplate entity) => await EnforceMailTemplateExistenceAsync(entity);

        public async Task<bool> ExistsAsync(MailTemplate entity)
        {
            //entity.StatusId = null;
            var template = await _templateRepository.GetOneAsync(entity.Id);
            return template != null;
        }

        public async Task<MailTemplate> InsertAsync(MailTemplate entity)
        {
            //if (await ExistsAsync(entity))
            //    throw new MailTemplateExistsException(entity);
            var insertedMailTemplate = await _templateRepository.AddAsync(entity);
            return insertedMailTemplate;
        }
        public async Task<MailTemplate> UpdateAsync(MailTemplate entity)
        {
            await EnforceMailTemplateExistenceAsync(new MailTemplate { Id = entity.Id });
            var updatedMailTemplate = await _templateRepository.UpdateAsync(entity);
            return updatedMailTemplate;
        }
        public Task<bool> ChangeStatusAsync(MailTemplate entity)
        => throw new NotSupportedException();
        public async Task<bool> DeleteAsync(MailTemplate entity)
        {
            var exists = await ExistsAsync(entity);
            MailTemplate mailTemplate = null;
            if (exists)
                await _templateRepository.DeleteAsync(entity.Id);
            return mailTemplate!=null;
        }
    }
}
