import { Component, OnInit, HostListener } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';


import { Client } from '../app.client'
import { Photo } from '../models/photo.model';

@Component({
  selector: 'app-single-photo',
  templateUrl: './single-photo.component.html',
  styleUrls: [ './photos.component.scss' ]
})
export class SinglePhotoComponent implements OnInit {
  id: number; 
  currentPhoto: Photo;
  photos: Photo[];
  private sub: any;
  
  constructor(
    private _route: ActivatedRoute,
    private _router: Router,
    private _client: Client) { }

  ngOnInit() {
    this.photos = this._client.getPhotos().sort(x => x.sortOrder).reverse();
    this.sub = this._route.params.subscribe(params => {
      this.id = +params['id'];
      console.log(this.id);
      this.currentPhoto = this.photos.find(x => x.id === this.id);
   });
  }

  ngOnDestroy() {
    console.log("new");
    this.sub.unsubscribe();
  }

  @HostListener('document:keydown.ArrowRight', ['$event'])
  forward(): void {
    let index = this.photos.findIndex(x => x.id == this.currentPhoto.id);
    let newIndex = this.photos.length - 1 > index ? index + 1 : 0;
    this._router.navigate(['/photos', this.photos[newIndex].id]);
  }

  @HostListener('document:keydown.ArrowLeft', ['$event'])
  back(): void {
    let index = this.photos.findIndex(x => x.id == this.currentPhoto.id);
    let newIndex =  index != 0 ? index - 1 : this.photos.length-1;
    this._router.navigate(['/photos', this.photos[newIndex].id]);
  }
}
