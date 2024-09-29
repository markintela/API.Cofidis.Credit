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
        /// <summary>
        /// Número de Identificação Fiscal do empregado (NIF).
        /// </summary>
        [Required(ErrorMessage = "O NIF é obrigatório.")]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "O NIF deve ter 9 caracteres.")]
        [JsonProperty("nif")]
        public string NIF { get; set; }

        /// <summary>
        /// Salário base do empregado.
        /// </summary>
        [Required(ErrorMessage = "O salário base é obrigatório.")]
        [Range(0, double.MaxValue, ErrorMessage = "O salário base deve ser maior ou igual a zero.")]
        [JsonProperty("baseSalary")]
        public decimal BaseSalary { get; set; }
    }
}
