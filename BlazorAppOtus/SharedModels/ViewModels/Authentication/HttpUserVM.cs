using SharedModels.Entities.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.ViewModels.Authentication
{
    public class HttpUserVM
    {
        public HttpUserVM()
        {

        }
        public HttpUserVM(string id, string userName, string name)
        {
            this.Id = id;
            this.UserName = userName;
            this.Name = name;
        }
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; } = "";
        public string Role { get; set; }
        public bool PasswordReset { get; set; }
        public int? StatusId { get; set; }
        //public string Status { get => StatusId.HasValue ? ((Status)StatusId).ToString() : null; }
        // Implicit Operator to Map from Entity to entity
        public static implicit operator HttpUserVM(User entity)
            => new()
            {
                Id = entity.Id.ToString(),
                UserName = entity.UserName,
                Name = entity.Name,
                Email = entity.Email,
                Role = entity.Role,
                PasswordReset = entity.PasswordReset,
                StatusId = entity.StatusId
            };

        // Implicit Operator to Map from DTO to Entity
        public static implicit operator HttpUserVM(ClaimsIdentity claimsIdentity)
            => new()
            {
                //Id =  int.Parse(claimsIdentity.FindFirst("Id")?.Value),
                Id = claimsIdentity.FindFirst("Id")?.Value,
                UserName = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value,
                Name = claimsIdentity.FindFirst(ClaimTypes.GivenName)?.Value,
                Role = claimsIdentity.FindFirst(ClaimTypes.Role)?.Value,
                PasswordReset = claimsIdentity.FindFirst("PasswordReset")!=null? bool.Parse(claimsIdentity.FindFirst("PasswordReset")?.Value):false
            };
    }

    public class LoginVM
    {
        [RegularExpression(@"^[^\\/:*;\.\)\(]+$", ErrorMessage = "Estos Caractares ':', '.' ';', '*', '/', '\' no son permitidos")]
        [Required(ErrorMessage = "El campo {0} es Requerido")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        // Implicit Operator to Map from DTO to Entity
        public static implicit operator User(LoginVM dto)
            => new()
            {
                UserName = dto.UserName,
                Password = dto.Password
            };
    }

    public class ChangePasswordVM
    {
        public int Id { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "El campo {0} es Requerido")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "El campo {0} es Requerido")]
        public string PasswordConfirm { get; set; }
        // Implicit Operator to Map from DTO to Entity
        public static implicit operator User(ChangePasswordVM dto)
            => new()
            {
                Id = dto.Id,
                Password = dto.Password,
                PasswordConfirm = dto.PasswordConfirm
            };
    }
}
