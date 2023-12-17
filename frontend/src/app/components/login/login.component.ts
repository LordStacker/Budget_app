import {Component, OnInit} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import { Router } from '@angular/router';
import {ModalController, ToastController} from "@ionic/angular";
import {DataService} from "../../../services/data.service";
import {RegisterUserComponent} from "../register-user/register-user.component";
import {TokenService} from "../../../services/token.service";


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  constructor(
    private http: HttpClient,
    private router: Router,
    public dataService: DataService,
    public tokenService: TokenService,
    private modalController: ModalController,
    private toastController: ToastController,

  ) {

  }

  loginModel: any = {};
  backendUrl = 'http://localhost:5000/api/account/login';

  //Reset password action

  login() {
    const formData = {
      email: this.loginModel.email,
      password: this.loginModel.password,
    };

    this.http.post(this.backendUrl, formData).subscribe(
      async (response: any) => {
        this.tokenService.setToken(response.token)
        this.dataService.isLoggedIn = true;
        this.getUserData();
        await this.router.navigate(['/']);
        await (await this.toastController.create({
          message: "Welcome back!",
          color: "success",
          duration: 5000
        })).present();
        this.closeModal();
      },
      (error) => {
        console.error('An error occurred:', error);
      },
    );
    this.loginModel = [];
  }
  closeModal() {
    this.modalController.dismiss();
  }

  getUserData() {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.tokenService.getToken()}`
    });
    const requestOptions = {
      headers: headers
    };
    this.http.get<any>('http://localhost:5000/api/account/me', requestOptions).subscribe(
      data => {
        this.dataService.isUsername = data.username;
      },
      error => {
        console.error('Error:', error);
      }
    );
  }

  async registrationModal() {
    this.closeModal()
    const modal = await this.modalController.create({
      component: RegisterUserComponent,
    });
    await modal.present();
  }
}
