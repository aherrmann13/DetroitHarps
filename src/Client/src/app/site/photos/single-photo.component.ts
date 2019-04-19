import { Component, OnInit, HostListener, Inject, Optional, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { API_BASE_URL } from '../../core/client/api.client';
import { PhotoService, PhotoGroup } from './photos.service';

@Component({
  selector: 'dh-single-photo',
  templateUrl: './single-photo.component.html',
  styleUrls: [ './photos.component.scss' ]
})
export class SinglePhotoComponent implements OnInit, OnDestroy {
  currentPhotoId: number;
  private id: number;
  private groupId: number;
  private photoGroups: PhotoGroup[];
  private sub: any;

  constructor(
    private _route: ActivatedRoute,
    private _router: Router,
    private _photoService: PhotoService,
    @Optional() @Inject(API_BASE_URL) private _baseUrl?: string) { }

  ngOnInit() {
    this._photoService.getAll().subscribe(
      data => this.processPhotoGroupsReturned(data),
      error => console.error(error)
    );
  }

  ngOnDestroy() {
    this.sub.unsubscribe();
  }

  @HostListener('document:keydown.ArrowRight', ['$event'])
  forward(): void {
    const currentGroupPhotos = this.getCurrentPhotoGroupPhotos();
    const index = currentGroupPhotos.findIndex(x => x === this.currentPhotoId);
    const newIndex = currentGroupPhotos.length - 1 > index ? index + 1 : 0;
    this._router.navigate(['/photos', this.groupId, currentGroupPhotos[newIndex]]);
  }

  @HostListener('document:keydown.ArrowLeft', ['$event'])
  back(): void {
    const currentGroupPhotos = this.getCurrentPhotoGroupPhotos();
    const index = currentGroupPhotos.findIndex(x => x === this.currentPhotoId);
    const newIndex =  index !== 0 ? index - 1 : currentGroupPhotos.length - 1;
    this._router.navigate(['/photos', this.groupId, currentGroupPhotos[newIndex]]);
  }

  toPhotoUrl(id: number): string {
    return this._baseUrl + '/Photo/Get/' + id;
  }

  private getCurrentPhotoGroupPhotos(): number[] {
    return this.photoGroups
      .find(x => x.group.id === this.groupId)
      .photos
      .map(x => x.photoId);
  }

  private processPhotoGroupsReturned(groups: PhotoGroup[]): void {
    this.photoGroups = groups;
    this.sub = this._route.params.subscribe(params => {
      this.groupId = +params['groupId'];
      this.id = +params['id'];
      this.currentPhotoId = this.photoGroups
        .find(x => x.group.id === this.groupId)
        .photos
        .find(x => x.photoId === this.id)
        .photoId;
   });
  }
}