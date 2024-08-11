﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookMySpotAPI.Modul.Models
{
    [Table("Rezervacija")]
    public class Rezervacija
    {
        [Key]
        public int rezervacijaID { get; set; }
        public DateTime datumRezervacije { get; set; }
        public string rezervacijaPocetak {  get; set; }
        public string rezervacijaKraj {  get; set; }
        public bool otkazano = false;
        public bool zavrseno = false;
        [ForeignKey(nameof(Korisnik))]
        public int korisnikID { get; set; }
        public Korisnik korisnik { get; set; }
        [ForeignKey(nameof(Usluga))]
        public int uslugaID { get; set;}
        public Usluga usluga { get; set; }
  
    }
}
