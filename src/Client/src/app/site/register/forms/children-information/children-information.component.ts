import { Component } from '@angular/core';
import { FormBuilder, Validators, FormArray, AbstractControl } from '@angular/forms';

import { configuration } from '../../../../configuration';
import { RegisterChildModel, RegisterChildModelGender } from '../../../../core/client/api.client';
import { FormBaseDirective } from '../../form-base.directive';

@Component({
  selector: 'dh-register-children-information',
  templateUrl: './children-information.component.html',
  styleUrls: ['../../register.component.scss']
})
export class ChildrenInformationComponent extends FormBaseDirective {
  shirtSizes = configuration.shirtSizes;

  male = RegisterChildModelGender.Male;
  female = RegisterChildModelGender.Female;

  formArray: FormArray;

  constructor(formBuilder: FormBuilder) {
    super(formBuilder);
  }

  get maxDate(): Date {
    return new Date();
  }

  get control(): AbstractControl {
    return this.formArray ? this.formArray : null;
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

  protected get formGroupProperties(): any {
    return {
      childFirstName: ['', Validators.required],
      childLastName: ['', Validators.required],
      childDob: ['', Validators.required],
      childGender: ['', Validators.required],
      childShirtSize: ['', Validators.required]
    };
  }

  protected buildControl(): void {
    this.formArray = new FormArray([]);
  }

  protected updateModel(): void {
    this.registration.children = this.formArray.controls.map((x, i) => {
      const existingData = this.registration && this.registration.children ? this.registration.children[i] : null;
      return new RegisterChildModel({
        ...existingData,
        firstName: x.value.childFirstName,
        lastName: x.value.childLastName,
        dateOfBirth: x.value.childDob,
        gender: x.value.childGender,
        shirtSize: x.value.childShirtSize
      });
    });
  }
}
