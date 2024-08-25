import {Component, OnInit} from '@angular/core';
import {PostojeciRacun} from "../models/postojeciRacun.model";
import {MojConfig} from "../moj-config";
import {HttpClient} from "@angular/common/http";
import {NgForOf, NgIf} from "@angular/common";

@Component({
  selector: 'app-upravljaj-obrisanim-racunima',
  standalone: true,
  imports: [
    NgForOf,
    NgIf
  ],
  templateUrl: './upravljaj-obrisanim-racunima.component.html',
  styleUrl: './upravljaj-obrisanim-racunima.component.css'
})
export class UpravljajObrisanimRacunimaComponent implements OnInit{
  nizObrisanihhRacuna: PostojeciRacun[] | null = [];
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
        obrisan: true,
        suspendovan: false
      }
    }).subscribe({
      next: (response) => {
        this.nizObrisanihhRacuna = response.data;
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
    this.httpClient.put(`${this.url}/Korisnik/AktivirajObrisanRacun/${osobaID}`, {}).subscribe({
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
