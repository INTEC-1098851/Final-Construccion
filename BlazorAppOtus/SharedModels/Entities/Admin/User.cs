using GenericRepository.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SharedModels.Entities.Admin
{ 
    public class User: IEntity
    {
        public int? Id { get; set; }
        [Display(Name = "Usuario")]
        [RegularExpression(@"^[^\\/:*;\.\)\(]+$", ErrorMessage = "Estos Caractares ':', '.' ';', '*', '/', '\' no son permitidos")]
        [Required(ErrorMessage = "El campo {0} es Requerido")]
        public string UserName { get; set; }

        [Display(Name = "Nombre(s) y Apellido(s)")]
        [Required(ErrorMessage = "El campo {0} es Requerido")]
        public string Name { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "El campo {0} es Requerido")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Rol")]
        [Required(ErrorMessage = "El campo {0} es Requerido")]
        public string Role { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [NotMapped]
        public string PasswordConfirm { get; set; }
        public bool PasswordReset { get; set; }
        [Display(Name = "Estatus")]

        public int? StatusId { get; set; }
        public string Status { get => StatusId.HasValue ? ((Status)StatusId).ToString() : null; }
        public string CreatedBy{ get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime CreatedDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string UpdatedBy{ get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime? UpdatedDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override bool Equals(object obj)
        {
            if (obj is User other)
                return (Id.Equals(other.Id)) || (UserName.Equals(other.UserName) && Password.Equals(other.Password));
            else
                return false;
        }
        public override int GetHashCode() => base.GetHashCode();

        // Implicit Operator to Map from DTO to Entity
        public static implicit operator User(ClaimsIdentity claimsIdentity)
            => new()
            {
                //Id = int.Parse(claimsIdentity.FindFirst("Id")?.Value),
                Id = int.Parse(claimsIdentity.FindFirst("Id")?.Value),
                UserName = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value,
                Name = claimsIdentity.FindFirst(ClaimTypes.GivenName)?.Value,
                Role = claimsIdentity.FindFirst(ClaimTypes.Role)?.Value,
                PasswordReset = bool.Parse(claimsIdentity.FindFirst("PasswordReset")?.Value)
            };
    }
}
