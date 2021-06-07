import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormArray } from '@angular/forms';
import { configuration } from '../../../../configuration';
import { RegisterChildModelGender } from '../../../../core/client/api.client';

@Component({
  selector: 'dh-register-children-information',
  templateUrl: './children-information.component.html',
  styleUrls: ['../../register.component.scss']
})
export class ChildrenInformationComponent {
  @Input() form: FormArray;

  shirtSizes = configuration.shirtSizes;
  maxDate = new Date();
  male = RegisterChildModelGender.Male;
  female = RegisterChildModelGender.Female;
}
