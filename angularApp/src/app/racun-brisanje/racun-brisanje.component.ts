import { Component } from '@angular/core';
import {ReactiveFormsModule} from "@angular/forms";
import {RouterLink} from "@angular/router";

@Component({
  selector: 'app-racun-brisanje',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    RouterLink
  ],
  templateUrl: './racun-brisanje.component.html',
  styleUrl: './racun-brisanje.component.css'
})
export class RacunBrisanjeComponent {
  showOldPassword = false;
}
