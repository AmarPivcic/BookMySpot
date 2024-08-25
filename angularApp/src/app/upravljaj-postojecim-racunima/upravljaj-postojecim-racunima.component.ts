import {Component, OnInit} from '@angular/core';
import {PostojeciRacun} from "../models/postojeciRacun.model";
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../moj-config";
import {NgForOf, NgIf, NgOptimizedImage} from "@angular/common";
import {FormsModule} from "@angular/forms";

@Component({
  selector: 'app-upravljaj-postojecim-racunima',
  standalone: true,
  imports: [
    NgIf,
    NgForOf,
    NgOptimizedImage,
    FormsModule
  ],
  templateUrl: './upravljaj-postojecim-racunima.component.html',
  styleUrl: './upravljaj-postojecim-racunima.component.css'
})
export class UpravljajPostojecimRacunimaComponent implements OnInit{
  nizPostojecihRacuna: PostojeciRacun[] | null = [];
  totalRecords: number = 0;
  pageNumber: number = 1;
  pageSize: number = 2;
  url = MojConfig.adresa_servera;
  brojDana: number | null = 1;
  selectedUserId: number | null = 0;
  razlogSuspenzije: string | null = "";

  constructor(private httpClient: HttpClient) { }

  ngOnInit(): void {
    this.loadAccounts();
  }

  loadAccounts(): void {
    this.httpClient.get<any>(`${this.url}/Korisnik/GetKorisnickeRacune`, {
      params: {
        pageNumber: this.pageNumber.toString(),
        pageSize: this.pageSize.toString(),
        obrisan: false,
        suspendovan: false
      }
    }).subscribe({
      next: (response) => {
        this.nizPostojecihRacuna = response.data;
        this.totalRecords = response.totalRecords;
      },
      error: (error) => {
        console.log("Podaci nisu dobavljeni!");
      }
    });
  }

  onPageChange(pageNumber: number): void {
    this.pageNumber = pageNumber;
    this.loadAccounts();
  }

  protected readonly Math = Math;

  ObrisiKorisnika(osobaID: number): void {
    this.httpClient.put(`${this.url}/Korisnik/ObrisiKorisnickiRacunAdmin/${osobaID}`, {}).subscribe({
      next: () => {
        console.log(`Korisnik sa ID ${osobaID} je uspješno obrisan.`);
        this.loadAccounts();
      },
      error: (error) => {
        console.error('Greška pri brisanju korisnika!', error);
      }
    });
  }

  suspendujKorisnika(): void {
    if (this.selectedUserId !== null && this.brojDana !== null && this.razlogSuspenzije) {
      const url = `${this.url}/Korisnik/SuspendujKorisnickiRacun/${this.selectedUserId}`;
      const params = new URLSearchParams({
        brojDana: this.brojDana.toString(),
        razlogSuspenzije: this.razlogSuspenzije
      }).toString();

      this.httpClient.put(`${url}?${params}`, {})
        .subscribe({
          next: () => {
            this.closePopup();
            this.loadAccounts();
          },
          error: (error) => {
            console.error('Došlo je do greške prilikom suspendovanja korisnika.', error);
          }
        });
    } else {
      alert('Molimo unesite sve potrebne podatke.');
    }
  }
  otvoritePopup(userId: number): void {
    this.selectedUserId = userId;
    const popup = document.getElementById('suspendPopup');
    if (popup) {
      popup.style.display = 'flex';
    }
  }
  closePopup(): void {
    const popup = document.getElementById('suspendPopup');
    if (popup) {
      popup.style.display = 'none';
    }
    this.brojDana = null;
  }

  odustani(): void {
    this.closePopup();
  }
}
