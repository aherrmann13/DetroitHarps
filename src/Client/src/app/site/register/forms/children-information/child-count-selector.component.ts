import { Component, Input } from '@angular/core';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'dh-child-count-selector',
  template: `
    <mat-form-field>
      <mat-label>Number of Children</mat-label>
      <mat-select [formControl]="form" required>
        <mat-option *ngFor="let number of numberOfChildrenSelector" [value]="number">
          {{ number }}
        </mat-option>
      </mat-select>
      <mat-error>select a number</mat-error>
    </mat-form-field>
  `,
  styleUrls: ['../../register.component.scss']
})
export class ChildCountSelectorComponent {
  @Input() form: FormControl;

  numberOfChildrenSelector: Array<number> = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
}
