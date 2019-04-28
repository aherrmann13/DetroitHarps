import { MAT_DIALOG_DATA, MatDialogRef, MatSnackBar } from "@angular/material";
import { Inject, Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators, AbstractControl } from "@angular/forms";
import {
  formMatchValidator,
  FORM_MATCH_ERROR 
} from "../../core/validators/form-match.directive";
import { Observable } from "rxjs";
import { EventModel } from "../../core/client/api.client";

export interface ScheduleModalDialogInput {
  onClick: (EventModel) => Observable<EventModel>;
  event?: EventModel;
}

@Component({
    selector: 'dh-admin-schedule-modal',
    templateUrl: 'schedule-modal.component.html',
    styleUrls: [ 'schedule.component.scss' ]
})
export class ScheduleModalDialogComponent implements OnInit {
  
  private _createHeader = "Create New Event";
  private _editHeader = "Edit Event";
  private _timeRegex =
    new RegExp("(^$)|(^([0-9]|0[0-9]|1[0-2]):[0-5][0-9] (AM|PM|am|pm)$)");
  private _event: EventModel;
  loading = false;
  header: string;
  formGroup: FormGroup;
  canRegisterChecked = false;

  constructor(
    private _dialogRef: MatDialogRef<ScheduleModalDialogComponent>,
    @Inject(MAT_DIALOG_DATA) private _data: ScheduleModalDialogInput,
    private _formBuilder: FormBuilder,
    private _snackBar: MatSnackBar) {
      // TODO: validate data always passed in
      if(_data.event){
        this._event = _data.event;
        this.header = this._editHeader
      } else {
        this._event = null;
        this.header = this._createHeader;
      }
  }

  ngOnInit(){
    const title = this._event ? this._event.title : "";
    const startDate = this._event ? this._event.startDate : "";
    const startTime = this._event ? this.getTime(this._event.startDate) : ""; 
    const endDate = this._event ? this._event.endDate : "";
    const endTime = this._event ? this.getTime(this._event.endDate) : ""; 
    const description = this._event ? this._event.description : "";
    this.formGroup = this._formBuilder.group({
      title: [ title, Validators.required ],
      startDate: [ startDate, Validators.required ],
      startTime: [
        startTime,
        [ Validators.required, formMatchValidator(this._timeRegex) ]
      ],
      endDate: [ endDate ],
      endTime: [ endTime, formMatchValidator(this._timeRegex)],
      description: [description]
    });

    this.canRegisterChecked = this._event ? this._event.canRegister : false;
  }

  onAcceptClick(): void {
    this.loading = true;
    this.formGroup.disable();
    this._data.onClick(this.mapEvent())
      .subscribe(
        data => this.handleSuccess(data),
        err => this.handleError(err))
  }

  handleSuccess(data: EventModel): void{
    this.loading = false;
    this.formGroup.enable();
    this._event = data;
    this._dialogRef.close(data);
  }

  handleError(err: any): void{
    this.loading = false;
    this.formGroup.enable();
    this._snackBar.open(
      "an error occurred",
      "Dismiss",
      { duration: 10000 });
    console.error(err);
    this.onCancelClick();
  }

  onCancelClick(): void {
    this._dialogRef.close();
  }

  getErrorMessage(control: AbstractControl): string {
    if(control.hasError(FORM_MATCH_ERROR)){
      return "format time hh:mm am/pm";
    }
  }

  private mapEvent(): EventModel {
    return new EventModel({
      id: this._event ? this._event.id : 0,
      title: this.formGroup.controls.title.value,
      startDate: this.getDate(
        this.formGroup.controls.startDate.value,
        this.formGroup.controls.startTime.value),
      endDate: this.getDate(
        this.formGroup.controls.endDate.value,
        this.formGroup.controls.endTime.value),
      description: this.formGroup.controls.description.value,
      canRegister: this.canRegisterChecked
    });
  }

  private getDate(date: Date, time: string): Date {
    if(!date || !time) {
      return undefined;
    }
    var timeParts = time.split(/[ :]+/);
    if(timeParts.length !== 3) {
      console.log(time);
    }
    const hours = timeParts[2].toLocaleLowerCase() === "am" ?
      +timeParts[0] :
      +timeParts[0] + 12;
    const minutes = +timeParts[1];

    return new Date(
      date.getFullYear(),
      date.getMonth(),
      date.getDate(),
      hours,
      minutes);
  }

  private getTime(time: Date): string {
    if(!time){
      return undefined;
    }
    var hours = time.getHours();
    const minutes = time.getMinutes();
    var period = "am";
    if(hours > 12){
      period = "pm";
      hours = hours - 12;
    } else if (hours == 12) {
      period = "pm";
    } else if (hours == 0){
      hours = 12;
    }

    return `${this.addZero(hours)}:${this.addZero(minutes)} ${period}`
  }

  private addZero(num: number): string{
    return num < 10 ? `0${num}` : `${num}`;
  }
}