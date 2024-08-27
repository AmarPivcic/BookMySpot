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

  getListaDostupnihTermina(objektID: number,datum: string, trajanje: number, managerID: number)
  {
    let params = new HttpParams()
      .set('usluzniObjektID', objektID)
      .set('odabraniDatum', datum)
      .set('trajanje', trajanje)
      .set('managerID', managerID);

    return this.httpKlijent.get<string[]>(MojConfig.adresa_servera+"/Rezervacija/GetListaSlobodnihTermina", {params});
  }

  rezervisiTermin(datum:string, pocetak: string, usluga:Usluga, karticno: boolean, osoba: any, managerID: number)
  {
    let parametri: any = {
      datumRezervacije: datum,
      rezervacijaPocetak: pocetak,
      trajanje: usluga.trajanje,
      osobaID: osoba,
      uslugaID: usluga.uslugaID,
      usluzniObjektID: usluga.usluzniObjekt.usluzniObjektID,
      karticnoPlacanje: karticno,
      managerID: managerID
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

  obaviRezervaciju(datumPocetka:string, datumKraja: string, osoba:any, usluga: Usluga, karticno: boolean, managerID: number)
  {
    let rezervacijaPodaci: any = {
      rezervacijaPocetak: datumPocetka,
      rezervacijaKraj: datumKraja,
      osobaID: osoba,
      uslugaID: usluga.uslugaID,
      usluzniObjektID: usluga.usluzniObjekt.usluzniObjektID,
      karticnoPlacanje: karticno,
      managerID
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

  loadGodineIseljenja() {
    return this.httpKlijent.get<number[]>(MojConfig.adresa_servera + '/Rezervacija/GetGodine');
  }

  loadGodine() {
    return this.httpKlijent.get<number[]>(MojConfig.adresa_servera + '/Rezervacija/GetGodine');
  }

  loadDani(selectedGodina: number, selectedMjesec: number, uslugaID: number) {
    return this.httpKlijent.get<number[]>(MojConfig.adresa_servera + `/Rezervacija/GetDani?godina=${selectedGodina}&mjesec=${selectedMjesec}&uslugaId=${uslugaID}`);
  }

  onGodinaChange(selectedGodina: number) {
    return this.httpKlijent.get<number[]>(MojConfig.adresa_servera + `/Rezervacija/GetMjeseci?godina=${selectedGodina}`)
  }

  onMjesecChange(selectedGodina: number, selectedMjesec: number, uslugaID: number) {
    return this.httpKlijent.get<number[]>(MojConfig.adresa_servera + `/Rezervacija/GetDani?godina=${selectedGodina}&mjesec=${selectedMjesec}&uslugaId=${uslugaID}`);
  }

  onGodinaIseljenjaChange(selectedGodinaIseljenja: number) {
    return this.httpKlijent.get<number[]>(MojConfig.adresa_servera + `/Rezervacija/GetMjeseci?godina=${selectedGodinaIseljenja}`);
  }

  onMjesecIseljenjaChange(selectedGodinaIseljenja: number, selectedMjesecIseljenja: number, uslugaID: number) {
    return this.httpKlijent.get<number[]>(MojConfig.adresa_servera + `/Rezervacija/GetDani?godina=${selectedGodinaIseljenja}&mjesec=${selectedMjesecIseljenja}&uslugaId=${uslugaID}`);
  }

  rezervisiTerminSmjestaja(
    selectedDan: number,
    selectedMjesec: number,
    selectedGodina:number,
    selectedDanIseljenja: number,
    selectedMjesecIseljenja: number,
    selectedGodinaIseljenja: number,
    dani: number[],
    osobaID: number,
    odabranaUsluga: Usluga,
    karticnoPlacanje: boolean,
    managerID: number
    ) {

      if(selectedMjesec == selectedMjesecIseljenja)
        {
          for (let i = Number(selectedDan!) + 1; i <= Number(selectedDanIseljenja!); i++) {
            console.log("Provjeravam dan: " , i);
            if (!dani.includes(Number(i))) {
              console.log("Nedostaje dan:", i);
              alert("Dani između odabranog intervala su već rezervisani. Molimo izaberite drugi period.");
              return;
            }
          }
        }

        else if (selectedMjesec < selectedMjesecIseljenja)
        {
          for (let i = Number(selectedDan!) + 1; i <= Number(dani.at(-1)); i++)
          {
            console.log("Provjeravam dan: " , i);
            if (!dani.includes(Number(i))) {
              console.log("Nedostaje dan:", i);
              alert("Dani između odabranog intervala su već rezervisani. Molimo izaberite drugi period.");
              return;
            }
          }

          for(let i = 1; i <= Number(selectedDanIseljenja!); i++)
          {
            console.log("Provjeravam dan: " , i);
            if (!dani.includes(Number(i))) {
              console.log("Nedostaje dan:", i);
              alert("Dani između odabranog intervala su već rezervisani. Molimo izaberite drugi period.");
              return;
            }
          }
        }

      const datumPocetka = `${selectedGodina}/${selectedMjesec.toString().padStart(2, '0')}/${selectedDan.toString().padStart(2, '0')}`;
      const datumKraja = `${selectedGodinaIseljenja}/${selectedMjesecIseljenja.toString().padStart(2, '0')}/${selectedDanIseljenja.toString().padStart(2, '0')}`;

      const datumPocetkaDate = new Date(selectedGodina, selectedMjesec - 1, selectedDan);
      const datumKrajaDate = new Date(selectedGodinaIseljenja, selectedMjesecIseljenja - 1, selectedDanIseljenja);

      if (datumKrajaDate < datumPocetkaDate) {
        alert("Datum iseljenja ne može biti raniji od datuma useljenja.");
        return;
      }

    this.obaviRezervaciju(
        datumPocetka,
        datumKraja,
        osobaID,
        odabranaUsluga,
        karticnoPlacanje,
        managerID);
  }
}


