import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormArray, FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { EventRegistrationComponent } from './event-registration.component';
import {
  EventModel,
  RegisterChildEventModelAnswer,
  RegisterChildModel,
  RegisterChildModelGender
} from '../../../../core/client/api.client';
import { MatIconModule } from '@angular/material/icon';
import { MatRadioModule } from '@angular/material/radio';
import { MatListModule } from '@angular/material/list';
import { MatFormFieldModule } from '@angular/material/form-field';
import { By } from '@angular/platform-browser';
import { DatePipe } from '@angular/common';

describe('EventRegistrationComponent', () => {
  let component: EventRegistrationComponent;
  let fixture: ComponentFixture<EventRegistrationComponent>;
  let form: FormArray;
  let children: Array<RegisterChildModel>;
  let events: Array<EventModel>;
  let pipe: DatePipe;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        NoopAnimationsModule,
        ReactiveFormsModule,
        MatFormFieldModule,
        MatIconModule,
        MatRadioModule,
        MatListModule
      ],
      declarations: [EventRegistrationComponent]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EventRegistrationComponent);
    component = fixture.componentInstance;

    const baseEventProps = { canRegister: true, description: 'n/a' };

    events = [
      new EventModel({ id: 1, title: 'evt1', startDate: new Date('2021-06-05'), ...baseEventProps }),
      new EventModel({ id: 2, title: 'evt2', startDate: new Date('2021-06-12'), ...baseEventProps }),
      new EventModel({
        id: 3,
        title: 'evt3',
        startDate: new Date('2021-06-12'),
        endDate: new Date('2021-06-13'),
        ...baseEventProps
      })
    ];

    const baseChildProps = { gender: RegisterChildModelGender.Male, dateOfBirth: new Date() };
    children = [
      new RegisterChildModel({ firstName: 'firstname1', lastName: 'lastname1', ...baseChildProps }),
      new RegisterChildModel({ firstName: 'firstname2', lastName: 'lastname2', ...baseChildProps }),
      new RegisterChildModel({ firstName: 'firstname3', lastName: 'lastname3', ...baseChildProps })
    ];

    form = new FormBuilder().array([
      new FormBuilder().group({ 1: [''], 2: [''], 3: [''] }),
      new FormBuilder().group({ 1: [''], 2: [''], 3: [''] }),
      new FormBuilder().group({ 1: [''], 2: [''], 3: [''] })
    ]);

    component.events = events;
    component.children = children;
    component.form = form;

    fixture.detectChanges();

    pipe = new DatePipe('en-US');
  });

  it('should create component', () => {
    expect(component).toBeTruthy();
  });

  it('should display each child', () => {
    const childForms = fixture.debugElement.queryAll(By.css('.registration-event-form'));

    expect(childForms.length).toBe(3);
  });
  it('should display event selector for each child', () => {
    const childForms = fixture.debugElement.queryAll(By.css('.registration-event-form'));

    childForms.forEach(childForm => {
      const eventSelectors = childForm.queryAll(By.css('mat-list-item'));

      expect(eventSelectors.length).toBe(3);

      expect(eventSelectors[0].query(By.css('h4')).nativeElement.innerHTML.trim()).toBe('evt1');
      expect(eventSelectors[1].query(By.css('h4')).nativeElement.innerHTML.trim()).toBe('evt2');
      expect(eventSelectors[2].query(By.css('h4')).nativeElement.innerHTML.trim()).toBe('evt3');
    });
  });
  it('should update event answer for child when clicked', () => {
    const childForms = fixture.debugElement.queryAll(By.css('.registration-event-form'));

    const eventSelectors = childForms[1].queryAll(By.css('mat-list-item'));
    const buttons = eventSelectors[1].queryAll(By.css('mat-radio-button'));
    buttons[1].query(By.css('label')).nativeElement.click();

    fixture.detectChanges();

    expect(form.controls[1].value['2']).toBe(RegisterChildEventModelAnswer.No);
  });
  it('should display start and end date for event with start and end date', () => {
    const childForms = fixture.debugElement.queryAll(By.css('.registration-event-form'));

    const eventSelectors = childForms[1].queryAll(By.css('mat-list-item'));
    const date = eventSelectors[2].queryAll(By.css('p'));

    expect(date.length).toBe(1);

    const formattedStart = pipe.transform(events[2].startDate, 'short');
    const formattedEnd = pipe.transform(events[2].endDate, 'short');
    expect(date[0].nativeElement.innerHTML.trim()).toBe(`${formattedStart} - ${formattedEnd}`);
  });
  it('should only display start date for event with start and no end date', () => {
    const childForms = fixture.debugElement.queryAll(By.css('.registration-event-form'));

    const eventSelectors = childForms[1].queryAll(By.css('mat-list-item'));
    const date = eventSelectors[1].queryAll(By.css('p'));

    expect(date.length).toBe(1);

    const formattedStart = pipe.transform(events[1].startDate, 'short');
    expect(date[0].nativeElement.innerHTML.trim()).toBe(`${formattedStart}`);
  });
});
