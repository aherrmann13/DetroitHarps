import { Component, OnInit, Inject, Optional } from '@angular/core';

import { Client, PhotoGroupModel, API_BASE_URL, PhotoDisplayPropertiesDetailModel } from '../../shared/client/api.client';
import { forkJoin } from 'rxjs';

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

  constructor(private _client: Client, @Optional() @Inject(API_BASE_URL) private _baseUrl?: string) { }

  ngOnInit() {
    const photoGroupObservable = this._client.getAllPhotoGroups();
    const photoObservable = this._client.getAllPhotos();
    forkJoin(photoGroupObservable, photoObservable)
      .subscribe(
        data => this.photoGroups = this.groupPhotos(data[0], data[1]),
        error => console.log(error),
      )
  }

  toPhotoUrl(model: PhotoDisplayPropertiesDetailModel): string {
    return this._baseUrl + '/Photo/Get/' + model.photoId;
  }

  private groupPhotos(
    groups: Array<PhotoGroupModel>,
    photos: Array<PhotoDisplayPropertiesDetailModel>
  ) : Array<PhotoGroup> {
    return groups.map(x => <PhotoGroup> {
      group: x,
      photos: photos.filter(y => y.photoGroupId === x.id)
    })
  }
}
