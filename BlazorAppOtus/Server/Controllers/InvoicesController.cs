using BlazorAppOtus.Shared.Models;
using HttpResponseHandler;
using LoggerService;
using MailService.Models;
using MailService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OfficeOpenXml.Style;
using SharedModels.Configurations;
using SharedModels.Handlers;
using SharedResources.Handlers;
using SharedResources.Helpers.Methods;
using System.ComponentModel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlazorAppOtus.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private readonly FileServer _invoiceFileServer;
        private readonly IMailService _mailService;
        private readonly IWebHostEnvironment _env;
        private readonly ILoggerManager<InvoicesController> _logger;
        public InvoicesController(IOptions<List<FileServer>> configurationOptionsSnapshot
            ,IMailService mailService
            ,IWebHostEnvironment environment
            , ILoggerManager<InvoicesController> loggerManager)
        {
            _invoiceFileServer = configurationOptionsSnapshot.Value.FirstOrDefault(x => x.Name == "Invoices");
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService)); 
            _env = environment ?? throw new ArgumentNullException(nameof(environment)); 
            _logger = loggerManager ?? throw new ArgumentNullException(nameof(loggerManager));
            _logger.LogInformation("Iniciando controlador");
        }
        // GET: api/<InvociesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<InvociesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<InvociesController>
        [HttpPost("send")]
        public async Task<IActionResult> Send([FromBody] PetitionHandler<Invoice> petitionHandler)
        {
            try
            {
                var invoice = petitionHandler.Data;
                invoice.InvoiceNumber = $"FT{IdGenerator.NewId()}";
                string serverAddress = _invoiceFileServer.IsLocalPath ? _env.ContentRootPath : _invoiceFileServer.ServerAddress;
                var folderPath = Path.Combine(_invoiceFileServer.Path);
                var orignalFilePath = Path.Combine(serverAddress, folderPath);

                var orignalFileLocation = FileHandler.GetFilePath(serverAddress, folderPath, "InvoiceTemplate", ".xlsx");
                var duplicatedFileLocation = FileHandler.GetFilePath(serverAddress, folderPath, invoice.InvoiceNumber, ".xlsx");
                FileHandler.DuplicateFile(orignalFileLocation, duplicatedFileLocation);
                Dictionary<(int row, int col), string> info = new();
                List<ExcelCell> cells = new();
                int startRow = 20;
                cells.Add(new(4, 8, DateTime.Now.ToString("dd/M/yyyy"), ExcelHorizontalAlignment.Right));
                cells.Add(new(6, 8, invoice.InvoiceNumber, ExcelHorizontalAlignment.Right));
                cells.Add(new(12, 4, invoice.CustomerName));
                cells.Add(new(13, 4, invoice.CustomerPhone));
                foreach (var detail in invoice.Details)
                {
                    cells.Add(new(startRow, 4, detail.Description));
                    cells.Add(new(startRow, 6, detail.Quantity.ToString(), ExcelHorizontalAlignment.Center));
                    cells.Add(new(startRow, 7, detail.UnitPrice.ToString("C"), ExcelHorizontalAlignment.Center));
                    cells.Add(new(startRow, 8, detail.SubTotal.ToString("C"), ExcelHorizontalAlignment.Right));
                    startRow++;
                }
                cells.Add(new(29, 8, invoice.TotalAmount.ToString("C"), ExcelHorizontalAlignment.Right));
                cells.Add(new(30, 8, invoice.DiscountAmount.ToString("C"), ExcelHorizontalAlignment.Right));
                cells.Add(new(31, 8, invoice.NetAmount.ToString("C"), ExcelHorizontalAlignment.Right));
                var response = MSOfficeHandler.WriteExcel(duplicatedFileLocation, cells);
                if (!response)
                    return HttpResponseHandler<object>.NotFound("Not Found", "La plantilla de Excel no fue localizada, contacte al administrador.");
                string pdfFileLocation= MSOfficeHandler.ConvertToPdf(duplicatedFileLocation, orignalFilePath, invoice.InvoiceNumber);
                invoice.FileUrl = FileHandler.GetFileUrl($"{invoice.InvoiceNumber}.pdf");
         await FileHandler.RemoveFile(duplicatedFileLocation);
                var fileBytes = await FileHandler.GetBytes(pdfFileLocation);
         
                var mailRequest = new MailRequest(invoice.CustomerEmail, $"Factura No. {invoice.InvoiceNumber}", new FileAttachment($"{invoice.InvoiceNumber}.pdf", fileBytes, "application/pdf"));
                await _mailService.SendEmailAsync(mailRequest);

                return HttpResponseHandler<Invoice>.Ok(invoice);
            }
            catch (Exception e)
            {
                await _logger.LogError(e, "Error al enviar factura");
                return HttpResponseHandler<object>.BadRequest(e.GetBaseException().Message,"Error");
            }
          
        }

        // PUT api/<InvociesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<InvociesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
