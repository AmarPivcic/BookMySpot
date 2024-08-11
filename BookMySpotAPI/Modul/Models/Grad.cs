using System.ComponentModel.DataAnnotations;
namespace BookMySpotAPI.Modul.Models
{
    public class Grad
    {
        [Key]
        public int gradID { get; set; }
        public string? naziv { get; set; }
    }
}
