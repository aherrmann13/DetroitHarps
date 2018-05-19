import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { HttpModule } from '@angular/http';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { MaterialModule } from '../shared/material.module'
import { Client, API_BASE_URL } from '../shared/client/api.client';

import { LayoutModule } from "../shared/layout/layout.module"
import { AuthRoutingModule } from './auth-routing.module';
import { AuthComponent } from './auth.component'
import { LoginComponent } from './login/login.component'

import { APP_BASE_HREF } from '@angular/common';
// TODO what to do about this?
import { environment } from '../../environments/environment';

export function getApiUrl(){
  return environment.apiUrl;
}

@NgModule({
  declarations: [
    AuthComponent,
    LoginComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    HttpModule,
    HttpClientModule,
    MaterialModule,
    ReactiveFormsModule,
    AuthRoutingModule,
    LayoutModule
  ],
  providers: [
    Client,
    { provide: API_BASE_URL, useFactory: getApiUrl }
  ]
})
export class AuthModule { }