import { Component } from '@angular/core';
import { UserInfo } from "../../model/profile-info.model";
import { ModalController, NavParams } from "@ionic/angular";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { DataService } from "../../../services/data.service";
import { TokenService } from "../../../services/token.service";
import { Router } from "@angular/router";

@Component({
  selector: 'app-edit-password',
  templateUrl: './edit-password.component.html',
  styleUrls: ['./edit-password.component.css']
})
export class EditPasswordComponent {
  userData: UserInfo;
  passwordData: {
    oldPassword: string;
    newPassword: string;
  } = {
    oldPassword: '',
    newPassword: ''
  };

  backendUrl = '/api/account/edit/password';

  constructor(
    private navParams: NavParams,
    private http: HttpClient,
    private modalController: ModalController,
    public dataService: DataService,
    public tokenService: TokenService,
    private router: Router,
  ) {
    this.userData = this.navParams.get('userData');
  }

  editPassword() {
    const passwordUpdateData = {
      userEmail: this.userData.userEmail,
      oldPassword: this.passwordData.oldPassword,
      newPassword: this.passwordData.newPassword
    };
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.tokenService.getToken()}`
    });

    // Set up request options
    const requestOptions = {
      headers: headers
    };
    this.http.put(this.backendUrl, passwordUpdateData, requestOptions).subscribe(
      (response: any) => {
        console.log('Password updated successfully:', response);
        this.closeModal();
      },
      (error) => {
        console.error('Error updating password:', error);
      }
    );
  }

  closeModal() {
    this.modalController.dismiss();
  }
}
