using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BookMySpotAPI.Modul.Models
{
    [Table("Administrator")]
    public class Administrator: KorisnickiNalog
    {
        [JsonIgnore]
        public string PIN{ get; set; }
    }
}
