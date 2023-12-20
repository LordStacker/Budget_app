import { Component } from '@angular/core';
import {ModalController} from "@ionic/angular";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {DataService} from "../../../services/data.service";
import {TokenService} from "../../../services/token.service";


@Component({
  selector: 'app-update-amount',
  templateUrl: './update-amount.component.html',
  styleUrls: ['./update-amount.component.css']
})
export class UpdateAmountComponent {
  ActualMonth: any;
  backendUrlPut: any = '/api/update-total-amount';
  constructor(

    private http: HttpClient,
    private modalController: ModalController,
    public dataService: DataService,
    public tokenService: TokenService,


  ) {}


  closeModal() {
    this.modalController.dismiss();
  }

  updateItem() {
    const requestBody = {
      updatedStartAmount: this.ActualMonth
    };

    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.tokenService.getToken()}`
    });
    const requestOptions = {
      headers: headers
    };

    this.http.put<any>(this.backendUrlPut, requestBody, requestOptions).subscribe(
      (response: any) => {
        console.log(response);
        this.closeModal();
      },
      error => {
        console.error('Error:', error);
      }
    );
  }
}
