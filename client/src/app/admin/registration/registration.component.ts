import { Component, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';

import { Client, ContactModel, UserCredentialsModel, RegistrationReadModel, ChildInformationReadModel } from '../../api.client';
import { Router } from '@angular/router';

import { IRegistrationInformationModel } from './registration.model'
import { Observable } from 'rxjs/Observable';

import 'rxjs/add/observable/forkJoin';


@Component({
  selector: 'admin-registration',
  templateUrl: './registration.component.html',
  styleUrls: [ ]
})
export class RegistrationComponent implements OnInit {
  info: IRegistrationInformationModel[];
  columnsToDisplay: string[] = ['parentName', 'childName', 'gender', 'dateOfBirth', 'shirtSize'];
  
  constructor(private _client: Client, private _router: Router) { }
  ngOnInit() {

    let parentsObservable = this._client.getAllRegistered();
    let childObservable = this._client.getAllRegisteredChildren();

    Observable.forkJoin([parentsObservable, childObservable]).subscribe(
      data => this.ProcessRegistration(data),
    )
  }

  private ProcessRegistration(data: any): void {
    let parents = data['0'] as RegistrationReadModel[];
    let children = data['1'] as ChildInformationReadModel[];
    this.info = children.map(function(x) {
      let parent = parents.find(y => y.id === x.parentId);
      return {
        parentName: parent.firstName + ' ' + parent.lastName,
        childName: x.firstName + ' ' + x.lastName,
        gender: x.gender,
        dateOfBirth: x.dateOfBirth,
        shirtSize: x.shirtSize
      }
    });

    console.log(this.info);
  }

}