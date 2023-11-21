import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css'],
})
export class ResetPasswordComponent {
  constructor(
    private router: Router,
    private http: HttpClient,
  ) {}

  backendUrl = 'http://localhost:5000/api/account/reset';

  resetModel: any = {};

  formData = {
    email: this.resetModel.email,
  };

  reset() {
    this.http.post(this.backendUrl, this.formData).subscribe(
      (response: any) => {
        console.log(response);
        this.router.navigate(['/login']);
      },
      (error) => {
        console.error('An error occurred:', error);
      },
    );
  }

  onCancelClick() {
    // Navigate to the "home" route when the "Cancel" button is clicked
    this.router.navigate(['/login']);
  }
}
