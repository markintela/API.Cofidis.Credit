using Cofidis.Data.Interfaces;
using Cofidis.Services.Constants;
using Cofidis.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cofidis.Services.Services
{
    public class CreditValidatorService : ICreditValidatorService
    {

        private readonly ILogger<CreditValidatorService> _logger;

        public CreditValidatorService(ILogger<CreditValidatorService> logger)
        {
            _logger = logger; 
        }
        public async Task<double> CalculateRiskIndex(double UnemploymentTax, double Inflation, int LoansActives, double LoanToPay, double baseSalary)
        {

            double riskIndex = (UnemploymentTax * ConstantsRiskAnalysis.UnemploymentRateWeight) +
                                (Inflation * ConstantsRiskAnalysis.InflationRateWeight) +
                                ((100 - LoansActives) / 100 * ConstantsRiskAnalysis.DebtWeight) +
                                (LoanToPay * ConstantsRiskAnalysis.CreditHistoryWeight) -  (baseSalary * ConstantsRiskAnalysis.BaseSalaryWeight);

            return riskIndex;
        }

        public async Task<bool> CreditAvailability(double riskIndex, double companyRisk, decimal baseSalary)
        {
            if (baseSalary < ConstantsRiskAnalysis.MinimumSalary)
            {
                return false; 
            }

            return riskIndex <= companyRisk;
        }
    }
}
