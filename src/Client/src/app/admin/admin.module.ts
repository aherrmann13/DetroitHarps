import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { MaterialModule } from '../core/material.module';
import { LayoutModule } from '../shared/layout/layout.module';
import { ClientModule } from '../core/client/client.module';

import { AdminRoutingModule } from './admin-routing.module';

import { AdminComponent } from './admin.component';
import { RegistrationComponent } from './registration/registration.component';
import { ScheduleComponent } from './schedule/schedule.component';
import { ScheduleModalDialogComponent } from './schedule/schedule-modal.component';
import { CallbackComponent } from './callback/callback.component';
import { AdminToolbarComponent } from './admin-toolbar/admin-toolbar.component';
import { DeletePromptDialogComponent } from './delete-prompt/delete-prompt.component';
import { ContactComponent } from './contact/contact.component';
import { MessageModalComponent } from './contact/message-modal.component';

@NgModule({
  declarations: [
    RegistrationComponent,
    CallbackComponent,
    ScheduleComponent,
    ScheduleModalDialogComponent,
    DeletePromptDialogComponent,
    AdminComponent,
    AdminToolbarComponent,
    ContactComponent,
    MessageModalComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    MaterialModule,
    ReactiveFormsModule,
    AdminRoutingModule,
    LayoutModule
  ],
  entryComponents: [
    ScheduleModalDialogComponent,
    DeletePromptDialogComponent,
    MessageModalComponent
  ]
})
export class AdminModule { }
