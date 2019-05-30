import { Component, OnInit } from '@angular/core';
import { configuration } from '../../configuration';

@Component({
  selector: 'dh-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  title = 'Detroit Harps';
  registrationText = `Register Summer ${configuration.year}`;

  constructor() {}

  ngOnInit() {}
}
