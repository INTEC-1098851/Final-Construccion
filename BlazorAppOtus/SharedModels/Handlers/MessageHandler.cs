using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.Handlers
{
    public class MessageHandler
    {
        public MessageHandler(string statusMessage=null,string message=null,string description=null)
        {
            StatusMessage = statusMessage;
            Message = message;
            Description = description;
        }
        public string StatusMessage { get; set; }
        public string Message { get; set; }
        public string Description { get; set; }
    }
}
