import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { MaterialModule } from '../shared/material.module';
import { LayoutModule } from '../shared/layout/layout.module';
import { ClientModule } from '../shared/client/client.module';

import { AdminRoutingModule } from './admin-routing.module';

import { AdminComponent } from './admin.component';
import { RegistrationComponent } from './registration/registration.component';
import { ScheduleComponent } from './schedule/schedule.component';
import { ScheduleModalDialogComponent } from './schedule/schedule-modal.component';
import { CallbackComponent } from './callback/callback.component';
import { AdminToolbarComponent } from './admin-toolbar/admin-toolbar.component';
import { DeletePromptDialogComponent } from './delete-prompt/delete-prompt.component';

@NgModule({
  declarations: [
    RegistrationComponent,
    CallbackComponent,
    ScheduleComponent,
    ScheduleModalDialogComponent,
    DeletePromptDialogComponent,
    AdminComponent,
    AdminToolbarComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    MaterialModule,
    ReactiveFormsModule,
    AdminRoutingModule,
    LayoutModule,
    ClientModule
  ],
  entryComponents: [
    ScheduleModalDialogComponent,
    DeletePromptDialogComponent
  ]
})
export class AdminModule { }
