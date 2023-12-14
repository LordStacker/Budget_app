import { ChangeDetectorRef, Component, NgZone } from '@angular/core';
import { ModalController } from '@ionic/angular';
import { LoginComponent } from '../login/login.component';
import {DataService} from "../../services/data.service";

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})

export class NavbarComponent {

  constructor(
    public dataService: DataService,
    private modalController: ModalController,
    private changeDetectorRef: ChangeDetectorRef,
    private ngZone: NgZone
  ) {
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
  ngDoCheck() {
    this.changeDetectorRef.detectChanges();
  }


}
