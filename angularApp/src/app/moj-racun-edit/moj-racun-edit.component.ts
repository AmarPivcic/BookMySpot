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

export class MojRacunEditComponent implements OnInit {
  korisnikInformacije: KorisnikInformacije;
  url = MojConfig.adresa_servera;
  selectedFile?: File;

  constructor(private httpClient: HttpClient, private router: Router) {
    this.korisnikInformacije = {
      ime: "",
      prezime: "",
      email: "",
      telefon: "",
      korisnickoIme: "",
      slika: null
    };
  }

  ngOnInit(): void {
    this.httpClient.get<KorisnikInformacije>(this.url + "/Korisnik/Get?id=" + this.loginInfo().autentifikacijaToken?.korisnickiNalog.osobaID).subscribe({
      next: (response) => {
        this.korisnikInformacije = response;
        console.log("Uspjesno dobavljen korisnik!");
      },
      error: (error) => {
        console.log("Nije dobavljen korisnik!", error);
      }
    });
  }

  loginInfo(): LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }

  SpasiPromjene() {
    const formData = new FormData();
    formData.append('ime', this.korisnikInformacije.ime);
    formData.append('prezime', this.korisnikInformacije.prezime);
    formData.append('email', this.korisnikInformacije.email);
    formData.append('telefon', this.korisnikInformacije.telefon);
    formData.append('korisnickoIme', this.korisnikInformacije.korisnickoIme);

    if (this.selectedFile) {
      formData.append('slika', this.selectedFile);
    }

    this.httpClient.put(this.url + "/Korisnik/EditKorisnickiRacun/" + this.loginInfo().autentifikacijaToken?.korisnickiNalog.osobaID, formData).subscribe({
      next: (response) => {
        console.log("Uspjesno editovan korisnik!");
        this.router.navigate(['/mojRacun']);
      },
      error: (error) => {
        console.log("Neuspjesno editovan korisnik!", error);
      }
    });
  }

  getImagePath() {
    return this.korisnikInformacije?.slika ? this.korisnikInformacije.slika : "../../assets/user.png";
  }

  onFileUploadChange(event: any) {
    this.selectedFile = event.target.files[0];
  }
}
