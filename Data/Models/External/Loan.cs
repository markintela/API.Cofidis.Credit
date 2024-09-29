using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cofidis.Data.Models.External
{
    public class Loan
    {
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public bool IsActive { get; set; }
    }
}
