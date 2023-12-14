import { Component } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {DataService} from "../../services/data.service";
import {ModalController} from "@ionic/angular";

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent {
  constructor(
    private http: HttpClient,
    private modalController: ModalController
  ) {

  }

  editModel: any = {};
  backendUrl = 'pending';

  editUser() {
    const formData = {
      Name: this.editModel.Name ,
      Lastname: this.editModel.Lastname,
      Username: this.editModel.Username,
      email: this.editModel.email,
      University: this.editModel.University,
      Birthday: this.editModel.Birthday,
    };
    console.log(formData)
/*
    this.http.put(this.backendUrl, formData).subscribe(
      (response: any) => {
        console.log(response)
        this.closeModal();
      },
      (error) => {
        console.error('An error occurred:', error);
      },
    );
    this.editModel = [];
    */
  }
  closeModal() {
    this.modalController.dismiss();
  }
}
