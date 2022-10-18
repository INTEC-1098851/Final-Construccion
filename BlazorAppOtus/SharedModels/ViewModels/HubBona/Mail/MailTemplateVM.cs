using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.ViewModels.HubBona.Mail
{
    public class MailTemplateVM
    {
        public int? Id { get; set; }
        public int Code { get; set; }
        public int SystemCode { get; set; }
        public string Description { get; set; }
        public string Html { get; set; }
        public bool StatusCode { get; set; }
        public string TemplateCode { get; set; }
        public string CreatorUserId { get; set; }
        public DateTime CreationDate { get; set; }
        public int? StatusId { get; set; }
        public string UpdaterUserId { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
