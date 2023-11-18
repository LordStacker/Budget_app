import { Component } from '@angular/core';
import { HttpClient } from "@angular/common/http";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})


export class LoginComponent {
  constructor(private http : HttpClient) {
  }
  loginModel: any = {};

  onSubmit(){
    const backendUrl = "" ;

    const jsonString = JSON.stringify(this.loginModel);

    this.http.post(backendUrl, jsonString)
      .subscribe(response =>
      {
        console.log(response);
      }, error => {
        console.error(error)
      });


  }


}

