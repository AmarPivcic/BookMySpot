import {Component, OnInit} from '@angular/core';
import {FormsModule} from "@angular/forms";
import {MarkdownComponent} from "ngx-markdown";
import {Router, RouterLink} from "@angular/router";
import {ONamaSadrzaj} from "../models/ONamaSadrzaj.model";
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../moj-config";
import {ONamaComponent} from "../o-nama/o-nama.component";

@Component({
  selector: 'app-o-nama-edit',
  standalone: true,
  imports: [
    FormsModule,
    MarkdownComponent,
    RouterLink
  ],
  templateUrl: './o-nama-edit.component.html',
  styleUrl: './o-nama-edit.component.css'
})
export class ONamaEditComponent implements OnInit{
  tekst: string | null;
  url = MojConfig.adresa_servera;
  constructor(private httpClient: HttpClient, private router: Router) {
    this.tekst = "";
  }

  ngOnInit(): void {
    this.httpClient.get<ONamaSadrzaj>(this.url + '/api/ONama').subscribe({
      next: (response) => {
        this.tekst = response.tekst;
      },
      error: (error) => {
        console.log("Neuspjesno dobavljen tekst", error);
      }
    })
  }

  SpasiPromjene() {
    if (this.tekst !== null) {
      const payload = { Tekst: this.tekst };

      this.httpClient.post<ONamaSadrzaj>(`${this.url}/api/ONama`, payload, {
        headers: {
          'Content-Type': 'application/json'
        }
      }).subscribe({
        next: (response) => {
          this.router.navigate(['/oNama']);
        },
        error: (error) => {
          console.log("Neuspješno poslano", error);
        }
      });
    }
  }
}
