namespace BookMySpotAPI.Modul.ViewModels
{
    public class PitanjaOdgovoriResponseVM
    {
        public int Id { get; set; }
        public string Pitanje { get; set; }
        public string? Odgovor { get; set; }
        public DateTime DatumKreiranja { get; set; }
        public int osobaID { get; set; }
        public string? ime { get; set; }
        public string? prezime { get; set; }
        public string? email { get; set; }
        public string? telefon { get; set; }
        public string? korisnickoIme { get; set; }
    }
}
