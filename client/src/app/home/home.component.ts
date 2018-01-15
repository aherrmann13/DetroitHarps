import { Component, OnInit } from '@angular/core';

import { Client } from '../app.client'

@Component({
  selector: 'app-home',
  templateUrl: "./home.component.html",
  styleUrls: [ "./home.component.scss" ]
})
export class HomeComponent implements OnInit {

  announcements: string[];
  title: string = "Detroit Harps";

  constructor(private _client: Client) { }

  ngOnInit() {
    this.announcements = this._client.getAnnouncements();
  }

}
