using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SharedModels.Entities.HubBona.Authentication
{
    public class HubUserInfoVM
    {
        [Required(ErrorMessage = "Debe proveer el código de usuario/aplicación")]
        public string AppCode { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage = "Debe proveer la contrasena del usuario")]
        public string Password { get; set; }
        public HubUserTokenVM UserToken { get; set; }
        public IList<string> Roles { get; set; } = new List<string>();
    }
    public class HubUserTokenVM
    {
        public HubUserTokenVM()
        {

        }
        public HubUserTokenVM(string token)
        {
            this.Token = token;
        }
        public HubUserTokenVM(string token,DateTime expiration)
        {
            this.Token = token;
            this.Expiration = expiration;
        }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public bool Renewed { get; set; } = false;
    }
}
