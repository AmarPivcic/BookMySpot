import {Component, OnInit} from '@angular/core';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {Router, RouterLink} from "@angular/router";
import {ObrisiRacun} from "../models/obrisiRacun.model";
import {HttpClient} from "@angular/common/http";
import {AutentifikacijaHelper} from "../_helpers/autentifikacija-helper";
import {MojConfig} from "../moj-config";

@Component({
  selector: 'app-racun-brisanje',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    RouterLink,
    FormsModule
  ],
  templateUrl: './racun-brisanje.component.html',
  styleUrl: './racun-brisanje.component.css'
})
export class RacunBrisanjeComponent implements OnInit{
  showOldPassword = false;
  obrisiRacun: ObrisiRacun;
  url = MojConfig.adresa_servera;
  constructor(private httpClient: HttpClient, private router: Router) {
    this.obrisiRacun = {
      korisnickoIme: "",
      lozinka: ""
    };
  }

  ngOnInit(): void {
    const loginInfo = this.loginInfo();
    this.obrisiRacun.korisnickoIme = loginInfo?.autentifikacijaToken?.korisnickiNalog?.korisnickoIme || "";
  }

  loginInfo() {
    return AutentifikacijaHelper.getLoginInfo();
  }

  ObrisiKorisnickiRacun() {
    this.httpClient.put(this.url + '/Korisnik/ObrisiKorisnickiRacun', this.obrisiRacun).subscribe({
      next: (response) => {
        console.log("Uspjesno obrisan korisnik!");
        this.Odjava();
        this.router.navigate(['/login'])

      },
      error: (error) => {
        alert("Lozinka nije ispravna!")
      }
    })
  }

  Odjava() {
    let token = MojConfig.http_opcije();
    AutentifikacijaHelper.setLoginInfo(null);
    this.httpClient.post(MojConfig.adresa_servera + "/Autentifikacija/LogOut/", null, token)
      .subscribe((x: any) => {
      });
  }
}
