import { Component } from '@angular/core';
import {NgForOf, NgIf} from "@angular/common";
import {PostojeciRacun} from "../models/postojeciRacun.model";
import {MojConfig} from "../moj-config";
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-upravljaj-banovanim-racunima',
  standalone: true,
  imports: [
    NgForOf,
    NgIf
  ],
  templateUrl: './upravljaj-banovanim-racunima.component.html',
  styleUrl: './upravljaj-banovanim-racunima.component.css'
})
export class UpravljajBanovanimRacunimaComponent {
  nizBanovanihRacuna: PostojeciRacun[] | null = [];
  totalRecords: number = 0;
  pageNumber: number = 1;
  pageSize: number = 2;
  url = MojConfig.adresa_servera;

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
        suspendovan: true
      }
    }).subscribe({
      next: (response) => {
        this.nizBanovanihRacuna = response.data;
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
  AktivirajKorisnika(osobaID: number): void {
    this.httpClient.put(`${this.url}/Korisnik/AktivirajSuspendovanRacun/${osobaID}`, {}).subscribe({
      next: () => {
        console.log(`Korisnik sa ID ${osobaID} je uspješno aktiviran.`);
        this.loadAccounts();
      },
      error: (error) => {
        console.error('Greška pri brisanju korisnika!', error);
      }
    });
  }
}
