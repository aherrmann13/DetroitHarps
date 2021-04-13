import { Component, Output, EventEmitter, OnInit } from '@angular/core';
import { MatSelectChange } from '@angular/material/select';
import { FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'dh-child-count-selector',
  template: `
    <mat-form-field>
      <mat-label>Number of Children</mat-label>
      <mat-select [formControl]="childCount" required>
        <mat-option *ngFor="let number of numberOfChildrenSelector" [value]="number">
          {{ number }}
        </mat-option>
      </mat-select>
      <mat-error>select a number</mat-error>
    </mat-form-field>
  `,
  styleUrls: ['../../register.component.scss']
})
export class ChildCountSelectorComponent implements OnInit {
  @Output() change = new EventEmitter<number>();

  childCount = new FormControl('', [Validators.required]);

  numberOfChildrenSelector: Array<number> = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];

  ngOnInit() {
    this.childCount.valueChanges.subscribe(x => this.change.emit(this.childCount.value));
  }
}
