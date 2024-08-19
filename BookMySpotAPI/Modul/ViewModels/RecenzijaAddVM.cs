using BookMySpotAPI.Modul.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookMySpotAPI.Modul.ViewModels
{
    public class RecenzijaAddVM
    {
        public int recenzijaOcjena { get; set; }
        public string recenzijaTekst { get; set; }
        public int usluzniObjektID { get; set; }
        public int osobaID { get; set; }
    }
}
