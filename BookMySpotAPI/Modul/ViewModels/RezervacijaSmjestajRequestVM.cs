﻿namespace BookMySpotAPI.Modul.ViewModels
{
    public class RezervacijaSmjestajRequestVM
    {
        public string rezervacijaPocetak { get; set; }
        public string rezervacijaKraj { get; set; }
        public int osobaID { get; set; }
        public int uslugaID { get; set; }
        public int usluzniObjektID { get; set; }
        public bool karticnoPlacanje { get; set; }
        public int managerID { get; set; }
    }
}
