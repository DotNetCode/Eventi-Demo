using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventiWebAPI.Models
{
    [Serializable]
    public class Indirizzo
    {
        [MaxLength(150)]
        public string Via { get; set; }

        [MaxLength(20)]
        [Display(Name = "Numero Civico")]
        public string NumeroCivico { get; set; }

        [MaxLength(50)]
        public string Citta { get; set; }

        [MaxLength(5)]
        public string Provincia { get; set; }

        [MaxLength(10)]
        public string Cap { get; set; }
    }
}
