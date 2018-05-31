import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'dh-home',
  templateUrl: './home.component.html',
  styleUrls: [ './home.component.scss' ]
})
export class HomeComponent implements OnInit {

  announcements: string[];
  title = 'Detroit Harps';
  registrationText = 'Register Summer 2018';

  constructor() { }

  ngOnInit() {
  }

}
