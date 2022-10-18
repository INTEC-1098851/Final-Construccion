using GenericRepository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailService.Models
{
    public class MailTemplate : IEntity
    {
        public int? Id { get; set; }
        public int Code { get; set; }
        public int SystemCode { get; set; }
        public string Description { get; set; }
        public string Html { get; set; }
        public bool StatusCode { get; set; }
        public string TemplateCode { get; set; }
        public string CreatedBy { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime CreatedDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string UpdatedBy { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime? UpdatedDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int? StatusId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      
        //public virtual Sistemas Sistemas { get; set; }
    }
}
