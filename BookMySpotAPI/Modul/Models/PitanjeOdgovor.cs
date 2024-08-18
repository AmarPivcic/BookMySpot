using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookMySpotAPI.Modul.Models
{
    public class PitanjeOdgovor
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(KorisnickiNalog))]
        public int KorisnickiNalogId { get; set; }
        public KorisnickiNalog korisnickiNalog { get; set; }
        public string Pitanje { get; set; }
        public string? Odgovor { get; set; }
        public DateTime DatumKreiranja { get; set; }
    }
}
