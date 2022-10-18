using SharedModels.Entities.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedModels.Exceptions
{
    public class UserExistsException : BonaException
    {
        public UserExistsException(User user)
        : this(user.ToString())
        {
        }

        public UserExistsException(string user)
            : base($"{user} ya existe en la base de datos.")
        {
        }

        public UserExistsException(int id)
          : base($"El usuario con el Id: {id} ya existe en la base de datos.")
        {
        }
    }
}
