import {Component, OnInit} from '@angular/core';
import {ModalController, ToastController} from '@ionic/angular';
import { LoginComponent } from '../login/login.component';
import {DataService} from "../../../services/data.service";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {TokenService} from "../../../services/token.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})

export class NavbarComponent implements OnInit{

  constructor(
    public dataService: DataService,
    private modalController: ModalController,
    public tokenService: TokenService,
    private http: HttpClient,
    private router: Router,
    private toastController: ToastController,
  ) {
  }
  ngOnInit():void{
    if(this.tokenService.getToken()){
      this.dataService.isLoggedIn = true;
      this.getUserData();
    }
  }
  getUserData() {
    try {
      const headers = new HttpHeaders({
        'Authorization': `Bearer ${this.tokenService.getToken()}`
      });
      const requestOptions = {
        headers: headers
      };

      this.http.get<any>('/api/account/me', requestOptions).subscribe(
        data => {
          this.dataService.isUsername = data.username;
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
      this.tokenService.clearToken()
      this.dataService.isLoggedIn = false;
      this.dataService.isUser = undefined;
      this.dataService.isUsername = '';
      await (await this.toastController.create({
        message: 'Successfully logged out',
        duration: 5000,
        color: 'success',
      })).present()
      await this.router.navigate(['/']);
    } else {
      const modal = await this.modalController.create({
        component: LoginComponent,
      });
      await modal.present();
    }
  }
}
