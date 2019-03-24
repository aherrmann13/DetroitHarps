import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CoreRoutingModule } from './core-routing.module';
import { RouterModule } from '@angular/router';

import { AuthService } from './services/auth.service';
import { AuthGuardService } from './services/auth-guard.service';

@NgModule({
  imports: [
    CommonModule,
    CoreRoutingModule
  ],
  declarations: [],
  exports: [
    RouterModule
  ],
  providers: [
    AuthService,
    AuthGuardService
  ]
})
export class CoreModule { }
