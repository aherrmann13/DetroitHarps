import { Component, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';

import { Client, ContactModel } from '../../shared/client/api.client';
import { Router } from '@angular/router';

@Component({
  selector: 'dh-contact',
  templateUrl: './contact.component.html',
  styleUrls: [ ]
})
export class ContactComponent implements OnInit {

  // TODO this should be one form
  name = new FormControl('', [Validators.required]);
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
    return !this.name.invalid &&
      !this.email.invalid &&
      !this.message.invalid &&
      this.enableForm;
  }

  send(): void {
    this.enableForm = false;
    const model = new ContactModel({
      email: this.email.value,
      name: this.name.value,
      message: this.message.value
    });
    this._client.contact(model).subscribe(
      data => this.onComplete(),
      error => console.log(error)
    );
  }

  private onComplete() {
    this.enableForm = true;
    this._router.navigate(['/']);
  }


}
