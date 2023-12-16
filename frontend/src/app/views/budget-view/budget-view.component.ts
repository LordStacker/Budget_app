import {Component, OnInit} from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Router} from "@angular/router";
import {DataService} from "../../services/data.service";

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

  constructor(
    private http: HttpClient,
    public dataService: DataService,
  ) {

  }



  submitBudget() {
    const formData = {
      itemName: this.budgetModel.itemName,
      itemAmount: this.budgetModel.itemAmount,
      totalCost: this.budgetModel.totalCost,
    };
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${localStorage.getItem('token')}`
    });
    const requestOptions = {
      headers: headers
    };
    this.http.post(this.backendUrl, formData, requestOptions).subscribe(
      (response: any) => {
        console.log(response);
        this.getTransactions();
      },
      (error) => {
        console.error('An error occurred:', error);
      },
    );
    this.budgetModel = [];
  }

  getTransactions() {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${localStorage.getItem('token')}`
    });
    const requestOptions = {
      headers: headers
    };
    this.http.get<any>(this.backendUrlGet, requestOptions).subscribe(
      (data: any[]) => {
        this.dataSource = data.map((item, index) => ({
          position: index + 1,
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
    if(localStorage.getItem('token')){
      this.getTransactions();
    }
  }

}
