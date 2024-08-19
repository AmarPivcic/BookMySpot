import {Component, OnInit} from '@angular/core';
import {RouterLink} from "@angular/router";
import {KorisnikInformacije} from "../models/korisnikInformacije.model";
import {HttpClient} from "@angular/common/http";
import {LoginInformacije} from "../_helpers/login-informacije";
import {AutentifikacijaHelper} from "../_helpers/autentifikacija-helper";
import {MojConfig} from "../moj-config";
import {FormsModule} from "@angular/forms";
import {NgIf} from "@angular/common";

@Component({
  selector: 'app-moj-racun-edit',
  standalone: true,
  imports: [
    RouterLink,
    FormsModule,
    NgIf
  ],
  templateUrl: './moj-racun-edit.component.html',
  styleUrl: './moj-racun-edit.component.css'
})
export class MojRacunEditComponent implements OnInit{
  korisnikInformacije: KorisnikInformacije;
  url = MojConfig.adresa_servera;
  constructor(private httpClient: HttpClient) {
    this.korisnikInformacije = {
      ime: "",
      prezime: "",
      email: "",
      telefon: "",
      korisnickoIme: "",
      slika: ""
    };
  }
  ngOnInit(): void {
    this.httpClient.get<KorisnikInformacije>(this.url + "/Korisnik/Get?id="+this.loginInfo().autentifikacijaToken?.korisnickiNalog.osobaID).subscribe({
      next: (response) => {
        this.korisnikInformacije = response;
        console.log("Uspjesno dobavljen korisnik!");
      },
      error: (error) => {
        console.log("Nije dobavljen korisnik!", error);
      }
    })
  }

  loginInfo(): LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }



}
