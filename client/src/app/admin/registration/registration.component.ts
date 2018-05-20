import { Component, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';

import {
  Client,
  ContactModel,
  UserCredentialsModel,
  RegistrationReadModel,
  ChildInformationReadModel
} from '../../shared/client/api.client';
import { Router } from '@angular/router';

import { IRegistrationInformationModel } from './registration.model';
import { Observable } from 'rxjs/Observable';

import 'rxjs/add/observable/forkJoin';


@Component({
  selector: 'dh-registration',
  templateUrl: './registration.component.html',
  styleUrls: [ ]
})
export class RegistrationComponent implements OnInit {
  info: IRegistrationInformationModel[];
  columnsToDisplay: string[] = ['parentName', 'childName', 'gender', 'dateOfBirth', 'shirtSize'];

  constructor(private _client: Client, private _router: Router) { }
  ngOnInit() {

    const parentsObservable = this._client.getAllRegistered();
    const childObservable = this._client.getAllRegisteredChildren();

    Observable.forkJoin([parentsObservable, childObservable]).subscribe(
      data => this.ProcessRegistration(data),
    );
  }

  private ProcessRegistration(data: any): void {
    const parents = data['0'] as RegistrationReadModel[];
    const children = data['1'] as ChildInformationReadModel[];
    this.info = children.map(function(x) {
      const parent = parents.find(y => y.id === x.parentId);
      return {
        parentName: parent.firstName + ' ' + parent.lastName,
        childName: x.firstName + ' ' + x.lastName,
        gender: x.gender,
        dateOfBirth: x.dateOfBirth,
        shirtSize: x.shirtSize
      };
    });

    console.log(this.info);
  }

}
