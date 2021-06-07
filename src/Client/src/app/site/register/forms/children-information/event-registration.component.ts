import { Component, Input } from '@angular/core';
import { FormArray } from '@angular/forms';
import { EventModel, RegisterChildEventModelAnswer, RegisterChildModel } from '../../../../core/client/api.client';

/**
 * would like to do something like the following
 * https://stackoverflow.com/questions/58731610/how-to-add-meta-data-for-formcontrol
 *
 * vs code has trouble with this, transpiles fine tho
 *
 * basically the three array lists that are passed in are related and assume the other has
 * a certain structure. the event form group needs to have an id for each event passed in
 * and the child names array needs to have the same length as the formarray controls
 *
 *
 * TODO: investigate
 */

@Component({
  selector: 'dh-register-events',
  templateUrl: './event-registration.component.html',
  styleUrls: ['../../register.component.scss']
})
export class EventRegistrationComponent {
  @Input() form: FormArray;
  @Input() children: RegisterChildModel[];
  @Input() events: EventModel[];

  options = [RegisterChildEventModelAnswer.Yes, RegisterChildEventModelAnswer.No, RegisterChildEventModelAnswer.Maybe];

  yes = RegisterChildEventModelAnswer.Yes;
  no = RegisterChildEventModelAnswer.No;
  maybe = RegisterChildEventModelAnswer.Maybe;
}
