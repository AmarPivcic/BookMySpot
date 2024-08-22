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

  rezervisiTerminSmjestaja()
  {
    if(this.datumPocetka && this.datumKraja)
    {
      //@ts-ignore
      this.terminFunkcije.rezervisiTerminSmjestaja(this.datumPocetka, this.datumKraja, this.loginInfo().autentifikacijaToken?.osobaID, this.odabranaRezervacija?.usluga, this.karticnoPlacanje);
      this.ugasiPopup();
    }
    else
    {
      alert("Morate odabrati datum početka i datum kraja rezervacije!");
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
      this.terminFunkcije.getNajdaljiDatum(rezervacija.usluga).subscribe({
        next: (response: Date) => {
          if (response) {
            let najdaljiDatum = new Date(response);
            najdaljiDatum.setDate(najdaljiDatum.getDate() + 2);
            this.minDate = najdaljiDatum.toISOString().split('T')[0];
          } else {
            this.minDate = new Date().toISOString().split('T')[0];
          }
        },
        error: (error) => {
          console.error("Greška pri dohvaćanju najudaljenijeg datuma:", error);
          this.minDate = new Date().toISOString().split('T')[0];
        }
      });
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
  }

  formatirajDatum(datum: any) {
    const date = new Date(datum);
    const year = date.getFullYear();
    const month = (date.getMonth() + 1).toString().padStart(2, '0'); // getMonth is 0-based
    const day = date.getDate().toString().padStart(2, '0');
  
    return `${year}-${month}-${day}`;
  }

}
