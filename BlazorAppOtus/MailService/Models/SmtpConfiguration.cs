using GenericRepository.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MailService.Models
{
    public class SmtpConfiguration:IEntity
    {
        public int? Id { get; set; }
        public string Mail { get; set; }
        [NotMapped]
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }
        public bool EnableSsl { get; set; }
        public bool UseDefaultCredentials { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? StatusId { get; set; }
    }
}
