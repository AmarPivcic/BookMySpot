import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import { provideHttpClient, withInterceptorsFromDi } from "@angular/common/http";
import {RouterModule} from '@angular/router';
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import {AppComponent} from "./app.component";
import {LoginComponent} from "./login/login.component";
import {HomeComponent} from "./home/home.component";
import { HeaderComponent } from './shared/header.component';
import {FormsModule} from "@angular/forms";
import { MojRacunComponent } from './moj-racun/moj-racun.component';
import {ONamaComponent} from "./o-nama/o-nama.component";
import {MarkdownModule} from "ngx-markdown";
import {ONamaEditComponent} from "./o-nama-edit/o-nama-edit.component";
import { KategorijaComponent } from './kategorija/kategorija.component';
import { UsluzniObjektComponent } from './usluzni-objekt/usluzni-objekt.component';
import {PitanjaOdgovoriListaComponent} from "./pitanja-odgovori-lista/pitanja-odgovori-lista.component";
import {NeodgovorenaPitanjaListaComponent} from "./neodgovorena-pitanja-lista/neodgovorena-pitanja-lista.component";
import {PitanjaAdminListaComponent} from "./pitanja-admin-lista/pitanja-admin-lista.component";
import {MojRacunEditComponent} from "./moj-racun-edit/moj-racun-edit.component";
import {EditLozinkaComponent} from "./edit-lozinka/edit-lozinka.component";

@NgModule({
  declarations: [
        AppComponent,
        LoginComponent,
        HomeComponent,
        HeaderComponent,
        MojRacunComponent,
        KategorijaComponent,
        UsluzniObjektComponent
    ],
    bootstrap: [AppComponent],
    imports: [
      BrowserModule,
      MarkdownModule.forRoot(),
      RouterModule.forRoot([
            { path: 'login', component: LoginComponent },
            { path: '', component: HomeComponent },
            { path: 'mojRacun', component: MojRacunComponent },
            { path: 'oNama', component: ONamaComponent },
            { path: 'oNama/oNamaEdit', component: ONamaEditComponent },
            { path: 'kategorija/:id', component: KategorijaComponent},
            { path: 'usluzniObjekt/:id', component: UsluzniObjektComponent},
            { path: 'pitanjaOdgovori', component: PitanjaOdgovoriListaComponent },
            { path: 'pitanjaOdgovori/neodgovorenaPitanja', component: NeodgovorenaPitanjaListaComponent },
            { path: 'pitanjaOdgovori/pitanjaAdminLista', component: PitanjaAdminListaComponent },
            { path: 'mojRacun/mojRacunEdit', component: MojRacunEditComponent },
            { path: 'mojRacun/mojRacunEdit/editLozinka', component: EditLozinkaComponent },
        ]),
      BrowserAnimationsModule,
      FormsModule
    ],
  providers: [provideHttpClient(withInterceptorsFromDi())] })

export class AppModule {}
