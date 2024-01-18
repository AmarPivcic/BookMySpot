using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookMySpotAPI.Modul.Models
{
    [Table("Manager")]
    public class Manager :Osoba
    {
        [Key]
        public int ManagerID { get; set; }
    }
}
