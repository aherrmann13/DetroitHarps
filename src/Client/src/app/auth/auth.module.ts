import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { MaterialModule } from '../shared/material.module';
import { LayoutModule } from '../shared/layout/layout.module';
import { ClientModule } from '../shared/client/client.module';

import { AuthRoutingModule } from './auth-routing.module';

import { AuthComponent } from './auth.component';
import { LoginComponent } from './login/login.component';

@NgModule({
  declarations: [
    AuthComponent,
    LoginComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    MaterialModule,
    ReactiveFormsModule,
    AuthRoutingModule,
    LayoutModule,
    ClientModule
  ]
})
export class AuthModule { }
