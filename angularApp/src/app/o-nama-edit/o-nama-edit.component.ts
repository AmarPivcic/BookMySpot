import { Component } from '@angular/core';
import {FormsModule} from "@angular/forms";
import {MarkdownComponent} from "ngx-markdown";
import {RouterLink} from "@angular/router";

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
export class ONamaEditComponent {
  tekst = "";
}
