import { ComponentFixture, inject, TestBed } from '@angular/core/testing';
import { OverlayContainer } from '@angular/cdk/overlay';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { By } from '@angular/platform-browser';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { PaymentInformationComponent } from './payment-information.component';
import { MatRadioModule } from '@angular/material/radio';
import { RegisterPaymentModelPaymentType } from 'app/core/client/api.client';

describe('ChildCountSelectorComponent', () => {
  let component: PaymentInformationComponent;
  let fixture: ComponentFixture<PaymentInformationComponent>;
  let form: FormGroup;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [NoopAnimationsModule, ReactiveFormsModule, MatRadioModule],
      declarations: [PaymentInformationComponent]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PaymentInformationComponent);
    component = fixture.componentInstance;
    form = new FormBuilder().group({ paymentType: [] });
    component.form = form;
    fixture.detectChanges();
  });

  it('should create component', () => {
    expect(component).toBeTruthy();
  });

  it('should have radio buttons for cash, paypal, and other', () => {
    const radioButtons = fixture.debugElement.queryAll(By.css('mat-radio-button'));

    expect(radioButtons.length).toBe(3);
    expect(radioButtons[0].query(By.css('.mat-radio-label-content')).nativeElement.textContent.trim()).toBe(
      'Cash/Check'
    );
    expect(radioButtons[1].query(By.css('.mat-radio-label-content')).nativeElement.textContent.trim()).toBe(
      'Paypal/Credit Card'
    );
    expect(radioButtons[2].query(By.css('.mat-radio-label-content')).nativeElement.textContent.trim()).toBe(
      'Already paid - adding child'
    );
  });

  it('should update form when cash selected', () => {
    const radioButtons = fixture.debugElement.queryAll(By.css('mat-radio-button'));

    radioButtons[0].query(By.css('label')).nativeElement.click();

    fixture.detectChanges();

    expect(form.controls.paymentType.value).toBe(RegisterPaymentModelPaymentType.Cash);
  });
  it('should update form when paypal selected', () => {
    const radioButtons = fixture.debugElement.queryAll(By.css('mat-radio-button'));

    radioButtons[1].query(By.css('label')).nativeElement.click();

    fixture.detectChanges();

    expect(form.controls.paymentType.value).toBe(RegisterPaymentModelPaymentType.Paypal);
  });
  it('should update form when other selected', () => {
    const radioButtons = fixture.debugElement.queryAll(By.css('mat-radio-button'));

    radioButtons[2].query(By.css('label')).nativeElement.click();

    fixture.detectChanges();

    expect(form.controls.paymentType.value).toBe(RegisterPaymentModelPaymentType.AlreadyPaid);
  });
  it('should display cash/check message when cash selected', () => {
    const radioButtons = fixture.debugElement.queryAll(By.css('mat-radio-button'));

    radioButtons[0].query(By.css('label')).nativeElement.click();

    fixture.detectChanges();

    const text = fixture.debugElement.queryAll(By.css('p'));

    expect(text.length).toBe(2);
    expect(text[0].nativeElement.innerHTML.trim()).toBe(
      'If paying by check please make out to "Detroit Youth GAA" and send to ' +
        '28156 North Clement Circle, Livonia MI 48150. For cash payments catch up with us this summer.'
    );
    expect(text[1].nativeElement.innerHTML.trim()).toBe("After selecting payment option, click 'register'");
  });
  it('should display paypal message when paypal selected', () => {
    const radioButtons = fixture.debugElement.queryAll(By.css('mat-radio-button'));

    radioButtons[1].query(By.css('label')).nativeElement.click();

    fixture.detectChanges();

    const text = fixture.debugElement.queryAll(By.css('p'));

    expect(text.length).toBe(2);
    expect(text[0].nativeElement.innerHTML.trim()).toBe('Link to pay will be on next page');
    expect(text[1].nativeElement.innerHTML.trim()).toBe("After selecting payment option, click 'register'");
  });
  it('should display no message when already paid selected', () => {
    const radioButtons = fixture.debugElement.queryAll(By.css('mat-radio-button'));

    radioButtons[2].query(By.css('label')).nativeElement.click();

    fixture.detectChanges();

    const text = fixture.debugElement.queryAll(By.css('p'));

    expect(text.length).toBe(1);
    expect(text[0].nativeElement.innerHTML.trim()).toBe("After selecting payment option, click 'register'");
  });
});
