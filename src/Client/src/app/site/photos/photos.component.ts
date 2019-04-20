import { Component, OnInit, Inject, Optional } from '@angular/core';

import { Client, PhotoGroupModel, API_BASE_URL, PhotoDisplayPropertiesDetailModel } from '../../core/client/api.client';
import { PhotoService } from './photos.service';

interface PhotoGroup
{
  group: PhotoGroupModel,
  photos: Array<PhotoDisplayPropertiesDetailModel>
}

@Component({
  selector: 'dh-photos',
  templateUrl: './photos.component.html',
  styleUrls: [ './photos.component.scss' ]
})
export class PhotosComponent implements OnInit {

  photoGroups: Array<PhotoGroup>;

  constructor(
    private _client: Client,
    private _photoService: PhotoService,
    @Optional() @Inject(API_BASE_URL) private _baseUrl?: string) { }

  ngOnInit() {
    this._photoService.getAll()
    .subscribe(
      data => this.photoGroups = data
    )
  }

  toPhotoUrl(model: PhotoDisplayPropertiesDetailModel): string {
    return this._baseUrl + '/Photo/Get/' + model.photoId;
  }
}
