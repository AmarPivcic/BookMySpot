using BookMySpotAPI.Modul.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookMySpotAPI.Autentifikacija.Models
{
    public class LogKretanjePoSistemu
    {
        [Key] public int Id { get; set; }
        [ForeignKey(nameof(KorisnickiNalog))]
        public int OsobaID { get; set; }
        public KorisnickiNalog osoba { get; set; }
        public string queryPath { get; set; }
        public string postData { get; set; }
        public DateTime vrijeme { get; set; }
        public string ipAdresa { get; set; }
        public string exceptionMessage { get; set; }
        public bool isException { get; set; }

    }
}
