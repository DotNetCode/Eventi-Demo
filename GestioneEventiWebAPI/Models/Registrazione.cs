using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventiWebAPI.Models
{
    [Table("Registrazioni")]
    public class Registrazione
    {
        [Key]
        public int IdRegistrazione { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DataRichiestaRegistrazione { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? DataConfermaRegistrazione { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime DataUltimoStato { get; set; }

        public StatoRegistrazione StatoRegistrazione { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DataPartecipazione { get; set; }
        public string Nota { get; set; }

        public int IdEvento { get; set; }

        [ForeignKey("IdEvento")]
        public virtual IQueryable<Evento> Eventi { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual IQueryable<ApplicationUser> Utenti { get; set; }

    }



    public enum StatoRegistrazione
    {
        Richiesta = 1,
        StandBy = 2,
        Confermata = 3,
        Negata = 4,
        Annullata = 5
    }
}
