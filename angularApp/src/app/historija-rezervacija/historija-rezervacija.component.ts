import { Component, OnInit } from '@angular/core';
import { Rezervacija } from '../models/rezervacija.model';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { MojConfig } from '../moj-config';
import { LoginInformacije } from '../_helpers/login-informacije';
import { AutentifikacijaHelper } from '../_helpers/autentifikacija-helper';

@Component({
  selector: 'app-historija-rezervacija',
  templateUrl: './historija-rezervacija.component.html',
  styleUrl: './historija-rezervacija.component.css'
})
export class HistorijaRezervacijaComponent implements OnInit{


  listaRezervacija: Rezervacija[] | null = null;
  logiraniKorisnik: any;
  aktivne: boolean = true;

  constructor(private httpKlijent: HttpClient, private router: Router) 
  {}

  ngOnInit(): void {
    this.getListaTrenutnih();
    this.getPodaci();
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

  formatirajDatum(datum: Date)
  {
    const date = new Date(datum);
    return date.toISOString().split('T')[0];
  }

}
