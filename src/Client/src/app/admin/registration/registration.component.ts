import { Component, OnInit } from '@angular/core';

import {
  RegisteredChildModel,
  Client,
  FileResponse,
  EventModel
} from '../../core/client/api.client';

import { saveAs } from "file-saver";
import 'rxjs/add/observable/forkJoin';



@Component({
  selector: 'dh-admin-registration',
  templateUrl: './registration.component.html'
})

export class RegistrationComponent implements OnInit {
  info: RegisteredChildModel[];
  events: EventModel[];
  columnsToDisplay: string[] = [
    'parentName',
    'childName',
    'gender',
    'emailAddress',
    'dateOfBirth',
    'shirtSize'];
  
  items = [{
    icon: "file_download",
    onClick: () => this._client
      .getAllChildrenCsv()
      .subscribe(this.downloadFile)
  }];

  constructor(private _client: Client) { 
    this.downloadFile = this.downloadFile.bind(this);
  }

  ngOnInit() {
    this._client.getAllChildren().subscribe(data => this.info = data);
    this._client.getRegistrationEvents().subscribe(
      data =>
      {
        this.events = data;
        this.events.forEach(x => 
          this.columnsToDisplay.push(x.id.toString())
        )
      })
  }

  downloadFile(data: FileResponse) {
    saveAs(data.data, "registration.csv");
  }

  getEventAnswer(child: RegisteredChildModel, event: EventModel): string {
    var regEvent = child.events.find(x => x.eventId === event.id);
    return regEvent ? regEvent.answer.toString() : 'No Answer';
  }
}
