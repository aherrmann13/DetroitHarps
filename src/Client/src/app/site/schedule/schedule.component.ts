import { Component, OnInit } from '@angular/core';

import { Client, EventModel } from '../../core/client/api.client';
import { descendingDateSorter, ascendingDateSorter } from '../../core/utilities/date-functions';

@Component({
  selector: 'dh-schedule',
  templateUrl: './schedule.component.html',
  styleUrls: []
})
export class ScheduleComponent implements OnInit {
  events: EventModel[];

  constructor(private _client: Client) {}

  ngOnInit() {
    const currentYear = new Date().getFullYear();
    this._client.getAllEvents().subscribe(data => {
      this.events = this.orderDates(data).filter(evt => evt.startDate.getFullYear() >= currentYear);
    });
  }

  private orderDates(events: Array<EventModel>): Array<EventModel> {
    return events.sort(ascendingDateSorter(x => x.startDate));
  }
}
