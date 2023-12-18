import { Component } from '@angular/core';
import {ModalController, NavParams} from "@ionic/angular";
import {HttpClient} from "@angular/common/http";
import {DataService} from "../../../services/data.service";
import {TokenService} from "../../../services/token.service";

@Component({
  selector: 'app-edit-item',
  templateUrl: './edit-item.component.html',
  styleUrls: ['./edit-item.component.css']
})
export class EditItemComponent {
  item: any;



  constructor(
    private navParams: NavParams,
    private modalController: ModalController,
    ) {
    this.item = this.navParams.get('item');
  }
  updateItem() {
    console.log("update")
    this.closeModal();
  }

  deleteItem() {
    console.log("delete")
    this.closeModal();
  }


  closeModal() {
    this.modalController.dismiss();
  }
}
