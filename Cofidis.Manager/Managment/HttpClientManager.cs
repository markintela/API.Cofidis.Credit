using AutoMapper;
using Cofidis.Data.Models.External;
using Cofidis.Manager.Interfaces;
using Cofidis.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cofidis.Data.Models;

namespace Cofidis.Manager.Managment
{
    public class HttpClientManager : IHttpClientManager
    {
        private readonly ILogger<HttpClientManager> _logger;

        private readonly IHttpClientService _httpClientService;

        public readonly IMapper _mapper;

        public HttpClientManager(ILogger<HttpClientManager> logger, IHttpClientService httpClientService, IMapper mapper)
        { 
            _logger = logger;   
            _httpClientService = httpClientService;
            _mapper = mapper;
        }
        public async Task<User> GetExternalUserByNIF(string nif)
        {
            var user = await _httpClientService.GetExternalUserByNIF(nif);
            var result = _mapper.Map<User>(user);
            return result;

        }
    }
}
