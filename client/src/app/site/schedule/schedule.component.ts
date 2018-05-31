import { Component, OnInit } from '@angular/core';

import { Client, EventReadModel } from '../../shared/client/api.client';

@Component({
  selector: 'dh-schedule',
  templateUrl: './schedule.component.html',
  styleUrls: [ ]
})
export class ScheduleComponent implements OnInit {

  events: EventReadModel[];

  constructor(private _client: Client) { }

  ngOnInit() {
    this._client.getAllEvents().subscribe(
      data => this.orderDates(data),
      error => console.error(error)
    );
  }

  private orderDates(events: EventReadModel[]): void {
    this.events = events.sort((a: EventReadModel, b: EventReadModel) => {
      return a.date.getTime() - b.date.getTime();
    });
  }
}
