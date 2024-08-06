using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookMySpotAPI.Modul.Models
{
    public class KreditnaKartica
    {
        [Key]
        public int KarticaID { get; set; }
        public string? BrojKartice { get; set; }
        public string? DatumIsteka { get; set; }
        public string? SigurnosniBroj { get; set; }
        [ForeignKey(nameof(Korisnik))]
        public int KorisnikID { get; set; }
    }
}
