import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { MatButtonModule,
  MatCardModule ,
  MatDatepickerModule,
  MatFormFieldModule,
  MatIconModule,
  MatInputModule,
  MatListModule,
  MatNativeDateModule,
  MatRadioModule,
  MatSelectModule,
  MatSidenavModule,
  MatStepperModule,
  MatToolbarModule } from '@angular/material';

import { AppComponent } from './app.component';
import { SinglePhotoComponent } from './photos/single-photo.component';
import { HomeComponent } from './home/home.component';
import { ScheduleComponent } from './schedule/schedule.component'

import { Client, API_BASE_URL } from './api.client';

import { routing } from './app.routes';
import { ContactComponent } from './contact/contact.component';
import { PhotosComponent } from './photos/photos.component';
import { AboutComponent } from './about/about.component';
import { RegisterComponent } from './register/register.component';
import { SupportComponent } from './support/support.component'
import { APP_BASE_HREF } from '@angular/common';
import { environment } from '../environments/environment.prod';

function getApiUrl(){
  return environment.apiUrl;
}

@NgModule({
  declarations: [
    AppComponent,
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
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    HttpClientModule,
    MatButtonModule,
    MatCardModule,
    MatDatepickerModule,
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,
    MatListModule,
    MatNativeDateModule,
    MatRadioModule,
    MatSelectModule,
    MatSidenavModule,
    MatStepperModule,
    MatToolbarModule,
    ReactiveFormsModule,
    routing
  ],
  providers: [
    Client,
    { provide: API_BASE_URL, useFactory: getApiUrl }
  ],
  bootstrap: [
    AppComponent
  ]
})
export class AppModule { }
