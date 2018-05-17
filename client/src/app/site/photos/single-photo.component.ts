import { Component, OnInit, HostListener, Inject, Optional } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';


import { Client, PhotoGroupReadModel, API_BASE_URL } from '../../shared/client/api.client';

@Component({
  selector: 'site-single-photo',
  templateUrl: './single-photo.component.html',
  styleUrls: [ './photos.component.scss' ]
})
export class SinglePhotoComponent implements OnInit {
  currentPhotoId: number;
  private id: number; 
  private groupId: number;
  private photoGroups: PhotoGroupReadModel[];
  private sub: any;
  
  constructor(
    private _route: ActivatedRoute,
    private _router: Router,
    private _client: Client,
    @Optional() @Inject(API_BASE_URL) private _baseUrl?: string) { }

  ngOnInit() {
    this._client.getAllPhotoGroups().subscribe(
      data => this.processPhotoGroupsReturned(data),
      error => console.error(error)
    );
    
  }

  ngOnDestroy() {
    this.sub.unsubscribe();
  }

  @HostListener('document:keydown.ArrowRight', ['$event'])
  forward(): void {
    let currentGroupPhotos = this.getCurrentPhotoGroupPhotos();
    let index = currentGroupPhotos.findIndex(x => x == this.currentPhotoId);
    let newIndex = currentGroupPhotos.length - 1 > index ? index + 1 : 0;
    this._router.navigate(['/photos', this.groupId, currentGroupPhotos[newIndex]]);
  }

  @HostListener('document:keydown.ArrowLeft', ['$event'])
  back(): void {
    let currentGroupPhotos = this.getCurrentPhotoGroupPhotos();
    let index = currentGroupPhotos.findIndex(x => x == this.currentPhotoId);
    let newIndex =  index != 0 ? index - 1 : currentGroupPhotos.length-1;
    this._router.navigate(['/photos', this.groupId, currentGroupPhotos[newIndex]]);
  }

  toPhotoUrl(id: number): string{
    return this._baseUrl + "/Photo/Get/" + id;
  }

  private getCurrentPhotoGroupPhotos() : number[]{
    return this.photoGroups.find(x => x.id === this.groupId).photoIds;
  }

  private processPhotoGroupsReturned(groups: PhotoGroupReadModel[]): void {
    this.photoGroups = groups;
    this.sub = this._route.params.subscribe(params => {
      this.groupId = +params['groupId'];
      this.id = +params['id'];
      this.currentPhotoId = this.photoGroups
        .find(x => x.id === this.groupId).photoIds
        .find(x => x === this.id);
   });

   
  }
}
