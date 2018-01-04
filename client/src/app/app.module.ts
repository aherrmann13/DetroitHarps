import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';


import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';


import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';

import { Client } from './app.client';
import { ScheduleComponent } from './schedule/schedule.component'

import { routing } from './app.routes'


@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    ScheduleComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    MatButtonModule,
    MatIconModule,
    MatSidenavModule,
    MatToolbarModule,
    routing
  ],
  providers: [
    Client
  ],
  bootstrap: [
    AppComponent
  ]
})
export class AppModule { }
