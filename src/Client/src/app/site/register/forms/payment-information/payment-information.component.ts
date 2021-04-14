import { Component, Input, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, AbstractControl } from '@angular/forms';
import { FormBaseDirective } from '../../form-base.directive';
import { RegisterPaymentModel, RegisterPaymentModelPaymentType } from '../../../../core/client/api.client';

export interface PaymentInformationComponentData {
  type: string;
}

@Component({
  selector: 'dh-register-payment-information',
  templateUrl: 'payment-information.component.html',
  styleUrls: ['../../register.component.scss']
})
export class PaymentInformationComponent extends FormBaseDirective implements OnInit {
  formGroup: FormGroup;

  cash = RegisterPaymentModelPaymentType.Cash;
  paypal = RegisterPaymentModelPaymentType.Paypal;
  other = RegisterPaymentModelPaymentType.AlreadyPaid;

  constructor(formBuilder: FormBuilder) {
    super(formBuilder);
  }

  ngOnInit(): void {
    super.ngOnInit();
    this.updateModel();
  }

  get control(): AbstractControl {
    return this.formGroup ? this.formGroup : null;
  }

  protected buildControl(): void {
    this.formGroup = this.formBuilder.group({
      paymentType: ['paypal', Validators.required]
    });
  }

  protected updateModel(): void {
    this.registration.payment = new RegisterPaymentModel({
      paymentType: this.formGroup.value.paymentType
    });
  }
}
