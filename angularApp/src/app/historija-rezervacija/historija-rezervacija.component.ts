import { Component, OnInit } from '@angular/core';
import { Rezervacija } from '../models/rezervacija.model';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { MojConfig } from '../moj-config';
import { LoginInformacije } from '../_helpers/login-informacije';
import { AutentifikacijaHelper } from '../_helpers/autentifikacija-helper';
import { TerminFunckijeService } from '../shared/termin-manager/termin-funckije.service';

@Component({
  selector: 'app-historija-rezervacija',
  templateUrl: './historija-rezervacija.component.html',
  styleUrl: './historija-rezervacija.component.css'
})
export class HistorijaRezervacijaComponent implements OnInit{


  listaRezervacija: Rezervacija[] | null = null;
  logiraniKorisnik: any;
  aktivne: boolean = true;
  ponoviRezervacijuBool: boolean = false;
  minDate: string = '';
  odabraniDatum: string | null = null;
  odabranoVrijeme: string | null = null;
  dostupniTermini: string[] = [];
  odabranaRezervacija: Rezervacija | null = null;
  karticnoPlacanje: boolean = false;
  datumPocetka: string | null = null;
  datumKraja: string | null = null;
  isSmjestaj: boolean = false;

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

  constructor(private httpKlijent: HttpClient, private router: Router, private terminFunkcije: TerminFunckijeService) 
  {}

  ngOnInit(): void {
    this.getListaTrenutnih();
    this.getPodaci();
    const danas = new Date();
    this.minDate = danas.toISOString().split('T')[0];
  }

  loginInfo():LoginInformacije
  {
    return AutentifikacijaHelper.getLoginInfo();
  }

  getPodaci()
  {
    this.logiraniKorisnik=this.loginInfo().autentifikacijaToken?.korisnickiNalog;
  }

  getListaTrenutnih()
  {
    const trenutneSvg = document.getElementById("trenutneSvg");
    const prethodneSvg = document.getElementById("prethodneSvg");

    this.httpKlijent.get<Rezervacija[]>(MojConfig.adresa_servera + "/Rezervacija/GetListaTrenutnihKorisnik?id="+this.loginInfo().autentifikacijaToken?.osobaID, MojConfig.http_opcije())
    .subscribe({
        next: (response) => 
          {
            this.listaRezervacija = response;
          },
        error: (error) => 
          {
            alert(error);
          }
    })   
        //@ts-ignore
        trenutneSvg.style.fill="white";
        //@ts-ignore
        prethodneSvg.style.fill="#b0a695"
        this.aktivne = true;
  }

  getListaPrethodnih()
  {
    const trenutneSvg = document.getElementById("trenutneSvg");
    const prethodneSvg = document.getElementById("prethodneSvg");

    this.httpKlijent.get<Rezervacija[]>(MojConfig.adresa_servera + "/Rezervacija/GetListaPrethodnihKorisnik?id="+this.loginInfo().autentifikacijaToken?.osobaID, MojConfig.http_opcije())
    .subscribe({
      next: (response) => 
        {
          this.listaRezervacija = response;
        },
      error: (error) => 
        {
          alert(error);
        }
    })
    //@ts-ignore
    trenutneSvg.style.fill="#b0a695";
    //@ts-ignore
    prethodneSvg.style.fill="white"
    this.aktivne=false;
  }

  otkaziRezervaciju(rezervacijaID: number) 
  {
    this.httpKlijent.put(MojConfig.adresa_servera+ "/Rezervacija/OtkaziRezervaciju?id="+rezervacijaID, MojConfig.http_opcije())
    .subscribe({
      next: (response) => {
        console.log(response);
        this.getListaTrenutnih();
      },
      error: (error) => {
        alert(error);
      }
    })
  }

  onDatumChange()
  {
    if(this.odabraniDatum)
      {
        this.terminFunkcije
        //@ts-ignore
        .getListaDostupnihTermina(this.odabranaRezervacija?.usluzniObjekt.usluzniObjektID, this.odabraniDatum, this.odabranaRezervacija?.usluga.trajanje).subscribe(x=>{
          this.dostupniTermini=x;
        });
      }
  }

  onDatumPocetkaChange() {
    if (this.datumPocetka) {
      const datumPocetkaDate = new Date(this.datumPocetka);
      datumPocetkaDate.setDate(datumPocetkaDate.getDate() + 1);
      this.datumKraja = datumPocetkaDate.toISOString().split('T')[0];
    }
  }

  rezervisiTermin()
  {
    if(this.odabraniDatum && this.odabranoVrijeme)
    {
      //@ts-ignore
      this.terminFunkcije.rezervisiTermin(this.odabraniDatum, this.odabranoVrijeme, this.odabranaRezervacija?.usluga, this.karticnoPlacanje, this.loginInfo().autentifikacijaToken?.osobaID);
      this.ugasiPopup();
    }
    else{
      alert("Morate odabrati datum rezervacije i početno vrijeme rezervacije!");
    }
  }

  loadGodineIseljenja() {
    this.terminFunkcije.loadGodineIseljenja().subscribe(godineIseljenja => {
      this.godineIseljenja = godineIseljenja;
      this.onGodinaIseljenjaChange();
    });

  }

  loadGodine() {
    this.terminFunkcije.loadGodine().subscribe(godine => {
      this.godine = godine;
      this.godineIseljenja = godine;
      this.onGodinaChange();
      this.onGodinaIseljenjaChange();
    });
  }

  onGodinaChange() {
    if (this.selectedGodina !== undefined) {
      this.terminFunkcije.onGodinaChange(this.selectedGodina).subscribe(mjeseci => {
        this.mjeseci = mjeseci;
        this.selectedMjesec = mjeseci[0];
        this.onMjesecChange();
      });
    }
  }

  onMjesecChange() {
    if (this.selectedGodina !== undefined && this.selectedMjesec !== undefined && this.odabranaRezervacija) {
      this.terminFunkcije.onMjesecChange(this.selectedGodina, this.selectedMjesec, this.odabranaRezervacija.usluga?.uslugaID).subscribe(dani => {
        this.dani = dani;
        this.selectedDan = dani[0];
      });
    }
  }

  onGodinaIseljenjaChange() {
    if (this.selectedGodinaIseljenja !== undefined) {
      this.terminFunkcije.onGodinaIseljenjaChange(this.selectedGodinaIseljenja).subscribe(mjeseciIseljenja => {
        this.mjeseciIseljenja = mjeseciIseljenja;
        this.selectedMjesecIseljenja = mjeseciIseljenja.length > 0 ? mjeseciIseljenja[0] : undefined;
        this.onMjesecIseljenjaChange();
      });
    }
  }

  onMjesecIseljenjaChange() {
    if (this.selectedGodinaIseljenja !== undefined && this.selectedMjesecIseljenja !== undefined && this.odabranaRezervacija) {
      this.terminFunkcije.onMjesecIseljenjaChange(this.selectedGodinaIseljenja, this.selectedMjesecIseljenja, this.odabranaRezervacija.usluga?.uslugaID).subscribe(daniIseljenja => {
        this.daniIseljenja = daniIseljenja;
        console.log('Dani iseljenja:', daniIseljenja);
        this.selectedDanIseljenja = daniIseljenja.length > 0 ? daniIseljenja[0] : undefined;
      });
    }
  }

  loadDani() {
    if (this.selectedGodina !== undefined && this.selectedMjesec !== undefined && this.odabranaRezervacija) {
      this.terminFunkcije.loadDani(this.selectedGodina, this.selectedMjesec, this.odabranaRezervacija?.usluga.uslugaID).subscribe({
        next: (dani: number[]) => {
          this.dani = dani;
        },
        error: (error) => {
          console.error("Greška pri dohvaćanju dana:", error);
        }
      });
    }
  }

  onSmjestajUsluga() {
    this.loadGodine();
    this.loadGodineIseljenja();
    this.loadDani();
  }

  rezervisiTerminSmjestaja() {
    if (this.odabranaRezervacija &&
      this.selectedGodina && this.selectedMjesec && this.selectedDan &&
      this.selectedGodinaIseljenja && this.selectedMjesecIseljenja && this.selectedDanIseljenja) {

      this.terminFunkcije.rezervisiTerminSmjestaja(
        this.selectedDan,
        this.selectedMjesec,
        this.selectedGodina,
        this.selectedDanIseljenja,
        this.selectedMjesecIseljenja,
        this.selectedGodinaIseljenja,
        this.dani,
        this.logiraniKorisnik.osobaID,
        this.odabranaRezervacija?.usluga,
        this.karticnoPlacanje
      )


      this.selectedGodina = undefined;
      this.selectedMjesec = undefined;
      this.selectedDan = undefined;
      this.selectedGodinaIseljenja = undefined;
      this.selectedMjesecIseljenja = undefined;
      this.selectedDanIseljenja = undefined;
      this.karticnoPlacanje = false;
      this.ugasiPopup();
    } 
    else {
      alert("Molimo unesite sve potrebne podatke za rezervaciju.");
    }
  }

  

  ponoviRezervaciju(rezervacija: Rezervacija)
  {
    this.ponoviRezervacijuBool=true;
    document.body.style.overflow='hidden';
    window.scrollTo(0,0);
    this.odabranaRezervacija=rezervacija;

    if(rezervacija.usluzniObjekt.isSmjestaj)
    {
         this.isSmjestaj = true;
         this.onSmjestajUsluga();
     }
  }

  ugasiPopup()
  {
    this.ponoviRezervacijuBool=false;
    this.odabraniDatum=null;
    this.odabranoVrijeme=null;
    document.body.style.overflow='auto';
    this.isSmjestaj=false;
    this.datumKraja=null;
    this.datumPocetka=null;
    this.selectedGodina = undefined;
    this.selectedMjesec = undefined;
    this.selectedDan = undefined;
    this.selectedGodinaIseljenja = undefined;
    this.selectedMjesecIseljenja = undefined;
    this.selectedDanIseljenja = undefined;
  }

  formatirajDatum(datum: any) {
    const date = new Date(datum);
    const year = date.getFullYear();
    const month = (date.getMonth() + 1).toString().padStart(2, '0'); // getMonth is 0-based
    const day = date.getDate().toString().padStart(2, '0');
  
    return `${year}-${month}-${day}`;
  }

}
