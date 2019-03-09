import { Component } from "@angular/core";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";

export interface ParentInformationComponentData {
    parentFirstName: string,
    parentLastName: string,
    phoneNumber: string,
    emailAddress: string
}

@Component({
    selector: 'dh-register-parent-information',
    template: `
        <form [formGroup]='formGroup'>
            <mat-form-field>
                <input matInput placeholder='Parent first name' formControlName='parentFirstName' required>
            </mat-form-field>
            <br />
            <mat-form-field>
                <input matInput placeholder='Parent last name' formControlName='parentLastName' required>
            </mat-form-field>
            <br />
            <mat-form-field>
                <input matInput placeholder='Phone Number' formControlName='phoneNumber' required>
            </mat-form-field>
            <br />
            <mat-form-field>
                <input matInput placeholder='Email' formControlName='emailAddress' required>
            </mat-form-field>
        </form>`,
    styleUrls: [ '../register.component.scss' ]
})
export class ParentInformationComponent {
    
    formGroup: FormGroup;

    get data(): ParentInformationComponentData {
        // TODO: is this instant access every time it is called?
        return {
            parentFirstName: this.formGroup.value.parentFirstName,
            parentLastName: this.formGroup.value.parentLastName,
            phoneNumber: this.formGroup.value.phoneNumber,
            emailAddress: this.formGroup.value.emailAddress,
        }
    }

    constructor(formBuilder: FormBuilder) {
        // TODO: can this be moved out of ctor?
        // based on this https://stackblitz.com/edit/angular-material-stepper-with-component-steps
        // and based on https://stackoverflow.com/questions/48498966/angular-material-stepper-component-for-each-step?rq=1
        this.formGroup = formBuilder.group({
            parentFirstName: ['', Validators.required],
            parentLastName: ['', Validators.required],
            phoneNumber: ['', Validators.required],
            emailAddress: ['', Validators.email]
          });
    }
}