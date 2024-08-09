using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookMySpotAPI.Modul.Models
{
    [Table("UsluzniObjekt")]
    public class UsluzniObjekt
    {
        [Key]
        public int usluzniObjektID { get; set; }
        public string? nazivObjekta { get; set; }
        public string? adresa { get; set; }
        public string? telefon { get; set; }
        [ForeignKey(nameof(Manager))]
        public int managerID { get; set; }
        public Manager manager { get; set; }
        public string? slika { get; set; }
        [ForeignKey(nameof(Kategorija))]
        public int KategorijaID { get; set;}
        public Kategorija kategorija { get; set; }
        public float prosjecnaOcjena { get; set; }
        public ICollection<Usluga> usluge { get; set; } = new List<Usluga>();
    }
}
