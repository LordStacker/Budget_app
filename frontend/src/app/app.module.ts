import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {
  PreloadAllModules,
  RouteReuseStrategy,
  RouterLink,
  RouterLinkActive,
  RouterModule,
  RouterOutlet,
  Routes
} from "@angular/router";
import { AppComponent } from './app.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { HomeViewComponent} from "./views/home-view/home-view.component";
import { ProfileViewComponent } from './views/profile-view/profile-view.component';
import {IonicModule, IonicRouteStrategy} from '@ionic/angular';
import { LoginComponent } from './components/login/login.component';
import {FormsModule} from "@angular/forms";
import {HTTP_INTERCEPTORS, HttpClientModule} from "@angular/common/http";

import {RegisterUserComponent} from "./components/register-user/register-user.component";
import {ResetPasswordComponent} from "./components/reset-password/reset-password.component";
import { BudgetViewComponent } from './views/budget-view/budget-view.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatTableModule} from "@angular/material/table";
import {DataService} from "../services/data.service";
import { EditComponent } from './components/edit/edit.component';
import {NgOptimizedImage} from "@angular/common";
import {ErrorHttpInterceptor} from "../interceptors/error-http-interceptor";
import {AuthHttpInterceptor} from "../interceptors/auth-http-interceptor";
import {TokenService} from "../services/token.service";
import {AuthenticatedGuard} from "./guard";
import { UpdateAmountComponent } from './components/update-amount/update-amount.component';
import { EditItemComponent } from './components/edit-item/edit-item.component';




const routes: Routes = [
  {path: '',
    component: HomeViewComponent
  },
  {path: 'profile', component: ProfileViewComponent,
    canActivate: [AuthenticatedGuard]},
  {path: 'budget', component: BudgetViewComponent,
    canActivate: [AuthenticatedGuard]},
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
    UpdateAmountComponent,
    EditItemComponent,
  ],
  imports: [
    RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules }),
    IonicModule.forRoot(),
    BrowserModule,
    FormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MatTableModule,
  ],
  providers: [
    DataService,
    {provide: RouteReuseStrategy, useClass: IonicRouteStrategy},
    {provide: HTTP_INTERCEPTORS, useClass: ErrorHttpInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: AuthHttpInterceptor, multi: true},
    AuthenticatedGuard,
    TokenService
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
