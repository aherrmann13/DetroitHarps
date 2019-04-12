import { Component, Input } from "@angular/core";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";

@Component({
    selector: 'dh-register-final-step',
    templateUrl: 'final-step.component.html',
    styleUrls: [ '../../register.component.scss' ]
})
export class FinalStepComponent {
    @Input() loading: boolean = false;
    @Input() paymentType: string;
}