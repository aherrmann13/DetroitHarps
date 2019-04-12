import { Component, OnInit } from "@angular/core";
import { FormGroup, FormBuilder, Validators, AbstractControl } from "@angular/forms";
import { Observable } from "rxjs";
import { map, startWith } from "rxjs/operators";

export interface AddressInformationComponentData {
    address: string,
    address2: string,
    city: string,
    state: string,
    zip: string
}

@Component({
    selector: 'dh-register-address-information',
    templateUrl: './address-information.component.html',
    styleUrls: [ '../../register.component.scss' ]
})
export class AddressInformationComponent implements OnInit {
    formGroup: FormGroup;
    states: string[] = [
        'AK', 'AL', 'AR', 'AZ', 'CA', 'CO', 'CT', 'DE', 'FL', 'GA',
        'HI', 'IA', 'ID', 'IL', 'IN', 'KS', 'KY', 'LA', 'MA', 'MD',
        'ME', 'MI', 'MN', 'MO', 'MS', 'MT', 'NC', 'ND', 'NE', 'NH',
        'NJ', 'NM', 'NV', 'NY', 'OH', 'OK', 'OR', 'PA', 'RI', 'SC',
        'SD', 'TN', 'TX', 'UT', 'VA', 'VT', 'WA', 'WI', 'WV', 'WY'
      ];
    filteredStates: Observable<string[]>;

    constructor(formBuilder: FormBuilder) {
        this.filter = this.filter.bind(this);
        this.valueIsState = this.valueIsState.bind(this);

        // TODO: can this be moved out of ctor?
        // based on this https://stackblitz.com/edit/angular-material-stepper-with-component-steps
        // and based on https://stackoverflow.com/questions/48498966/angular-material-stepper-component-for-each-step?rq=1
        this.formGroup = formBuilder.group({
            address: ['', Validators.required],
            address2: [''],
            city: ['', Validators.required],
            state: ['', [Validators.required, this.valueIsState]],
            zip: ['', Validators.required]
          });
    }

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

    ngOnInit() {
        this.filteredStates = this.formGroup.controls.state.valueChanges
          .pipe(
            startWith(''),
            map(value => this.filter(value))
        );
    }
    
    private filter(value: string): string[] {
        const filterValue = value.toLowerCase();
    
        return this.states.filter(state => state.toLowerCase().indexOf(filterValue) === 0);
    }

    private valueIsState(control: AbstractControl) {
        return this.states.indexOf(control.value) === -1 ?
            {'forbiddenValue': true} :
            null;
    }
}