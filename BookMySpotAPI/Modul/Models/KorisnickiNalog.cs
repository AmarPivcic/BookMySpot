using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BookMySpotAPI.Modul.Models
{
    [Table("KorisnickiNalog")]
    public class KorisnickiNalog
    {
        [Key]
        public int OsobaID { get; set; }
        public string? Ime { get; set; }
        public string? Prezime { get; set; }
        public string? Email { get; set; }
        public string? Telefon { get; set; }
        public string? korisnickoIme { get; set; }
        public byte[]? Slika { get; set; }
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
