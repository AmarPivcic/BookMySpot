using System.ComponentModel.DataAnnotations;

namespace BookMySpotAPI.Modul.Models
{
    public class ONamaSadrzaj
    {
        [Key]
        public int Id { get; set; }
        public string Tekst { get; set; }
    }
}
