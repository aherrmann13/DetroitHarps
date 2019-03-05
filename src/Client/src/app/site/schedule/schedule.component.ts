import { Component, OnInit } from '@angular/core';

import { Client, EventModel } from '../../shared/client/api.client';

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
    return events.sort((a: EventModel, b: EventModel) => {
      return a.startDate.getTime() - b.startDate.getTime();
    });
  }
}
