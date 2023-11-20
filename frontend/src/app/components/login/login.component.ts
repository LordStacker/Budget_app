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
    const backendUrl = 'http://localhost:8080/api/login';
    const jsonString = JSON.stringify(this.loginModel);

    this.http.post(backendUrl, jsonString).subscribe((response: any) => {
      console.log(response);
      console.log('Button works!');
    });
  }
}
