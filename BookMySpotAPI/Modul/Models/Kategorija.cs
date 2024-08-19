using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookMySpotAPI.Modul.Models
{
    [Table("Kategorija")]
    public class Kategorija
    {
        [Key]
        public int kategorijaID { get; set; }
        public string? naziv { get; set; }
        public string? slika { get; set; }
        [NotMapped]
        public int brojOpcija { get; set; }
    }
}
