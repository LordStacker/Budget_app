import {Injectable} from "@angular/core";

Injectable({providedIn: 'root'})
export class DataService {
  isLoggedIn = false;
  isUsername: string = '';
  isId: any ;
}
