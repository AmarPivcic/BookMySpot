import {Component, OnInit} from '@angular/core';
import {Router, RouterLink} from "@angular/router";
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
  constructor(private httpClient: HttpClient, private router: Router) {
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


  SpasiPromjene() {
    this.httpClient.put<KorisnikInformacije>(this.url + "/Korisnik/EditKorisnickiRacun/"+this.loginInfo().autentifikacijaToken?.korisnickiNalog.osobaID, this.korisnikInformacije).subscribe({
      next: (response) => {
        console.log("Uspjesno editovan korisnik!");
        this.router.navigate(['/mojRacun']);
      },
      error: (error) => {
        console.log("Neuspjesno editovan korisnik!", error);
      }
    })
  }
}
