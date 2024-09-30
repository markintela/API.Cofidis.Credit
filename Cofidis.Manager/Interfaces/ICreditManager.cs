using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cofidis.Manager.Interfaces
{
    public interface ICreditManager
    {
        Task<decimal> GrantingCredit(decimal baseSalary);
    }
}
