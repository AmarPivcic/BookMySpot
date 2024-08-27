import {HttpClient, HttpErrorResponse, HttpParams} from '@angular/common/http';
import {Component, Input, OnInit} from '@angular/core';
import { ActivatedRoute, Router} from '@angular/router';
import { UsluzniObjekt } from '../models/usluzniObjekt.model';
import { MojConfig } from '../moj-config';
import { Usluga } from '../models/usluga.model';
import { LoginInformacije } from '../_helpers/login-informacije';
import { AutentifikacijaHelper } from '../_helpers/autentifikacija-helper';
import { HeaderComponent } from '../shared/header/header.component';
import { Recenzija } from '../models/recenzija.model';
import { TerminFunckijeService } from '../shared/termin-manager/termin-funckije.service';
import {compareSegments} from "@angular/compiler-cli/src/ngtsc/sourcemaps/src/segment_marker";
import {EditKoordinate} from "../models/editKoordinate.model";
import { dodajFavorit } from '../models/dodajFavorit.model';
import { Manager } from '../models/manager.model';

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
  prosjecnaOcjena: any;
  listaRecenzija: Recenzija[] | null = null;
  defaultSlika: any = "../../assets/user.png";
  odabranaOcjena: number | null = null;
  tekstRecenzije: string = "";
  logiraniKorisnik: any;
  datumPocetka: string | null = null;
  datumKraja: string | null = null;
  karticnoPlacanje: boolean = false;
  listaManagera: Manager[] | null = null;
  odabraniManager: Manager | null = null;


  //Nova logika
  godine: number[] = [];
  mjeseci: number[] = [];
  dani: number[] = [];
  godineIseljenja: number[] = [];
  mjeseciIseljenja: number[] = [];
  daniIseljenja: number[] = [];

  selectedGodina?: number;
  selectedMjesec?: number;
  selectedDan?: number;

  selectedGodinaIseljenja?: number;
  selectedMjesecIseljenja?: number;
  selectedDanIseljenja?: number;


  //Mape
  @Input() latitude: number = 24;
  @Input() longitude: number = 12;

  center!: google.maps.LatLngLiteral;
  zoom = 14;
  markerOptions: google.maps.marker.AdvancedMarkerElementOptions = {
    collisionBehavior: google.maps.CollisionBehavior.REQUIRED,
  };
  constructor(private httpKlijent: HttpClient, private route: ActivatedRoute, private router: Router, private menu: HeaderComponent, private terminFunkcije: TerminFunckijeService)
{
}

  ngOnInit(): void {
    this.usluzniObjektID=Number(this.route.snapshot.paramMap.get('usluzniObjektId'));
    this.getUsluzniObjekt(this.usluzniObjektID);
    this.getListaUsluga(this.usluzniObjektID);
    this.getListaRecenzija(this.usluzniObjektID);
    this.getPodaci();
    this.getListaManagera();
    const danas = new Date();
    this.minDate = danas.toISOString().split('T')[0];

    if (this.datumPocetka) {
      this.datumKraja = this.datumPocetka;
    }
  }

  getStars(): number[] {
    const totalStars = 5;
    return Array(totalStars).fill(0);
  }

  get filledStars(): number {
    return Math.round(this.prosjecnaOcjena);
  }

  getImagePath(putanja: string): string {
    return putanja ? putanja : this.defaultSlika;
  }

  loginInfo():LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }

  getPodaci()
  {
    this.logiraniKorisnik=this.loginInfo().autentifikacijaToken?.korisnickiNalog;
  }

  getListaManagera()
  {
    this.httpKlijent.get<Manager[]>(MojConfig.adresa_servera + "/Manager/GetListaManager?usluzniObjektID="+this.usluzniObjektID, MojConfig.http_opcije()).subscribe(x=>{
      this.listaManagera=x;
    });
  }

  getUsluzniObjekt(usluzniObjektID: number) {

    const korisnikID = this.loginInfo().autentifikacijaToken?.osobaID;
    
    const url = korisnikID !== null && korisnikID !== undefined
    ? `${MojConfig.adresa_servera}/UsluzniObjekt/Get?usluzniObjektID=${usluzniObjektID}&korisnikID=${korisnikID}`
    : `${MojConfig.adresa_servera}/UsluzniObjekt/Get?usluzniObjektID=${usluzniObjektID}`;

    this.httpKlijent.get<UsluzniObjekt>(url, MojConfig.http_opcije()).subscribe(x=>{
      this.usluzniObjekt=x;
      this.prosjecnaOcjena=x.prosjecnaOcjena;

      if(this.usluzniObjekt){
      this.center = {
        lat: this.usluzniObjekt!.latitude,
        lng: this.usluzniObjekt!.longitude
        };
      }
    })
    
  }

  getListaUsluga(id:number)
  {
    this.httpKlijent.get<Usluga[]>(MojConfig.adresa_servera+ "/Usluga/GetByObjektID?id="+id, MojConfig.http_opcije()).subscribe(x=>{
      this.listaUsluga=x;
    })
  }

  getListaRecenzija(id:number)
  {
    this.httpKlijent.get<Recenzija[]>(MojConfig.adresa_servera+ "/Recenzija/GetByUsluzniObjektID?id="+id, MojConfig.http_opcije()).subscribe(x=>{
      this.listaRecenzija = x;
    })
  }

  prijava()
  {
    this.menu.NavigirajIZatvori("/login");
  }

  onDatumUslugaChange()
  {
    if(this.odabraniDatum && this.odabranaUsluga && this.odabraniManager)
    {
       this.terminFunkcije.getListaDostupnihTermina(this.usluzniObjektID,this.odabraniDatum, this.odabranaUsluga.trajanje, this.odabraniManager.osobaID).subscribe(x=>{
        this.dostupniTermini=x;
       });
    }
  }

  rezervisiTermin() {
    console.log(this.karticnoPlacanje);

    if(this.odabraniDatum && this.odabranaUsluga && this.odabranoVrijeme && this.odabraniManager)
    {
      this.terminFunkcije.rezervisiTermin(this.odabraniDatum, this.odabranoVrijeme, this.odabranaUsluga, this.karticnoPlacanje, this.logiraniKorisnik.osobaID, this.odabraniManager?.osobaID);
      this.odabraniDatum=null;
      this.odabranoVrijeme=null;
      this.odabranaUsluga=null;
      this.odabraniManager=null;
    }

    else{
      alert("Morate odabrati radnika, datum rezervacije, uslugu i početno vrijeme rezervacije!");
    }
  }

  posaljiRecenziju() {
    if(this.odabranaOcjena && this.tekstRecenzije)
    {
      let parametri: any=
      {
        recenzijaOcjena: this.odabranaOcjena,
        recenzijaTekst: this.tekstRecenzije,
        osobaID: this.loginInfo().autentifikacijaToken?.osobaID,
        usluzniObjektID: this.usluzniObjektID
      };

      this.httpKlijent.post(MojConfig.adresa_servera+"/Recenzija/Add", parametri, MojConfig.http_opcije()).subscribe({
        next: (response) => {
          alert("Recenzija uspješno dodana!");
          console.log(response);
          this.getListaRecenzija(this.usluzniObjektID);
        },
        error: (error) => {
          alert("Greška pri dodavanju recenzije!");
          console.log(error);
        }
      });
      this.odabranaOcjena = null;
      this.tekstRecenzije = "";
    }

    else{
      alert("Morate odabrati ocjenu i unijeti tekst recenzije!");
    }
  }

  loadGodineIseljenja() {
    this.terminFunkcije.loadGodineIseljenja().subscribe(godineIseljenja => {
      this.godineIseljenja = godineIseljenja;
      this.onGodinaIseljenjaChange();
    });

  }

  loadGodine() {
    this.terminFunkcije.loadGodine().subscribe(godine => {
      this.godine = godine;
      this.godineIseljenja = godine;
      this.onGodinaChange();
      this.onGodinaIseljenjaChange();
    });
  }

  onGodinaChange() {
    if (this.selectedGodina !== undefined) {
      this.terminFunkcije.onGodinaChange(this.selectedGodina).subscribe(mjeseci => {
        this.mjeseci = mjeseci;
        this.selectedMjesec = mjeseci[0];
        this.onMjesecChange();
      });
    }
  }

  onMjesecChange() {
    if (this.selectedGodina !== undefined && this.selectedMjesec !== undefined && this.odabranaUsluga) {
      this.terminFunkcije.onMjesecChange(this.selectedGodina, this.selectedMjesec, this.odabranaUsluga?.uslugaID).subscribe(dani => {
        this.dani = dani;
        this.selectedDan = dani[0];
      });
    }
  }

  onGodinaIseljenjaChange() {
    if (this.selectedGodinaIseljenja !== undefined) {
      this.terminFunkcije.onGodinaIseljenjaChange(this.selectedGodinaIseljenja).subscribe(mjeseciIseljenja => {
        this.mjeseciIseljenja = mjeseciIseljenja;
        this.selectedMjesecIseljenja = mjeseciIseljenja.length > 0 ? mjeseciIseljenja[0] : undefined;
        this.onMjesecIseljenjaChange();
      });
    }
  }

  onMjesecIseljenjaChange() {
    if (this.selectedGodinaIseljenja !== undefined && this.selectedMjesecIseljenja !== undefined && this.odabranaUsluga) {
      this.terminFunkcije.onMjesecIseljenjaChange(this.selectedGodinaIseljenja, this.selectedMjesecIseljenja, this.odabranaUsluga?.uslugaID).subscribe(daniIseljenja => {
        this.daniIseljenja = daniIseljenja;
        console.log('Dani iseljenja:', daniIseljenja);
        this.selectedDanIseljenja = daniIseljenja.length > 0 ? daniIseljenja[0] : undefined;
      });
    }
  }

  loadDani() {
    if (this.selectedGodina !== undefined && this.selectedMjesec !== undefined && this.odabranaUsluga) {
      this.terminFunkcije.loadDani(this.selectedGodina, this.selectedMjesec, this.odabranaUsluga?.uslugaID).subscribe({
        next: (dani: number[]) => {
          this.dani = dani;
        },
        error: (error) => {
          console.error("Greška pri dohvaćanju dana:", error);
        }
      });
    }
  }

  onSmjestajUsluga() {
    this.loadGodine();
    this.loadGodineIseljenja();
    this.loadDani();
  }

  rezervisiTerminSmjestaja() {
    if (this.odabranaUsluga &&
      this.selectedGodina && this.selectedMjesec && this.selectedDan &&
      this.selectedGodinaIseljenja && this.selectedMjesecIseljenja && this.selectedDanIseljenja && this.listaManagera) {

      this.terminFunkcije.rezervisiTerminSmjestaja(
        this.selectedDan,
        this.selectedMjesec,
        this.selectedGodina,
        this.selectedDanIseljenja,
        this.selectedMjesecIseljenja,
        this.selectedGodinaIseljenja,
        this.dani,
        this.logiraniKorisnik.osobaID,
        this.odabranaUsluga,
        this.karticnoPlacanje,
        this.listaManagera[0].osobaID
      )

      this.odabranaUsluga = null;
      this.selectedGodina = undefined;
      this.selectedMjesec = undefined;
      this.selectedDan = undefined;
      this.selectedGodinaIseljenja = undefined;
      this.selectedMjesecIseljenja = undefined;
      this.selectedDanIseljenja = undefined;
      this.karticnoPlacanje = false;
    }
    else {
      alert("Molimo unesite sve potrebne podatke za rezervaciju.");
    }
  }

  onMapClick(event: google.maps.MapMouseEvent) {
    if (!this.loginInfo().isLogiran) {
      return;
    }

    const position = event.latLng;

    if (position) {
      if (this.loginInfo().autentifikacijaToken?.korisnickiNalog.isManager) {
        const requestBody = {
          osobaID: this.loginInfo().autentifikacijaToken?.korisnickiNalog.osobaID,
          usluzniObjektID: this.usluzniObjektID
        };

        this.httpKlijent.post(`${MojConfig.adresa_servera}/ManagerUsluzniObjekt/ProvjeriManagera`, requestBody)
          .subscribe({
            next: (response) => {
              this.updateCoordinates(position.lat(), position.lng());
            },
            error: (error: HttpErrorResponse) => {
              if (error.status === 404) {
                alert("Nemate permisiju editovanja koordinata nad ovim uslužnim objektom!\nNiste menadžer ovog objekta!");
              } else {
                console.error('Greška pri pozivanju API-a:', error);
              }
            }
          });
      } else if (this.loginInfo().autentifikacijaToken?.korisnickiNalog.isAdministrator) {
        this.updateCoordinates(position.lat(), position.lng());
      }
    }
  }
  updateCoordinates(latitude: number, longitude: number) {
    const requestBody: EditKoordinate = { latitude, longitude };
    this.httpKlijent.put<UsluzniObjekt>(`${MojConfig.adresa_servera}/UsluzniObjekt/EditKoordinateObjekta/${this.usluzniObjektID}`, requestBody, MojConfig.http_opcije())
      .subscribe({
        next: (response) => {
          this.getUsluzniObjekt(this.usluzniObjektID);
        },
        error: (error) => {
          console.error('Greška pri ažuriranju koordinata:', error);
        }
      });
  }

  dodajFavorit()
  {
    const requestBody: dodajFavorit =
    {
      korisnickiNalogId: this.logiraniKorisnik.osobaID,
      usluzniObjektID: this.usluzniObjektID
    }
    this.httpKlijent.post(MojConfig.adresa_servera + "/Favorit/Add", requestBody, MojConfig.http_opcije()).subscribe({
      next: (response) => {
        alert("Uspješno dodano u favorite.");
        this.getUsluzniObjekt(this.usluzniObjektID);
      },
      error: (error) => {
        alert("Greška pri dodavanju u favorite!");
        console.log(error);
      }
    })
  }

  ukloniFavorit()
  {
    const requestBody: dodajFavorit =
    {
      korisnickiNalogId: this.logiraniKorisnik.osobaID,
      usluzniObjektID: this.usluzniObjektID
    }

    this.httpKlijent.delete(MojConfig.adresa_servera+"/Favorit/Remove", {body: requestBody, headers: MojConfig.http_opcije().headers}).subscribe({
      next: (response) => {
        alert("Uspješno uklonjeno iz favorita.");
        this.getUsluzniObjekt(this.usluzniObjektID);
      },
      error: (error)=>{
        alert("Greška pri uklanjanju iz favorita!");
        console.log(error);
      }
    })
  }
}
