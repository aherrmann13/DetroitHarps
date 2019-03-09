import { Component } from "@angular/core";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";

export interface PaymentInformationComponentData {
    type: string
}

@Component({
    selector: 'dh-register-payment-information',
    template: `
        <form [formGroup]='formGroup'>                 
            <mat-radio-group formControlName='paymentType'>
                <br />
                <mat-radio-button value='cash'>Cash/Check</mat-radio-button>
                <br />
                <br />
                <mat-radio-button value='paypal'>Paypal</mat-radio-button>
                <br />
                <br />
                <mat-radio-button value='other'>Already paid - adding child</mat-radio-button>
                <br />
                <br />
            </mat-radio-group>
            <p *ngIf='formGroup.value.paymentType === "cash"'>
                If paying by check please make out to "Detroit Youth GAA"
                and send to 28156 North Clement Circle, Livonia MI 48150.
                For cash payments catch up with us this summer.
            </p>
            <p *ngIf='formGroup.value.paymentType === "paypal"'>
                Link to pay will be on next page
            </p>
        </form>`,
    styleUrls: [ '../register.component.scss' ]
})
export class PaymentInformationComponent {
    
    formGroup: FormGroup;

    get data(): PaymentInformationComponentData {
        return { type: this.formGroup.value.paymentType }
    }

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