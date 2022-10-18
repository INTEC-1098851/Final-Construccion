using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.Configurations
{
    public class DynamicsGPClientConfiguration
    {
        public string EndpointUrl { get; set; }
        public string Domain { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string InTransitWarehouse { get; set; }
        public string InTransitWarehouseAccount { get; set; }
        public int CompanyId { get; set; }
    }
}
