import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-register-user',
  templateUrl: './register-user.component.html',
  styleUrls: ['./register-user.component.css'],
})
export class RegisterUserComponent {
  constructor(
    private router: Router,
    private http: HttpClient,
  ) {}

  registerModel: any = {};
  backendUrl = 'http://localhost:5000/api/account/register';

  register() {
    const formData = {
      username: this.registerModel.username,
      password: this.registerModel.password,
      firstname: this.registerModel.firstname,
      lastname: this.registerModel.lastname,
      email: this.registerModel.email,
      education: this.registerModel.education,
      dateofbirth: this.registerModel.dateofbirth,
    };

    this.http.post(this.backendUrl, formData).subscribe(
      (response: any) => {
        console.log(response);
      },
      (error) => {
        console.error('An error occurred:', error);
      },
    );
    this.registerModel = [];
  }

  onCancelClick() {
    // Navigate to the "home" route when the "Cancel" button is clicked
    this.router.navigate(['/login']);
  }
}
