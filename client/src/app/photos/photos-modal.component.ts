import { Component, OnInit, Inject } from '@angular/core';
import { MatDialog, MAT_DIALOG_DATA } from '@angular/material';

@Component({
    selector: 'photos-modal',
    templateUrl: 'photos-modal.component.html',
    styleUrls: [ 'photos-modal.component.scss' ]
  })
  export class PhotosModalComponent {
    constructor(@Inject(MAT_DIALOG_DATA) public data: any) {}
  }
  