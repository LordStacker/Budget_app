import {Component} from '@angular/core';

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

}