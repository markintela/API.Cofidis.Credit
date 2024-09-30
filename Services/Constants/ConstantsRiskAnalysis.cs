using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cofidis.Services.Constants
{
    public class ConstantsRiskAnalysis
    {
       
        public const decimal MinimumSalary = 700;
        public const double UnemploymentRateWeight = 0.3;
        public const double InflationRateWeight = 0.2;
        public const double CreditHistoryWeight = 0.3;
        public const double DebtWeight = 0.2;
        public const double BaseSalaryWeight = 0.1;
        public const double HighRiskThresholdCompany = 0.1;

        public const double UnemploymentTax = 5.0;
        public const double InflationTaxes = 3.0;
    }
}
