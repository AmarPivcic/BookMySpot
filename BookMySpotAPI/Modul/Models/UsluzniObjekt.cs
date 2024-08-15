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
        public string? radnoVrijemePocetak { get; set; }
        public string? radnoVrijemeKraj { get; set; }
        public string? slika { get; set; }
        [ForeignKey(nameof(Kategorija))]
        public int kategorijaID { get; set;}
        public Kategorija kategorija { get; set; }
        public float prosjecnaOcjena { get; set; }
        [ForeignKey(nameof (Grad))]
        public int gradID { get; set; }
        public Grad grad { get; set; }
        public ICollection<ManagerUsluzniObjekt> managerUsluzniObjekt { get; set; } = new List<ManagerUsluzniObjekt>();
    }
}
