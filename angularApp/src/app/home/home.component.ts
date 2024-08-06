import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MojConfig } from '../moj-config';
import { Kategorija } from '../models/kategorija.model';



@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit{


  kategorijeLista: Kategorija[] | null=null;
  imeKategorije: any;
  selectedFile?: File;

  constructor(private httpKlijent: HttpClient, private router: Router) {
  }

  ngOnInit(): void {
    this.GetKategorije();
  }

  GetKategorije() {
    this.httpKlijent.get<Kategorija[]>(MojConfig.adresa_servera+ "/Kategorija/GetListaKategorija", MojConfig.http_opcije()).subscribe(x=>{
      this.kategorijeLista = x;
    })
  }

  onFileUploadChange(event: any) {
    this.selectedFile = event.target.files[0];
  }
  
  onSubmit() {
    if (this.selectedFile && this.imeKategorije !== '') {

      const formData = new FormData();
      formData.append('naziv', this.imeKategorije);
      formData.append('slika', this.selectedFile);

      this.httpKlijent.post(MojConfig.adresa_servera + '/Kategorija/Add', formData).subscribe({
        next: (response) => {
          console.log('Kategorija dodana uspješno', response);
          this.GetKategorije();
        },
        error: (error) => {
          console.error('Error pri dodavanju kategorije', error);
        }
      });
    } else {
      console.error('Please provide all required fields: file, imageName, and imageDescription');
    }
  }

}