using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cofidis.Data.Models
{
    public class GrantingCredit
    {

        public int Id { get; set; }

        public string CustumerNif { get; set; }

        public decimal TotalAmount { get; set; }

        public string Status { get; set; }
    

    }
}
