import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MojConfig } from '../moj-config';
import { Kategorija } from '../models/kategorija.model';
import {LoginInformacije} from "../_helpers/login-informacije";
import {AutentifikacijaHelper} from "../_helpers/autentifikacija-helper";
import { HeaderComponent } from '../shared/header/header.component';



@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit{


  kategorijeLista: Kategorija[] | null=null;
  imeKategorije: any;
  selectedFile?: File;

  constructor(private httpKlijent: HttpClient, private router: Router, private menu: HeaderComponent) {
  }

  ngOnInit(): void {
    this.GetKategorije();
  }

  GetKategorije() {
    this.httpKlijent.get<Kategorija[]>(MojConfig.adresa_servera+ "/Kategorija/GetListaKategorija", MojConfig.http_opcije()).subscribe(x=>{
      this.kategorijeLista = x;
    })
  }

  onCategoryClick(kategorijaID: number)
  {
    const burgerMenu = document.querySelector(".burger-menu");
    const slider = document.querySelector("#slider");
    // @ts-ignore
    burgerMenu.style.left = "-280px";
    // @ts-ignore
    slider.style.width="0px"
    this.router.navigate(['/kategorija', kategorijaID]);
    window.scrollTo(0,0);
  }

  loginInfo():LoginInformacije
  {
    return AutentifikacijaHelper.getLoginInfo();
  }

  novaKategorija()
  {
    this.menu.NavigirajIZatvori("/novaKategorija")
  }

}
