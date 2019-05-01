import { Component, Output, EventEmitter, OnInit } from "@angular/core";
import { MatSelectChange } from "@angular/material";

@Component({
    selector: 'dh-child-count-selector',
    template: `
    <mat-form-field>
        <mat-label>Number of Children</mat-label>
        <mat-select (selectionChange)="selectionChange($event)" [value]="startingValue">
            <mat-option *ngFor="let number of numberOfChildrenSelector" [value]="number">
                {{number}}
            </mat-option>
        </mat-select>
    </mat-form-field>`,
    styleUrls: [ '../../register.component.scss' ]
})
export class ChildCountSelectorComponent implements OnInit {
    @Output() onInit = new EventEmitter<number>();
    @Output() onChange = new EventEmitter<number>();

    numberOfChildrenSelector: Array<number> = [ 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 ]
    startingValue = this.numberOfChildrenSelector[0];

    selectionChange(change: MatSelectChange): void {
        this.onChange.emit(change.value);
    }

    ngOnInit(): void {
        this.onInit.emit(this.startingValue)
    }
}