import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { MatIconRegistry } from '@angular/material/icon';

import { MaterialModule } from '../core/material.module';
import { LayoutModule } from '../core/layout/layout.module';

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
import { ParentInformationComponent } from './register/forms/parent-information/parent-information.component';
import { AddressInformationComponent } from './register/forms/address-information/address-information.component';
import { ChildrenInformationComponent } from './register/forms/children-information/children-information.component';
import { PaymentInformationComponent } from './register/forms/payment-information/payment-information.component';
import { CommentsComponent } from './register/forms/comments/comments.component';
import { FinalStepComponent } from './register/forms/final-step/final-step.component';

import { PhotoService } from './photos/photos.service';
import { LoggerModule } from '../core/logger/logger.module';
import { FooterComponent } from './footer/footer.component';
import { EventRegistrationComponent } from './register/forms/children-information/event-registration.component';
import { ChildCountSelectorComponent } from './register/forms/children-information/child-count-selector.component';
import { PaypalFooterComponent } from './footer/paypal-footer.component';

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
    ChildCountSelectorComponent,
    PaymentInformationComponent,
    EventRegistrationComponent,
    CommentsComponent,
    FinalStepComponent,
    PaypalFooterComponent,
    FooterComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    MaterialModule,
    ReactiveFormsModule,
    SiteRoutingModule,
    LayoutModule,
    LoggerModule
  ],
  providers: [PhotoService, MatIconRegistry]
})
export class SiteModule {}
