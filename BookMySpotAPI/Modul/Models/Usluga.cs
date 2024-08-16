using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookMySpotAPI.Modul.Models
{
    [Table("Usluga")]
    public class Usluga
    {
        [Key]
        public int uslugaID { get; set; }
        public string? naziv { get; set; }
        public int trajanje { get; set; }
        public string cijena { get; set; }
        [ForeignKey(nameof(UsluzniObjekt))]
        public int usluzniObjektID { get; set; }
        public UsluzniObjekt usluzniObjekt { get; set; }
    }
}
