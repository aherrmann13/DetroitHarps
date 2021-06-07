import { ComponentFixture, inject, TestBed } from '@angular/core/testing';
import { OverlayContainer } from '@angular/cdk/overlay';
import { FormBuilder, FormControl, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { By } from '@angular/platform-browser';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { ChildCountSelectorComponent } from './child-count-selector.component';

describe('ChildCountSelectorComponent', () => {
  let component: ChildCountSelectorComponent;
  let fixture: ComponentFixture<ChildCountSelectorComponent>;
  let form: FormControl;
  let overlayContainer: OverlayContainer;
  let overlayContainerElement: HTMLElement;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [NoopAnimationsModule, ReactiveFormsModule, MatFormFieldModule, MatSelectModule],
      declarations: [ChildCountSelectorComponent]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ChildCountSelectorComponent);
    component = fixture.componentInstance;
    form = new FormBuilder().control({});
    component.form = form;
    fixture.detectChanges();

    inject([OverlayContainer], (oc: OverlayContainer) => {
      overlayContainer = oc;
      overlayContainerElement = oc.getContainerElement();
    })();
  });

  it('should create component', () => {
    expect(component).toBeTruthy();
  });

  it('should show numbers 1 - 10 as options', () => {
    const control = fixture.debugElement.query(By.css('mat-select'));
    control.nativeElement.click();
    fixture.detectChanges();

    const options = overlayContainerElement.querySelectorAll('mat-option');

    expect(options.length).toBe(10);
    options.forEach((option, i) => {
      expect(option.querySelector('span').innerHTML.trim()).toBe(`${i + 1}`);
    });
  });

  it('should update form with selected count', () => {
    const control = fixture.debugElement.query(By.css('mat-select'));
    control.nativeElement.click();
    fixture.detectChanges();

    const options = overlayContainerElement.querySelectorAll('mat-option');

    (options[3] as HTMLElement).click();
    fixture.detectChanges();

    expect(form.value).toBe(4);
  });
});
