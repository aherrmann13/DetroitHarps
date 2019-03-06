import { Component, OnInit } from '@angular/core';

import {
  RegisteredChildModel,
  Client
} from '../../shared/client/api.client';

import 'rxjs/add/observable/forkJoin';


@Component({
  selector: 'dh-registration',
  templateUrl: './registration.component.html',
  styleUrls: [ ]
})

export class RegistrationComponent implements OnInit {
  info: RegisteredChildModel[];
  columnsToDisplay: string[] = [
    'parentName',
    'childName',
    'gender',
    'emailAddress',
    'dateOfBirth',
    'shirtSize'];

  constructor(private _client: Client) { }

  ngOnInit() {
    this._client.getAllChildren().subscribe(
      data => this.info = data,
      error => console.error(error)
    );
  }
}
