using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using EmailSender.DTOS;

namespace EmailSender.Repositories
{
    public abstract class EmailProvider : IEmailService
    {
        private readonly ILogger<EmailProvider> _logger;
        protected readonly IConfiguration _configuration;

        public EmailProvider(ILogger<EmailProvider> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public abstract HttpClient CreateProviderClient();
        public abstract Task<HttpResponseMessage> SendProviderEmail(HttpClient httpClient, StringContent requestBodyAsString);
        public async Task<bool> SendEmail(EmailDTO emailDTO)
        {
            try
            {
                string filePath = "";
                string fileToUpload = "";
                if (emailDTO.Attachments != null && emailDTO.Attachments.Length > 0)
                {
                   fileToUpload = await this.ProcessAttachment(filePath, emailDTO.Attachments);
                }
                var httpClient = this.CreateProviderClient();
                var requestBody = new MailMessage
                {
                    Personalizations = new List<Personalization>(){
                        new  Personalization{
                            To = new List<NameEmail>(){
                                new NameEmail{
                                    Email = emailDTO.To
                                }
                            },
                            Subject= emailDTO.Subject
                        }
                    },
                    From = new NameEmail
                    {
                        Email = _configuration.GetSection("AppSettings:AdminEmail").Value,
                        Name = _configuration.GetSection("AppSettings:AdminName").Value
                    },
                    Content = new List<object>(){
                        new {
                            type = "text/plain",
                            value = emailDTO.Body
                        }
                    }
                };
                if (emailDTO.Bcc != null)
                {
                    List<NameEmail> bccs = emailDTO.Bcc.Select(bcc => new NameEmail
                    {
                        Email = bcc
                    }).ToList();
                    requestBody.Personalizations[0].Bcc = bccs;
                }

                if (emailDTO.Cc != null)
                {
                    List<NameEmail> ccs = emailDTO.Cc.Select(cc => new NameEmail
                    {
                        Email = cc
                    }).ToList();
                    requestBody.Personalizations[0].Cc = ccs;
                }
                if (emailDTO.Attachments != null)
                {
                    requestBody.Attachments = new List<object>() {
                        new {
                            content = fileToUpload,
                            type = emailDTO.Attachments.ContentType,
                            filename = emailDTO.Attachments.FileName
                        }
                    };
                }
                var requestBodyAsString = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

                var response = await this.SendProviderEmail(httpClient, requestBodyAsString);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                throw new Exception(response.ReasonPhrase);
            }
            catch (Exception exception)
            {
                _logger.LogCritical($"An error occurred while sending email Message: {exception.Message}");
                throw exception;
            }
        }

        private async Task<string>  ProcessAttachment(string filePath, IFormFile attachment)
        {
            filePath = await this.StoreAttachment(attachment);
            byte[] attachedFiles = File.ReadAllBytes(filePath);
            var fileToUpload = Convert.ToBase64String(attachedFiles);
            return fileToUpload;
        }

        private async Task<string> StoreAttachment(IFormFile formFile)
        {
            var path = Path.GetTempFileName();

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }
            return path;
        }
    }
}