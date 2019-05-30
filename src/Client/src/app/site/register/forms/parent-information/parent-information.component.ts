import { Component } from '@angular/core';
import { FormGroup, AbstractControl, Validators, FormBuilder } from '@angular/forms';

import { FormBase } from '../../form.base';
import { RegisterParentModel, RegisterContactInformationModel } from '../../../../core/client/api.client';

@Component({
  selector: 'dh-register-parent-information',
  templateUrl: 'parent-information.component.html',
  styleUrls: ['../../register.component.scss']
})
export class ParentInformationComponent extends FormBase {
  formGroup: FormGroup;

  constructor(formBuilder: FormBuilder) {
    super(formBuilder);
  }

  get control(): AbstractControl {
    return this.formGroup ? this.formGroup : null;
  }

  protected buildControl(): void {
    this.formGroup = this.formBuilder.group({
      parentFirstName: ['', Validators.required],
      parentLastName: ['', Validators.required],
      phoneNumber: ['', Validators.required],
      emailAddress: ['', Validators.email]
    });
  }

  protected updateModel(): void {
    this.registration.parent = new RegisterParentModel({
      firstName: this.formGroup.value.parentFirstName,
      lastName: this.formGroup.value.parentLastName
    });
    this.registration.contactInformation = new RegisterContactInformationModel({
      ...this.registration.contactInformation,
      email: this.formGroup.value.emailAddress,
      phoneNumber: this.formGroup.value.phoneNumber
    });
  }
}
