import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register-user',
  templateUrl: './register-user.component.html',
  styleUrls: ['./register-user.component.css']
})
export class RegisterUserComponent {

  constructor(private router: Router) { }

  register() {
    console.log("Works");
  }

  onCancelClick() {
    // Navigate to the "home" route when the "Cancel" button is clicked
    this.router.navigate(['/login']);
  }

}
