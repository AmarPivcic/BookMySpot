import { Grad } from "./grad.model";
import { Kategorija } from "./kategorija.model";

export interface UsluzniObjekt{
    usluzniObjektID: number;
    nazivObjekta: string;
    adresa: string;
    telefon: string;
    radnoVrijemePocetak: string;
    radnoVrijemeKraj: string;
    slika: string;
    kategorija: Kategorija;
    prosjecnaOcjena: number;
    grad: Grad;
    isSmjestaj: boolean;
    latitude: number;
    longitude: number;
  }
