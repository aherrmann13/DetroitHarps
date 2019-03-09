import { NgModule } from '@angular/core';

import {
    MatButtonModule,
    MatCardModule,
    MatDatepickerModule,
    MatFormFieldModule,
    MatIconModule,
    MatIconRegistry,
    MatInputModule,
    MatListModule,
    MatNativeDateModule,
    MatProgressBarModule,
    MatRadioModule,
    MatSelectModule,
    MatSidenavModule,
    MatStepperModule,
    MatTableModule,
    MatToolbarModule } from '@angular/material';

@NgModule({
    imports: [
        MatButtonModule,
        MatCardModule,
        MatDatepickerModule,
        MatFormFieldModule,
        MatIconModule,
        MatInputModule,
        MatListModule,
        MatNativeDateModule,
        MatProgressBarModule,
        MatRadioModule,
        MatSelectModule,
        MatSidenavModule,
        MatStepperModule,
        MatTableModule,
        MatToolbarModule
    ],
    exports: [
        MatButtonModule,
        MatCardModule,
        MatDatepickerModule,
        MatFormFieldModule,
        MatIconModule,
        MatInputModule,
        MatListModule,
        MatNativeDateModule,
        MatProgressBarModule,
        MatRadioModule,
        MatSelectModule,
        MatSidenavModule,
        MatStepperModule,
        MatTableModule,
        MatToolbarModule
    ]
  })
export class MaterialModule {}
