import { HttpClient, HttpParams } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router} from '@angular/router';
import { UsluzniObjekt } from '../models/usluzniObjekt.model';
import { MojConfig } from '../moj-config';
import { Usluga } from '../models/usluga.model';
import { LoginInformacije } from '../_helpers/login-informacije';
import { AutentifikacijaHelper } from '../_helpers/autentifikacija-helper';
import { HeaderComponent } from '../shared/header.component';

@Component({
  selector: 'app-usluzni-objekt',
  templateUrl: './usluzni-objekt.component.html',
  styleUrl: './usluzni-objekt.component.css'
})
export class UsluzniObjektComponent implements OnInit {

  usluzniObjektID: any;
  usluzniObjekt: UsluzniObjekt | null = null;
  listaUsluga: Usluga[] | null = null;
  odabraniDatum: string | null = null;
  odabranaUsluga: Usluga | null = null;
  odabranoVrijeme: string | null = null;
  dostupniTermini: string[] = [];
  minDate: string = '';
  prosjecnaOcjena: any

constructor(private httpKlijent: HttpClient, private route: ActivatedRoute, private router: Router, private menu: HeaderComponent) 
{}

  ngOnInit(): void {
    this.usluzniObjektID=Number(this.route.snapshot.paramMap.get('id'));
    this.getUsluzniObjekt(this.usluzniObjektID);
    this.getListaUsluga(this.usluzniObjektID);
    const danas = new Date();
    this.minDate = danas.toISOString().split('T')[0];
  }

  getStars(): number[] {
    const totalStars = 5;
    return Array(totalStars).fill(0);
  }

  get filledStars(): number {
    return Math.round(this.prosjecnaOcjena);
  }

  loginInfo():LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }

  getUsluzniObjekt(id: number) {
    this.httpKlijent.get<UsluzniObjekt>(MojConfig.adresa_servera + "/UsluzniObjekt/Get?id="+id, MojConfig.http_opcije()).subscribe(x=>{
      this.usluzniObjekt=x;
      this.prosjecnaOcjena=x.prosjecnaOcjena;
    })
  }

  getListaUsluga(id:number)
  {
    this.httpKlijent.get<Usluga[]>(MojConfig.adresa_servera+ "/Usluga/GetByObjektID?id="+id, MojConfig.http_opcije()).subscribe(x=>{
      this.listaUsluga=x;
      console.log(this.listaUsluga);
    })
  }

  prijava()
  {
    this.menu.NavigirajIZatvori("/login");
  }

  getListaDostupnihTermina(objektID: number,datum: string, trajanje: number)
  {
    let params = new HttpParams()
      .set('usluzniObjektID', objektID)
      .set('odabraniDatum', datum)
      .set('trajanje', trajanje);

    this.httpKlijent.get<string[]>(MojConfig.adresa_servera+"/Rezervacija/GetListaSlobodnihTermina", {params}).subscribe(x =>{
      this.dostupniTermini=x;
    });
  }

  onDatumUslugaChange()
  {
    if(this.odabraniDatum && this.odabranaUsluga)
    {
      this.getListaDostupnihTermina(this.usluzniObjektID, this.odabraniDatum, this.odabranaUsluga.trajanje)
    }
  }

  rezervisiTermin() {

    if(this.odabraniDatum && this.odabranaUsluga && this.odabranoVrijeme)
    {
      let parametri: any = {
        datumRezervacije: this.odabraniDatum,
        rezervacijaPocetak: this.odabranoVrijeme,
        trajanje: this.odabranaUsluga.trajanje,
        korisnikID: this.loginInfo().autentifikacijaToken?.osobaID,
        uslugaID: this.odabranaUsluga.uslugaID,
        usluzniObjektID: this.usluzniObjektID,
      };
      
      this.httpKlijent.post(MojConfig.adresa_servera+"/Rezervacija/Add", parametri, MojConfig.http_opcije()).subscribe({
        next: (response) => {
          alert("Uspješna rezervacija!");
          console.log(response);
        },
        error: (error) => {
          alert("Greška pri pravljenju rezervacije!");
          console.log(error);
        }
      });
      this.odabraniDatum=null;
      this.odabranoVrijeme=null;
      this.odabranaUsluga=null;
    }

    else{
      alert("Morate odabrati datum rezervacije, uslugu i početno vrijeme rezervacije!");
    }
  }
}
