using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookMySpotAPI.Modul.Models
{
    [Table("Kategorija")]
    public class Kategorija
    {
        [Key]
        public int KategorijaID { get; set; }
        public string? Naziv { get; set; }
    }
}
