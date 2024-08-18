import {Component, Input, OnInit} from '@angular/core';
import {DatePipe, NgClass, NgForOf, NgIf} from "@angular/common";
import {LoginInformacije} from "../_helpers/login-informacije";
import {OdgovorenoPitanje} from "../models/odgovorenoPitanje.model";
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../moj-config";
import {formatDistanceToNow} from "date-fns";
import {NovoPitanje} from "../models/novoPitanje.model";
import {AutentifikacijaHelper} from "../_helpers/autentifikacija-helper";
import {FormsModule} from "@angular/forms";
import {RouterLink} from "@angular/router";

@Component({
  selector: 'app-pitanja-odgovori-lista',
  standalone: true,
  templateUrl: './pitanja-odgovori-lista.component.html',
  imports: [
    NgForOf,
    NgIf,
    NgClass,
    DatePipe,
    FormsModule,
    RouterLink
  ],
  styleUrl: './pitanja-odgovori-lista.component.css'
})
export class PitanjaOdgovoriListaComponent implements OnInit{

  faqItems : OdgovorenoPitanje[] = [];
  url = MojConfig.adresa_servera;
  pitanjeZaBazu: NovoPitanje;
  novoPitanje = "";
  brojPristiglihPitanja = 0;

  constructor(private httpClient: HttpClient) {
    this.pitanjeZaBazu = {
      korisnickiNalogId: 0,
      pitanje: ''
    };
  }

  toggle(item: any) {
    item.isExpanded = !item.isExpanded;
  }

  expandAll() {
    this.faqItems.forEach(item => item.isExpanded = true);
  }

  unexpandAll() {
    this.faqItems.forEach(item => item.isExpanded = false);
  }

  loginInfo(): LoginInformacije{
    return AutentifikacijaHelper.getLoginInfo();
  }

  ngOnInit(): void {
    this.getBrojNeodgovorenihPitanja();

    this.httpClient.get<OdgovorenoPitanje[]>(this.url + '/PitanjeOdgovor/VratiOdgovorenaPitanja').subscribe({
      next: (response => {
        this.faqItems = response.map(item => ({
          ...item,
          relativeTime: this.getRelativeTime(item.datumKreiranja)
        }));
        console.log("Uspješno dobavljena pitanja i odgovori!");
      }),
      error: (error => {
        console.log("Neuspješno dobavljena pitanja i odgovori!", error);
      })
    });
  }

  getRelativeTime(dateString: Date): string {
    const date = new Date(dateString);
    return formatDistanceToNow(date, { addSuffix: true });
  }

  PostaviPitanje() {
    const osobaID = this.loginInfo().autentifikacijaToken?.korisnickiNalog.osobaID ?? 0;
    console.log('osobaID:', osobaID);

    this.pitanjeZaBazu = {
      korisnickiNalogId: osobaID,
      pitanje: this.novoPitanje
    };

    this.httpClient.post(this.url + '/PitanjeOdgovor/PostaviPitanje', this.pitanjeZaBazu).subscribe({
      next: (response) => {
        console.log("Uspješno poslano pitanje!");
        this.novoPitanje = "";
        this.ngOnInit();
      },
      error: (error) => {
        console.log("Neuspješno poslano pitanje!", error);
      }
    });
  }

  getBrojNeodgovorenihPitanja(){
    this.httpClient.get<number>(this.url + '/PitanjeOdgovor/VratiBrojNeodgovorenihPitanja').subscribe({
      next: (response) => {
        this.brojPristiglihPitanja = response;
      },
      error: (error) => {
        console.log("Nije dobavljen broj!");
      }
    })
  }

  Pretraga($event: Event) {
    this.httpClient.get<OdgovorenoPitanje[]>(this.url + `/PitanjeOdgovor/VratiOdgovorenaPitanja?filter=${($event.target as HTMLInputElement).value}`).subscribe({
      next: (response => {
        this.faqItems = response.map(item => ({
          ...item,
          relativeTime: this.getRelativeTime(item.datumKreiranja)
        }));
        console.log("Uspješno dobavljena pitanja i odgovori!");
      }),
      error: (error => {
        console.log("Neuspješno dobavljena pitanja i odgovori!", error);
      })
    })
  }
}
