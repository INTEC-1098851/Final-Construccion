using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SharedModels.ViewModels.HubBona.Authentication
{
    public class HubLoginVM
    {
        public HubLoginVM()
        {

        }
        public HubLoginVM(string appCode,string password)
        {
            this.AppCode = appCode;
            this.Password = password;
        }
        [Required(ErrorMessage = "Debe proveer el código de usuario/aplicación")]
        public string AppCode { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage = "Debe proveer la contrasena del usuario")]
        public string Password { get; set; }
    }
}
