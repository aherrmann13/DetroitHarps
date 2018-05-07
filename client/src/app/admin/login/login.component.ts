import { Component, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';

import { Client, ContactModel, UserCredentialsModel } from '../../api.client';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: [ ]
})
export class LoginComponent implements OnInit {

  // TODO this should be one form
  email = new FormControl('', [Validators.required, Validators.email]);
  password = new FormControl('', [Validators.required]);

  enableForm: boolean = true;
  
  constructor(private _client: Client, private _router: Router) { }
  ngOnInit() {
  }

  getErrorMessage() {
    return this.email.hasError('required') ? 'You must enter a value' :
      this.email.hasError('email') ? 'Not a valid email' :
        '';
  }

  isValidForm(){
    return !this.email.invalid &&
      !this.password.invalid &&
      this.enableForm;
  }

  login(): void{
    this.enableForm = false;
    var model = new UserCredentialsModel({
      email: this.email.value,
      password: this.password.value
    });
    this._client.login(model).subscribe(
      data => this.onComplete(data),
      error => console.log(error)
    );
  }

  private onComplete(token: string){
    localStorage.setItem("token", token);
    this.enableForm = true;
    this._router.navigate(['/admin/registration']);
  }
  

}
