import { ChangeDetectorRef, Component, NgZone } from '@angular/core';
import { ModalController } from '@ionic/angular';
import { LoginComponent } from '../login/login.component';
import {DataService} from "../../data.service";

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})

export class NavbarComponent {
  isLoggedIn = false;

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
      // Logging in
      const modal = await this.modalController.create({
        component: LoginComponent,
      });
      await modal.present();
    }
  }
  ngDoCheck() {
    // Trigger change detection whenever ngDoCheck is called
    this.changeDetectorRef.detectChanges();
  }


}
