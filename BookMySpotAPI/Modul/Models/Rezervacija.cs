using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookMySpotAPI.Modul.Models
{
    [Table("Rezervacija")]
    public class Rezervacija
    {
        [Key]
        public int RezervacijaID { get; set; }
        public DateTime RezervacijaDatumVrijeme { get; set; }
        public bool Otkazano = false;
        public bool Zavrseno = false;
        public bool KarticnoPlacanje = false;
        [ForeignKey(nameof(Korisnik))]
        public int KorisnikID { get; set; }
        [ForeignKey(nameof(Usluga))]
        public int UslugaID { get; set;}
    }
}
