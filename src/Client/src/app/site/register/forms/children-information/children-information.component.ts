import { Component } from "@angular/core";
import { FormBuilder, Validators, FormArray } from "@angular/forms";
import { MatSelectChange } from "@angular/material";

import { configuration } from "../../../../configuration";

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
    templateUrl: './children-information.component.html',
    styleUrls: [ '../../register.component.scss' ]
})
export class ChildrenInformationComponent {
    
    formArray: FormArray;
    shirtSizes = configuration.shirtSizes;
    numberOfChildrenSelector: Array<number> = [ 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 ]
    private childFormProperties = {
        childFirstName: ['', Validators.required],
        childLastName: ['', Validators.required],
        childDob: ['', Validators.required],
        childGender: ['', Validators.required],
        childShirtSize: ['', Validators.required]
      }

    get maxDate(): Date {
        return new Date();
    }

    get data(): Array<ChildrenInformationComponentData> {
        // TODO: is this instant access every time it is called?
        return this.formArray.controls.map(x => <ChildrenInformationComponentData>{
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
        this.formArray = new FormArray([ _formBuilder.group(this.childFormProperties) ]);
    }

    numberOfChildrenChanged(change: MatSelectChange): void {
        const currentNumberOfChildren = this.formArray.length;
        const newNumberOfChildren: number = change.value;
        const difference = newNumberOfChildren - currentNumberOfChildren;

        if(difference < 0) {
            const childrenToRemove = difference * -1;
            for(let i = 0; i < childrenToRemove; i++) {
                this.removeChild();
            }
        } else {
            const childrenToAdd = difference;
            for(let i = 0; i < childrenToAdd; i++) {
                this.addChild();
            }
        }
    }

    addChild(): void {
        this.formArray.push(
          this._formBuilder.group(this.childFormProperties));
    }

    removeChild(): void {
        this.formArray.removeAt(this.formArray.length - 1);
    }
}