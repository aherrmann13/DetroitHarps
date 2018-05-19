import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { HttpModule } from '@angular/http';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';


import { MaterialModule } from '../shared/material.module'
import { SinglePhotoComponent } from './photos/single-photo.component';
import { HomeComponent } from './home/home.component';
import { ScheduleComponent } from './schedule/schedule.component'

import { Client, API_BASE_URL } from '../shared/client/api.client';

import { SiteRoutingModule } from './site-routing.module';
import { LayoutModule } from "../shared/layout/layout.module"
import { SiteComponent } from './site.component';
import { ContactComponent } from './contact/contact.component';
import { PhotosComponent } from './photos/photos.component';
import { AboutComponent } from './about/about.component';
import { RegisterComponent } from './register/register.component';
import { SupportComponent } from './support/support.component'

import { APP_BASE_HREF } from '@angular/common';
// TODO what to do about this?
import { environment } from '../../environments/environment';

export function getApiUrl(){
  return environment.apiUrl;
}

@NgModule({
  declarations: [
    SiteComponent,
    HomeComponent,
    ScheduleComponent,
    ContactComponent,
    PhotosComponent,
    SinglePhotoComponent,
    AboutComponent,
    RegisterComponent,
    SupportComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    HttpModule,
    HttpClientModule,
    MaterialModule,
    ReactiveFormsModule,
    SiteRoutingModule,
    LayoutModule
  ],
  providers: [
    Client,
    { provide: API_BASE_URL, useFactory: getApiUrl }
  ]
})
export class SiteModule { }
