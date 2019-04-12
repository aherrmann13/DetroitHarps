import { Component, OnInit } from '@angular/core';

import {
  Client,
  EventModel,
  EventCreateModel
} from '../../shared/client/api.client';
import { MatDialog, MatTableDataSource } from '@angular/material';
import { ScheduleModalDialogComponent } from './schedule-modal.component';import { Observable } from 'rxjs';
import { DeletePromptDialogComponent } from '../delete-prompt/delete-prompt.component';
3

@Component({
  selector: 'dh-admin-schedule',
  templateUrl: './schedule.component.html',
  styleUrls: [ './schedule.component.scss' ]
})
export class ScheduleComponent implements OnInit {
  private _events: Array<EventModel>;

  columnsToDisplay = ['title', 'startDate', 'endDate', 'edit' ];
  dataSource = new MatTableDataSource<EventModel>();

  items = [{
      icon: "add_circle",
      onClick: () => this.openCreateDialog()
    }];

  constructor(
    private _dialog: MatDialog,
    private _client: Client) { 
      this.updateEvent = this.updateEvent.bind(this);
      this.createEvent = this.createEvent.bind(this);
    }

  openCreateDialog(): void {
    this.openDialog(null, this.createEvent)
  }

  openUpdateDialog(event: EventModel): void {
    this.openDialog(event, this.updateEvent)
  }

  openDeleteDialog(id: number): void {
    const dialogRef = this._dialog.open(
      DeletePromptDialogComponent,
      {
        disableClose: true,
        data: {
          onClick: () => this.deleteEvent(id),
          data: {
            itemName: "Event"
          }
        }
      });

    dialogRef.afterClosed().subscribe(result => {
      if(result && result.yes){
        this._events = this._events.filter(x => x.id !== id);
        this.refreshDataSource();
      }
    });
  }

  ngOnInit() {
    this._client.getAllEvents().subscribe(
      data => {
        this._events = data,
        this.refreshDataSource()
      },
      error => console.error(error)
    );
  }

  private refreshDataSource(){
    this.dataSource.data = this._events;
  }

  private openDialog(event: EventModel, action: (EventModel) => Observable<EventModel>): void {
    const dialogRef = this._dialog.open(
      ScheduleModalDialogComponent,
      {
        disableClose: true,
        width: '500px',
        data: {
          onClick: action,
          event
        }
      });

    dialogRef.afterClosed().subscribe(result => {
      if(result){
        const index = this._events.findIndex(x => x.id === result.id);
        if(index !== -1){
          this._events[index] = result;
        } else {
          this._events.push(result);
        }
        this.refreshDataSource();
      }
    });
  }

  private createEvent(event: EventModel): Observable<EventModel> {
    const createModel = <EventCreateModel>event;
    return this._client.createEvent(createModel)
      .map(x => {
        const createdModel = new EventModel({...createModel, id: x});
        return createdModel;
      });
  }

  private updateEvent(event: EventModel): Observable<EventModel> {
    return this._client.updateEvent(event)
      .map(x => event);
  }

  private deleteEvent(id: number): Observable<void> {
    return this._client.deleteEvent(id)
  }
}