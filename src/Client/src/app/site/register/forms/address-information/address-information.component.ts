import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { STATES } from './states';

@Component({
  selector: 'dh-register-address-information',
  templateUrl: './address-information.component.html',
  styleUrls: ['../../register.component.scss']
})
export class AddressInformationComponent implements OnInit {
  @Input() form: FormGroup;

  filteredStates: Observable<string[]>;

  ngOnInit(): void {
    this.filteredStates = this.form.controls.state?.valueChanges.pipe(
      startWith(''),
      map(value => AddressInformationComponent.filter(value))
    );
  }

  // https://github.com/angular/material2/issues/3414
  fixStateAutoFill(state: Event): void {
    this.form.controls.state?.setValue((state.target as HTMLInputElement)?.value);
  }

  private static filter(value: string): string[] {
    const filterValue = value.toLowerCase();
    return STATES.filter(state => state.toLowerCase().indexOf(filterValue) === 0);
  }
}
