using MailService.Services.Interfaces;
using MailService.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MailService.Repository;
using MailService.Context;
using Microsoft.EntityFrameworkCore;
using MailService.Models;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace MailService.Extensions
{
    public static class MailServiceExtensions
    {
        public static void ConfigureMailService(this IServiceCollection services, IConfiguration config)
        {
            var mailServiceConnectionString = config.GetConnectionString("MailConnectionString");
            if (!string.IsNullOrEmpty(mailServiceConnectionString))
            {
                services.AddDbContext<MailDbContext>(options =>
                {
                    options.UseSqlServer(mailServiceConnectionString);
                    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                  
                });
                services.AddScoped<SmtpMailConfigurationRepository>();
                services.AddScoped<MailTemplateRepository>();
                services.AddTransient<IMailTemplateService, MailTemplateService>();
                services.AddTransient<IMailSharedService, MailSharedService>();
            }
      

            services.Configure<SmtpConfiguration>(config.GetSection("3rdPartyServices:SmtpConfiguration"));
            services.AddScoped<IMailService, Services.MailService>();

        }

        public static FileAttachment BuildMailAttachment(string fileName,string contentType,string body)
        {
            var tempPath = Path.GetTempFileName();
            byte[] fileBytes = System.Array.Empty<byte>();
            using (StreamWriter writer = File.CreateText(tempPath))
            {
                writer.WriteLine(body);
              
                //writer.BaseStream.Write(fileBytes, 0, fileBytes.Length);
            }
            fileBytes = File.ReadAllBytes(tempPath);
            //using FileStream stream = File.OpenRead(tempPath);
            //var file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name))
            //{
            //    Headers = new HeaderDictionary(),
            //    ContentType = contentType
            //};
            //using MemoryStream ms = new();
            //file.CopyTo(ms);
            //var fileBytes = ms.ToArray();
            return new FileAttachment(fileName, fileBytes, contentType);
        }
    }
}
