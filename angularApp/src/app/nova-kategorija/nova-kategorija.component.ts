import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { MojConfig } from '../moj-config';


@Component({
  selector: 'app-nova-kategorija',
  templateUrl: './nova-kategorija.component.html',
  styleUrl: './nova-kategorija.component.css'
})
export class NovaKategorijaComponent {

imeKategorije: any;
selectedFile?: File;

  constructor(private httpKlijent: HttpClient) {
  }

  triggerFileInput() {
    const fileInput = document.querySelector('#fileInput') as HTMLInputElement;
    if (fileInput) {
      fileInput.click();
    }
  }

  onFileUploadChange(event: any) {
    this.selectedFile = event.target.files[0];
  }

  onSubmit() {
    if (this.selectedFile && this.imeKategorije) {

      const formData = new FormData();
      formData.append('naziv', this.imeKategorije);
      formData.append('slika', this.selectedFile);

      this.httpKlijent.post(MojConfig.adresa_servera + '/Kategorija/Add', formData).subscribe({
        next: (response) => {
          alert('Kategorija dodana uspješno');
          this.selectedFile=undefined;
          this.imeKategorije=null;
        },
        error: (error) => {
          alert('Error pri dodavanju kategorije');
          console.log(error);
        }
      });
    } else {
      alert('Morate unijeti ime kategorije i dodati sliku kategorije!');
    }
  }

}
