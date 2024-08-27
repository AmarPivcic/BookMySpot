import {Component, OnInit} from '@angular/core';
import {FormsModule} from "@angular/forms";
import {DatePipe, NgForOf, NgIf} from "@angular/common";
import {RouterLink} from "@angular/router";
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../moj-config";
import {SvaPitanjaAdmin} from "../models/svaPitanjaAdmin.model";
import {ParPitanjeOdgovor} from "../models/parPitanjeOdgovor.model";
import {LoginInformacije} from "../_helpers/login-informacije";
import {AutentifikacijaHelper} from "../_helpers/autentifikacija-helper";
import {HeaderComponent} from "../shared/header/header.component";

@Component({
  selector: 'app-pitanja-admin-lista',
  standalone: true,
  imports: [
    FormsModule,
    NgForOf,
    NgIf,
    RouterLink,
    DatePipe
  ],
  templateUrl: './pitanja-admin-lista.component.html',
  styleUrl: './pitanja-admin-lista.component.css'
})
export class PitanjaAdminListaComponent implements OnInit{
  pitanja: SvaPitanjaAdmin[] = [];
  brojPristiglihPitanja = 0;
  url = MojConfig.adresa_servera;
  currentPage = 1;
  totalPages = 1;
  pageSize = 5;
  parPitanjeOdgovor: ParPitanjeOdgovor | null = null;
  isModalVisible = false;

  constructor(private httpClient: HttpClient, private headerMenu: HeaderComponent) {}

  ngOnInit(): void {
    this.headerMenu.zatvoriSlajder();
    this.getBrojNeodgovorenihPitanja();
    this.getPitanja(this.currentPage, this.pageSize);
  }

  getPitanja(pageNumber: number, pageSize: number) {
    this.httpClient.get<any>(`${this.url}/PitanjeOdgovor/VratiSvaPitanjaAdministrator?pageNumber=${pageNumber}&pageSize=${pageSize}`).subscribe({
      next: (response) => {
        this.pitanja = response.pitanja;
        this.totalPages = response.totalPages;
        console.log("Uspješno dobavljena pitanja!");
      },
      error: (error) => {
        console.error("Neuspješno dobavljena pitanja!", error);
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

  previousPage() {
    if (this.currentPage > 1) {
      this.currentPage--;
      this.getPitanja(this.currentPage, this.pageSize);
    }
  }

  nextPage() {
    if (this.currentPage < this.totalPages) {
      this.currentPage++;
      this.getPitanja(this.currentPage, this.pageSize);
    }
  }

  ObrisiPitanje(id: number) {
    this.httpClient.delete(this.url + `/PitanjeOdgovor/ObrisiPitanjeOdgovor/${id}`).subscribe({
      next: (response) => {
        console.log("Uspjesno obrisano!");
        this.ngOnInit();
      },
      error: (error) => {
        console.log("Neuspjesno obrisano!", error);
      }
    })
  }

  toggleModal() {
    this.isModalVisible = !this.isModalVisible;
  }

  editPitanje(id: number) {
    this.httpClient.get<ParPitanjeOdgovor>(this.url + `/PitanjeOdgovor/GetPitanjeById/${id}`).subscribe({
      next: (response) => {
        this.parPitanjeOdgovor = response;
        console.log("Uspjesno dobavljeno!", this.parPitanjeOdgovor);
        this.isModalVisible = true;
      },
      error: (error) => {
        console.log("Neuspjesno dobavljeno!");
      }
    })
  }

  UpdatePitanjeOdgovor(id: number | undefined, pitanje: string | undefined, odgovor: string | undefined) {
    var object = {
      pitanje: pitanje,
      odgovor: odgovor
    };
    this.httpClient.put(this.url + `/PitanjeOdgovor/UpdatePitanjeOdgovorById/${id}`, object).subscribe({
      next: (response) => {
        console.log("Uspjesno azurirano!");
        this.isModalVisible = false;
        this.ngOnInit();
      },
      error: (error) => {
        console.log("Neuspjesno azurirano!");
      }
    })
  }

  loginInfo(): LoginInformacije{
    return AutentifikacijaHelper.getLoginInfo();
  }
}

