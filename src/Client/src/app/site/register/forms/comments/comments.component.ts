import { Component, Input } from '@angular/core';
import { FormGroup, FormBuilder, AbstractControl, FormControl } from '@angular/forms';

@Component({
  selector: 'dh-register-comments',
  templateUrl: './comments.component.html',
  styleUrls: ['../../register.component.scss']
})
export class CommentsComponent {
  @Input() form: FormControl;
}
