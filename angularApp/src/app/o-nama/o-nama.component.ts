import { Component } from '@angular/core';
import {MarkdownComponent} from "ngx-markdown";
import {FormsModule} from "@angular/forms";
import {RouterLink} from "@angular/router";

@Component({
  selector: 'app-o-nama',
  standalone: true,
  imports: [
    MarkdownComponent,
    FormsModule,
    RouterLink
  ],
  templateUrl: './o-nama.component.html',
  styleUrl: './o-nama.component.css'
})
export class ONamaComponent {
  tekst = "";
}