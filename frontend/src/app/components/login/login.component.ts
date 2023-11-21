import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  constructor(private http: HttpClient) {}

  loginModel: any = {};

  login() {
    const backendUrl = 'http://localhost:5000/api/account/login';

    const formData = {
      email: this.loginModel.email,
      password: this.loginModel.password
    };

    this.http.post(backendUrl, formData).subscribe(
      (response: any) => {
        console.log(response);
        console.log('Button works!');
      },
      (error) => {
        console.error('An error occurred:', error);
      }
    );
  }
}
