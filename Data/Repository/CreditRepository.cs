using Cofidis.Data.Interfaces;
using Microsoft.Data.SqlClient;
using Data.DBContext;
using Microsoft.EntityFrameworkCore;



namespace Cofidis.Data.Repository
{
    public class CreditRepository : ICreditRepository
    {

        private readonly DataContext _context;

        public CreditRepository(DataContext context)
        {

            _context = context;

        }
        public async Task<decimal> GrantingCredit(decimal baseSalary)
        {
    
            var baseSalaryParam = new SqlParameter("@baseSalary", baseSalary);

      
            var creditLimitParam = new SqlParameter
            {
                ParameterName = "@creditLimit",
                SqlDbType = System.Data.SqlDbType.Decimal,
                Direction = System.Data.ParameterDirection.Output,
                Precision = 18,
                Scale = 2
            };

            _context.Database.ExecuteSqlRaw(
                "EXEC SP_DetermineCreditLimit @baseSalary, @creditLimit OUTPUT",
                baseSalaryParam,
                creditLimitParam);

            if(creditLimitParam.Value == null)
            {

            }

            var result = (decimal)creditLimitParam.Value;
   
            return result;
        }
    }
}
