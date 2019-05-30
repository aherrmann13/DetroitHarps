import { Component, Input } from '@angular/core';
import { configuration } from '../../../../configuration';

@Component({
  selector: 'dh-register-final-step',
  templateUrl: 'final-step.component.html',
  styleUrls: ['../../register.component.scss']
})
export class FinalStepComponent {
  @Input() loading = false;
  @Input() paymentType: string;

  year = configuration.year;
}
