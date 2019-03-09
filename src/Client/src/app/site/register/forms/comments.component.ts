import { Component } from "@angular/core";
import { FormGroup, FormBuilder } from "@angular/forms";

@Component({
    selector: 'dh-register-comments',
    template: `
    <form [formGroup]='formGroup'>                       
        <div class='form-container'>
            <mat-form-field>
                <textarea matInput placeholder='Comments/Special Requests' formControlName='comments'></textarea>
            </mat-form-field>
        </div>
    </form>`,
    styleUrls: [ '../register.component.scss' ]
})
export class CommentsComponent {
    
    formGroup: FormGroup;

    get data(): string {
        return this.formGroup.value.comments
    }

    constructor(formBuilder: FormBuilder) {
        // TODO: can this be moved out of ctor?
        // based on this https://stackblitz.com/edit/angular-material-stepper-with-component-steps
        // and based on https://stackoverflow.com/questions/48498966/angular-material-stepper-component-for-each-step?rq=1
        this.formGroup = formBuilder.group({
            comments: ['']
          });
    }
}