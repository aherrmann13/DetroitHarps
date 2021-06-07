import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { By } from '@angular/platform-browser';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { ParentInformationComponent } from './parent-information.component';

describe('ParentInformationComponent', () => {
  let component: ParentInformationComponent;
  let fixture: ComponentFixture<ParentInformationComponent>;
  let form: FormGroup;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [NoopAnimationsModule, ReactiveFormsModule, MatFormFieldModule, MatInputModule],
      declarations: [ParentInformationComponent]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ParentInformationComponent);
    component = fixture.componentInstance;
    form = new FormBuilder().group({
      parentFirstName: [''],
      parentLastName: [''],
      phoneNumber: [''],
      emailAddress: ['']
    });
    component.form = form;
    fixture.detectChanges();
  });

  it('should create component', () => {
    expect(component).toBeTruthy();
  });

  it('should update form with parent first name', () => {
    const control = fixture.debugElement.query(By.css("[formControlName='parentFirstName']")).nativeElement;
    control.value = 'first name';
    control.dispatchEvent(new Event('input'));
    fixture.detectChanges();
    expect(form.controls.parentFirstName.value).toBe('first name');
  });

  it('should update form with parent last name', () => {
    const control = fixture.debugElement.query(By.css("[formControlName='parentLastName']")).nativeElement;
    control.value = 'last name';
    control.dispatchEvent(new Event('input'));
    fixture.detectChanges();
    expect(form.controls.parentLastName.value).toBe('last name');
  });

  it('should update form with phone number', () => {
    const control = fixture.debugElement.query(By.css("[formControlName='phoneNumber']")).nativeElement;
    control.value = 'phone number';
    control.dispatchEvent(new Event('input'));
    fixture.detectChanges();
    expect(form.controls.phoneNumber.value).toBe('phone number');
  });

  it('should update form with email address', () => {
    const control = fixture.debugElement.query(By.css("[formControlName='emailAddress']")).nativeElement;
    control.value = 'email address';
    control.dispatchEvent(new Event('input'));
    fixture.detectChanges();
    expect(form.controls.emailAddress.value).toBe('email address');
  });
});
