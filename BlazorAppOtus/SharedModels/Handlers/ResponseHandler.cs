using SharedModels.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace SharedModels.Handlers
{
    public class ResponseHandler<T> where T : class
    {
        public ResponseHandler()
        {
            Data = new List<T>();
            Messages = new List<MessageHandler>();
            Errors = new List<ErrorHandler>();
            Count = DataCount;
        }
        public ResponseHandler(T obj = null, IEnumerable<T> data = null, MessageHandler messageHandler = null,
            ErrorHandler errorHandler = null)
        {
            //AppCode = appCode;

            if (obj != null)
                Data = new List<T> { obj };
            else
                Data = data?.ToList();

            if (Data == null)
                Data = new List<T>();

            if (messageHandler != null)
                Messages = new List<MessageHandler> { messageHandler };
            else
                Messages = new List<MessageHandler>();
            if (errorHandler != null)
                Errors = new List<ErrorHandler> { errorHandler };
            else
                Errors = new List<ErrorHandler>();

            Count = DataCount;
        }
        public ResponseHandler(List<ErrorHandler> errorHandler = null)
        {
            Errors = errorHandler;
        }
        public List<T> Data { get; set; }
        public List<ErrorHandler> Errors { get; set; }
        public List<MessageHandler> Messages { get; set; }
        public string EmissionDate { get; set; } = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccessStatusCode { get; set; }
        public int? DataCount { get => Data?.Count; }
        public int? Count { get; set; }

  
    }

}
