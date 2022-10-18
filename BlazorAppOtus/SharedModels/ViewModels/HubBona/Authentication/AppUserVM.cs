using SharedModels.Entities.HubBona.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.ViewModels.HubBona.Authentication
{
    public class AppUserVM
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public override string ToString()
           => $"Usuario: ({nameof(Id)}: {Id}; {nameof(UserName)}: {UserName}; {nameof(Email)}: {Email})";
        // Implicit Operator to Map from Entity to dto
        public static implicit operator AppUserVM(ApplicationUser  entity)
            => new()
            {
                Id = entity.Id,
                UserName = entity.NormalizedUserName,
                Email = entity.Email,
                PhoneNumber = entity.PhoneNumber

            };
    }
}
