import { Component, Input } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { RegisterPaymentModelPaymentType } from '../../../../core/client/api.client';

export interface PaymentInformationComponentData {
  type: string;
}

@Component({
  selector: 'dh-register-payment-information',
  templateUrl: 'payment-information.component.html',
  styleUrls: ['../../register.component.scss']
})
export class PaymentInformationComponent {
  @Input() form: FormGroup;

  cash = RegisterPaymentModelPaymentType.Cash;
  paypal = RegisterPaymentModelPaymentType.Paypal;
  other = RegisterPaymentModelPaymentType.AlreadyPaid;
}
