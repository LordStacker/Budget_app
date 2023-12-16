import {Component, OnInit} from '@angular/core';
import { ModalController } from '@ionic/angular';
import { LoginComponent } from '../login/login.component';
import {DataService} from "../../services/data.service";
import {HttpClient, HttpHeaders} from "@angular/common/http";

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})

export class NavbarComponent implements OnInit{

  constructor(
    public dataService: DataService,
    private modalController: ModalController,
    private http: HttpClient,
  ) {
  }
  ngOnInit():void{
    if(localStorage.getItem('token')){
      this.dataService.isLoggedIn = true;
      this.getUserData();
    }
  }
  getUserData() {
    try {
      const headers = new HttpHeaders({
        'Authorization': `Bearer ${localStorage.getItem('token')}`
      });
      const requestOptions = {
        headers: headers
      };

      this.http.get<any>('http://localhost:5000/api/account/me', requestOptions).subscribe(
        data => {
          this.dataService.isUsername = data.username;
          this.dataService.isUser = data;
        },
        e => {
          console.error('Your token has been expired');
          this.dataService.isLoggedIn = false;
          localStorage.clear()
        }
      );
    } catch (error) {
      console.error('Token has been expired');
      this.dataService.isLoggedIn = false;
      localStorage.clear()
    }
  }

  async toggleLoginStatus() {
    if (this.dataService.isLoggedIn) {
      localStorage.removeItem('token');
      this.dataService.isLoggedIn = false;
    } else {
      const modal = await this.modalController.create({
        component: LoginComponent,
      });
      await modal.present();
    }
  }
}
