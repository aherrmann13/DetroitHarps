import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { MaterialModule } from '../material.module';
import { DrawerComponent } from './drawer/drawer.component';
import { ToolbarComponent } from './toolbar/toolbar.component';

@NgModule({
  declarations: [ToolbarComponent, DrawerComponent],
  imports: [CommonModule, FormsModule, HttpClientModule, MaterialModule, ReactiveFormsModule, RouterModule],
  exports: [ToolbarComponent, DrawerComponent],
  providers: []
})
export class LayoutModule {}
