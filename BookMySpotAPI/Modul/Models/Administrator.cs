using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookMySpotAPI.Modul.Models
{
    [Table("Administrator")]
    public class Administrator: Osoba
    {
        [Key]
        public int AdministratorID { get; set; }
    }
}
