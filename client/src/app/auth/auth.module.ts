import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { HttpModule } from '@angular/http';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { 
    MatButtonModule,
    MatCardModule,
    MatIconModule,
    MatInputModule,
    MatToolbarModule } from '@angular/material';

import { Client, API_BASE_URL } from '../shared/client/api.client';

import { AuthRoutingModule } from './auth-routing.module';
import { AuthComponent } from './auth.component'
import { LoginComponent } from './login/login.component'
import { ToolbarComponent } from '../shared/layout/toolbar/toolbar.component'

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
    ToolbarComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    HttpModule,
    HttpClientModule,
    MatButtonModule,
    MatCardModule,
    MatIconModule,
    MatInputModule,
    MatToolbarModule,
    ReactiveFormsModule,
    AuthRoutingModule
  ],
  providers: [
    Client,
    { provide: API_BASE_URL, useFactory: getApiUrl }
  ]
})
export class AuthModule { }