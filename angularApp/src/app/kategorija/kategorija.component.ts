import { HttpClient } from '@angular/common/http';
import { AfterViewInit, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router} from '@angular/router';
import { MojConfig } from '../moj-config';
import { UsluzniObjekt } from '../models/usluzniObjekt.model';
import { Grad } from '../models/grad.model';
import { korisnickiNalog, LoginInformacije } from '../_helpers/login-informacije';
import { AutentifikacijaHelper } from '../_helpers/autentifikacija-helper';

@Component({
  selector: 'app-kategorija',
  templateUrl: './kategorija.component.html',
  styleUrl: './kategorija.component.css'
})
export class KategorijaComponent implements OnInit, AfterViewInit {

  kategorijaID: number | null = null;
  listaUsluzniObjekt: UsluzniObjekt[] | undefined = undefined;
  filtriranaListaUsluzniObjekt: UsluzniObjekt[] | undefined = undefined;
  logiraniKorisnik: any;
  odabraniGrad: Grad | null = null;
  listaGradova: Grad[] | null = null;
  favoriti: boolean = false;


  @ViewChild('listaStandard') listaStandard: any;
  @ViewChild('listaProsjek') listaProsjek: any;
  @ViewChild('listaFavorit') listaFavorit: any;

constructor(private httpKlijent: HttpClient, private route: ActivatedRoute, private router: Router) {}

  ngOnInit(): void {
    this.kategorijaID=Number(this.route.snapshot.paramMap.get('kategorijaId'));
    this.getListaGradova();
    this.getPodaci();
  }

  ngAfterViewInit(): void {
    this.getListaUsluzniObjekt();
  }

  loginInfo():LoginInformacije
  {
    return AutentifikacijaHelper.getLoginInfo();
  }

  getPodaci()
  {
    this.logiraniKorisnik=this.loginInfo().autentifikacijaToken?.korisnickiNalog;
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
    this.httpKlijent.get<UsluzniObjekt[]>(MojConfig.adresa_servera + "/UsluzniObjekt/GetByKategorija?kategorijaID="+this.kategorijaID,MojConfig.http_opcije()).subscribe(x=>{
        this.listaUsluzniObjekt = x;
        this.filtriranaListaUsluzniObjekt=x;
        this.favoriti=false;
      });

    this.odabraniGrad=null;
    this.listaStandard.nativeElement.style.fill = "#b0a695";
    this.listaProsjek.nativeElement.style.fill = "white";

    if(this.listaFavorit)
    {
      this.listaFavorit.nativeElement.style.fill="white";
    }
  }


  getListaProsjek()
  {
    this.httpKlijent.get<UsluzniObjekt[]>(MojConfig.adresa_servera + "/UsluzniObjekt/GetByProsjecnaOcjena?kategorijaID="+this.kategorijaID, MojConfig.http_opcije()).subscribe(x=>{
        this.listaUsluzniObjekt = x;
        this.filtriranaListaUsluzniObjekt=x;
        this.favoriti=false;
      });
    
    this.odabraniGrad=null;
    this.listaStandard.nativeElement.style.fill = "white";
    this.listaProsjek.nativeElement.style.fill = "#b0a695";

    if(this.listaFavorit)
    {
      this.listaFavorit.nativeElement.style.fill="white";
    }
  }

  getListaFavorit()
  {
    this.httpKlijent.get<UsluzniObjekt[]>(MojConfig.adresa_servera + "/Favorit/GetListaFavorita?korisnikID="+this.logiraniKorisnik.osobaID + "&kategorijaID="+this.kategorijaID, MojConfig.http_opcije()).subscribe(x=>{
      this.listaUsluzniObjekt = x;
      this.filtriranaListaUsluzniObjekt=x;
      this.favoriti = true;
    });

    this.odabraniGrad=null;
    this.listaStandard.nativeElement.style.fill = "white";
    this.listaProsjek.nativeElement.style.fill = "white";
    this.listaFavorit.nativeElement.style.fill="#b0a695";
  }

  getListaGradova()
  {
    this.httpKlijent.get<Grad[]>(MojConfig.adresa_servera + "/Grad/GetListaGradova", MojConfig.http_opcije()).subscribe(x =>
    {
      this.listaGradova=x;
    });
  }

  getByGrad() 
  {
    if(this.odabraniGrad)
    {
      this.filtriranaListaUsluzniObjekt = this.listaUsluzniObjekt?.filter(x => x.grad.naziv === this.odabraniGrad?.naziv);
    }
  }

  onUsluzniObjektClick(usluzniObjektID: number) {
    const burgerMenu = document.querySelector(".burger-menu");
      const slider = document.querySelector("#slider");
      // @ts-ignore
      burgerMenu.style.left = "-280px";
      // @ts-ignore
      slider.style.width="0px"
      this.router.navigate(['/kategorija',this.kategorijaID, 'usluzniObjekt', usluzniObjektID]);
      window.scrollTo(0,0);
  }

}
