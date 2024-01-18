using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookMySpotAPI.Modul.Models
{
    [Table("Usluga")]
    public class Usluga
    {
        [Key]
        public int UslugaID { get; set; }
        public string? Naziv { get; set; }
        public string? Trajanje { get; set; }
        public float Cijena { get; set; }
        [ForeignKey(nameof(UsluzniObjekt))]
        public int UsluzniObjektID { get; set; }
    }
}
