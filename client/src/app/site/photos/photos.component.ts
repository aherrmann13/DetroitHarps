import { Component, OnInit, Inject, Optional } from '@angular/core';

import { Client, PhotoGroupReadModel, API_BASE_URL } from '../../shared/client/api.client';

@Component({
  selector: 'dh-photos',
  templateUrl: './photos.component.html',
  styleUrls: [ './photos.component.scss' ]
})
export class PhotosComponent implements OnInit {

  photoGroups: PhotoGroupReadModel[];

  constructor(private _client: Client, @Optional() @Inject(API_BASE_URL) private _baseUrl?: string) { }

  ngOnInit() {
    this._client.getAllPhotoGroups().subscribe(
      data => this.photoGroups = data,
      error => console.log(error)
    );
  }

  toPhotoUrl(id: number): string {
    return this._baseUrl + '/Photo/Get/' + id;
  }
}
