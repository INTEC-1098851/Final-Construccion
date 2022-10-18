using Newtonsoft.Json;
using SharedModels.Extensions;
using SharedModels.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace HttpRequestHandler
{
    public class HttpRequestHandler<RequestModel, ResponseModel> where RequestModel : class where ResponseModel : class
    {
        #region PostAsJsonAsync
        public async static Task<ResponseHandler<ResponseModel>> PostAsJsonAsync(HttpClient httpClient, string requestUri, RequestModel model)
        {
            //var logger = NLogBuilder.ConfigureNLog("Config/nlog.config").GetCurrentClassLogger();
            string reason=null,body=null;
            try
            {
                ResponseHandler<ResponseModel> response = new();
                PetitionHandler<RequestModel> petition = new(model);
                var result = await httpClient.PostAsJsonAsync(requestUri, petition);
                reason = result.ReasonPhrase;
                body = await result.Content.ReadAsStringAsync();
                if (body.TryParseJSON())
                    response = JsonConvert.DeserializeObject<ResponseHandler<ResponseModel>>(body);
                else if (!result.IsSuccessStatusCode)
                    response.Errors.Add(new ErrorHandler(result.ReasonPhrase));
                response.StatusCode = result.StatusCode;
                response.IsSuccessStatusCode = result.IsSuccessStatusCode;
                return response;
            }
            catch (Exception e)
            {
                //logger.Error(e,$"Reason: {reason} -> Body: {body}");
                throw;
            }
          
        }

        #endregion

        #region PutAsJsonAsync
        public async static Task<ResponseHandler<ResponseModel>> PutAsJsonAsync(HttpClient httpClient, string requestUri, RequestModel model)
        {
            //var logger = NLogBuilder.ConfigureNLog("Config/nlog.config").GetCurrentClassLogger();
            string body=null,reason = null;
            try
            {
                ResponseHandler<ResponseModel> response = new();
                PetitionHandler<RequestModel> petition = new(model);
                var result = await httpClient.PutAsJsonAsync(requestUri, petition);
                reason = result.ReasonPhrase;
                body = await result.Content.ReadAsStringAsync();
                if (body.TryParseJSON())
                    response = JsonConvert.DeserializeObject<ResponseHandler<ResponseModel>>(body);
                else if (!result.IsSuccessStatusCode)
                    response.Errors.Add(new ErrorHandler(result.ReasonPhrase));
                response.StatusCode = result.StatusCode;
                response.IsSuccessStatusCode = result.IsSuccessStatusCode;
                return response;
            }   
            catch (Exception e)
            {
                //logger.Error(e,$"Reason: {reason} -> Body: {body}");
                throw;
            }
    
        }
        public async static Task<ResponseHandler<ResponseModel>> PutAsJsonAsync(HttpClient httpClient, string requestUri, string userId)
        {
            //var logger = NLogBuilder.ConfigureNLog("Config/nlog.config").GetCurrentClassLogger();
            string body=null,reason = null;
            try
            {
                ResponseHandler<ResponseModel> response = new();
                var result = await httpClient.SendAsync(BuildRequest(httpClient, HttpMethod.Put, requestUri, userId));
                reason = result.ReasonPhrase;
                body = await result.Content.ReadAsStringAsync();
                if (body.TryParseJSON())
                    response = JsonConvert.DeserializeObject<ResponseHandler<ResponseModel>>(body);
                else if (!result.IsSuccessStatusCode)
                    response.Errors.Add(new ErrorHandler(result.ReasonPhrase));
                response.StatusCode = result.StatusCode;
                response.IsSuccessStatusCode = result.IsSuccessStatusCode;
                return response;
            }
            catch (Exception e)
            {
                //logger.Error(e,$"Reason: {reason} -> Body: {body}");
                throw;
            }
        }

        #endregion

        #region GetFromJsonAsync
        public async static Task<ResponseHandler<ResponseModel>> GetFromJsonAsync(HttpClient httpClient, string requestUri)
        {
            //var logger = NLogBuilder.ConfigureNLog("Config/nlog.config").GetCurrentClassLogger();
            string body=null,reason = null;
            try
            {
                ResponseHandler<ResponseModel> response = new();
                var result = await httpClient.GetAsync(requestUri);
                reason = result.ReasonPhrase;
                body = await result.Content.ReadAsStringAsync();
                if (body.TryParseJSON())
                    response = JsonConvert.DeserializeObject<ResponseHandler<ResponseModel>>(body);
                else if (!result.IsSuccessStatusCode)
                {
                    //_logger.LogError(body);
                    response.Errors.Add(new ErrorHandler(result.ReasonPhrase));
                }
                response.StatusCode = result.StatusCode;
                response.IsSuccessStatusCode = result.IsSuccessStatusCode;
                return response;
            }
            catch (Exception e)
            {

                //logger.Error(e,$"Reason: {reason} -> Body: {body}");
                throw;
            }

        }
        #endregion

        #region SendJsonAsync
        public async static Task<string> SendJsonAndReturnAsJsonAsync(HttpClient httpClient, HttpMethod method, string requestUri, RequestModel model)
        {
            //var logger = NLogBuilder.ConfigureNLog("Config/nlog.config").GetCurrentClassLogger();
            string body=null,reason = null;
            try
            {
                ResponseHandler<ResponseModel> response = new();
                PetitionHandler<RequestModel> petition = new(model);
                var result = await httpClient.SendAsync(BuildRequest(httpClient, method, requestUri, model));
                if (result != null)
                    response = JsonConvert.DeserializeObject<ResponseHandler<ResponseModel>>(result.Content.ReadAsStringAsync().Result);
                return JsonConvert.SerializeObject(new
                {
                    result.IsSuccessStatusCode,
                    result.StatusCode,
                    response.Data,
                    response.Messages,
                    response.Errors
                });
            }
            catch (Exception e)
            {
                //logger.Error(e,$"Reason: {reason} -> Body: {body}");
                throw;
            }
           
        }
        public async static Task<ResponseHandler<ResponseModel>> SendAsync(HttpClient httpClient, HttpMethod method, string requestUri, RequestModel model)
        {
            //var logger = NLogBuilder.ConfigureNLog("Config/nlog.config").GetCurrentClassLogger();
            string body=null,reason = null;
            try
            {
                ResponseHandler<ResponseModel> response = new();
                var result = await httpClient.SendAsync(BuildRequest(httpClient, method, requestUri, model));
                reason = result.ReasonPhrase;
                body = await result.Content.ReadAsStringAsync();
                if (body.TryParseJSON())
                    response = JsonConvert.DeserializeObject<ResponseHandler<ResponseModel>>(body);
                else if (!result.IsSuccessStatusCode)
                    response.Errors.Add(new ErrorHandler(result.ReasonPhrase));
                response.StatusCode = result.StatusCode;
                response.IsSuccessStatusCode = result.IsSuccessStatusCode;
                return response;
            }
            catch (Exception e)
            {
                //logger.Error(e,$"Reason: {reason} -> Body: {body}");
                throw;
            }
           
        }
        public async static Task<ResponseHandler<ResponseModel>> SendAsync(HttpClient httpClient, HttpMethod method, string requestUri, object model)
        {
            //var logger = NLogBuilder.ConfigureNLog("Config/nlog.config").GetCurrentClassLogger();
            string body=null,reason = null;
            try
            {
               
                ResponseHandler<ResponseModel> response = new();
                var result = await httpClient.SendAsync(BuildRequest(httpClient, method, requestUri, model));
                reason = result.ReasonPhrase;
                body = await result.Content.ReadAsStringAsync();
                if (body.TryParseJSON())
                    response = JsonConvert.DeserializeObject<ResponseHandler<ResponseModel>>(body);
                else if (!result.IsSuccessStatusCode)
                    response.Errors.Add(new ErrorHandler(result.ReasonPhrase));
                response.StatusCode = result.StatusCode;
                response.IsSuccessStatusCode = result.IsSuccessStatusCode;
                return response;
            }
            catch (Exception e)
            {
                //logger.Error(e,$"Reason: {reason} -> Body: {body}");
                throw;
            }
           
        }
    
        #endregion

        #region Delete Async
        public async static Task<ResponseHandler<ResponseModel>> DeleteAsync(HttpClient httpClient, string requestUri,string userId)
        {
            //var logger = NLogBuilder.ConfigureNLog("Config/nlog.config").GetCurrentClassLogger();
            string body=null,reason = null;
            try
            {
                ResponseHandler<ResponseModel> response = new();
                var result = await httpClient.SendAsync(BuildRequest(httpClient, HttpMethod.Delete, requestUri, userId));
                reason = result.ReasonPhrase;
                body = await result.Content.ReadAsStringAsync();
                if (body.TryParseJSON())
                    response = JsonConvert.DeserializeObject<ResponseHandler<ResponseModel>>(body);
                else if (!result.IsSuccessStatusCode)
                    response.Errors.Add(new ErrorHandler(result.ReasonPhrase));
                response.StatusCode = result.StatusCode;
                response.IsSuccessStatusCode = result.IsSuccessStatusCode;
                return response;
            }
            catch (Exception e)
            {
                //logger.Error(e,$"Reason: {reason} -> Body: {body}");
                throw;
            }

        }
   
        #endregion

        public static HttpRequestMessage BuildRequest(HttpClient httpClient, HttpMethod method, string requestUri, RequestModel model)
        {
            PetitionHandler<RequestModel> petition = new(model);
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(petition),
                   Encoding.UTF8, "application/json");

            return new HttpRequestMessage
            {
                Method = method,
                RequestUri = new Uri(httpClient.BaseAddress, requestUri),
                Content = httpContent
            };

        }
        public static HttpRequestMessage BuildRequest(HttpClient httpClient, HttpMethod method, string requestUri, object model)
        {
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(model),
                   Encoding.UTF8, "application/json");

            return new HttpRequestMessage
            {
                Method = method,
                RequestUri = new Uri(httpClient.BaseAddress, requestUri),
                Content = httpContent
            };

        }
    
    }
}
