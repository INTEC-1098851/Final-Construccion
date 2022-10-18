using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.Handlers
{
    public class ErrorHandler
    {
        public ErrorHandler(string statusError = null, string message = null,string stackTrace=null, string description = null)
        {
            StatusError = statusError;
            Message = message;
            StackTrace = stackTrace;
            Description = description;
        }
        public string StatusError { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string Description { get; set; }
        public override string ToString()
            => $"Error: ({nameof(StatusError)}: {StatusError}; {nameof(Message)}: {Message}; {nameof(StackTrace)}: {StackTrace})";


    }
}
