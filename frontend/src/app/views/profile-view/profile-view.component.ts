import {Component, OnInit} from '@angular/core';
import {UserInfo} from "../../model/profile-info.model";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {ModalController} from "@ionic/angular";
import {EditComponent} from "../../components/edit/edit.component";
import {DataService} from "../../../services/data.service";
import {TokenService} from "../../../services/token.service";

@Component({
  selector: 'app-profile-view',
  templateUrl: './profile-view.component.html',
  styleUrls: ['./profile-view.component.css']
})
export class ProfileViewComponent implements OnInit{
  originalUserData: any;
  user: UserInfo | undefined;
  backendUrl = 'pending';


  constructor(
    private http: HttpClient,
    private modalController: ModalController,
    public tokenService: TokenService,
    private dataService: DataService
  ) {}
  ngOnInit(): void {
    this.getUserData();
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
        this.user = data;
      },
      error => {
        console.error('Error:', error);
      }
    );
  }

  async changeInfo() {
    const modal = await this.modalController.create({
      component: EditComponent,
      componentProps: {
        userData: this.user // Pass the user data to the EditComponent
      }
    });
    await modal.present();
  }

}
