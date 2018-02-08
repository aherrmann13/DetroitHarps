import { Component, OnInit } from '@angular/core';
import { MatStepper } from '@angular/material';

import { FormBuilder, FormGroup, Validators } from '@angular/forms';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: [ './register.component.scss' ]
})
export class RegisterComponent implements OnInit {
  firstFormGroup: FormGroup;
  secondFormGroup: FormGroup;
  thirdFormGroupArray: FormGroup[];
  states: string[] = [
    "AK","AL","AR","AZ","CA","CO","CT","DE","FL","GA",
    "HI","IA","ID","IL","IN","KS","KY","LA","MA","MD",
    "ME","MI","MN","MO","MS","MT","NC","ND","NE","NH",
    "NJ","NM","NV","NY","OH","OK","OR","PA","RI","SC",
    "SD","TN","TX","UT","VA","VT","WA","WI","WV","WY"
  ];
  shirtSizes: string[] = ["YXS","YS","YM","YL","YXL","AS","AM"];
  formIndex: number = 0;

  constructor(private _formBuilder: FormBuilder) { }

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
  }

  addChild(): void{
    console.log(this.thirdFormGroupArray);
    this.thirdFormGroupArray.push(
      this._formBuilder.group({
      childFirstName: [''],
      childLastName: [''],
      childDob: [''],
      childGender: [''],
      childShirtSize: ['']
    }))
  }

  goBack(stepper: MatStepper): void {
    stepper.previous();
  }

  goForward(stepper: MatStepper): void {
    stepper.next();
  }

  test(stepper: MatStepper): void {
    // mat stepper doesnt update till 1 ms after change
    setTimeout(() => this.formIndex = stepper._focusIndex, 1);
  }
}