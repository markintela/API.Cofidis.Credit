using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cofidis.ViewModel.Credit
{
    public class GetGratingCreditViewModel
    {
       
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("nif")]
        public string NIF { get; set; }

        [JsonProperty("baseSalary")]
        public decimal BaseSalary { get; set; }

        [JsonProperty("isAbleToCredit")]
        public bool IsAbleToCredit { get; set; }

        [JsonProperty("creditAvailabilityValue")]
        public decimal CreditAvailabilityValue { get; set; }
    }
}
