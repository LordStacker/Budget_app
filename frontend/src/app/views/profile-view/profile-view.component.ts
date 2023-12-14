import {Component, OnInit} from '@angular/core';
import {UserInfo} from "./profile-info.model";
import {HttpClient} from "@angular/common/http";
import {LoginComponent} from "../../components/login/login.component";
import {ModalController} from "@ionic/angular";
import {EditComponent} from "../../components/edit/edit.component";

@Component({
  selector: 'app-profile-view',
  templateUrl: './profile-view.component.html',
  styleUrls: ['./profile-view.component.css']
})
export class ProfileViewComponent implements OnInit {
  user: UserInfo[] | undefined;
  backendUrl = 'pending';


  constructor(
    private http: HttpClient,
    private modalController: ModalController
  ) {}
  ngOnInit(): void {
    this.user = [{
      Name: 'John',
      Lastname: 'Doe',
      Username: 'johndoe123',
      Email: 'john@example.com',
      University: 'Scheißen',
      Birthday: '1990-01-15'
    }];
//pending URL
    /*this.http.get<UserInfo[]>(this.backendUrl).subscribe(
      data => {
        this.user = data;
      },
      error => {
        console.error('Error:', error);
      }
    );*/

  }

  async changeInfo() {
    const modal = await this.modalController.create({
      component: EditComponent,
    });
    await modal.present();
  }
}
