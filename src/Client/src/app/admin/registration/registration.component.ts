import { Component, OnInit } from '@angular/core';

import {
  RegisteredChildModel,
  Client,
  FileResponse
} from '../../core/client/api.client';

import { saveAs } from "file-saver";
import 'rxjs/add/observable/forkJoin';



@Component({
  selector: 'dh-admin-registration',
  templateUrl: './registration.component.html'
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
    this._client.getAllChildren().subscribe(
      data => this.info = data,
      error => console.error(error)
    );
  }

  downloadFile(data: FileResponse) {
    saveAs(data.data, "registration.csv");
  }
}
