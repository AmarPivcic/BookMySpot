import { korisnickiNalog } from "../_helpers/login-informacije";

export interface Korisnik extends korisnickiNalog{
    brojRezervacija: number;
  }