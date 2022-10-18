using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.Entities.Admin
{
    public class SP_GetMenu_Result
    {
        public int? Id { get; set; }
        public string nameOption { get; set; }
        public string DisplayOptionName { get; set; }
        public string controller { get; set; }
        public string action { get; set; }
        public bool status { get; set; }
        public bool isParent { get; set; }
        public int parentId { get; set; }
    }
}
