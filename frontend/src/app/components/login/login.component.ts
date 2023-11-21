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
  backendUrl = 'http://localhost:5000/api/account/login';

  login() {
    const formData = {
      email: this.loginModel.email,
      password: this.loginModel.password,
    };

    this.http.post(this.backendUrl, formData).subscribe(
      (response: any) => {
        console.log(response);
        console.log('Button works!');
      },
      (error) => {
        console.error('An error occurred:', error);
      },
    );
    this.loginModel = [];
  }
}
