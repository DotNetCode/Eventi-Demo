using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventiWebAPI.Models
{
    [Serializable]
    [Table("Locations")]
    public class Location
    {
        public Location()
        {
            Indirizzo = new Indirizzo();
        }

        [Key]
        public int IdLocation { get; set; }

        [MaxLength(250)]
        [Required(ErrorMessage = "Inserire un nome per la location")]
        public string Nome { get; set; }

        public string Descrizione { get; set; }

        [Range(0, 100000)]
        public int Capienza { get; set; }

        public Indirizzo Indirizzo { get; set; }
    }

    public class LocationViewModel
    {

        public int IdLocation { get; set; }

        public string Nome { get; set; }

        public string Descrizione { get; set; }

        public int Capienza { get; set; }

        public string Via { get; set; }
        public string NumeroCivico { get; set; }
        public string Citta { get; set; }
        public string Provincia { get; set; }
        public string Cap { get; set; }
    }

}
