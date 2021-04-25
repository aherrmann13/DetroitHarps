import { Component, OnInit } from '@angular/core';
import { configuration } from '../../configuration';

@Component({
  selector: 'dh-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {
  title = 'Detroit Harps';
  registrationText = `Register Summer ${configuration.year}`;

  campRegistrationUrl = 'https://forms.gle/Hnj5QhDWSBGhfZra6';
  displayCampRegistration = new Date().getTime() < new Date(2019, 7, 30).getTime();
  campRegistrationText = 'Register 2019 Camp July 30 & 31';

  constructor() {}
}
