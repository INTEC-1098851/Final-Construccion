using Microsoft.AspNetCore.Mvc;
using SharedModels.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace HttpResponseHandler
{
    public class HttpResponseHandler<T>: ResponseHandler<T> where T : class
    {
        #region Bad Request
        public static BadRequestObjectResult BadRequest(T obj, string statusError, string message, string stackTrace = null, string description = null)
        {
            ResponseHandler<T> response = new(obj, null, null,
                new ErrorHandler(statusError, message, stackTrace, description));
            response.IsSuccessStatusCode = false;
            response.StatusCode = HttpStatusCode.BadRequest;
            return new BadRequestObjectResult(response);
        }
        public static BadRequestObjectResult BadRequest(IEnumerable<T> data, string statusError, string message, string stackTrace = null, string description = null)
        {
            ResponseHandler<T> response = new(null, data, null,
                new ErrorHandler(statusError, message, stackTrace, description));
            response.IsSuccessStatusCode = false;
            response.StatusCode = HttpStatusCode.BadRequest;
            return new BadRequestObjectResult(response);
        }
        public static BadRequestObjectResult BadRequest(string statusError, string message, string stackTrace = null, string description = null)
        {
            ResponseHandler<T> response = new(null, null, null,
                new ErrorHandler(statusError, message, stackTrace, description));
            response.IsSuccessStatusCode = false;
            response.StatusCode = HttpStatusCode.BadRequest;
            return new BadRequestObjectResult(response);
        }
        public static BadRequestObjectResult BadRequest(List<ErrorHandler> errors)
        {
            ResponseHandler<T> response = new(errors);
            response.IsSuccessStatusCode = false;
            response.StatusCode = HttpStatusCode.BadRequest;
            return new BadRequestObjectResult(response);
        }
        #endregion

        #region Unauthorized
        public static UnauthorizedObjectResult Unauthorized(string statusError, string message, string stackTrace = null, string description = null)
        {
            ResponseHandler<T> response = new(null, null, null,
                new ErrorHandler(statusError, message, stackTrace, description));
            response.StatusCode = HttpStatusCode.Unauthorized;
            response.IsSuccessStatusCode = false;
            return new UnauthorizedObjectResult(response);
        }

        public static UnauthorizedObjectResult Unauthorized()
        {
            ResponseHandler<T> response = new(null, null, null,
                new ErrorHandler());
            response.StatusCode = HttpStatusCode.Unauthorized;
            response.IsSuccessStatusCode = false;
            return new UnauthorizedObjectResult(response);
        }
        #endregion

        #region Ok
        public static OkObjectResult Ok()
        {
            ResponseHandler<T> response = new(null, null,
                new MessageHandler("OK", null, null));
            response.StatusCode = HttpStatusCode.OK;
            response.IsSuccessStatusCode = true;
            return new OkObjectResult(response);
        }
        public static OkObjectResult Ok(T obj, string statusMessage = "OK", string message = null, string description = null)
        {
            ResponseHandler<T> response = new(obj, null,
                new MessageHandler(statusMessage, message, description));
            response.StatusCode = HttpStatusCode.OK;
            response.IsSuccessStatusCode = true;
            return new OkObjectResult(response);
        }
        public static OkObjectResult Ok(IEnumerable<T> data = null, string statusMessage = "OK", string message = null, string description = null)
        {
            ResponseHandler<T> response = new(null, data,
                new MessageHandler(statusMessage, message, description));
            response.StatusCode = HttpStatusCode.OK;
            response.IsSuccessStatusCode = true;
            return new OkObjectResult(response);
        }
        public static OkObjectResult Ok(string statusMessage = "OK", string message = null, string description = null)
        {
            ResponseHandler<T> response = new(null, null,
                new MessageHandler(statusMessage, message, description));
            response.StatusCode = HttpStatusCode.OK;
            response.IsSuccessStatusCode = true;
            return new OkObjectResult(response);
        }
        #endregion

        #region Not Found
        public static NotFoundObjectResult NotFound(T obj = null, string statusError = "No Encontrado", string message = null, string stackTrace = null, string description = null)
        {
            ResponseHandler<T> response = new(obj, null, null,
                new ErrorHandler(statusError, message, stackTrace, description));
            response.StatusCode = HttpStatusCode.NotFound;
            response.IsSuccessStatusCode = false;
            return new NotFoundObjectResult(response);
        }
        public static NotFoundObjectResult NotFound(IEnumerable<T> data = null, string statusError = "No Encontrado", string message = null, string stackTrace = null, string description = null)
        {
            ResponseHandler<T> response = new(null, data, null,
                new ErrorHandler(statusError, message, stackTrace, description));
            response.StatusCode = HttpStatusCode.NotFound;
            response.IsSuccessStatusCode = false;
            return new NotFoundObjectResult(response);
        }
        public static NotFoundObjectResult NotFound(string statusError = "No Encontrado", string message = null, string stackTrace = null, string description = null)
        {
            ResponseHandler<T> response = new(null, null, null,
                new ErrorHandler(statusError, message, stackTrace, description));
            response.StatusCode = HttpStatusCode.NotFound;
            response.IsSuccessStatusCode = false;
            return new NotFoundObjectResult(response);
        }
        #endregion

        #region Unprocessable Entity
        public static UnprocessableEntityObjectResult UnprocessableEntity(T obj = null, string statusError = "No Encontrado", string message = null, string stackTrace = null, string description = null)
        {
            ResponseHandler<T> response = new(obj, null, null,
                new ErrorHandler(statusError, message, stackTrace, description));
            response.StatusCode = HttpStatusCode.UnprocessableEntity;
            response.IsSuccessStatusCode = false;
            return new UnprocessableEntityObjectResult(response);
        }
        public static UnprocessableEntityObjectResult UnprocessableEntity(IEnumerable<T> data = null, string statusError = "No Encontrado", string message = null, string stackTrace = null, string description = null)
        {
            ResponseHandler<T> response = new(null, data, null,
                new ErrorHandler(statusError, message, stackTrace, description));
            response.StatusCode = HttpStatusCode.UnprocessableEntity;
            response.IsSuccessStatusCode = false;
            return new UnprocessableEntityObjectResult(response);
        }
        public static UnprocessableEntityObjectResult UnprocessableEntity(string statusError = "No Encontrado", string message = null, string stackTrace = null, string description = null)
        {
            ResponseHandler<T> response = new(null, null, null,
                new ErrorHandler(statusError, message, stackTrace, description));
            response.StatusCode = HttpStatusCode.UnprocessableEntity;
            response.IsSuccessStatusCode = false;
            return new UnprocessableEntityObjectResult(response);
        }
        #endregion

        #region No Content
        public static NoContentResult NoContent()
        {
            return new NoContentResult();
        }

        #endregion



        #region Internal Server Error

        public static InternalServerErrorObjectResult InternalServerError(string statusError = "Internal Server Error", string message = null, string stackTrace = null, string description = null)
        {
            ResponseHandler<T> response = new(null, null, null,
                new ErrorHandler(statusError, message, stackTrace, description));
            return new InternalServerErrorObjectResult(response);
        }

        #endregion
    }
    public class InternalServerErrorObjectResult : ObjectResult
    {
        public InternalServerErrorObjectResult(object obj) : base(obj)
        {
            this.StatusCode = (int)HttpStatusCode.InternalServerError;
        }
    }
}
