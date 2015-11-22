using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventiWebAPI.Models
{
    [Table("Eventi")]
    public class Evento
    {
        [Key]
        public int IdEvento { get; set; }

        [Required(ErrorMessage = "Inserire un nome")]
        [MaxLength(100)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Inserire una descrizione")]
        public string Descrizione { get; set; }

        [Column("DescBreve")]
        [Display(Name = "Descrizione Breve")]
        [MaxLength(500)]
        public string DescrizioneBreve { get; set; }

        [Required]
        [Range(0, 100000)]
        [Display(Name = "Numero Posti")]
        public int NumeroPosti { get; set; }

        [DataType(DataType.Url, ErrorMessage = "Inserire un url valido")]
        [MaxLength(250)]
        [Display(Name = "Url Evento")]
        public string UrlEvento { get; set; }

        [DataType(DataType.ImageUrl, ErrorMessage = "Inserire un url valido")]
        [MaxLength(250)]
        [Display(Name = "Url Immagine")]
        public string UrlImmagine { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Data Inizio")]
        public DateTime Inizio { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Data Fine")]
        public DateTime Fine { get; set; }

        public int IdLocation { get; set; }

        [ForeignKey("IdLocation")]
        public virtual Location Location { get; set; }

    }

    public class EventoViewModel
    {
        public int IdEvento { get; set; }

        public string Nome { get; set; }

        public string Descrizione { get; set; }

        [Display(Name = "Descrizione Breve")]
        public string DescrizioneBreve { get; set; }

        [Display(Name = "Numero Posti")]
        public int NumeroPosti { get; set; }

        [Display(Name = "Url Evento")]
        public string UrlEvento { get; set; }

        [Display(Name = "Url Immagine")]
        public string UrlImmagine { get; set; }

        [Display(Name = "Data Inizio")]
        public DateTime Inizio { get; set; }

        [Display(Name = "Data Fine")]
        public DateTime Fine { get; set; }

        public int IdLocation { get; set; }

        [Display(Name = "Location")]
        public string Location { get; set; }

        public bool isUserRegistred { get; set; }

        public StatoRegistrazione StatoRegistrazioneUtente { get; set; }

    }
}
