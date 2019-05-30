import { Component, ViewChild, OnInit } from '@angular/core';
import { MatStepper, MatSnackBar } from '@angular/material';

import { Client, MessageModel, EventModel, RegisterModel } from '../../core/client/api.client';
import { ParentInformationComponent } from './forms/parent-information/parent-information.component';
import { AddressInformationComponent } from './forms/address-information/address-information.component';
import { ChildrenInformationComponent } from './forms/children-information/children-information.component';
import { configuration } from '../../configuration';
import { EventRegistrationComponent } from './forms/children-information/event-registration.component';
import { CommentsComponent } from './forms/comments/comments.component';
import { PaymentInformationComponent } from './forms/payment-information/payment-information.component';
import { Router } from '@angular/router';
import { environment } from '../../../environments/environment';
import { ChildCountSelectorComponent } from './forms/children-information/child-count-selector.component';
import { AbstractControl, FormArray } from '@angular/forms';

@Component({
  selector: 'dh-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  @ViewChild(ParentInformationComponent) parentInformation: ParentInformationComponent;
  @ViewChild(AddressInformationComponent) addressInformation: AddressInformationComponent;
  @ViewChild(ChildrenInformationComponent) childrenInformation: ChildrenInformationComponent;
  @ViewChild(ChildCountSelectorComponent) childCount: ChildCountSelectorComponent;
  @ViewChild(EventRegistrationComponent) eventRegistration: EventRegistrationComponent;
  @ViewChild(CommentsComponent) comments: CommentsComponent;
  @ViewChild(PaymentInformationComponent) paymentInformation: PaymentInformationComponent;

  formIndex = 0;
  isDev = !environment.production;
  isRegistering = false;
  isSendingComment = false;
  year = configuration.year;

  events: Array<EventModel>;
  registerModel: RegisterModel = new RegisterModel({});

  constructor(private _router: Router, private _snackBar: MatSnackBar, private _client: Client) {}

  get childrenInformationControl(): AbstractControl {
    return new FormArray([this.childrenInformation.control, this.childCount.childCount]);
  }

  get paymentType(): string {
    return this.registerModel && this.registerModel.payment ? this.registerModel.payment.paymentType.toString() : null;
  }

  ngOnInit(): void {
    this._client.getRegistrationEvents().subscribe(data => (this.eventRegistration.events = data));
  }

  childCountChanged(change: number): void {
    this.childrenInformation.childCount = change;
    this.eventRegistration.childCount = change;
  }

  goBack(stepper: MatStepper): void {
    stepper.previous();
  }

  goForward(stepper: MatStepper): void {
    stepper.next();
  }

  step(stepper: MatStepper): void {
    // mat stepper doesnt update till 1 ms after change
    setTimeout(() => (this.formIndex = stepper._getFocusIndex()), 1);
  }

  register(stepper: MatStepper): void {
    this.goForward(stepper);
    this.isRegistering = true;
    const message = this.getMessage();

    if (message.body && message.body !== '') {
      this.isSendingComment = true;
      this._client.contact(message).subscribe(() => (this.isSendingComment = false), this.registrationError);
    }
    this._client
      .register(this.registerModel)
      .subscribe(() => (this.isRegistering = false), err => this.registrationError(err));
  }

  private getMessage(): MessageModel {
    return new MessageModel({
      firstName: this.registerModel.parent.firstName,
      lastName: this.registerModel.parent.lastName,
      email: this.registerModel.contactInformation.email,
      body: this.comments.data
    });
  }

  private registrationError(err: any) {
    this._snackBar.open('error during registration, please try again', 'Dismiss', { duration: 10000 });
    console.error(err);
    this._router.navigate(['/']);
  }
}
