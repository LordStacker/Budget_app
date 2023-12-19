import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import {ModalController, ToastController} from "@ionic/angular";

@Component({
  selector: 'app-register-user',
  templateUrl: './register-user.component.html',
  styleUrls: ['./register-user.component.css'],
})
export class RegisterUserComponent {
  constructor(
    private router: Router,
    private http: HttpClient,
    private modalController: ModalController,
  private toastController: ToastController
  ) {}

  registerModel: any = {};
  backendUrl = '/api/account/register';

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
        async (response: any) => {
          await this.modalController.dismiss();
          await (await this.toastController.create({
            message: "Thank you for signing up!",
            color: "success",
            duration: 5000
          })).present();
        },
        (error) => {
          console.error('An error occurred:', error);
        },
      );
      this.registerModel = {};
    }
  }

  onCancelClick() {
    this.modalController.dismiss();
  }
}
