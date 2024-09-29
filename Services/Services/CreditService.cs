using Cofidis.Data.Interfaces;
using Microsoft.Extensions.Logging;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cofidis.Services.Services
{
    public class CreditService : ICreditService
    {
        private readonly ILogger<ICreditService> _logger;

        private readonly ICreditRepository _creditRepository;

        public CreditService(ILogger<CreditService> logger, ICreditRepository creditRepository)
        {
            _logger = logger;
            _creditRepository = creditRepository;
        }
        public async Task<decimal> GrantingCredit(decimal baseSalary)
        {
            _logger.LogInformation("[CreditService - GrantingCredit] -> baseSalary:", baseSalary);

            var result = await _creditRepository.GrantingCredit(baseSalary);

            if (result == null) {
                _logger.LogError("[CreditService - GrantingCredit] -> null object");
                throw new Exception();
            }

            _logger.LogInformation("[CreditService - GrantingCredit] -> Result:", result);

            return result;
        }
    }
}
