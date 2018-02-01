import { Component, OnInit, HostListener } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';


import { Client } from '../app.client'
import { Photo } from '../models/photo.model';
import { PhotoGroup } from '../models/photo-group.model';

@Component({
  selector: 'app-single-photo',
  templateUrl: './single-photo.component.html',
  styleUrls: [ './photos.component.scss' ]
})
export class SinglePhotoComponent implements OnInit {
  currentPhoto: Photo;
  private id: number; 
  private groupName: string;
  private photoGroups: PhotoGroup[];
  private sub: any;
  
  constructor(
    private _route: ActivatedRoute,
    private _router: Router,
    private _client: Client) { }

  ngOnInit() {
    this.photoGroups = this._client.getPhotos();
    this.sub = this._route.params.subscribe(params => {
      this.groupName = params['groupName'];
      this.id = +params['id'];
      this.currentPhoto = this.photoGroups
        .find(x => x.groupName === this.groupName).photos
        .find(x => x.id === this.id);
   });
  }

  ngOnDestroy() {
    this.sub.unsubscribe();
  }

  @HostListener('document:keydown.ArrowRight', ['$event'])
  forward(): void {
    let currentGroupPhotos = this.getCurrentPhotoGroupPhotos();
    let index = currentGroupPhotos.findIndex(x => x.id == this.currentPhoto.id);
    let newIndex = currentGroupPhotos.length - 1 > index ? index + 1 : 0;
    this._router.navigate(['/photos', this.groupName, currentGroupPhotos[newIndex].id]);
  }

  @HostListener('document:keydown.ArrowLeft', ['$event'])
  back(): void {
    let currentGroupPhotos = this.getCurrentPhotoGroupPhotos();
    let index = currentGroupPhotos.findIndex(x => x.id == this.currentPhoto.id);
    let newIndex =  index != 0 ? index - 1 : currentGroupPhotos.length-1;
    this._router.navigate(['/photos', this.groupName, currentGroupPhotos[newIndex].id]);
  }

  private getCurrentPhotoGroupPhotos() : Photo[]{
    return this.photoGroups.find(x => x.groupName === this.groupName).photos;
  }
}
