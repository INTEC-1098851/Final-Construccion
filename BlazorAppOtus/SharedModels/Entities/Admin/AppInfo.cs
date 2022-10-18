using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.Entities.Admin
{
    public class AppInfo
    {
        public int SystemId { get; set; }
        public int SystemCode { get; set; }
        public string SystemName{ get; set; }
        public string SystemTitle{ get; set; }
        public string Mode { get; set; }
        public bool ApiAuthentication { get; set; }
    }
}
