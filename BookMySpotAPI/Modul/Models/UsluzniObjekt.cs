using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookMySpotAPI.Modul.Models
{
    [Table("UsluzniObjekt")]
    public class UsluzniObjekt
    {
        [Key]
        public int UsluzniObjektID { get; set; }
        public string? NazivObjekta { get; set; }
        public string? Adresa { get; set; }
        public string? Telefon { get; set; }
        [ForeignKey(nameof(Manager))]
        public int ManagerID { get; set; }
        public byte[]? Slika { get; set; }
        [ForeignKey(nameof(Kategorija))]
        public int KategorijaID { get; set;}
    }
}
