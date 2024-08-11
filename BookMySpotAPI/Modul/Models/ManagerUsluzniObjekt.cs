using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookMySpotAPI.Modul.Models
{
    [Table("ManagerUsluzniObjekt")]
    public class ManagerUsluzniObjekt
    {
        public int osobaID { get; set; }
        public Manager manager { get; set; }
        public int usluzniObjektID { get; set; }
        public UsluzniObjekt usluzniObjekt { get; set; }
    }
}
