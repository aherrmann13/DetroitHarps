import { Component } from "@angular/core";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";

export interface AddressInformationComponentData {
    address: string,
    address2: string,
    city: string,
    state: string,
    zip: string
}

@Component({
    selector: 'dh-register-address-information',
    template: `
    <form [formGroup]='formGroup'>                    
        <mat-form-field>
            <input matInput placeholder='Street Address' formControlName='address' required>
        </mat-form-field>
        <br />
        <mat-form-field>
            <input matInput placeholder='Street Address 2' formControlName='address2'>
        </mat-form-field>
        <br />
        <mat-form-field>
            <input matInput placeholder='City' formControlName='city' required>
        </mat-form-field>
        <br />
        <mat-form-field>
            <mat-select placeholder='State' formControlName='state' required>
                <mat-option *ngFor='let state of states' value='{{state}}'>
                    {{state}}
                </mat-option>
            </mat-select>
        </mat-form-field>
        <br />
        <mat-form-field>
            <input matInput placeholder='Zip' formControlName='zip' required>
        </mat-form-field>
    </form>`,
    styleUrls: [ '../register.component.scss' ]
})
export class AddressInformationComponent {
    
    formGroup: FormGroup;
    states: string[] = [
        'AK', 'AL', 'AR', 'AZ', 'CA', 'CO', 'CT', 'DE', 'FL', 'GA',
        'HI', 'IA', 'ID', 'IL', 'IN', 'KS', 'KY', 'LA', 'MA', 'MD',
        'ME', 'MI', 'MN', 'MO', 'MS', 'MT', 'NC', 'ND', 'NE', 'NH',
        'NJ', 'NM', 'NV', 'NY', 'OH', 'OK', 'OR', 'PA', 'RI', 'SC',
        'SD', 'TN', 'TX', 'UT', 'VA', 'VT', 'WA', 'WI', 'WV', 'WY'
      ];

    get data(): AddressInformationComponentData {
        // TODO: is this instant access every time it is called?
        return {
            address: this.formGroup.value.address,
            address2: this.formGroup.value.address2,
            city: this.formGroup.value.city,
            state: this.formGroup.value.state,
            zip: this.formGroup.value.zip,
        }
    }

    constructor(formBuilder: FormBuilder) {
        // TODO: can this be moved out of ctor?
        // based on this https://stackblitz.com/edit/angular-material-stepper-with-component-steps
        // and based on https://stackoverflow.com/questions/48498966/angular-material-stepper-component-for-each-step?rq=1
        this.formGroup = formBuilder.group({
            address: ['', Validators.required],
            address2: [''],
            city: ['', Validators.required],
            state: ['', Validators.required],
            zip: ['', Validators.required]
          });
    }
}