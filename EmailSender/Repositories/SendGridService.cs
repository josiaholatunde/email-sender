using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace EmailSender.Repositories
{
    public class SendGridProvider: EmailProvider 
    {
        private readonly IHttpClientFactory _httpClient;

        public SendGridProvider(IHttpClientFactory httpClient, 
        IConfiguration configuration,
        ILogger<SendGridProvider> logger): base(logger, configuration)
        {
            _httpClient = httpClient;
        }

        public override HttpClient CreateProviderClient()
        {
            var httpClient = _httpClient.CreateClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer "+_configuration.GetSection("ApiKeys:SendgridApiKey").Value);
            return httpClient;
        }

        public override async Task<HttpResponseMessage> SendProviderEmail(HttpClient httpClient, StringContent requestBodyAsString)
        {
            string sendGridUrl = "https://api.sendgrid.com/v3/mail/send";
            return await httpClient.PostAsync(sendGridUrl, requestBodyAsString);
        }
    }
}