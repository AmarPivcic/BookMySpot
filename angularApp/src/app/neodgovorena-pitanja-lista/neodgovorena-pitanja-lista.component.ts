import {Component, OnInit} from '@angular/core';
import {FormsModule} from "@angular/forms";
import {NgClass, NgForOf, NgIf} from "@angular/common";
import {OdgovorenoPitanje} from "../models/odgovorenoPitanje.model";
import {MojConfig} from "../moj-config";
import {HttpClient} from "@angular/common/http";
import {formatDistanceToNow} from "date-fns";
import {RouterLink} from "@angular/router";
import {LoginInformacije} from "../_helpers/login-informacije";
import {AutentifikacijaHelper} from "../_helpers/autentifikacija-helper";
import {HeaderComponent} from "../shared/header/header.component";

@Component({
  selector: 'app-neodgovorena-pitanja-lista',
  standalone: true,
  imports: [
    FormsModule,
    NgForOf,
    NgIf,
    RouterLink,
    NgClass
  ],
  templateUrl: './neodgovorena-pitanja-lista.component.html',
  styleUrl: './neodgovorena-pitanja-lista.component.css'
})
export class NeodgovorenaPitanjaListaComponent implements OnInit{

  faqItems : OdgovorenoPitanje[] = [];
  url = MojConfig.adresa_servera;
  brojPristiglihPitanja = 0;
  odgovorNaPitanje = "";

  constructor(private httpClient: HttpClient, private headerMenu: HeaderComponent) {}
  toggle(item: any) {
    item.isExpanded = !item.isExpanded;
  }

  expandAll() {
    this.faqItems.forEach(item => item.isExpanded = true);
  }

  unexpandAll() {
    this.faqItems.forEach(item => item.isExpanded = false);
  }
  ngOnInit(): void {
    this.headerMenu.zatvoriSlajder();
    this.getBrojNeodgovorenihPitanja();
    this.loadNeodgovorenaPitanja();
  }

  loadNeodgovorenaPitanja() {
    this.httpClient.get<OdgovorenoPitanje[]>(this.url + '/PitanjeOdgovor/VratiNeodgovorenaPitanja').subscribe({
      next: (response) => {
        this.faqItems = response.map(item => ({
          ...item,
          relativeTime: this.getRelativeTime(item.datumKreiranja)
        }));
        console.log("Uspješno dobavljena pitanja i odgovori!");
      },
      error: (error) => {
        console.error("Neuspješno dobavljena pitanja i odgovori!", error);
      }
    });
    this.getBrojNeodgovorenihPitanja();
  }
  getRelativeTime(dateString: Date): string {
    const date = new Date(dateString);
    return formatDistanceToNow(date, { addSuffix: true });
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
  PosaljiOdgovor(id: string, odgovor: string) {
    if (!odgovor) {
      alert("Odgovor ne može biti prazan!");
      return;
    }
    const body = { odgovor };

    this.httpClient.put(`${this.url}/PitanjeOdgovor/PostaviOdgovor/${id}`, body, {
      headers: { 'Content-Type': 'application/json' }
    }).subscribe({
      next: (response) => {
        console.log("Uspjesno poslano u bazu!");
        this.loadNeodgovorenaPitanja();
      },
      error: (error) => {
        console.error("Neuspjesno poslano u bazu!", error);
      }
    });
  }

  ObrisiPitanje(id: string) {
    this.httpClient.delete(this.url + `/PitanjeOdgovor/ObrisiPitanjeOdgovor/${id}`).subscribe({
      next: (response) => {
        console.log("Uspjesno obrisano");
        this.loadNeodgovorenaPitanja();
      },
      error: (error) => {
        console.log("Neuspjesno obrisano", error);
      }
    });
  }

  loginInfo(): LoginInformacije{
    return AutentifikacijaHelper.getLoginInfo();
  }
}

