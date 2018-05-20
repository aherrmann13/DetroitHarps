import { Component, OnInit } from '@angular/core';
import { MatStepper } from '@angular/material';

import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {
  Client,
  RegistrationCreateModel,
  ChildInformationCreateModel,
  RegistrationCreateModelRegistrationType
} from '../../shared/client/api.client';


@Component({
  selector: 'dh-register',
  templateUrl: './register.component.html',
  styleUrls: [ './register.component.scss' ]
})
export class RegisterComponent implements OnInit {
  firstFormGroup: FormGroup;
  secondFormGroup: FormGroup;
  thirdFormGroupArray: FormGroup[];
  fourthFormGroup: FormGroup;
  fifthFormGroup: FormGroup;
  states: string[] = [
    'AK', 'AL', 'AR', 'AZ', 'CA', 'CO', 'CT', 'DE', 'FL', 'GA',
    'HI', 'IA', 'ID', 'IL', 'IN', 'KS', 'KY', 'LA', 'MA', 'MD',
    'ME', 'MI', 'MN', 'MO', 'MS', 'MT', 'NC', 'ND', 'NE', 'NH',
    'NJ', 'NM', 'NV', 'NY', 'OH', 'OK', 'OR', 'PA', 'RI', 'SC',
    'SD', 'TN', 'TX', 'UT', 'VA', 'VT', 'WA', 'WI', 'WV', 'WY'
  ];
  shirtSizes: string[] = ['YXS', 'YS', 'YM', 'YL', 'YXL', 'AS', 'AM'];
  formIndex = 0;
  isRegistering = false;

  constructor(private _formBuilder: FormBuilder, private _client: Client) { }

  ngOnInit() {
    this.firstFormGroup = this._formBuilder.group({
      parentFirstName: ['', Validators.required],
      parentLastName: ['', Validators.required],
      phoneNumber: ['', Validators.required],
      emailAddress: ['', Validators.email]
    });
    this.secondFormGroup = this._formBuilder.group({
      address: ['', Validators.required],
      address2: [''],
      city: ['', Validators.required],
      state: ['', Validators.required],
      zip: ['', Validators.required]
    });
    this.thirdFormGroupArray = [
      this._formBuilder.group({
        childFirstName: ['', Validators.required],
        childLastName: ['', Validators.required],
        childDob: ['', Validators.required],
        childGender: ['', Validators.required],
        childShirtSize: ['', Validators.required]
      })
    ];
    this.fourthFormGroup = this._formBuilder.group({
      comments: ['']
    });
    this.fifthFormGroup = this._formBuilder.group({
      paymentType: ['', Validators.required]
    });

    this.fifthFormGroup.setValue({paymentType: 'paypal'});
  }

  addChild(): void {
    console.log(this.thirdFormGroupArray);
    this.thirdFormGroupArray.push(
      this._formBuilder.group({
      childFirstName: [''],
      childLastName: [''],
      childDob: [''],
      childGender: [''],
      childShirtSize: ['']
    }));
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
    this.isRegistering = true;
    const model = this.getRegistrationCreateModel();

    this._client.register(model).subscribe(
      data => {
        this.isRegistering = false;
      },
      error => console.error(error)
    );

    this.goForward(stepper);
  }

  private getRegistrationCreateModel(): RegistrationCreateModel {
    const registrationCreateModel: RegistrationCreateModel = new RegistrationCreateModel({
      children: this.getRegistrationCreateModelChildren(),
      // TODO integrate stripe
      stripeToken: '',
      registrationType: this.getRegistrationTypeEnum(),
      comments: this.fourthFormGroup.value.comments,
      firstName: this.firstFormGroup.value.parentFirstName,
      lastName: this.firstFormGroup.value.parentLastName,
      emailAddress:  this.firstFormGroup.value.emailAddress,
      phoneNumber: this.firstFormGroup.value.phoneNumber,
      address: this.secondFormGroup.value.address,
      address2: this.secondFormGroup.value.address2,
      city: this.secondFormGroup.value.city,
      state: this.secondFormGroup.value.state,
      zip: this.secondFormGroup.value.zip,
    });
    return registrationCreateModel;
  }

  private getRegistrationCreateModelChildren(): ChildInformationCreateModel[] {
    return this.thirdFormGroupArray.map(x => new ChildInformationCreateModel({
      firstName: x.value.childFirstName,
      lastName: x.value.childLastName,
      gender: x.value.childGender,
      dateOfBirth: x.value.childDob,
      shirtSize: x.value.childShirtSize
    }));
  }

  private getRegistrationTypeEnum(): RegistrationCreateModelRegistrationType {
    switch (this.fifthFormGroup.value.paymentType) {
      case('cash'):
        return RegistrationCreateModelRegistrationType.Cash;
      case('paypal'):
        return RegistrationCreateModelRegistrationType.Paypal;
      case('other'):
      default:
        return RegistrationCreateModelRegistrationType.Other;
    }
  }
}