import { ComponentFixture, inject, TestBed } from '@angular/core/testing';
import { OverlayContainer } from '@angular/cdk/overlay';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { By } from '@angular/platform-browser';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { MatRadioModule } from '@angular/material/radio';
import { RegisterPaymentModelPaymentType } from 'app/core/client/api.client';
import { FinalStepComponent } from './final-step.component';
import { MatProgressBarModule } from '@angular/material/progress-bar';

describe('ChildCountSelectorComponent', () => {
  let component: FinalStepComponent;
  let fixture: ComponentFixture<FinalStepComponent>;
  let paymentType: string;
  let loading: boolean;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [NoopAnimationsModule, MatProgressBarModule],
      declarations: [FinalStepComponent]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FinalStepComponent);
    component = fixture.componentInstance;
    paymentType = RegisterPaymentModelPaymentType.Cash;
    loading = false;

    component.loading = loading;
    component.paymentType = paymentType;

    fixture.detectChanges();
  });

  it('should create component', () => {
    expect(component).toBeTruthy();
  });

  it('should display progress bar when loading', () => {
    component.loading = true;
    fixture.detectChanges();

    const progressBar = fixture.debugElement.queryAll(By.css('mat-progress-bar'));

    expect(progressBar.length).toBe(1);
  });
  it('should hide progress bar when not loading', () => {
    component.loading = false;
    fixture.detectChanges();

    const progressBar = fixture.debugElement.queryAll(By.css('mat-progress-bar'));

    expect(progressBar.length).toBe(0);
  });
  it('should display successful registration message when not loading', () => {
    component.loading = false;
    fixture.detectChanges();

    const headline = fixture.debugElement.query(By.css('.mat-headline'));
    const subHeadline = fixture.debugElement.query(By.css('.mat-subheading-2'));

    expect(headline.nativeElement.innerHTML.trim()).toBe('Registration Success!');
    expect(subHeadline.nativeElement.innerHTML.trim()).toBe(
      `Thank for registering for the ${component.year} summer program`
    );
  });
  it('should display paypal form when not loading and payment type is paypal', () => {
    component.loading = false;
    component.paymentType = RegisterPaymentModelPaymentType.Paypal;
    fixture.detectChanges();

    const paypalForm = fixture.debugElement.query(By.css('.register-success > div'));

    expect(paypalForm.nativeElement.innerHTML.trim()).toContain(
      'PayPal (select from menu below and then click Buy Now)'
    );
    expect(paypalForm.query(By.css('form'))).not.toBeNull();
  });
  it('should hide paypal form when not loading and payment type is cash', () => {
    component.loading = false;
    component.paymentType = RegisterPaymentModelPaymentType.Cash;
    fixture.detectChanges();

    const paypalForm = fixture.debugElement.query(By.css('.register-success > div'));

    expect(paypalForm).toBeNull();
  });
  it('should hide paypal form when not loading and payment type is other', () => {
    component.loading = false;
    component.paymentType = RegisterPaymentModelPaymentType.AlreadyPaid;
    fixture.detectChanges();

    const paypalForm = fixture.debugElement.query(By.css('.register-success > div'));

    expect(paypalForm).toBeNull();
  });
});
