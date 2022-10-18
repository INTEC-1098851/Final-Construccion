using BlazorAppOtus.Shared.Models;
using HttpRequestHandler;
using SharedModels.Handlers;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
namespace BlazorAppOtus.Client.Services
{
    public interface IInvoiceService
    {
        #region Invoice Service
        Task<ResponseHandler<Invoice>> SendInvoice(Invoice invoice);
        #endregion


    }
    public class InvoiceService : IInvoiceService
    {
        private readonly HttpClient _http;
        private readonly string invoiceBaseAddress = "invoices";
        public InvoiceService(HttpClient httpClient,/* IAccessTokenService tokenService, */IConfiguration _configuration)
        {
            _http = httpClient;
         
        }

        #region Invoice Service
     
        public async Task<ResponseHandler<Invoice>> SendInvoice(Invoice invoice)
        => await HttpRequestHandler<Invoice, Invoice>.PostAsJsonAsync(_http, $"{invoiceBaseAddress}/send", invoice);

        #endregion


        //#region Invoice Service
        //public async Task<IEnumerable<InvoiceVM>> GetAllPropertiesAsync()
        //{
        //    var result = await _http.GetFromJsonAsync<IEnumerable<InvoiceVM>>($"{propertyBaseAddress}");
        //    return result;
        //}

        //public async Task<ResponseHandler<InvoiceVM>> GetOneInvoiceAsync(int id)
        //=> await _http.GetFromJsonAsync<InvoiceVM>($"{propertyBaseAddress}/{id}");


        //public async Task<InvoiceVM> InsertInvoiceAsync(InsertInvoiceVM model)
        //{ 
        //    var httpResponseMessage = await _http.PostAsJsonAsync(propertyBaseAddress, model);
        //    return await httpResponseMessage.Content.ReadFromJsonAsync<InvoiceVM>();
        //}
        //public async Task<InvoiceVM> UpdateInvoiceAsync(UpdateInvoiceVM model)
        //{
        //    var httpResponseMessage = await _http.PutAsJsonAsync(propertyBaseAddress, model);
        //    return await httpResponseMessage.Content.ReadFromJsonAsync<InvoiceVM>();
        //}
        //public async Task<InvoiceVM> ChangeInvoiceStatusAsync(ChangeStatusVM model)
        //{
        //    var httpResponseMessage = await _http.PutAsJsonAsync($"{propertyBaseAddress}/status", model);
        //    return await httpResponseMessage.Content.ReadFromJsonAsync<InvoiceVM>();
        //}
        //public async Task<InvoiceVM> DeleteInvoiceAsync(int id, string userId)
        //{
        //    var httpResponseMessage = await _http.DeleteAsync($"{propertyBaseAddress}/{id}");
        //    return await httpResponseMessage.Content.ReadFromJsonAsync<InvoiceVM>();
        //}
        //#endregion


    }
}
