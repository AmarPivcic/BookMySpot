using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookMySpotAPI.Modul.Models
{
    [Table("Recenzija")]
    public class Recenzija
    {
        [Key]
        public int recenzijaID { get; set; }
        public int recenzijaOcjena { get; set; }
        public string? recenzijaTekst {  get; set; }
        [ForeignKey(nameof(UsluzniObjekt))]
        public int usluzniObjektID { get; set; }
        public UsluzniObjekt usluzniObjekt { get; set; }
        [ForeignKey(nameof(KorisnickiNalog))]
        public int KorisnickiNalogId { get; set; }
        public KorisnickiNalog korisnickiNalog { get; set; }
    }
}
