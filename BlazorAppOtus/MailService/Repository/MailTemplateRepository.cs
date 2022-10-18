using GenericRepository.Repositories;
using MailService.Context;
using MailService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using static System.Linq.Queryable;
using System.Text;
using System.Threading.Tasks;

namespace MailService.Repository
{
    public class MailTemplateRepository : EntityRepository<MailTemplate, MailDbContext>
    {
        private readonly MailDbContext _context;
        protected DbSet<MailTemplate> _collections;
        public MailTemplateRepository(MailDbContext context) : base(context)
        {
            _context = context;
            _collections = context.Set<MailTemplate>();
        }
        public async Task<MailTemplate> GetOneAsync(string templateCode)
        {
            return await Task.FromResult(_collections
                .Where(customer => customer.TemplateCode.ToLower() == templateCode.ToLower().Trim())
                .SingleOrDefault()
                );
        }
    }
}
