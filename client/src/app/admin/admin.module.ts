import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { HttpModule } from '@angular/http';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { MatButtonModule,
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
  MatTableModule,
  MatToolbarModule } from '@angular/material';

import { Client, API_BASE_URL } from '../shared/client/api.client';

import { AdminRoutingModule } from './admin-routing.module';
import { RegistrationComponent } from './registration/registration.component'

import { APP_BASE_HREF } from '@angular/common';
// TODO what to do about this?
import { environment } from '../../environments/environment';

export function getApiUrl(){
  return environment.apiUrl;
}

@NgModule({
  declarations: [
    RegistrationComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    HttpModule,
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
    MatTableModule,
    MatToolbarModule,
    ReactiveFormsModule,
    AdminRoutingModule
  ],
  providers: [
    Client,
    { provide: API_BASE_URL, useFactory: getApiUrl }
  ]
})
export class AdminModule { }