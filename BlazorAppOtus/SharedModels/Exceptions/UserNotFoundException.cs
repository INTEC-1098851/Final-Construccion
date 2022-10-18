using SharedModels.Entities.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.Exceptions
{
    public class UserNotFoundException : BonaException
    {
        public UserNotFoundException(User user)
      : this(user.Id)
        {
        }

        public UserNotFoundException(string userId)
            : base($"No se encontro ningun usuario que contenga el id: {userId}.")
        {
        }

        public UserNotFoundException(int? id)
          : base($"El usuario con el Id: {id} no fue encontrado.")
        {
        }
    }
}
