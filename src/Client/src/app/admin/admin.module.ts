import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { MaterialModule } from '../shared/material.module';
import { LayoutModule } from '../shared/layout/layout.module';
import { ClientModule } from '../shared/client/client.module';

import { AdminRoutingModule } from './admin-routing.module';

import { AdminComponent } from './admin.component';
import { RegistrationComponent } from './registration/registration.component';
import { CallbackComponent } from './callback/callback.component';

@NgModule({
  declarations: [
    RegistrationComponent,
    CallbackComponent,
    AdminComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    MaterialModule,
    ReactiveFormsModule,
    AdminRoutingModule,
    LayoutModule,
    ClientModule
  ]
})
export class AdminModule { }
