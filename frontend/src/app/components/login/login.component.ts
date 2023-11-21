import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  constructor(
    private http: HttpClient,
    private router: Router,
  ) {}

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
        this.router.navigate(['/']);
      },
      (error) => {
        console.error('An error occurred:', error);
      },
    );
    this.loginModel = [];
  }
}
