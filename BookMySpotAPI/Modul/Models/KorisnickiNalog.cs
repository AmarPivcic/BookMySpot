using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BookMySpotAPI.Modul.Models
{
    [Table("KorisnickiNalog")]
    public class KorisnickiNalog
    {
        [Key]
        public int osobaID { get; set; }
        public string? ime { get; set; }
        public string? prezime { get; set; }
        public string? email { get; set; }
        public string? telefon { get; set; }
        public string? korisnickoIme { get; set; }
        public string? slika { get; set; }
        [JsonIgnore]
        public string? lozinka { get; set; }
        [JsonIgnore]
        public Administrator administrator => this as Administrator;
        [JsonIgnore]
        public Manager manager => this as Manager;
        [JsonIgnore]
        public Korisnik korisnik => this as Korisnik;
        public bool isKorisnik => korisnik != null;
        public bool isAdministrator => administrator != null;
        public bool isManager => manager != null;  
    }
}
