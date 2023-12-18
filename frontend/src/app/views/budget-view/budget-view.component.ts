import {Component, OnInit} from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {DataService} from "../../../services/data.service";
import {TokenService} from "../../../services/token.service";
import {EditItemComponent} from "../../components/edit-item/edit-item.component";
import {ModalController} from "@ionic/angular";
import {UpdateAmountComponent} from "../../components/update-amount/update-amount.component";

export interface budgedElement {
  name: string;
  position: number;
  price: number;
  quantity: string;
}

const ELEMENT_DATA: budgedElement[] = [];

@Component({
  selector: 'app-budget-view',
  templateUrl: './budget-view.component.html',
  styleUrls: ['./budget-view.component.css'],
})
export class BudgetViewComponent implements OnInit{
  displayedColumns: string[] = ['position', 'name', 'price', 'quantity'];
  dataSource = ELEMENT_DATA;
  budgetModel: any = {};
  backendUrl = 'http://localhost:5000/post/transactions';
  backendUrlGet: string = 'http://localhost:5000/transactions';
  backendUrlAmount: string = 'http://localhost:5000/api/get-current-amount';
  backendUrlTotalAmount: string = 'http://localhost:5000/api/total-amount';
  amounts: any = {
    month: '',
    avaiable: ''
  }

  constructor(
    private http: HttpClient,
    public dataService: DataService,
    public tokenService: TokenService,
    private modalController: ModalController
  ) {

  }



  submitBudget() {
    const formData = {
      itemName: this.budgetModel.itemName,
      itemAmount: this.budgetModel.itemAmount,
      totalCost: this.budgetModel.totalCost,
    };
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.tokenService.getToken()}`
    });
    const requestOptions = {
      headers: headers
    };
    this.http.post(this.backendUrl, formData, requestOptions).subscribe(
      (r: any) => {
        this.getTransactions();
      },
      (e) => {
        console.error('An error occurred:', e);
      },
    );

    this.budgetModel = [];
  }
  getAmounts() {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.tokenService.getToken()}`
    });
    const requestOptions = {
      headers: headers
    };
    this.http.get<any>(this.backendUrlAmount, requestOptions).subscribe(
      (data: any) => {
        console.log(data)
        this.amounts.month = data
      },
      error => {
        console.error('Error:', error);
      }
    );
    this.http.get<any>(this.backendUrlTotalAmount, requestOptions).subscribe(
      (data: any) => {
        this.amounts.avaiable = data
      },
      error => {
        console.error('Error:', error);
      }
    );
  }
  getAmounts2() {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.tokenService.getToken()}`
    });
    const requestOptions = {
      headers: headers
    };
    this.http.get<any>(this.backendUrlTotalAmount, requestOptions).subscribe(
      (data: any) => {

        this.amounts.avaiable = data
      },
      error => {
        console.error('Error:', error);
      }
    );
  }

  getTransactions() {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.tokenService.getToken()}`
    });
    const requestOptions = {
      headers: headers
    };
    this.http.get<any>(this.backendUrlGet, requestOptions).subscribe(
      (data: any[]) => {
        console.log(data)
        this.dataSource = data.map((item, index) => ({
          position: item.id,
          name: item.itemName,
          price: item.totalCost,
          quantity: item.itemAmount.toString()
        }));
      },
      error => {
        console.error('Error:', error);
      }
    );
  }
  ngOnInit():void{
    if(this.tokenService.getToken()){
      this.getTransactions();
      this.getAmounts();
      this.getAmounts2();
    }
  }
  async UpdateAmount() {
    const modal = await this.modalController.create({
      component: UpdateAmountComponent,
      componentProps: {
        ActualMonth: this.amounts.month
      }
    });
    await modal.present();
  }

  calculateTotalPrice(): number {
    let totalPrice = 0;
    this.dataSource.forEach((item) => {
      totalPrice += item.price;
    });
    return totalPrice;
  }

  async handleRowClick(row: any) {
    const modal = await this.modalController.create({
      component: EditItemComponent,
      componentProps: {
        item: row
      }
    });
    await modal.present();
  }
}
