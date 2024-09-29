using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cofidis.Data.Interfaces
{
    public interface ICreditRepository
    {
        Task<decimal> GrantingCredit(decimal baseSalary);
    }
}
