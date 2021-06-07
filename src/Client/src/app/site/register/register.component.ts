import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatStepper } from '@angular/material/stepper';

import {
  Client,
  MessageModel,
  EventModel,
  RegisterModel,
  RegisterPaymentModelPaymentType,
  RegisterParentModel,
  RegisterContactInformationModel,
  RegisterChildModel,
  RegisterChildEventModel,
  RegisterChildModelGender,
  RegisterChildEventModelAnswer,
  RegisterPaymentModel
} from '../../core/client/api.client';
import { configuration } from '../../configuration';
import { Router } from '@angular/router';
import { environment } from '../../../environments/environment';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { valueIsState } from './forms/address-information/states';

type FormDataStructure = {
  address?: {
    address: string;
    address2: string;
    city: string;
    state: string;
    zip: string;
  };
  children?: {
    childDob: Date;
    childFirstName: string;
    childGender: RegisterChildModelGender;
    childLastName: string;
    childShirtSize: string;
  }[];
  events?: {
    [key: number]: RegisterChildEventModelAnswer;
  }[];
  parent?: {
    emailAddress: string;
    parentFirstName: string;
    parentLastName: string;
    phoneNumber: string;
  };
  paymentInformation?: {
    paymentType: RegisterPaymentModelPaymentType;
  };
};

@Component({
  selector: 'dh-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  form: FormGroup;

  isDev = !environment.production;
  year = configuration.year;
  isRegistering = false;
  isSendingComment = false;

  events: Array<EventModel> = [];
  registerModel: RegisterModel = new RegisterModel({});

  constructor(
    private router: Router,
    private snackBar: MatSnackBar,
    private client: Client,
    private formBuilder: FormBuilder
  ) {}

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      parent: this.formBuilder.group({
        parentFirstName: ['', Validators.required],
        parentLastName: ['', Validators.required],
        phoneNumber: ['', Validators.required],
        emailAddress: ['', Validators.email]
      }),
      address: this.formBuilder.group({
        address: ['', Validators.required],
        address2: [''],
        city: ['', Validators.required],
        state: ['', [Validators.required, valueIsState]],
        zip: ['', Validators.required]
      }),
      childCount: this.formBuilder.control(0, [Validators.required]),
      children: this.formBuilder.array([]),
      events: this.formBuilder.array([]),
      paymentInformation: this.formBuilder.group({
        paymentType: [RegisterPaymentModelPaymentType.Paypal, Validators.required]
      }),
      comments: this.formBuilder.control('', [])
    });

    this.client.getRegistrationEvents().subscribe(this.onEventSuccess.bind(this));
    this.form.controls.childCount.valueChanges.subscribe(this.childCountUpdate.bind(this));
    this.form.valueChanges.subscribe(this.formUpdate.bind(this));
    this.form.controls.childCount.setValue(1); // we want the subscription to trigger
  }

  get payment(): RegisterPaymentModelPaymentType {
    return this.registerModel?.payment?.paymentType;
  }

  get children(): RegisterChildModel[] {
    return this.registerModel?.children ?? [];
  }

  goBack(stepper: MatStepper): void {
    stepper.previous();
  }

  goForward(stepper: MatStepper): void {
    stepper.next();
  }

  stepIndex(stepper: MatStepper): number {
    return stepper.selectedIndex;
  }

  register(stepper: MatStepper): void {
    this.goForward(stepper);
    this.isRegistering = true;
    const message = this.form.controls.comments.value;
    if (message && message !== '') {
      this.isSendingComment = true;
      this.client
        .contact(
          new MessageModel({
            firstName: this.registerModel?.parent?.firstName,
            lastName: this.registerModel?.parent?.lastName,
            email: this.registerModel?.contactInformation?.email,
            body: message
          })
        )
        .subscribe(() => (this.isSendingComment = false), this.registrationError.bind(this));
    }
    this.client
      .register(this.registerModel)
      .subscribe(() => (this.isRegistering = false), this.registrationError.bind(this));
  }

  private childForm() {
    return this.formBuilder.group({
      childFirstName: ['', Validators.required],
      childLastName: ['', Validators.required],
      childDob: ['', Validators.required],
      childGender: ['', Validators.required],
      childShirtSize: ['', Validators.required]
    });
  }

  private eventForm() {
    return this.formBuilder.group(this.events.reduce((acc, curr) => ({ [curr.id]: [], ...acc }), {}));
  }

  private registrationError(err: any) {
    this.snackBar.open('error during registration, please try again', 'Dismiss', { duration: 10000 });
    console.error(err);
    this.router.navigate(['/']);
  }

  // TODO: fix this
  private onEventSuccess(events: Array<EventModel>): void {
    console.log(events);
    this.events = events;
    const eventArray = this.form.controls.events as FormArray;
    eventArray.controls = eventArray.controls.map(() => this.eventForm());
  }

  // TODO: make this functional
  private childCountUpdate(newCount: number): void {
    const childForm = this.form.controls.children as FormArray;
    const eventForm = this.form.controls.events as FormArray;
    const difference = newCount - (this.form.controls.children as FormArray).length;
    if (difference > 0) {
      for (let i = 0; i < difference; i++) {
        childForm.push(this.childForm());
        eventForm.push(this.eventForm());
      }
    } else {
      for (let i = difference; i < 0; i++) {
        childForm.removeAt(childForm.length - 1);
        eventForm.removeAt(eventForm.length - 1);
      }
    }
  }

  // TODO: cleanup
  private formUpdate(value: FormDataStructure): void {
    this.registerModel = new RegisterModel({
      parent: new RegisterParentModel({
        firstName: value?.parent?.parentFirstName,
        lastName: value?.parent?.parentLastName
      }),
      contactInformation: new RegisterContactInformationModel({
        email: value?.parent?.emailAddress,
        phoneNumber: value?.parent?.phoneNumber,
        address: value.address?.address,
        address2: value.address?.address2,
        city: value.address?.city,
        state: value.address?.state,
        zip: value.address?.zip
      }),
      children: value?.children.map((child, i) => {
        return new RegisterChildModel({
          firstName: child?.childFirstName,
          lastName: child?.childFirstName,
          gender: child?.childGender,
          dateOfBirth: child?.childDob,
          shirtSize: child?.childShirtSize,
          events: !!value?.events?.[i]
            ? Object.entries(value.events[i]).map(entry => {
                const [id, answer] = entry;
                return new RegisterChildEventModel({
                  eventId: (id as unknown) as number,
                  answer: answer
                });
              })
            : []
        });
      }),
      payment: new RegisterPaymentModel({
        paymentType: value?.paymentInformation?.paymentType
      })
    });
  }
}
