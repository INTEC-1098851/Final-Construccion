using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.ViewModels.HubBona.Authentication
{
    public class AccessTokenVM
    {
        public AccessTokenVM()
        {

        }
        public AccessTokenVM(string appUserId, string token, DateTime expiration, string userId)
        {
            this.AppUserId = appUserId;
            this.Token = token;
            this.Expiration = expiration;
            this.CreatorUserId = userId;
            this.UpdaterUserId = userId;
            this.UpdateDate = DateTime.Now;
        }
        public string AppUserId { get; set; }
        public string Token { get; set; }
        public string CreatorUserId { get; set; }
        public DateTime? Expiration { get; set; }
        public string UpdaterUserId { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
