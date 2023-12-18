import { Component } from '@angular/core';
import {ModalController, NavParams} from "@ionic/angular";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {DataService} from "../../../services/data.service";
import {TokenService} from "../../../services/token.service";

@Component({
  selector: 'app-edit-item',
  templateUrl: './edit-item.component.html',
  styleUrls: ['./edit-item.component.css']
})
export class EditItemComponent {
  item: any;
  backendUrlDel: any = 'http://localhost:5000/delete/transactions/'
  backendUrlPut: any = 'http://localhost:5000/update/transactions'



  constructor(
    private navParams: NavParams,
    private modalController: ModalController,
    public tokenService: TokenService,
    private http: HttpClient,
    ) {
    this.item = this.navParams.get('item');
  }
  updateItem() {
    const requestBody = {
      id: this.item.position,
      itemName: this.item.name,
      itemAmount: this.item.quantity,
      totalCost: this.item.price
    };

    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.tokenService.getToken()}`
    });
    const requestOptions = {
      headers: headers
    };
    this.http.put<any>(this.backendUrlPut,requestBody, requestOptions).subscribe(
      (response:any) => {
        console.log(response)
      },
      error => {
        console.error('Error:', error);
      }
    );
    this.closeModal();
  }

  deleteItem() {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.tokenService.getToken()}`
    });
    const requestOptions = {
      headers: headers
    };
    this.http.delete<any>(this.backendUrlDel+this.item.position, requestOptions).subscribe(
      (response:any) => {
        console.log(response)
      },
      error => {
        console.error('Error:', error);
      }
    );
    this.closeModal();
  }


  closeModal() {
    this.modalController.dismiss();
  }
}
