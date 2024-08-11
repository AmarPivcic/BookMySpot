import { Component, OnInit } from '@angular/core';
import { AutentifikacijaHelper } from '../_helpers/autentifikacija-helper';
import { MojConfig } from '../moj-config';
import { HttpClient } from "@angular/common/http";
import {Router} from "@angular/router";
import { LoginInformacije } from '../_helpers/login-informacije';
import { HeaderComponent } from '../shared/header.component';

@Component({
  selector: 'app-moj-racun',
  templateUrl: './moj-racun.component.html',
  styleUrl: './moj-racun.component.css'
})
export class MojRacunComponent implements OnInit{

  racunPodaci: any;
  defaultSlika: any = "../../assets/user.png";

  constructor(private httpKlijent: HttpClient, private router: Router, private menu: HeaderComponent) {
  }

  loginInfo(): LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }

  ngOnInit(): void {
    // @ts-ignore
    if (this.loginInfo().autentifikacijaToken.korisnickiNalog.isKorisnik) {
      this.getKorisnik();
    }
  }

  getImagePath(): string {
    return this.racunPodaci?.slika ? this.racunPodaci.slika : this.defaultSlika;
  }

  getKorisnik():void{
    fetch(MojConfig.adresa_servera + "/Korisnik/Get?id="+this.loginInfo().autentifikacijaToken?.korisnickiNalog.osobaID).
    then(
      r=> {
        if(r.status!=200)
        {
          if(r.status==400)
            alert("Nepoznat korisnik!")
          else
            alert("Greška " + r.status);
          return;
        }
        r.json().then(x=>{
          this.racunPodaci=x;
        })
      }
    )
  }

Odjava() {
  let token = MojConfig.http_opcije();
    AutentifikacijaHelper.setLoginInfo(null);
    this.httpKlijent.post(MojConfig.adresa_servera + "/Autentifikacija/LogOut/", null, token)
      .subscribe((x: any) => {
      });
    this.menu.NavigirajIZatvori("/");
}

}
