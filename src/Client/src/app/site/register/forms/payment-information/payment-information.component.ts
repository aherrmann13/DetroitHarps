import { Component } from "@angular/core";
import { FormGroup, FormBuilder, Validators, AbstractControl } from "@angular/forms";

export interface PaymentInformationComponentData {
    type: string
}

@Component({
    selector: 'dh-register-payment-information',
    templateUrl: 'payment-information.component.html',
    styleUrls: [ '../../register.component.scss' ]
})
export class PaymentInformationComponent {
    
    formGroup: FormGroup;

    get data(): PaymentInformationComponentData {
        return { type: this.formGroup.value.paymentType }
    }

    get control(): AbstractControl {
        return this.formGroup ? this.formGroup : null;
    };

    constructor(formBuilder: FormBuilder) {
        // TODO: can this be moved out of ctor?
        // based on this https://stackblitz.com/edit/angular-material-stepper-with-component-steps
        // and based on https://stackoverflow.com/questions/48498966/angular-material-stepper-component-for-each-step?rq=1
        this.formGroup = formBuilder.group({
            paymentType: ['', Validators.required]
          });
      
        this.formGroup.setValue({paymentType: 'paypal'});
    }
}