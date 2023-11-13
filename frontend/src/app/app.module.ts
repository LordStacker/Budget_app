import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {RouterLink, RouterLinkActive, RouterModule, RouterOutlet, Routes} from "@angular/router";
import { AppComponent } from './app.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { HomeViewComponent} from "./views/home-view/home-view.component";
import { ProfileViewComponent } from './views/profile-view/profile-view.component';
import { IonicModule } from '@ionic/angular';



const routes: Routes = [
  {path: '', component: HomeViewComponent },
  {path: 'profile', component: ProfileViewComponent }
]


@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    HomeViewComponent,
    ProfileViewComponent,
  ],
  imports: [
    RouterModule.forRoot(routes),
    IonicModule.forRoot(),
    BrowserModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
  exports: [RouterModule]
})
export class AppModule {}
