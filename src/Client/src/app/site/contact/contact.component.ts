import { Component, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';

import { Client, MessageModel } from '../../shared/client/api.client';
import { Router } from '@angular/router';

@Component({
  selector: 'dh-contact',
  templateUrl: './contact.component.html',
  styleUrls: [ ]
})
export class ContactComponent implements OnInit {

  // TODO this should be one form
  firstName = new FormControl('', [Validators.required]);
  lastName = new FormControl('', [Validators.required]);
  email = new FormControl('', [Validators.required, Validators.email]);
  message = new FormControl('', [Validators.required]);

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
    return !this.firstName.invalid &&
      !this.lastName.invalid &&
      !this.email.invalid &&
      !this.message.invalid &&
      this.enableForm;
  }

  send(): void {
    this.enableForm = false;
    const model = new MessageModel({
      email: this.email.value,
      firstName: this.firstName.value,
      lastName: this.lastName.value,
      body: this.message.value
    });
    this._client.contact(model).subscribe(
      _ => this.onComplete(),
      error => console.log(error)
    );
  }

  private onComplete() {
    this.enableForm = true;
    this._router.navigate(['/']);
  }


}
