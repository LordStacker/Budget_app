import {Component} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {DataService} from "../../services/data.service";

export interface budgedElement {
  name: string;
  position: number;
  price: number;
  quantity: string;
}

const ELEMENT_DATA: budgedElement[] = [
  {position: 1, name: 'Pizza', price: 1.0079, quantity: '2'},
  {position: 2, name: 'Kartoffel', price: 4.0026, quantity: '30'},
];

@Component({
  selector: 'app-budget-view',
  templateUrl: './budget-view.component.html',
  styleUrls: ['./budget-view.component.css'],
})
export class BudgetViewComponent {
  displayedColumns: string[] = ['position', 'name', 'price', 'quantity'];
  dataSource = ELEMENT_DATA;
  budgetModel: any = {};
  backendUrl = 'pending';

  constructor(
    private http: HttpClient,
  ) {

  }



  submitBudget() {
    const formData = {
      itemName: this.budgetModel.itemName,
      Amount: this.budgetModel.Amount,
      Quantity: this.budgetModel.Quantity,
    };
    this.http.post(this.backendUrl, formData).subscribe(
      (response: any) => {
        console.log(response);
      },
      (error) => {
        console.error('An error occurred:', error);
      },
    );
    this.budgetModel = [];
  }

}
