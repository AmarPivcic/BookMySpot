namespace BookMySpotAPI.Autentifikacija.ViewModels
{
    public class RegistracijaVM
    {
        public string? Ime { get; set; }
        public string? Prezime { get; set; }
        public string? Email { get; set; }
        public string? korisnickoIme { get; set; }
        public string? lozinka { get; set; }
        public byte[]? Slika { get; set; }
    }
}
