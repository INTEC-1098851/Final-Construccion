using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedModels.Entities.HubBona.Authentication
{
    public class ApplicationUser: IdentityUser
    {
        public override string ToString()
       => $"Usuario: ({nameof(Id)}: {Id};{nameof(NormalizedUserName)}: {NormalizedUserName}; {nameof(Email)}: {Email};)";
        public override bool Equals(object obj)
        {
            if (obj is ApplicationUser other)
                return Id.Equals(other.Id) || Email.Equals(other.Email);
            else
                return false;
        }
        public override int GetHashCode() => base.GetHashCode();
    }
}
