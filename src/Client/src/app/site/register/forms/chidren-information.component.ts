import { Component } from "@angular/core";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";

export interface ChildrenInformationComponentData {
    childFirstName: string,
    childLastName: string,
    childDob: Date,
    childGender: ChildGender,
    childShirtSize: string
}

export enum ChildGender {
    Male = 0,
    Female = 1
}

@Component({
    selector: 'dh-register-children-information',
    template: `
    <form *ngFor='let formGroup of formGroups' [formGroup]='formGroup'>
        <mat-form-field>
            <input matInput placeholder='Child First name' formControlName='childFirstName'>
        </mat-form-field>
        <br />
        <mat-form-field>
            <input matInput placeholder='Child Last name' formControlName='childLastName'>
        </mat-form-field>
        <br />
        <mat-form-field>
            <input matInput [matDatepicker]='birthDatePicker' placeholder='Child Birth Date' formControlName='childDob'>
            <mat-datepicker-toggle matSuffix [for]='birthDatePicker'></mat-datepicker-toggle>
            <mat-datepicker #birthDatePicker></mat-datepicker>
        </mat-form-field>
        <br />
        <mat-radio-group formControlName='childGender'>
            <mat-radio-button value='male'>Male </mat-radio-button>
            <mat-radio-button value='female'>Female </mat-radio-button>
        </mat-radio-group>
        <br />
        <mat-form-field>
            <mat-select placeholder='Shirt Size' formControlName='childShirtSize'>
                <mat-option *ngFor='let shirtSize of shirtSizes' value='{{shirtSize}}'>
                    {{shirtSize}}
                </mat-option>
            </mat-select>
        </mat-form-field>
    </form>`,
    styleUrls: [ '../register.component.scss' ]
})
export class ChildrenInformationComponent {
    
    formGroups: Array<FormGroup>;
    firstForm: FormGroup;
    shirtSizes: string[] = ['YXS', 'YS', 'YM', 'YL', 'YXL', 'AS', 'AM'];

    get data(): Array<ChildrenInformationComponentData> {
        // TODO: is this instant access every time it is called?
        return this.formGroups.map(x => <ChildrenInformationComponentData>{
            childFirstName: x.value.childFirstName,
            childLastName: x.value.childLastName,
            childDob: x.value.childDob,
            childGender: x.value.childGender === 'male' ? ChildGender.Male : ChildGender.Female,
            childShirtSize: x.value.childShirtSize
        });
    }

    constructor(private _formBuilder: FormBuilder) {
        // TODO: can this be moved out of ctor?
        // based on this https://stackblitz.com/edit/angular-material-stepper-with-component-steps
        // and based on https://stackoverflow.com/questions/48498966/angular-material-stepper-component-for-each-step?rq=1
        this.firstForm = _formBuilder.group({
            childFirstName: ['', Validators.required],
            childLastName: ['', Validators.required],
            childDob: ['', Validators.required],
            childGender: ['', Validators.required],
            childShirtSize: ['', Validators.required]
          })
        this.formGroups = [ this.firstForm ];
    }

    addChild(): void {
        this.formGroups.push(
          this._formBuilder.group({
          childFirstName: [''],
          childLastName: [''],
          childDob: [''],
          childGender: [''],
          childShirtSize: ['']
        }));
      }
}