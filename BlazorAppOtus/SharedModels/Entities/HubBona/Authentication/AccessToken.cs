using GenericRepository.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SharedModels.Entities.HubBona.Authentication
{
    public class AccessToken : IEntity
    {
        public AccessToken()
        {

        }
        public AccessToken(string appUserId,string token,DateTime expiration,string userId)
        {
            this.AppUserId = appUserId;
            this.Token = token;
            this.Expiration = expiration;
            this.CreatedBy= userId;
            this.UpdatedBy = userId;
            this.UpdatedDate = DateTime.Now;
        }
        public int? Id { get; set; }
        public string AppUserId { get; set; }
        public string Token { get; set; }
        public DateTime? Expiration { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate{ get; set; }
        [NotMapped]
        public int? StatusId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        // Implicit Operator to Map from Entity to dto
        //public static implicit operator AccessToken(HubUserInfoVM entity)
        //    => new()
        //    {
        //        //Id = entity.Id,
        //        AppUserId = entity.AppUserId,

        //    };
    }
}
