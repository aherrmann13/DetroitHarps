import { Component, OnInit } from '@angular/core';

import { Client } from '../app.client'
import { Event } from '../models/event.model'

@Component({
  selector: 'app-schedule',
  templateUrl: './schedule.component.html',
  styleUrls: [ './schedule.component.scss' ]
})
export class ScheduleComponent implements OnInit {

  events: Event[];
  
  constructor(private _client: Client) { }
  
  ngOnInit() {
    this.events = this._client.getEvents();
  }

}
