import { UsluzniObjekt } from "./usluzniObjekt.model";

export interface Usluga{
    uslugaID: number;
    naziv: string;
    trajanje: string;
    cijena: number;
    usluzniObjekt: UsluzniObjekt;
  }