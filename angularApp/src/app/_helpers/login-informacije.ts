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
  Ime: string;
  Prezime: string;
  Email: string;
  Telefon: string;
  Adresa: string;
  korisnickoIme: string;
  Slika: string;
  lozinka: string;
  isKorisnik: boolean;
  isAdministrator: boolean;
  isManager: boolean;
}
