using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookMySpotAPI.Modul.Models
{
    [Table("Korisnik")]
    public class Korisnik :Osoba
    {
        [Key]
        public int KorisnikID { get; set; }
    }
}
