using Cofidis.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Cofidis.Data.Models.External;
using Microsoft.Net.Http.Headers;
using Microsoft.Net.Http;
using System.Text.Json;
namespace Cofidis.Services.Services
{
    public class HttpClientService : IHttpClientService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public User _user { get; set; }
        public HttpClientService(IHttpClientFactory httpClientFactory) =>
            _httpClientFactory = httpClientFactory;

        public async Task<User> GetExternalUserByNIF(string nif)
        {

            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                "https://localhost:5000/digitalkey/"+nif)
            {
                Headers =
            {
                { HeaderNames.Accept, "application/json" },
                { HeaderNames.UserAgent, "HttpRequestsSample" }
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

            return _user;
        }
    }
}
