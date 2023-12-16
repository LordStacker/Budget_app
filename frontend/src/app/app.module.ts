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

import {RegisterUserComponent} from "./components/register-user/register-user.component";
import {ResetPasswordComponent} from "./components/reset-password/reset-password.component";
import { BudgetViewComponent } from './views/budget-view/budget-view.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatTableModule} from "@angular/material/table";
import {DataService} from "./services/data.service";
import { EditComponent } from './components/edit/edit.component';
import {NgOptimizedImage} from "@angular/common";



const routes: Routes = [
  {path: '', component: HomeViewComponent },
  {path: 'profile', component: ProfileViewComponent },
  {path: 'budget', component: BudgetViewComponent},
  {path: 'register', component: RegisterUserComponent },
]


@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    HomeViewComponent,
    ProfileViewComponent,
    LoginComponent,
    RegisterUserComponent,
    ResetPasswordComponent,
    BudgetViewComponent,
    EditComponent,
  ],
    imports: [
        RouterModule.forRoot(routes),
        IonicModule.forRoot(),
        BrowserModule,
        FormsModule,
        HttpClientModule,
        BrowserAnimationsModule,
        MatTableModule,
        NgOptimizedImage,
    ],
  providers: [DataService],
  bootstrap: [AppComponent],
  exports: [RouterModule]
})
export class AppModule {}
