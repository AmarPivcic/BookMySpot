import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router} from '@angular/router';
import { MojConfig } from '../moj-config';
import { UsluzniObjekt } from '../models/usluzniObjekt.model';
import { Grad } from '../models/grad.model';

@Component({
  selector: 'app-kategorija',
  templateUrl: './kategorija.component.html',
  styleUrl: './kategorija.component.css'
})
export class KategorijaComponent implements OnInit {

  kategorijaID: number | null = null;
  listaUsluzniObjekt: UsluzniObjekt[] | null = null;


constructor(private httpKlijent: HttpClient, private route: ActivatedRoute, private router: Router) {}

  ngOnInit(): void {
    this.kategorijaID=Number(this.route.snapshot.paramMap.get('id'));
    this.getListaUsluzniObjekt();
  }

  getStars(): number[] {
    const totalStars = 5;
    return Array(totalStars).fill(0);
  }

  filledStars(prosjecnaOcjena: number): number {
    return Math.round(prosjecnaOcjena);
  }

  getListaUsluzniObjekt()
  {
    this.httpKlijent.get<UsluzniObjekt[]>(MojConfig.adresa_servera + "/UsluzniObjekt/GetByKategorijaID?id="+this.kategorijaID,MojConfig.http_opcije()).subscribe(x=>{
      this.listaUsluzniObjekt = x;
    })
  }

  onUsluzniObjektClick(usluzniObjektID: number) {
    const burgerMenu = document.querySelector(".burger-menu");
      const slider = document.querySelector("#slider");
      // @ts-ignore
      burgerMenu.style.left = "-280px";
      // @ts-ignore
      slider.style.width="0px"
      this.router.navigate(['/usluzniObjekt', usluzniObjektID]);
      window.scrollTo(0,0);
  }

}
