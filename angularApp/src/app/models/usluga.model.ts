import { UsluzniObjekt } from "./usluzniObjekt.model";

export interface Usluga{
    uslugaID: number;
    naziv: string;
    trajanje: number;
    cijena: number;
    usluzniObjekt: UsluzniObjekt;
  }