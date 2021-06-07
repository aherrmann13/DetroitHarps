import { ComponentFixture, inject, TestBed } from '@angular/core/testing';
import { FormArray, FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { By } from '@angular/platform-browser';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { ChildrenInformationComponent } from './children-information.component';
import { MatRadioModule } from '@angular/material/radio';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatInputModule } from '@angular/material/input';
import { OverlayContainer } from '@angular/cdk/overlay';
import { RegisterChildModelGender } from 'app/core/client/api.client';

describe('ChildrenInformationComponent', () => {
  let component: ChildrenInformationComponent;
  let fixture: ComponentFixture<ChildrenInformationComponent>;
  let form: FormArray;
  let overlayContainerElement: HTMLElement;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        NoopAnimationsModule,
        ReactiveFormsModule,
        MatFormFieldModule,
        MatSelectModule,
        MatRadioModule,
        MatDatepickerModule,
        MatNativeDateModule,
        MatInputModule
      ],
      declarations: [ChildrenInformationComponent]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ChildrenInformationComponent);
    component = fixture.componentInstance;
    form = new FormBuilder().array([
      new FormBuilder().group({
        childFirstName: [''],
        childLastName: [''],
        childDob: [''],
        childGender: [''],
        childShirtSize: ['']
      }),
      new FormBuilder().group({
        childFirstName: [''],
        childLastName: [''],
        childDob: [''],
        childGender: [''],
        childShirtSize: ['']
      }),
      new FormBuilder().group({
        childFirstName: [''],
        childLastName: [''],
        childDob: [''],
        childGender: [''],
        childShirtSize: ['']
      })
    ]);
    component.form = form;

    fixture.detectChanges();

    inject([OverlayContainer], (oc: OverlayContainer) => {
      overlayContainerElement = oc.getContainerElement();
    })();
  });

  it('should create component', () => {
    expect(component).toBeTruthy();
  });

  it('should display a form for each child in form group', () => {
    const control = fixture.debugElement.queryAll(By.css('.child-registration-form'));

    expect(control.length).toBe(3);
    control.forEach((control, i) => {
      const title = control.query(By.css('span'));
      expect(title.nativeElement.innerHTML).toBe(`Child ${i + 1} `);
    });
  });
  it('should display a form for each child in form group after child added', () => {
    form.push(
      new FormBuilder().group({
        childFirstName: [''],
        childLastName: [''],
        childDob: [''],
        childGender: [''],
        childShirtSize: ['']
      })
    );

    fixture.detectChanges();
    const control = fixture.debugElement.queryAll(By.css('.child-registration-form'));
    expect(control.length).toBe(4);
    control.forEach((control, i) => {
      const title = control.query(By.css('span'));
      expect(title.nativeElement.innerHTML).toBe(`Child ${i + 1} `);
    });
  });
  it('should display a form for each child in form group after child removed', () => {
    form.removeAt(1);
    fixture.detectChanges();
    const control = fixture.debugElement.queryAll(By.css('.child-registration-form'));
    expect(control.length).toBe(2);
    control.forEach((control, i) => {
      const title = control.query(By.css('span'));
      expect(title.nativeElement.innerHTML).toBe(`Child ${i + 1} `);
    });
  });
  it('should update child first name', () => {
    const forms = fixture.debugElement.queryAll(By.css('.child-registration-form'));

    const control = forms[1].query(By.css("[formControlName='childFirstName']")).nativeElement;
    control.value = 'firstName';
    control.dispatchEvent(new Event('input'));

    expect(form.controls[1].value.childFirstName).toBe('firstName');
  });
  it('should update child last name', () => {
    const forms = fixture.debugElement.queryAll(By.css('.child-registration-form'));

    const control = forms[1].query(By.css("[formControlName='childLastName']")).nativeElement;
    control.value = 'lastName';
    control.dispatchEvent(new Event('input'));

    expect(form.controls[1].value.childLastName).toBe('lastName');
  });
  it('should update child birth date', () => {
    const forms = fixture.debugElement.queryAll(By.css('.child-registration-form'));

    const control = forms[1].query(By.css("[formControlName='childDob']")).nativeElement;
    control.value = '01/01/2020';
    control.dispatchEvent(new Event('input'));

    expect(form.controls[1].value.childDob).toEqual(new Date('01/01/2020'));
  });
  it('should update child gender', () => {
    // https://github.com/angular/components/blob/master/src/material/radio/radio.spec.ts
    const forms = fixture.debugElement.queryAll(By.css('.child-registration-form'));

    const controls = forms[1].queryAll(By.css('mat-radio-button'));
    controls[1].query(By.css('label')).nativeElement.click();

    fixture.detectChanges();

    expect(form.controls[1].value.childGender).toBe(RegisterChildModelGender.Female);
  });
  it('should update child shirt size', () => {
    const forms = fixture.debugElement.queryAll(By.css('.child-registration-form'));

    const control = forms[1].query(By.css('mat-select'));
    control.nativeElement.click();
    fixture.detectChanges();

    const options = overlayContainerElement.querySelectorAll('mat-option');
    (options[3] as HTMLElement).click();
    fixture.detectChanges();

    expect(form.controls[1].value.childShirtSize).toBe('YL');
  });
  it('should display all shirt sizes from configuration when clicked', () => {
    const forms = fixture.debugElement.queryAll(By.css('.child-registration-form'));

    const control = forms[1].query(By.css('mat-select'));
    control.nativeElement.click();
    fixture.detectChanges();

    const options = overlayContainerElement.querySelectorAll('mat-option');

    expect(options.length).toBe(9);
    expect(options[0].querySelector('span').innerHTML.trim()).toBe('YXS');
    expect(options[1].querySelector('span').innerHTML.trim()).toBe('YS');
    expect(options[2].querySelector('span').innerHTML.trim()).toBe('YM');
    expect(options[3].querySelector('span').innerHTML.trim()).toBe('YL');
    expect(options[4].querySelector('span').innerHTML.trim()).toBe('YXL');
    expect(options[5].querySelector('span').innerHTML.trim()).toBe('AS');
    expect(options[6].querySelector('span').innerHTML.trim()).toBe('AM');
    expect(options[7].querySelector('span').innerHTML.trim()).toBe('AL');
    expect(options[8].querySelector('span').innerHTML.trim()).toBe('AXL');
  });
});
