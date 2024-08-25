using BookMySpotAPI.Modul.Models;

namespace BookMySpotAPI.Modul.ViewModels
{
    public class PostojeciRacuniResponseVM
    {
        public int osobaID { get; set; }
        public string? ime { get; set; }
        public string? prezime { get; set; }
        public string? email { get; set; }
        public string? telefon { get; set; }
        public string? korisnickoIme { get; set; }
        public string? slika { get; set; }
        public bool isKorisnik { get; set; }
        public bool isAdministrator { get; set; }
        public bool isManager { get; set; }
    }
}
