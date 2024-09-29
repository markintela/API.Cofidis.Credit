using Cofidis.Data.Models.External;
using Cofidis.Services.Interfaces;
using Cofidis.ViewModel.Credit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Services.Interfaces;
using Swashbuckle;
using System.Net.Http;
using System.Text.Json;

namespace API.Cofidis.Credit.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CreditController : ControllerBase
    {
        private readonly ILogger<CreditController> _logger;

        private readonly ICreditService _creditService;

        private readonly IHttpClientService _httpClientService;

        public User _user { get; set; }

        public CreditController(ILogger<CreditController> logger, ICreditService creditService, IHttpClientService httpClientService) 
        {
            _logger = logger;
            _creditService = creditService;
            _httpClientService = httpClientService;
    
        }

        [HttpGet(Name = "GrantingCredit")]
        public async Task<IActionResult> GrantingCredit(string nif, decimal baseSalaery)
        {


            var result = await _creditService.GrantingCredit(baseSalaery);

            var user = await _httpClientService.GetExternalUserByNIF(nif);

            if (result  == null)
            {
                throw new KeyNotFoundException("Error.");
            }
            return Ok(user);
        }

    }
}
