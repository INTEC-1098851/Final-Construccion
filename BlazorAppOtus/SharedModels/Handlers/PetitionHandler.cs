using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.Handlers
{
    public class PetitionHandler<T>
    {
        public PetitionHandler()
        {

        }
        public PetitionHandler(T data)
        {
            this.Data = data;
        }
        public string AppCode { get; set; }
        public string Password { get; set; }
        [Required(ErrorMessage = "Debe proveer los datos de la petición")]
        public T Data { get; set; }
        public DateTime ReceivedDate { get; set; } = DateTime.Now;

    }
}
