import { Component, OnInit } from '@angular/core';

import { RegisteredChildModel, Client, FileResponse, EventModel } from '../../core/client/api.client';

import { saveAs } from 'file-saver';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { DeletePromptDialogComponent } from '../delete-prompt/delete-prompt.component';
import { Observable } from 'rxjs';
import { configuration } from 'app/configuration';

@Component({
  selector: 'dh-admin-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['../admin.component.scss']
})
export class RegistrationComponent implements OnInit {
  dataSource = new MatTableDataSource<RegisteredChildModel>();
  events: EventModel[];
  columnsToDisplay: string[] = [
    'parentName',
    'childName',
    'gender',
    'emailAddress',
    'address',
    'phone',
    'dateOfBirth',
    'shirtSize',
    'edit'
  ];

  items = [
    {
      icon: 'file_download',
      onClick: () => this._client.getChildrenCsv(configuration.year).subscribe(this.downloadFile)
    }
  ];

  private _info: Array<RegisteredChildModel>;

  constructor(private _client: Client, private _dialog: MatDialog) {
    this.downloadFile = this.downloadFile.bind(this);
    this.addToColumnsToDisplay = this.addToColumnsToDisplay.bind(this);
  }

  ngOnInit() {
    this._client.getChildren(configuration.year).subscribe(data => {
      (this._info = data), this.refreshDataSource();
    });
    this._client.getRegistrationEvents().subscribe(data => {
      this.events = data;
      this.events.map(x => x.id.toString()).forEach(this.addToColumnsToDisplay);
    });
  }

  downloadFile(data: FileResponse) {
    saveAs(data.data, 'registration.csv');
  }

  getEventAnswer(child: RegisteredChildModel, event: EventModel): string {
    const regEvent = child.events.find(x => x.eventId === event.id);
    return regEvent ? regEvent.answer.toString() : 'No Answer';
  }

  openDeleteDialog(child: RegisteredChildModel): void {
    const { registrationId, firstName, lastName } = child;
    const dialogRef = this._dialog.open(DeletePromptDialogComponent, {
      disableClose: true,
      data: {
        onClick: () => this.deleteRegistration(registrationId, firstName, lastName),
        data: {
          itemName: 'Registered Child'
        }
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result && result.yes) {
        const index = this._info.indexOf(child);
        this._info.splice(index, 1);
        this.refreshDataSource();
      }
    });
  }

  getAddress(child: RegisteredChildModel): String {
    const { address, address2, city, state, zip } = child.contactInformation;
    const fullAddress = address2 ? address + '\n' + address2 : address;
    return `${fullAddress}\n${city}, ${state}, ${zip}`;
  }

  private refreshDataSource() {
    this.dataSource.data = this._info;
  }

  private addToColumnsToDisplay(column: string): void {
    const insertAt = this.columnsToDisplay.length - 2;
    this.columnsToDisplay.splice(insertAt, 0, column);
  }

  private deleteRegistration(id: number, firstName: string, lastName: string): Observable<void> {
    return this._client.deleteRegisteredChild(id, firstName, lastName);
  }
}
