import { Component, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';

import { Client, ContactModel, UserCredentialsModel } from '../../shared/client/api.client';
import { Router } from '@angular/router';

@Component({
  selector: 'dh-login',
  templateUrl: './login.component.html',
  styleUrls: [ './login.component.scss' ]
})
export class LoginComponent implements OnInit {

  // TODO this should be one form
  email = new FormControl('', [Validators.required, Validators.email]);
  password = new FormControl('', [Validators.required]);

  enableForm = true;

  constructor(private _client: Client, private _router: Router) { }
  ngOnInit() {
  }

  getErrorMessage() {
    return this.email.hasError('required') ? 'You must enter a value' :
      this.email.hasError('email') ? 'Not a valid email' :
        '';
  }

  isValidForm() {
    return !this.email.invalid &&
      !this.password.invalid &&
      this.enableForm;
  }

  login(): void {
    this.enableForm = false;
    const model = new UserCredentialsModel({
      email: this.email.value,
      password: this.password.value
    });
    this._client.login(model).subscribe(
      data => this.onComplete(data),
      error => this.onError(error)
    );
  }

  private onComplete(token: string) {
    localStorage.setItem('token', token);
    this.enableForm = true;
    this._router.navigate(['/admin']);
  }

  private onError(error: any) {
    // TODO message if 401
    console.log(error);
    this.enableForm = true;
    this.email.reset();
    this.password.reset();
  }
}
