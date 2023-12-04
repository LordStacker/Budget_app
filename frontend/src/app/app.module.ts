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
import { RegisterUserComponent } from './register-user/register-user.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';
import { BudgetViewComponent } from './views/budget-view/budget-view.component';
import { ForumViewComponent } from './views/forum-view/forum-view.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatTableModule} from "@angular/material/table";
import {DataService} from "./data.service";


const routes: Routes = [
  {path: '', component: HomeViewComponent },
  {path: 'profile', component: ProfileViewComponent },
  {path: 'budget', component: BudgetViewComponent},
  {path: 'forum', component: ForumViewComponent},
  {path: 'RegisterNewUser', component: RegisterUserComponent },
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
    ForumViewComponent,
  ],
  imports: [
    RouterModule.forRoot(routes),
    IonicModule.forRoot(),
    BrowserModule,
    FormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MatTableModule,
  ],
  providers: [DataService],
  bootstrap: [AppComponent],
  exports: [RouterModule]
})
export class AppModule {}
