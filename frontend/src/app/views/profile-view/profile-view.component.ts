import {Component, OnInit} from '@angular/core';
import {UserInfo} from "../../model/profile-info.model";
import {HttpClient} from "@angular/common/http";
import {ModalController} from "@ionic/angular";
import {EditComponent} from "../../components/edit/edit.component";
import {DataService} from "../../services/data.service";

@Component({
  selector: 'app-profile-view',
  templateUrl: './profile-view.component.html',
  styleUrls: ['./profile-view.component.css']
})
export class ProfileViewComponent {
  user: UserInfo | undefined;
  backendUrl = 'pending';


  constructor(
    private http: HttpClient,
    private modalController: ModalController,
    private dataService: DataService
  ) {}
  ngOnInit(): void {
    this.user = this.dataService.isUser;

    //password request also password modification view
    //Verify
    //api edit profile
    //api password

//pending URL
    /*this.http.put<UserInfo[]>(this.backendUrl).subscribe(
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
