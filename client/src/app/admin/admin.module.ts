import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { HttpModule } from '@angular/http';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { Client, API_BASE_URL } from '../shared/client/api.client';

import { MaterialModule } from '../shared/material.module'
import { AdminRoutingModule } from './admin-routing.module';
import { RegistrationComponent } from './registration/registration.component'
import { AdminComponent } from './admin.component'


import { LayoutModule } from "../shared/layout/layout.module"

import { APP_BASE_HREF } from '@angular/common';
// TODO what to do about this?
import { environment } from '../../environments/environment';

export function getApiUrl(){
  return environment.apiUrl;
}

@NgModule({
  declarations: [
    RegistrationComponent,
    AdminComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    HttpModule,
    HttpClientModule,
    MaterialModule,
    ReactiveFormsModule,
    AdminRoutingModule,
    LayoutModule
  ],
  providers: [
    Client,
    { provide: API_BASE_URL, useFactory: getApiUrl }
  ]
})
export class AdminModule { }