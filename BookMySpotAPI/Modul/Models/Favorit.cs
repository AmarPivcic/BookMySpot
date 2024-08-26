using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookMySpotAPI.Modul.Models
{
    [Table("Favorit")]
    public class Favorit
    {
        [Key]
        public int favoritID { get; set; }
        [ForeignKey(nameof(KorisnickiNalog))]
        public int KorisnickiNalogId { get; set; }
        public KorisnickiNalog korisnickiNalog { get; set; }
        [ForeignKey(nameof(UsluzniObjekt))]
        public int usluzniObjektID { get; set; }
        public UsluzniObjekt usluzniObjekt { get; set; }
    }
}
