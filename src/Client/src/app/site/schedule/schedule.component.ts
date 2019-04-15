import { Component, OnInit } from '@angular/core';

import { Client, EventModel } from '../../shared/client/api.client';
import { descendingDateSorter, ascendingDateSorter } from '../../shared/utilities/date-functions';

@Component({
  selector: 'dh-schedule',
  templateUrl: './schedule.component.html',
  styleUrls: [ ]
})
export class ScheduleComponent implements OnInit {

  events: EventModel[];

  constructor(private _client: Client) { }

  ngOnInit() {
    this._client.getAllEvents().subscribe(
      data => this.events = this.orderDates(data),
      error => console.error(error)
    );
  }

  private orderDates(events: Array<EventModel>): Array<EventModel> {
    return events.sort(ascendingDateSorter(x => x.startDate));
  }
}
