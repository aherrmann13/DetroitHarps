import { Component, OnInit } from '@angular/core';

import { Client, EventReadModel } from '../api.client'

@Component({
  selector: 'app-schedule',
  templateUrl: './schedule.component.html',
  styleUrls: [ ]
})
export class ScheduleComponent implements OnInit {

  events: EventReadModel[];
  
  constructor(private _client: Client) { }
  
  ngOnInit() {
    this._client.getAllEvents().subscribe(
      data => this.events = data,
      error => console.error(error)
    );
  }
}
