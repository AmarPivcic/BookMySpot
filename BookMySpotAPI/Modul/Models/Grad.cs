using System.ComponentModel.DataAnnotations;
namespace BookMySpotAPI.Modul.Models
{
    public class Grad
    {
        [Key]
        public int GradID { get; set; }
        public string? Naziv { get; set; }
    }
}
