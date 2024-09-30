using Cofidis.Data.Interfaces;
using Cofidis.Manager.Interfaces;
using Microsoft.Extensions.Logging;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cofidis.Manager.Managment
{
    public class CreditManager : ICreditManager
    {
        private readonly ILogger<CreditManager> _logger;

        private readonly ICreditService _creditService;
        public CreditManager(ILogger<CreditManager> logger, ICreditService creditService) 
        {
            _logger = logger;
            _creditService = creditService;
        }
        public async Task<decimal> GrantingCredit(decimal baseSalary)
        {
            var result = await _creditService.GrantingCredit(baseSalary);
            return result;
        }
    }
}
