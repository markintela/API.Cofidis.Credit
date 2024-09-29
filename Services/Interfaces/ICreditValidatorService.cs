using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cofidis.Services.Interfaces
{
    public interface ICreditValidatorService
    {

        Task<double> CalculateRiskIndex(double UnemploymentTax, double Inflation, int LoansActives, double LoanToPay, double baseSalary);

        Task<bool> CreditAvailability(double riskIndex, double companyRisk, decimal baseSalary);
    }
}
