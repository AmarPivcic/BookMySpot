import { korisnickiNalog } from "../_helpers/login-informacije";
import { UsluzniObjekt } from "./usluzniObjekt.model";

export interface Recenzija{
    recenzijaID: number;
    recenzijaOcjena: number;
    recenzijaTekst: string;
    usluzniObjekt: UsluzniObjekt;
    korisnickiNalog: korisnickiNalog;
  }