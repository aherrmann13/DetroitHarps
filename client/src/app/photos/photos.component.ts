import { Component, OnInit, Inject } from '@angular/core';
import { MatDialog, MatDialogConfig, MAT_DIALOG_DATA } from '@angular/material';


import { Client } from '../app.client'
import { Photo } from '../models/photo.model'

@Component({
  selector: 'app-photos',
  templateUrl: './photos.component.html',
  styleUrls: [ './photos.component.scss' ]
})
export class PhotosComponent implements OnInit {

  photos: Photo[];

  constructor(private _client: Client, public dialog: MatDialog) { }

  ngOnInit() {
    this.photos = this._client.getPhotos();
  }

}