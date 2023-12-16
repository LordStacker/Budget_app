import {Component, OnInit} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import { Router } from '@angular/router';
import {ModalController} from "@ionic/angular";
import {DataService} from "../../services/data.service";


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
    private modalController: ModalController
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
      (response: any) => {
        localStorage.setItem('token', response.token);
        this.dataService.isLoggedIn = true;
        this.getUserData();
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

  getUserData() {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${localStorage.getItem('token')}`
    });
    const requestOptions = {
      headers: headers
    };
    this.http.get<any>('http://localhost:5000/api/account/me', requestOptions).subscribe(
      data => {
        console.log(data);
        this.dataService.isUsername = data.username;
        this.dataService.isUser = data;
      },
      error => {
        console.error('Error:', error);
      }
    );
  }
}
