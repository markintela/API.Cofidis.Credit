using _External_API.DigitalKey.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _External_API.DigitalKey.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DigitalKeyController : ControllerBase
    {
        private readonly IDigitalKeyService _digitalKeyService;

        public DigitalKeyController(IDigitalKeyService digitalKeyService)
        {
            _digitalKeyService = digitalKeyService;
        }

        [HttpGet]
        [Route("{nif}")]
        public Task<Models.User> GetUserByNIF(string nif)
        {
            var user = _digitalKeyService.GetUserInformation(nif);

            if (user == null)
            {
                throw new Exception();
            }

            return user; 
        }
    }
}
