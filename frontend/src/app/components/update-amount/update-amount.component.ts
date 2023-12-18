import { Component } from '@angular/core';
import {ModalController} from "@ionic/angular";
import {HttpClient} from "@angular/common/http";
import {DataService} from "../../../services/data.service";
import {TokenService} from "../../../services/token.service";


@Component({
  selector: 'app-update-amount',
  templateUrl: './update-amount.component.html',
  styleUrls: ['./update-amount.component.css']
})
export class UpdateAmountComponent {
  ActualMonth: any;
  constructor(

    private http: HttpClient,
    private modalController: ModalController,
    public dataService: DataService,
    public tokenService: TokenService,


  ) {}


  closeModal() {
    this.modalController.dismiss();
  }
}
