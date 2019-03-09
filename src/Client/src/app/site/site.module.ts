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
import { ParentInformationComponent } from './register/forms/parent-information.component';
import { AddressInformationComponent } from './register/forms/address-information.component';
import { ChildrenInformationComponent } from './register/forms/chidren-information.component';
import { PaymentInformationComponent } from './register/forms/payment-information.component';
import { CommentsComponent } from './register/forms/comments.component';
import { FinalStepComponent } from './register/forms/final-step.component';

import { RegisterService } from './register/register.service'

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
    SupportComponent,
    ParentInformationComponent,
    AddressInformationComponent,
    ChildrenInformationComponent,
    PaymentInformationComponent,
    CommentsComponent,
    FinalStepComponent
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
    RegisterService,
    MatIconRegistry
  ]
})
export class SiteModule { }
