import { Component } from '@angular/core';
import { FormGroup, FormBuilder, Validators, AbstractControl } from '@angular/forms';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { FormBase } from '../../form.base';
import { RegisterContactInformationModel } from '../../../../core/client/api.client';

export const STATES: string[] = [
  'AK',
  'AL',
  'AR',
  'AZ',
  'CA',
  'CO',
  'CT',
  'DE',
  'FL',
  'GA',
  'HI',
  'IA',
  'ID',
  'IL',
  'IN',
  'KS',
  'KY',
  'LA',
  'MA',
  'MD',
  'ME',
  'MI',
  'MN',
  'MO',
  'MS',
  'MT',
  'NC',
  'ND',
  'NE',
  'NH',
  'NJ',
  'NM',
  'NV',
  'NY',
  'OH',
  'OK',
  'OR',
  'PA',
  'RI',
  'SC',
  'SD',
  'TN',
  'TX',
  'UT',
  'VA',
  'VT',
  'WA',
  'WI',
  'WV',
  'WY'
];

@Component({
  selector: 'dh-register-address-information',
  templateUrl: './address-information.component.html',
  styleUrls: ['../../register.component.scss']
})
export class AddressInformationComponent extends FormBase {
  formGroup: FormGroup;

  filteredStates: Observable<string[]>;

  constructor(formBuilder: FormBuilder) {
    super(formBuilder);
  }

  get control(): AbstractControl {
    return this.formGroup ? this.formGroup : null;
  }

  protected buildControl(): void {
    this.formGroup = this.formBuilder.group({
      address: ['', Validators.required],
      address2: [''],
      city: ['', Validators.required],
      state: ['', [Validators.required, x => this.valueIsState(x)]],
      zip: ['', Validators.required]
    });
    this.filteredStates = this.formGroup.controls.state.valueChanges.pipe(
      startWith(''),
      map(value => this.filter(value))
    );
  }

  protected updateModel(): void {
    this.registration.contactInformation = new RegisterContactInformationModel({
      ...this.registration.contactInformation,
      address: this.formGroup.value.address,
      address2: this.formGroup.value.address2,
      city: this.formGroup.value.city,
      state: this.formGroup.value.state,
      zip: this.formGroup.value.zip
    });
  }

  private filter(value: string): string[] {
    const filterValue = value.toLowerCase();
    return STATES.filter(state => state.toLowerCase().indexOf(filterValue) === 0);
  }

  private valueIsState(control: AbstractControl) {
    return STATES.indexOf(control.value) === -1 ? { forbiddenValue: true } : null;
  }

  // https://github.com/angular/material2/issues/3414
  fixStateAutoFill(state: Event) {
    this.formGroup.controls.state.setValue((state.target as HTMLInputElement).value);
  }
}
