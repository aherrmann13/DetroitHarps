import { Component, Input } from "@angular/core";
import { FormGroup, FormBuilder, Validators, AbstractControl } from "@angular/forms";
import { FormBase } from "../../form.base";
import { RegisterPaymentModel, RegisterPaymentModelPaymentType, RegisterModel } from "../../../../core/client/api.client";

export interface PaymentInformationComponentData {
    type: string
}

@Component({
    selector: 'dh-register-payment-information',
    templateUrl: 'payment-information.component.html',
    styleUrls: [ '../../register.component.scss' ]
})
export class PaymentInformationComponent extends FormBase {
    formGroup: FormGroup;

    cash = RegisterPaymentModelPaymentType.Cash;
    paypal = RegisterPaymentModelPaymentType.Paypal;
    other = RegisterPaymentModelPaymentType.AlreadyPaid;

    private _registration: RegisterModel;

    constructor(formBuilder: FormBuilder) {
        super(formBuilder)
    }

    @Input() set registration(val: RegisterModel) {
        this._registration = val;
        this.updateModel();
    }

    get control(): AbstractControl {
        return this.formGroup ? this.formGroup : null;
    };

    protected buildControl(): void {
        this.formGroup = this.FormBuilder.group({
            paymentType: ['paypal', Validators.required]
          });
    }

    protected updateModel(): void {
        this._registration.payment = new RegisterPaymentModel({
            paymentType: this.formGroup.value.paymentType
        });
    }
}