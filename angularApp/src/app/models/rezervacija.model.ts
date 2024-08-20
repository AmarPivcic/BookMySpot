import { Korisnik } from "./korisnik.model";
import { Usluga } from "./usluga.model";
import { UsluzniObjekt } from "./usluzniObjekt.model";

export interface Rezervacija{
    rezervacijaID: number;
    datumRezervacije: Date;
    rezervacijaPocetak: string;
    rezervacijaKraj: string;
    otkazano: boolean;
    zavrseno: boolean;
    korisnik: Korisnik;
    usluga: Usluga;
    usluzniObjekt: UsluzniObjekt;
  }