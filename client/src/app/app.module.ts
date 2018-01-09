import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';


import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';


import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { ScheduleComponent } from './schedule/schedule.component'

import { Client } from './app.client';

import { routing } from './app.routes';
import { ContactComponent } from './contact/contact.component'


@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    ScheduleComponent,
    ContactComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    MatButtonModule,
    MatCardModule,
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,
    MatListModule,
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
