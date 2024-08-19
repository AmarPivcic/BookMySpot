export class LoginInformacije {
  autentifikacijaToken?: AutentifikacijaToken|null=null;
  isLogiran: boolean=false;
}

export interface AutentifikacijaToken {
  id: number;
  vrijednost: string;
  osobaID: number;
  korisnickiNalog: korisnickiNalog;
  vrijemeEvidentiranja: Date;
  ipAdresa: string;
}

export interface korisnickiNalog {
  osobaID: number;
  ime: string;
  prezime: string;
  email: string;
  telefon: string;
  adresa: string;
  korisnickoIme: string;
  slika: string;
  lozinka: string;
  isKorisnik: boolean;
  isAdministrator: boolean;
  isManager: boolean;
}
