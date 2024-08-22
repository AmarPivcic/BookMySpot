import { Component, Injectable, OnInit } from '@angular/core';
import { AutentifikacijaHelper } from '../../_helpers/autentifikacija-helper';
import { LoginInformacije } from '../../_helpers/login-informacije';
import { Router } from '@angular/router';

@Component({
  selector: 'headerMenu',
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})

@Injectable({
  providedIn: 'root'
})
export class HeaderComponent implements OnInit { 

  constructor(private router: Router){}

  ngOnInit(): void {
    
  }

  loginInfo():LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }
  
  OtvoriMenu() {
    const burgerMenu = document.querySelector(".burger-menu");
    // @ts-ignore
    burgerMenu.style.left = "0px";
  }

  ZatvoriMenu() {
    const burgerMenu = document.querySelector(".burger-menu");
    // @ts-ignore
    burgerMenu.style.left = "-280px";
  }

  NavigirajIZatvori(route: string) {
    const burgerMenu = document.querySelector(".burger-menu");
    const slider = document.querySelector("#slider");
    // @ts-ignore
    burgerMenu.style.left = "-280px";
    // @ts-ignore
    slider.style.width="0px"
    this.router.navigate([route]);
    window.scrollTo(0,0);
  }
}

window.onscroll = () => {
  sliderScroll();
};
function sliderScroll() {
  var pocetna = document.documentElement.scrollTop;
  var visinaGl =
    document.documentElement.scrollHeight -
    document.documentElement.clientHeight;
  var skrolano = (pocetna / visinaGl) * 100;
  // @ts-ignore
  document.getElementById("slider").style.width = skrolano + "%";
}
