import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {HttpClientModule} from "@angular/common/http";
import {RouterModule} from '@angular/router';
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import {AppComponent} from "./app.component";
import {LoginComponent} from "./login/login.component";
import {HomeComponent} from "./home/home.component";
import { HeaderComponent } from './shared/header.component';
import {FormsModule} from "@angular/forms";
import { MojRacunComponent } from './moj-racun/moj-racun.component';
import {ONamaComponent} from "./o-nama/o-nama.component";

@NgModule({

  declarations: [
    AppComponent,
    LoginComponent,
    HomeComponent,
    HeaderComponent,
    MojRacunComponent
  ],
  imports: [
    BrowserModule,
    RouterModule.forRoot([
      {path: 'login', component: LoginComponent},
      {path:'', component: HomeComponent},
      {path:'mojRacun', component: MojRacunComponent},
      {path:'oNama', component: ONamaComponent},
    ]),
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule,
  ],

  bootstrap: [AppComponent]
})

export class AppModule {}
