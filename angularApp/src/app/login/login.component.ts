import { Component, OnInit} from '@angular/core';
import { HttpClient } from "@angular/common/http";
import {Router} from "@angular/router";
import { LoginInformacije } from '../_helpers/login-informacije';
import { MojConfig } from '../moj-config';
import { AutentifikacijaHelper } from '../_helpers/autentifikacija-helper';
import { HeaderComponent } from '../shared/header/header.component';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent implements OnInit{

  txtKorisnickoIme: any;
  txtLozinka: any;
  txtEmail: any;
  txtKorisnickoImeRegister: any;
  txtPrezime: any;
  txtIme: any;
  txtLozinkaRegister: any;
  constructor(private httpKlijent: HttpClient, private router: Router, private menu: HeaderComponent) {
  }

  ngOnInit(): void {
  }

  prijava() {
    let saljemo: any = {
      korisnickoIme: this.txtKorisnickoIme,
      lozinka: this.txtLozinka
    };

    if(this.txtKorisnickoIme && this.txtLozinka)
    {
      this.httpKlijent.post<LoginInformacije>(MojConfig.adresa_servera + "/Autentifikacija/Login", saljemo)
      .subscribe({
        next: (x: LoginInformacije) => {
          if (x.isLogiran) {
            AutentifikacijaHelper.setLoginInfo(x);
            this.menu.NavigirajIZatvori("/");
          } else {
            AutentifikacijaHelper.setLoginInfo(null);
            alert("Neispravan login!");
          }
        },
        error: (error) => {
          if (error.status === 410) {
            alert("Korisnički nalog je obrisan.");
          } else if (error.status === 403) {
            alert(error.error);
          } else {
            alert("Došlo je do greške. Pokušajte ponovo.");
          }
        }
      });
    }

    else
    {
      alert("Morate unijeti korisničko ime i lozinku!");
    }

  }


  registracija() {
    let saljemo = {
      korisnickoIme: this.txtKorisnickoImeRegister,
      ime: this.txtIme,
      prezime: this.txtPrezime,
      email: this.txtEmail,
      lozinka: this.txtLozinkaRegister
    };

    if(this.txtKorisnickoImeRegister && this.txtIme && this.txtPrezime && this.txtEmail && this.txtLozinkaRegister)
    {
      this.httpKlijent.post<LoginInformacije>(MojConfig.adresa_servera+ "/Autentifikacija/Registracija", saljemo).subscribe({
        next: (x: LoginInformacije) => {
          AutentifikacijaHelper.setLoginInfo(x);
          this.menu.NavigirajIZatvori("/");
        },

        error: (error) => {
          alert(error.error);
        }
    })
    }
    
    else
    {
      alert("Morate unijeti sve tražene podatke!");
    }
  }

  switchTab(tabId: string, event: Event): void {
    event.preventDefault();

    // Remove 'active' class from all tabs
    const tabs = document.querySelectorAll('.tabs-content > div');
    tabs.forEach(tab => {
      tab.classList.remove('active');
    });

    // Add 'active' class to the selected tab
    const selectedTab = document.getElementById(tabId);
    if (selectedTab) {
      selectedTab.classList.add('active');
    }

    // Update the active class on the tab links
    const tabLinks = document.querySelectorAll('.tabs h3 a');
    tabLinks.forEach(link => {
      link.classList.remove('active');
    });

    const clickedLink = event.target as HTMLElement;
    clickedLink.classList.add('active');
  }


}
