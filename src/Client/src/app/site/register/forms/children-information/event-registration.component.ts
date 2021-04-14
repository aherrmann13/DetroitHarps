import { Component } from '@angular/core';
import {
  EventModel,
  RegisterChildModel,
  RegisterChildEventModel,
  RegisterChildEventModelAnswer
} from '../../../../core/client/api.client';
import { Validators, AbstractControl, FormBuilder, FormArray } from '@angular/forms';
import { FormBaseDirective } from '../../form-base.directive';

@Component({
  selector: 'dh-register-events',
  templateUrl: './event-registration.component.html',
  styleUrls: ['../../register.component.scss']
})
export class EventRegistrationComponent extends FormBaseDirective {
  options = [RegisterChildEventModelAnswer.Yes, RegisterChildEventModelAnswer.No, RegisterChildEventModelAnswer.Maybe];

  yes = RegisterChildEventModelAnswer.Yes;
  no = RegisterChildEventModelAnswer.No;
  maybe = RegisterChildEventModelAnswer.Maybe;

  formArray: FormArray;
  eventsLoaded = false;
  formsLoaded = false;
  private _events: Array<EventModel>;
  private _formGroupProperties = {};

  constructor(formBuilder: FormBuilder) {
    super(formBuilder);
  }

  set events(val: Array<EventModel>) {
    this._events = val;
    this.eventsLoaded = true;
    this.rebuildFormGroupProperties();
    this.rebuildFormArray();
  }

  set childCount(count: number) {
    const difference = count - this.formArray.controls.length;

    if (difference < 0) {
      for (let i = 0; i < difference * -1; i++) {
        this.formArray.removeAt(this.formArray.length - 1);
      }
    } else {
      for (let i = 0; i < difference; i++) {
        this.formArray.push(this.formBuilder.group(this.formGroupProperties));
      }
    }
  }

  get control(): AbstractControl {
    return this.formArray ? this.formArray : null;
  }

  get events(): Array<EventModel> {
    return this._events;
  }

  getChildName(index: number): string {
    return this.registration && this.registration.children && this.registration.children[index]
      ? this.registration.children[index].firstName
      : null;
  }

  protected get formGroupProperties(): any {
    return this._formGroupProperties;
  }

  protected buildControl(): void {
    this.formArray = this.formBuilder.array([]);
  }

  protected updateModel(): void {
    this.registration.children = this.formArray.controls.map((x, i) => {
      const existingData = this.registration && this.registration.children ? this.registration.children[i] : null;
      return new RegisterChildModel({
        ...existingData,
        events: this.getEvents(x)
      });
    });
  }

  private getEvents(control: AbstractControl): Array<RegisterChildEventModel> {
    return Object.keys(control.value).map(
      eventId =>
        new RegisterChildEventModel({
          eventId: parseInt(eventId, 10),
          answer: control.value[eventId]
        })
    );
  }

  private rebuildFormGroupProperties() {
    const form: {
      [key: string]: any;
    } = {};
    for (const event of this._events) {
      form[event.id] = ['', Validators.required];
    }
    this._formGroupProperties = form;
  }

  private rebuildFormArray() {
    const formGroups = [];
    const formArrayLength = this.formArray.length;
    for (let i = 0; i < formArrayLength; i++) {
      formGroups.push(this.formBuilder.group(this.formGroupProperties));
    }
    this.formArray = this.formBuilder.array(formGroups);
    this.formArray.valueChanges.subscribe(x => {
      if (this.isControlValid()) {
        this.updateModel();
      }
    });
    this.formsLoaded = true;
  }
}
