import { Component } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {ModalController} from "@ionic/angular";
import {TokenService} from "../../../services/token.service";
import {DataService} from "../../../services/data.service";


@Component({
  selector: 'app-home-view',
  templateUrl: './home-view.component.html',
  styleUrls: ['./home-view.component.css']
})
export class HomeViewComponent {
  constructor(
    public dataService: DataService
  ) {}

}
