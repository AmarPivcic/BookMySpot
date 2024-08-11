using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookMySpotAPI.Modul.Models
{
    public class KreditnaKartica
    {
        [Key]
        public int karticaID { get; set; }
        public string? brojKartice { get; set; }
        public string? datumIsteka { get; set; }
        public string? sigurnosniBroj { get; set; }
        [ForeignKey(nameof(Korisnik))]
        public int korisnikID { get; set; }
        public Korisnik korisnik { get; set; }
    }
}
