import { Client, PhotoGroupModel, PhotoDisplayPropertiesDetailModel } from '../../core/client/api.client';
import { Observable } from 'rxjs/Observable';
import { forkJoin } from 'rxjs/observable/forkJoin';
import { Injectable } from '@angular/core';

export interface PhotoGroup {
  group: PhotoGroupModel;
  photos: Array<PhotoDisplayPropertiesDetailModel>;
}

// this only exists until a better api for photos grouped by photo group exists
@Injectable()
export class PhotoService {
  constructor(private _client: Client) {}

  // leaving payment param for now to maintain compatability when support is added
  getAll(): Observable<Array<PhotoGroup>> {
    const photoGroupObservable = this._client.getAllPhotoGroups();
    const photoObservable = this._client.getAllPhotos();

    return forkJoin(photoGroupObservable, photoObservable).map(x => this.groupPhotos(x[0], x[1]));
  }

  private groupPhotos(
    groups: Array<PhotoGroupModel>,
    photos: Array<PhotoDisplayPropertiesDetailModel>
  ): Array<PhotoGroup> {
    return groups
      .map(
        x =>
          <PhotoGroup>{
            group: x,
            photos: photos.filter(y => y.photoGroupId === x.id).sort(y => y.sortOrder)
          }
      )
      .sort(x => x.group.sortOrder);
  }
}
