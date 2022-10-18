using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.Configurations
{
    public class FileServer
    {
        public string Name { get; set; }
        public string ServerAddress { get; set; }
        public string Path { get; set; }
        public string RequestPath { get; set; }
        public bool EnableDirectoryBrowsing { get; set; }
        public bool IsLocalPath { get; set; }
    }
}
