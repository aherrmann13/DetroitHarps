import { Component, ViewChild } from '@angular/core';
import { MatStepper, MatSnackBar } from '@angular/material';

import { Client, MessageModel } from '../../core/client/api.client';
import { ParentInformationComponent } from './forms/parent-information/parent-information.component';
import { AddressInformationComponent } from './forms/address-information/address-information.component';
import { ChildrenInformationComponent } from './forms/children-information/children-information.component';
import { CommentsComponent } from './forms/comments/comments.component';
import { PaymentInformationComponent } from './forms/payment-information/payment-information.component';
import { RegisterService } from './register.service';
import { Router } from '@angular/router';


@Component({
  selector: 'dh-register',
  templateUrl: './register.component.html',
  styleUrls: [ './register.component.scss' ]
})
export class RegisterComponent {
  @ViewChild(ParentInformationComponent) parentInformation: ParentInformationComponent;
  @ViewChild(AddressInformationComponent) addressInformation: AddressInformationComponent;
  @ViewChild(ChildrenInformationComponent) childrenInformation: ChildrenInformationComponent;
  @ViewChild(CommentsComponent) comments: CommentsComponent;
  @ViewChild(PaymentInformationComponent) paymentInformation: PaymentInformationComponent;

  get parentInformationFormGroup() {
    return this.parentInformation ? this.parentInformation.formGroup : null;
  }

  get addressInformationFormGroup() {
    return this.addressInformation ? this.addressInformation.formGroup : null;
  }

  get childrenInformationFormGroup() {
    return this.childrenInformation ? this.childrenInformation.firstForm : null;
  }

  get commentsFormGroup() {
    return this.comments ? this.comments.formGroup : null;
  }

  get paymentInformationFormGroup() {
    return this.paymentInformation ? this.paymentInformation.formGroup : null;
  }

  formIndex = 0;
  isRegistering = false;
  isSendingComment = false;

  constructor(
    private _router: Router,
    private _snackBar: MatSnackBar,
    private _client: Client,
    private _registerService: RegisterService) {
      this.registrationError = this.registrationError.bind(this);
    }

  addChild(): void {
    this.childrenInformation.addChild();
  }

  goBack(stepper: MatStepper): void {
    stepper.previous();
  }

  goForward(stepper: MatStepper): void {
    stepper.next();
  }

  step(stepper: MatStepper): void {
    // mat stepper doesnt update till 1 ms after change
    setTimeout(() => this.formIndex = stepper._getFocusIndex(), 1);
  }

  register(stepper: MatStepper): void {
    this.goForward(stepper);
    this.isRegistering = true;
    const message = this.getMessage();

    if(message.body && message.body !== ''){
      this.isSendingComment = true;
      this._client.contact(message).subscribe(
        () => this.isSendingComment = false,
        this.registrationError
      );
    }

    this._registerService.register(
      this.parentInformation.data,
      this.addressInformation.data,
      this.childrenInformation.data,
      this.paymentInformation.data
    ).subscribe(
      () => this.isRegistering = false,
      this.registrationError
    );
  }

  private getMessage(): MessageModel {
    return new MessageModel({
      firstName: this.parentInformation.data.parentFirstName,
      lastName: this.parentInformation.data.parentLastName,
      email: this.parentInformation.data.emailAddress,
      body: this.comments.data
    });  
  }

  private registrationError(err: any)  {
    this._snackBar.open(
      "error during registration, please try again",
      "Dismiss",
      { duration: 10000 });
    console.error(err);
    this._router.navigate(['/']);
  }
}
