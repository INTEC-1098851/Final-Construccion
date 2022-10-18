using GenericRepository.Repositories;
using MailService.Context;
using MailService.Models;

namespace MailService.Repository
{
    public class SmtpMailConfigurationRepository : EntityRepository<SmtpConfiguration, MailDbContext>
    {
        public SmtpMailConfigurationRepository(MailDbContext context) : base(context)
        {
        }
    }
}
