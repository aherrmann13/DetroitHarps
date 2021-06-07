import { Component, Input } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'dh-register-parent-information',
  templateUrl: 'parent-information.component.html',
  styleUrls: ['../../register.component.scss']
})
export class ParentInformationComponent {
  @Input() form: FormGroup;
}
