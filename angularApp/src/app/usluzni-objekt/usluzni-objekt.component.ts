import { HttpClient, HttpParams } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router} from '@angular/router';
import { UsluzniObjekt } from '../models/usluzniObjekt.model';
import { MojConfig } from '../moj-config';
import { Usluga } from '../models/usluga.model';
import { LoginInformacije } from '../_helpers/login-informacije';
import { AutentifikacijaHelper } from '../_helpers/autentifikacija-helper';
import { HeaderComponent } from '../shared/header/header.component';
import { Recenzija } from '../models/recenzija.model';
import { TerminFunckijeService } from '../shared/termin-manager/termin-funckije.service';
import {compareSegments} from "@angular/compiler-cli/src/ngtsc/sourcemaps/src/segment_marker";

@Component({
  selector: 'app-usluzni-objekt',
  templateUrl: './usluzni-objekt.component.html',
  styleUrl: './usluzni-objekt.component.css'
})
export class UsluzniObjektComponent implements OnInit {

  usluzniObjektID: any;
  usluzniObjekt: UsluzniObjekt | null = null;
  listaUsluga: Usluga[] | null = null;
  odabraniDatum: string | null = null;
  odabranaUsluga: Usluga | null = null;
  odabranoVrijeme: string | null = null;
  dostupniTermini: string[] = [];
  minDate: string = '';
  prosjecnaOcjena: any;
  listaRecenzija: Recenzija[] | null = null;
  defaultSlika: any = "../../assets/user.png";
  odabranaOcjena: number | null = null;
  tekstRecenzije: string = "";
  logiraniKorisnik: any;
  datumPocetka: string | null = null;
  datumKraja: string | null = null;
  karticnoPlacanje: boolean = false;


  //Nova logika
  godine: number[] = [];
  mjeseci: number[] = [];
  dani: number[] = [];
  godineIseljenja: number[] = [];
  mjeseciIseljenja: number[] = [];
  daniIseljenja: number[] = [];

  selectedGodina?: number;
  selectedMjesec?: number;
  selectedDan?: number;

  selectedGodinaIseljenja?: number;
  selectedMjesecIseljenja?: number;
  selectedDanIseljenja?: number;

  constructor(private httpKlijent: HttpClient, private route: ActivatedRoute, private router: Router, private menu: HeaderComponent, private terminFunkcije: TerminFunckijeService)
{
}

  ngOnInit(): void {
    this.usluzniObjektID=Number(this.route.snapshot.paramMap.get('usluzniObjektId'));
    this.getUsluzniObjekt(this.usluzniObjektID);
    this.getListaUsluga(this.usluzniObjektID);
    this.getListaRecenzija(this.usluzniObjektID);
    this.getPodaci();
    const danas = new Date();
    this.minDate = danas.toISOString().split('T')[0];

    if (this.datumPocetka) {
      this.datumKraja = this.datumPocetka;
    }
  }

  loadGodineIseljenja() {
    this.httpKlijent.get<number[]>('https://localhost:7058/Rezervacija/GetGodine').subscribe(godineIseljenja => {
      this.godineIseljenja = godineIseljenja;
      this.onGodinaIseljenjaChange();
    });
  }

  loadGodine() {
    this.httpKlijent.get<number[]>('https://localhost:7058/Rezervacija/GetGodine').subscribe(godine => {
      this.godine = godine;
      this.godineIseljenja = godine;
      this.onGodinaChange();
      this.onGodinaIseljenjaChange();
    });
  }

  onGodinaChange() {
    if (this.selectedGodina !== undefined) {
      this.httpKlijent.get<number[]>(`https://localhost:7058/Rezervacija/GetMjeseci?godina=${this.selectedGodina}`).subscribe(mjeseci => {
        this.mjeseci = mjeseci;
        this.selectedMjesec = mjeseci[0];
        this.onMjesecChange();
      });
    }
  }

  onMjesecChange() {
    if (this.selectedGodina !== undefined && this.selectedMjesec !== undefined) {
      this.httpKlijent.get<number[]>(`https://localhost:7058/Rezervacija/GetDani?godina=${this.selectedGodina}&mjesec=${this.selectedMjesec}&uslugaId=${this.odabranaUsluga?.uslugaID}`).subscribe(dani => {
        this.dani = dani;
        this.selectedDan = dani[0];
      });
    }
  }

  onGodinaIseljenjaChange() {
    if (this.selectedGodinaIseljenja !== undefined) {
      this.httpKlijent.get<number[]>(`https://localhost:7058/Rezervacija/GetMjeseci?godina=${this.selectedGodinaIseljenja}`).subscribe(mjeseciIseljenja => {
        this.mjeseciIseljenja = mjeseciIseljenja;
        this.selectedMjesecIseljenja = mjeseciIseljenja.length > 0 ? mjeseciIseljenja[0] : undefined;
        this.onMjesecIseljenjaChange();
      });
    }
  }


  onMjesecIseljenjaChange() {
    if (this.selectedGodinaIseljenja !== undefined && this.selectedMjesecIseljenja !== undefined) {
      this.httpKlijent.get<number[]>(`https://localhost:7058/Rezervacija/GetDani?godina=${this.selectedGodinaIseljenja}&mjesec=${this.selectedMjesecIseljenja}&uslugaId=${this.odabranaUsluga?.uslugaID}`).subscribe(daniIseljenja => {
        this.daniIseljenja = daniIseljenja;
        console.log('Dani iseljenja:', daniIseljenja);
        this.selectedDanIseljenja = daniIseljenja.length > 0 ? daniIseljenja[0] : undefined;
      });
    }
  }




  getStars(): number[] {
    const totalStars = 5;
    return Array(totalStars).fill(0);
  }

  get filledStars(): number {
    return Math.round(this.prosjecnaOcjena);
  }

  getImagePath(putanja: string): string {
    return putanja ? putanja : this.defaultSlika;
  }

  loginInfo():LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }

  getPodaci()
  {
    this.logiraniKorisnik=this.loginInfo().autentifikacijaToken?.korisnickiNalog;
  }

  getUsluzniObjekt(id: number) {
    this.httpKlijent.get<UsluzniObjekt>(MojConfig.adresa_servera + "/UsluzniObjekt/Get?id="+id, MojConfig.http_opcije()).subscribe(x=>{
      this.usluzniObjekt=x;
      this.prosjecnaOcjena=x.prosjecnaOcjena;
    })
  }

  getListaUsluga(id:number)
  {
    this.httpKlijent.get<Usluga[]>(MojConfig.adresa_servera+ "/Usluga/GetByObjektID?id="+id, MojConfig.http_opcije()).subscribe(x=>{
      this.listaUsluga=x;
    })
  }

  getListaRecenzija(id:number)
  {
    this.httpKlijent.get<Recenzija[]>(MojConfig.adresa_servera+ "/Recenzija/GetByUsluzniObjektID?id="+id, MojConfig.http_opcije()).subscribe(x=>{
      this.listaRecenzija = x;
    })
  }

  prijava()
  {
    this.menu.NavigirajIZatvori("/login");
  }

  onDatumUslugaChange()
  {
    if(this.odabraniDatum && this.odabranaUsluga)
    {
       this.terminFunkcije.getListaDostupnihTermina(this.usluzniObjektID,this.odabraniDatum, this.odabranaUsluga.trajanje).subscribe(x=>{
        this.dostupniTermini=x;
       });
    }
  }

  rezervisiTermin() {
    console.log(this.karticnoPlacanje);

    if(this.odabraniDatum && this.odabranaUsluga && this.odabranoVrijeme)
    {
      this.terminFunkcije.rezervisiTermin(this.odabraniDatum, this.odabranoVrijeme, this.odabranaUsluga, this.karticnoPlacanje, this.logiraniKorisnik.osobaID);
      this.odabraniDatum=null;
      this.odabranoVrijeme=null;
      this.odabranaUsluga=null;
    }

    else{
      alert("Morate odabrati datum rezervacije, uslugu i početno vrijeme rezervacije!");
    }
  }

  posaljiRecenziju() {
    if(this.odabranaOcjena && this.tekstRecenzije)
    {
      let parametri: any=
      {
        recenzijaOcjena: this.odabranaOcjena,
        recenzijaTekst: this.tekstRecenzije,
        osobaID: this.loginInfo().autentifikacijaToken?.osobaID,
        usluzniObjektID: this.usluzniObjektID
      };

      this.httpKlijent.post(MojConfig.adresa_servera+"/Recenzija/Add", parametri, MojConfig.http_opcije()).subscribe({
        next: (response) => {
          alert("Recenzija uspješno dodana!");
          console.log(response);
          this.getListaRecenzija(this.usluzniObjektID);
        },
        error: (error) => {
          alert("Greška pri dodavanju recenzije!");
          console.log(error);
        }
      });
      this.odabranaOcjena = null;
      this.tekstRecenzije = "";
    }

    else{
      alert("Morate odabrati ocjenu i unijeti tekst recenzije!");
    }
  }

  onSmjestajUsluga() {
    this.loadGodine();
    this.loadGodineIseljenja();
    this.loadDani();
  }

  loadDani() {
    if (this.selectedGodina !== undefined && this.selectedMjesec !== undefined) {
      this.httpKlijent.get<number[]>(`https://localhost:7058/Rezervacija/GetDani?godina=${this.selectedGodina}&mjesec=${this.selectedMjesec}&uslugaId=${this.odabranaUsluga?.uslugaID}`)
        .subscribe({
          next: (dani: number[]) => {
            this.dani = dani;
          },
          error: (error) => {
            console.error("Greška pri dohvaćanju dana:", error);
          }
        });
    }
  }

  rezervisiTerminSmjestaja() {
    if (this.odabranaUsluga &&
      this.selectedGodina && this.selectedMjesec && this.selectedDan &&
      this.selectedGodinaIseljenja && this.selectedMjesecIseljenja && this.selectedDanIseljenja) {

      for (let i = Number(this.selectedDan!) + 1; i <= Number(this.selectedDanIseljenja!); i++) {
        console.log("Provjeravam dan: " , i);
        if (!this.dani.includes(Number(i))) {
          console.log("Nedostaje dan:", i);
          alert("Dani između odabranog intervala su već rezervisani. Molimo izaberite drugi period.");
          return;
        }
      }

      const datumPocetka = `${this.selectedGodina}/${this.selectedMjesec.toString().padStart(2, '0')}/${this.selectedDan.toString().padStart(2, '0')}`;
      const datumKraja = `${this.selectedGodinaIseljenja}/${this.selectedMjesecIseljenja.toString().padStart(2, '0')}/${this.selectedDanIseljenja.toString().padStart(2, '0')}`;

      const datumPocetkaDate = new Date(this.selectedGodina, this.selectedMjesec - 1, this.selectedDan);
      const datumKrajaDate = new Date(this.selectedGodinaIseljenja, this.selectedMjesecIseljenja - 1, this.selectedDanIseljenja);

      if (datumKrajaDate < datumPocetkaDate) {
        alert("Datum iseljenja ne može biti raniji od datuma useljenja.");
        return;
      }

      this.obaviRezervaciju(
        datumPocetka,
        datumKraja,
        this.logiraniKorisnik.osobaID,
        this.odabranaUsluga,
        this.karticnoPlacanje
      );

      this.odabranaUsluga = null;
      this.selectedGodina = undefined;
      this.selectedMjesec = undefined;
      this.selectedDan = undefined;
      this.selectedGodinaIseljenja = undefined;
      this.selectedMjesecIseljenja = undefined;
      this.selectedDanIseljenja = undefined;
      this.karticnoPlacanje = false;
    } else {
      alert("Molimo unesite sve potrebne podatke za rezervaciju.");
    }
  }

  obaviRezervaciju(
    datumPocetka: string,
    datumKraja: string,
    osoba: any,
    usluga: Usluga,
    karticno: boolean
  ) {
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

}
