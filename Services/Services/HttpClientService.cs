using Cofidis.Services.Interfaces;
using Cofidis.Data.Models.External;
using Microsoft.Net.Http.Headers;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
namespace Cofidis.Services.Services
{
    public class HttpClientService : IHttpClientService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public User _user { get; set; }

        public HttpClientService(IHttpClientFactory httpClientFactory, IConfiguration configuration) 
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }
           
        public async Task<User?> GetExternalUserByNIF(string nif)
        {
            var apiUrl = _configuration.GetValue<string>("DigitalKeyAPI");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, apiUrl+nif)
            {
                Headers =
            {
                { HeaderNames.Accept, "application/json" }
                }
            };

            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

                _user = await JsonSerializer.DeserializeAsync<User>(contentStream, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true 
                });
            }
            else
            {
                return null;
            }

            return _user;
        }
    }
}
