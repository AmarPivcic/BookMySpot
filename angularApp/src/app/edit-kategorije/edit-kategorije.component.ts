import {Component, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {ActivatedRoute, Router} from "@angular/router";
import {Kategorija} from "../models/kategorija.model";
import {MojConfig} from "../moj-config";
import {FormsModule} from "@angular/forms";
import {HeaderComponent} from "../shared/header/header.component";

@Component({
  selector: 'app-edit-kategorije',
  standalone: true,
  imports: [
    FormsModule
  ],
  templateUrl: './edit-kategorije.component.html',
  styleUrl: './edit-kategorije.component.css'
})
export class EditKategorijeComponent implements OnInit{
  kategorijaID: Number | null = 0;
  kategorija: Kategorija | null = null;
  url = MojConfig.adresa_servera;
  selectedFile?: File;
  constructor(private httpClient: HttpClient, private route: ActivatedRoute, private router: Router, private headerMenu: HeaderComponent) {
    this.kategorija = {
      kategorijaID: 0,
      naziv: "",
      slika: "",
      brojOpcija: 0
    };
  }
  ngOnInit(): void {
    this.kategorijaID=Number(this.route.snapshot.paramMap.get('kategorijaId'));
    this.ucitajKategoriju();
    this.headerMenu.zatvoriSlajder();
  }

  ucitajKategoriju(){
    this.httpClient.get<Kategorija>(this.url + '/Kategorija/Get?id=' + this.kategorijaID).subscribe({
      next: (response) => {
        this.kategorija = response;
      },
      error: (error) => {
        console.log("Neuspjesno dobavljeni podaci!", error);
      }
    })
  }
  onFileUploadChange(event: any) {
    this.selectedFile = event.target.files[0];
  }

  onSave() {
    if (this.kategorija) {
      const formData = new FormData();
      formData.append('naziv', this.kategorija.naziv);
      formData.append('slika', this.selectedFile ?? '');

      this.httpClient.put(this.url + '/Kategorija/EditKategorija/' + this.kategorijaID, formData).subscribe({
        next: (response) => {
          this.router.navigate(['']);
        },
        error: (error) => {
          console.error('Greška pri ažuriranju kategorije', error);
        }
      });
    }
  }

  Odustani() {
    this.router.navigate(['']);
  }
}
