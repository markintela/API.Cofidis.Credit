using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cofidis.Data.Models.External
{
    public class User
    {
        public string Name { get; set; }
        public string NIF { get; set; }
        public decimal Salary { get; set; }
        public int Age { get; set; }
        public string Nationality { get; set; }
        public IEnumerable<Loan> Loans { get; set; }
    }
}
