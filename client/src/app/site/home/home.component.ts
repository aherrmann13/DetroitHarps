import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'site-home',
  templateUrl: './home.component.html',
  styleUrls: [ './home.component.scss' ]
})
export class HomeComponent implements OnInit {

  announcements: string[];
  title: string = 'Detroit Harps';
  registrationText = 'Register Summer 2018'

  constructor() { }

  ngOnInit() {
  }

}
