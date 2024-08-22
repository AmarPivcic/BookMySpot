import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MojConfig } from '../../moj-config';
import { Usluga } from '../../models/usluga.model';

@Injectable({
  providedIn: 'root'
})
export class TerminFunckijeService {

  dostupniTermini: string[] = [];
  minDate: string = '';

  constructor(private httpKlijent: HttpClient) { }

  getListaDostupnihTermina(objektID: number,datum: string, trajanje: number)
  {
    let params = new HttpParams()
      .set('usluzniObjektID', objektID)
      .set('odabraniDatum', datum)
      .set('trajanje', trajanje);

    return this.httpKlijent.get<string[]>(MojConfig.adresa_servera+"/Rezervacija/GetListaSlobodnihTermina", {params});
  }

  rezervisiTermin(datum:string, pocetak: string, usluga:Usluga, karticno: boolean, osoba: any)
  {
    let parametri: any = {
      datumRezervacije: datum,
      rezervacijaPocetak: pocetak,
      trajanje: usluga.trajanje,
      osobaID: osoba,
      uslugaID: usluga.uslugaID,
      usluzniObjektID: usluga.usluzniObjekt.usluzniObjektID,
      karticnoPlacanje: karticno
    };

    this.httpKlijent.post(MojConfig.adresa_servera+"/Rezervacija/Add", parametri, MojConfig.http_opcije()).subscribe({
      next: (response) => {
        alert("Uspješna rezervacija!");
        console.log(response);
      },
      error: (error) => {
        alert("Greška pri pravljenju rezervacije!");
        console.log(error);
      }
    });
  }

  rezervisiTerminSmjestaja(datumPocetka:string, datumKraja: string, osoba:any, usluga: Usluga, karticno: boolean,)
  {
    let rezervacijaPodaci: any = {
      rezervacijaPocetak: datumPocetka,
      rezervacijaKraj: datumKraja,
      osobaID: osoba,
      uslugaID: usluga.uslugaID,
      usluzniObjektID: usluga.usluzniObjekt.usluzniObjektID,
      karticnoPlacanje: karticno
    };

    this.httpKlijent.post(MojConfig.adresa_servera + "/Rezervacija/RezervisiSmjestaj", rezervacijaPodaci, MojConfig.http_opcije()).subscribe({
      next: (response) => {
        alert("Uspješna rezervacija!");
        console.log(response);
        if (rezervacijaPodaci.karticnoPlacanje) {
          window.location.href = 'https://www.paypal.com/signin';
        }
      },
      error: (error) => {
        alert("Greška pri pravljenju rezervacije!");
        console.log(error);
      }
    });
  }

  getNajdaljiDatum(odabranaUsluga: Usluga)
  {
    return this.httpKlijent.get<Date>(MojConfig.adresa_servera + `/Rezervacija/VratiNajudaljenijiDatum?uslugaId=${odabranaUsluga.uslugaID}`, MojConfig.http_opcije());
  }
}
