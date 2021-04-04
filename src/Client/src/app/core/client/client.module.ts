import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';

import { Client, API_BASE_URL } from '../client/api.client';

import { environment } from '../../../environments/environment';

export function getApiUrl() {
  return environment.apiUrl;
}

@NgModule({
  declarations: [
  ],
  imports: [
    CommonModule,
    HttpClientModule
  ],
  providers: [
    Client,
    { provide: API_BASE_URL, useFactory: getApiUrl }
  ]
})
export class ClientModule { }
