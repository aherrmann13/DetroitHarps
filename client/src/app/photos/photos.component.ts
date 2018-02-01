import { Component, OnInit, Inject } from '@angular/core';

import { Client } from '../app.client'
import { PhotoGroup } from '../models/photo-group.model';

@Component({
  selector: 'app-photos',
  templateUrl: './photos.component.html',
  styleUrls: [ './photos.component.scss' ]
})
export class PhotosComponent implements OnInit {

  photoGroups: PhotoGroup[];

  constructor(private _client: Client) { }

  ngOnInit() {
    this.photoGroups = this._client.getPhotos();
  }
}