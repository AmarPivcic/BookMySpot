using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookMySpotAPI.Modul.Models
{
    [Table("Manager")]
    public class Manager :KorisnickiNalog
    {
        public string? pozicija{ get; set; }
    }
}
