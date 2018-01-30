import { Component, OnInit, Inject } from '@angular/core';


import { Client } from '../app.client'
import { Photo } from '../models/photo.model'

@Component({
  selector: 'app-photos',
  templateUrl: './photos.component.html',
  styleUrls: [ './photos.component.scss' ]
})
export class PhotosComponent implements OnInit {

  photos: Photo[];

  constructor(private _client: Client) { }

  ngOnInit() {
    this.photos = this._client.getPhotos();
  }

}