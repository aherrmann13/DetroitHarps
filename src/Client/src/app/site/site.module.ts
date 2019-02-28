import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { MatIconRegistry } from '@angular/material/icon';

import { MaterialModule } from '../shared/material.module';
import { LayoutModule } from '../shared/layout/layout.module';
import { ClientModule } from '../shared/client/client.module';

import { SiteRoutingModule } from './site-routing.module';


import { SiteComponent } from './site.component';
import { SinglePhotoComponent } from './photos/single-photo.component';
import { HomeComponent } from './home/home.component';
import { ScheduleComponent } from './schedule/schedule.component';
import { ContactComponent } from './contact/contact.component';
import { PhotosComponent } from './photos/photos.component';
import { AboutComponent } from './about/about.component';
import { RegisterComponent } from './register/register.component';
import { SupportComponent } from './support/support.component';

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
    MaterialModule,
    ReactiveFormsModule,
    SiteRoutingModule,
    LayoutModule,
    ClientModule
  ],
  providers: [
    MatIconRegistry
  ]
})
export class SiteModule { }
