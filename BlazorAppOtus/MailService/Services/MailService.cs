
using GenericRepository.Models;
using MailKit.Security;
using MailService.Models;
using MailService.Repository;
using MailService.Services.Interfaces;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MailService.Services
{
    public class MailService : IMailService
    {
        private readonly SmtpMailConfigurationRepository _smptMailConfigurationRepository;
        private SmtpConfiguration _smtpConfig;
        public MailService(IOptions<SmtpConfiguration> options, SmtpMailConfigurationRepository smtpMailConfigurationRepository)
        {
            _smptMailConfigurationRepository = smtpMailConfigurationRepository ?? throw new ArgumentNullException(nameof(smtpMailConfigurationRepository));
            _smtpConfig = options.Value;
        }
        public MailService(IOptions<SmtpConfiguration> options)
        {
            _smtpConfig = options.Value;
        }

        public async Task SendEmailAsync(MailRequest mailRequest)
        {
          
            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            try
            {
                if (_smptMailConfigurationRepository != null)
                {
                    var result = await _smptMailConfigurationRepository.GetAllAsync();
                    _smtpConfig = result.FirstOrDefault();
                }               
                mailRequest.From = new(_smtpConfig.Mail);
                await smtp.ConnectAsync(_smtpConfig.Host, int.Parse(_smtpConfig.Port), SecureSocketOptions.StartTls);
                //smtp.AuthenticationMechanisms.Remove("XOAUTH2");
                await smtp.AuthenticateAsync(_smtpConfig.Mail, _smtpConfig.Password);
                await smtp.SendAsync(mailRequest.Content);
            }
            catch (Exception e)
            {
                //log an error message or throw an exception or both.
                //throw;
            }
            finally
            {
                await smtp.DisconnectAsync(true);
                smtp.Dispose();
            }
        }

        public async Task SendEmail(MailRequest mailRequest)
        {
            MailMessage mail = new();
            try
            {
                if (_smptMailConfigurationRepository != null)
                {
                    var result = await _smptMailConfigurationRepository.GetAllAsync();
                    _smtpConfig = result.FirstOrDefault();
                }

                using SmtpClient smtp = new(_smtpConfig.Host, int.Parse(_smtpConfig.Port));
                mail.From = new(_smtpConfig.Mail);
                smtp.EnableSsl = _smtpConfig.EnableSsl;
                smtp.Port = int.Parse(_smtpConfig.Port);
                smtp.UseDefaultCredentials = _smtpConfig.UseDefaultCredentials;
                smtp.Credentials = new NetworkCredential(_smtpConfig.Mail, _smtpConfig.Password);
                if (mailRequest.To != null && mailRequest.To.Any())
                    foreach (var address in mailRequest.To)
                        mail.To.Add(address.Address);

                if (mailRequest.CC != null && mailRequest.CC.Any())
                    foreach (var address in mailRequest.CC)
                        mail.CC.Add(address.Address);

                if (!string.IsNullOrEmpty(mailRequest.Attachment))
                {
                    mail.Attachments.Add(new Attachment(mailRequest.Attachment));
                }
                mail.Subject = mailRequest.Subject;
                mail.Body = mailRequest.Body;
                mail.IsBodyHtml = true;

                smtp.Send(mail);
                smtp.Dispose();
                mail.Dispose();
            }
            catch (Exception e)
            {
                mail.Dispose();
                throw;
            }
        }
       
        public Task<string> BuilEmail(MailTemplate template, object parameters)
        {
            
            string date, time, body;/*, NombreSistema;*/

            //  'Carga Parámetros fijos
            date = DateTime.Now.ToShortDateString();
            time = String.Format("{0:t}", DateTime.Now);
            //   NombreSistema = BodyMensajes.ToList()[0].SystemName;

            body = template.Html;

            //'Se reeplazan los parámetros de la plantilla por los valores colocados en la call de la función.
            //'Parámetros fijos
            //body = body.Replace("@FechaISO", date);
            //body = body.Replace("@Hora", time);
            body = body.Replace("{Date}", date);
            body = body.Replace("{Time}", time);
            body = body.Replace("{Year}", DateTime.Now.Year.ToString());
            //Texto = Texto.Replace("@NombreSistema", NombreSistema);
            var i = 0;
            foreach (PropertyInfo prop in parameters.GetType().GetProperties())
            {
                //if(prop.HAsV)
                var propValue = prop.GetValue(parameters, null)?.ToString();
                var propName = $"{{{prop.Name}}}";
                body = body.Replace(propName, propValue);
                //body = body.Replace($"{prop.Name}", prop.GetValue());
            }
            //foreach (var param in parameters)
            //{
            //    var cod = "@" + string.Concat(i, "Parm");

            //    body = body.Replace(Convert.ToString(cod), param);
            //    i++;
            //}
            return Task.FromResult(body);
        }
    }
}
