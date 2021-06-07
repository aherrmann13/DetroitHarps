import { TestBed, ComponentFixture } from '@angular/core/testing';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { By } from '@angular/platform-browser';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';

import { AddressInformationComponent } from './address-information.component';

describe('AddressInformationComponent', () => {
  let component: AddressInformationComponent;
  let fixture: ComponentFixture<AddressInformationComponent>;
  let form: FormGroup;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [NoopAnimationsModule, MatFormFieldModule, MatAutocompleteModule, ReactiveFormsModule, MatInputModule],
      declarations: [AddressInformationComponent]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddressInformationComponent);
    component = fixture.componentInstance;
    form = new FormBuilder().group({
      address: [''],
      address2: [''],
      city: [''],
      state: [''],
      zip: ['']
    });
    component.form = form;
    fixture.detectChanges();
  });

  it('should create component', () => {
    expect(component).toBeTruthy();
  });

  it('should update form with street address', () => {
    const control = fixture.debugElement.query(By.css("[formControlName='address']")).nativeElement;
    control.value = 'address';
    control.dispatchEvent(new Event('input'));
    fixture.detectChanges();
    expect(form.controls.address.value).toBe('address');
  });
  it('should update form with street address 2', () => {
    const control = fixture.debugElement.query(By.css("[formControlName='address2']")).nativeElement;
    control.value = 'address2';
    control.dispatchEvent(new Event('input'));
    fixture.detectChanges();
    expect(form.controls.address2.value).toBe('address2');
  });
  it('should update form with city', () => {
    const control = fixture.debugElement.query(By.css("[formControlName='city']")).nativeElement;
    control.value = 'city';
    control.dispatchEvent(new Event('input'));
    fixture.detectChanges();
    expect(form.controls.city.value).toBe('city');
  });
  it('should update form with state', () => {
    const control = fixture.debugElement.query(By.css("[formControlName='state']")).nativeElement;
    control.value = 'state';
    control.dispatchEvent(new Event('input'));
    fixture.detectChanges();
    expect(form.controls.state.value).toBe('state');
  });
  it('should display filtered state options', async done => {
    done();
    const control = fixture.debugElement.query(By.css("[formControlName='state']")).nativeElement;

    control.value = 'M';
    control.dispatchEvent(new Event('input'));
    fixture.detectChanges();
    await fixture.whenStable();
    fixture.detectChanges();

    // need to set input as
  });
  it('should update form with zip', () => {
    const control = fixture.debugElement.query(By.css("[formControlName='zip']")).nativeElement;
    control.value = 'zip';
    control.dispatchEvent(new Event('input'));
    fixture.detectChanges();
    expect(form.controls.zip.value).toBe('zip');
  });
});
