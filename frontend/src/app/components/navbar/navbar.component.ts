import { ChangeDetectorRef, Component, NgZone } from '@angular/core';
import { ModalController } from '@ionic/angular';
import { LoginComponent } from '../login/login.component';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})

export class NavbarComponent {
  isLoggedIn = false;

  constructor(
    private modalController: ModalController,
    private changeDetectorRef: ChangeDetectorRef,
    private ngZone: NgZone
  ) {
    const isLoggedIn = localStorage.getItem('isLoggedIn');
    this.isLoggedIn = isLoggedIn === 'true';
  }

  async toggleLoginStatus() {
    if (this.isLoggedIn) {
      localStorage.setItem('isLoggedIn', 'false');
      localStorage.removeItem('token');
      this.isLoggedIn = false;
    } else {
      // Logging in
      const modal = await this.modalController.create({
        component: LoginComponent,
      });
      await modal.present();
    }
    this.updateLoginStatus();
  }
  ngDoCheck() {
    // Trigger change detection whenever ngDoCheck is called
    this.changeDetectorRef.detectChanges();
  }

  private updateLoginStatus() {
    const isLoggedIn = localStorage.getItem('isLoggedIn');
    this.isLoggedIn = isLoggedIn === 'true';
  }

}
