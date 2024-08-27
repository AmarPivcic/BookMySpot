import {Component, OnInit} from '@angular/core';
import {MarkdownComponent} from "ngx-markdown";
import {FormsModule} from "@angular/forms";
import {RouterLink} from "@angular/router";
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../moj-config";
import {ONamaSadrzaj} from "../models/ONamaSadrzaj.model";
import {NgIf} from "@angular/common";
import {LoginInformacije} from "../_helpers/login-informacije";
import {AutentifikacijaHelper} from "../_helpers/autentifikacija-helper";

@Component({
  selector: 'app-o-nama',
  standalone: true,
  imports: [
    MarkdownComponent,
    FormsModule,
    RouterLink,
    NgIf
  ],
  templateUrl: './o-nama.component.html',
  styleUrl: './o-nama.component.css'
})
export class ONamaComponent implements OnInit{
  tekst:  string | null;
  url = MojConfig.adresa_servera;

  constructor(private httpClient: HttpClient) {
    this.tekst = "";
  }

  ngOnInit(): void {
    this.loadData();
  }

  loadData(){
    this.httpClient.get<ONamaSadrzaj>(this.url + '/api/ONama').subscribe({
      next: (response) => {
        this.tekst = response.tekst;
      },
      error: (error) => {
        console.log("Neuspjesno dobavljen tekst", error);
      }
    })
  }

  loginInfo():LoginInformacije
  {
    return AutentifikacijaHelper.getLoginInfo();
  }
}
