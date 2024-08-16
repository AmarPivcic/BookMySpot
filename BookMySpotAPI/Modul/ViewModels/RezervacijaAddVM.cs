namespace BookMySpotAPI.Modul.ViewModels
{
    public class RezervacijaAddVM
    {
        public DateTime datumRezervacije {  get; set; }
        public string? rezervacijaPocetak { get; set; }
        public int trajanje {  get; set; }
        public int korisnikID { get; set; }
        public int uslugaID { get; set; }
        public int usluzniObjektID { get; set; }
    }
}
