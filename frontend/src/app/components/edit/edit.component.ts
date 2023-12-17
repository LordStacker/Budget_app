import {Component} from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Router} from "@angular/router";
import {DataService} from "../../../services/data.service";
import {TokenService} from "../../../services/token.service";
import {ModalController, NavParams} from "@ionic/angular";
import {UserInfo} from "../../model/profile-info.model";

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent {
  userData: UserInfo;

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

  editModel: any = {};
  backendUrl = 'http://localhost:5000/api/account/update/user';

  editUser() {
    const formData = {
      userEmail: this.editModel.userEmail,
      username: this.editModel.username,
      firstname: this.editModel.firstname ,
      lastname: this.editModel.lastname,
      education: this.editModel.education,
      birthDate: this.editModel.birthDate,
    };
      const headers = new HttpHeaders({
        'Authorization': `Bearer ${this.tokenService.getToken()}`
      });
      const requestOptions = {
        headers: headers
      }
    this.http.put(this.backendUrl, formData, requestOptions).subscribe(
      (response: any) => {
        this.getUserData();
        this.router.navigate(['/']);
        this.closeModal();
      },
      (error) => {
        console.error('An error occurred:', error);
      },
    );
    this.editModel = [];

  }
  getUserData() {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.tokenService.getToken()}`
    });
    const requestOptions = {
      headers: headers
    };
    this.http.get<any>('http://localhost:5000/api/account/me', requestOptions).subscribe(
      data => {
        this.dataService.isUsername = data.username;
        this.dataService.isUser = data;
      },
      error => {
        console.error('Error:', error);
      }
    );
  }
  closeModal() {
    this.modalController.dismiss();
  }
}
