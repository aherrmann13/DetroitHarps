import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';

import { HttpModule } from '@angular/http';

import { Client, API_BASE_URL } from '../client/api.client';

import { APP_BASE_HREF } from '@angular/common';
// TODO what to do about this?
import { environment } from '../../../environments/environment';

export function getApiUrl() {
  return environment.apiUrl;
}

@NgModule({
  declarations: [
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    /* tslint:disable-next-line:deprecation */
    HttpModule
  ],
  providers: [
    Client,
    { provide: API_BASE_URL, useFactory: getApiUrl }
  ]
})
export class ClientModule { }
