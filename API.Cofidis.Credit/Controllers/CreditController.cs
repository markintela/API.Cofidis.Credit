using Cofidis.Data.Models.External;
using Cofidis.Manager.Interfaces;
using Cofidis.Services.Constants;
using Cofidis.Services.Interfaces;
using Cofidis.ViewModel.Credit;
using Microsoft.AspNetCore.Mvc;

namespace API.Cofidis.Credit.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class CreditController : ControllerBase
    {
        private readonly ILogger<CreditController> _logger;

        private readonly ICreditManager _creditManager;

        private readonly IHttpClientManager _httpClientManager;

        private readonly ICreditValidatorService _creditValidatorService;

        public User _user { get; set; }

        public CreditController(ILogger<CreditController> logger, ICreditManager creditManager, IHttpClientManager httpClientManager, ICreditValidatorService creditValidatorService) 
        {
            _logger = logger;
            _creditManager = creditManager;
            _httpClientManager = httpClientManager;
            _creditValidatorService = creditValidatorService;
    
        }

        [HttpGet(Name = "GrantingCredit")]
        public async Task<GetGratingCreditViewModel> GrantingCredit(string nif, decimal baseSalary)
        {
            _logger.LogInformation("[CreditController - GrantingCredit] -> params:", nif, baseSalary);

            var user = await _httpClientManager.GetExternalUserByNIF(nif);

            _logger.LogInformation("[CreditController - GrantingCredit - GetExternalUserByNIF] -> User:", user);
          

            if (user == null)
            {
                _logger.LogError("[CreditController - GrantingCredit - - GetExternalUserByNIF] -> null object:");
                throw new Exception("Null object");
            }          
           
            var result = await _creditManager.GrantingCredit(baseSalary);

            _logger.LogInformation("[CreditController - GrantingCredit - GrantingCredit  -> result:", result);


            var getGratingCreditViewModel = new GetGratingCreditViewModel()
            {
                Name = user.Name,
                NIF = user.NIF,
                BaseSalary = baseSalary,
                CreditAvailabilityValue = result

            };

            
            var riskIndex = _creditValidatorService.CalculateRiskIndex(ConstantsRiskAnalysis.UnemploymentTax, ConstantsRiskAnalysis.InflationTaxes, user.Loans.Where(x => x.IsActive).Count(),(double)user.Loans.Where(x => x.IsActive).Sum(x => x.Amount),(double)baseSalary);
           
            _logger.LogInformation("[CreditController - GrantingCredit - CalculateRiskIndex] -> riskIndex:", riskIndex);

            var creditAvaibility = await _creditValidatorService.CreditAvailability(riskIndex.Result, 0.6, baseSalary);
           
            _logger.LogInformation("[CreditController - GrantingCredit - CalculateRiskIndex] -> CreditAvailability:", creditAvaibility);

            getGratingCreditViewModel.IsAbleToCredit = creditAvaibility;


            _logger.LogInformation("[CreditController - GrantingCredit - CalculateRiskIndex] -> getGratingCreditViewModel:", getGratingCreditViewModel);

            return getGratingCreditViewModel;
        }

    }
}
