using BookMySpotAPI.Modul.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BookMySpotAPI.Autentifikacija.Models
{
    [Table("AutentifikacijaToken")]
    public class AutentifikacijaToken
    {
        [Key]
        public int id { get; set; }
        public string vrijednost {  get; set; }
        [ForeignKey(nameof(KorisnickiNalog))]
        public int OsobaID { get; set; }
        public KorisnickiNalog KorisnickiNalog { get; set; }
        public DateTime vrijemeEvidentiranja { get; set; }
        public string ipAdresa { get; set; }

    }
}
