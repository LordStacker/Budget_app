import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {RouterLink, RouterLinkActive, RouterModule, RouterOutlet, Routes} from "@angular/router";
import { AppComponent } from './app.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { HomeViewComponent} from "./views/home-view/home-view.component";
import { ProfileViewComponent } from './views/profile-view/profile-view.component';
import { IonicModule } from '@ionic/angular';
import { LoginComponent } from './components/login/login.component';
import {FormsModule} from "@angular/forms";
import {HttpClientModule} from "@angular/common/http";


const routes: Routes = [
  {path: '', component: HomeViewComponent },
  {path: 'profile', component: ProfileViewComponent },
  {path: 'login', component: LoginComponent }
]


@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    HomeViewComponent,
    ProfileViewComponent,
    LoginComponent,
  ],
  imports: [
    RouterModule.forRoot(routes),
    IonicModule.forRoot(),
    BrowserModule,
    FormsModule,
    HttpClientModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
  exports: [RouterModule]
})
export class AppModule {}
