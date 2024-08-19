import {Component, OnInit} from '@angular/core';
import {FormsModule} from "@angular/forms";
import {Router, RouterLink} from "@angular/router";
import {HttpClient} from "@angular/common/http";
import {LoginInformacije} from "../_helpers/login-informacije";
import {AutentifikacijaHelper} from "../_helpers/autentifikacija-helper";
import {MojConfig} from "../moj-config";
import {IzmjenaLozinke} from "../models/izmjenaLozinke.model";

@Component({
  selector: 'app-edit-lozinka',
  standalone: true,
  imports: [
    FormsModule,
    RouterLink
  ],
  templateUrl: './edit-lozinka.component.html',
  styleUrl: './edit-lozinka.component.css'
})
export class EditLozinkaComponent implements OnInit{
  url = MojConfig.adresa_servera;
  izmjenaLozinke: IzmjenaLozinke;
  constructor(private httpClient: HttpClient, private router: Router) {
    this.izmjenaLozinke = {
      korisnickoIme: "",
      staraLozinka: "",
      novaLozinka: ""
    };
  }
  ngOnInit(): void {
    const loginInfo = this.loginInfo();
    this.izmjenaLozinke.korisnickoIme = loginInfo?.autentifikacijaToken?.korisnickiNalog?.korisnickoIme || "";
  }

  loginInfo() {
    return AutentifikacijaHelper.getLoginInfo();
  }

  SpremiNovuLozinku() {
    if (this.izmjenaLozinke.novaLozinka !== (document.getElementById('lnameInput') as HTMLInputElement).value) {
      alert('Nova lozinka i ponovljena lozinka se ne podudaraju.');
      return;
    }

    // Pošalji zahtev na server
    this.httpClient.put(this.url + '/Korisnik/EditLozinkuZaKorisnickiNalog', this.izmjenaLozinke).subscribe({
      next: (response) => {
        console.log("Sve okej!");
        this.router.navigate(['/mojRacun']);
      },
      error: (error) => {
        alert("Pogrešna stara lozinka.");
      },
    });
  }
}
