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
    if (this.registerModel) {
      const formData = {
        userEmail: this.registerModel.userEmail,
        password: this.registerModel.password,
        username: this.registerModel.username,
        firstname: this.registerModel.firstname,
        lastname: this.registerModel.lastname,
        education: this.registerModel.education,
        birthDate: this.registerModel.birthDate,
      };

      console.log(formData);

      this.http.post(this.backendUrl, formData).subscribe(
        (response: any) => {
          console.log(response);
        },
        (error) => {
          console.error('An error occurred:', error);
        },
      );
      this.registerModel = {};
    }
  }

  onCancelClick() {
    // Navigate to the "home" route when the "Cancel" button is clicked
    if (this.router) this.router.navigate(['/login']);
  }
}
