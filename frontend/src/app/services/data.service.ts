import {Injectable} from "@angular/core";
import {UserInfo} from "../model/profile-info.model";

Injectable({providedIn: 'root'})
export class DataService {
  isLoggedIn = false;
  isUsername: string = '';
  isUser: UserInfo | undefined;
}
