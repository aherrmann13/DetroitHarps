import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CoreRoutingModule } from './core-routing.module';
import { RouterModule } from '@angular/router';

@NgModule({
  imports: [
    CommonModule,
    CoreRoutingModule
  ],
  declarations: [],
  exports: [
    RouterModule
  ],
})
export class CoreModule { }