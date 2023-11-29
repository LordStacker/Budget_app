import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import {ModalController} from "@ionic/angular";


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  constructor(
    private http: HttpClient,
    private router: Router,
    private modalController: ModalController
  ) {

  }

  loginModel: any = {};
  backendUrl = 'http://localhost:5000/api/account/login';

  login() {
    const formData = {
      email: this.loginModel.email,
      password: this.loginModel.password,
    };

    this.http.post(this.backendUrl, formData).subscribe(
      (response: any) => {
        localStorage.setItem('isLoggedIn', 'true');
        localStorage.setItem('token', response.token);
        this.router.navigate(['/']);
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
}
