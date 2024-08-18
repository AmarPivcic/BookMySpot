namespace BookMySpotAPI.Modul.ViewModels
{
    public class PitanjaOdgovoriResponseUserVM
    {
        public int Id { get; set; }
        public string Pitanje { get; set; }
        public string? Odgovor { get; set; }
        public DateTime DatumKreiranja { get; set; }
    }
}
