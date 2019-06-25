import { NgModule, ErrorHandler } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CoreRoutingModule } from './core-routing.module';
import { RouterModule } from '@angular/router';
import { LoggerModule } from './logger/logger.module';

import { AuthService } from './services/auth.service';
import { AuthGuardService } from './services/auth-guard.service';
import { GlobalErrorHandler } from './handlers/error-handler';
import { MaterialModule } from './material.module';
import { ClientModule } from './client/client.module';

@NgModule({
  imports: [CommonModule, CoreRoutingModule, MaterialModule, LoggerModule, ClientModule],
  declarations: [],
  exports: [LoggerModule, RouterModule, ClientModule],
  providers: [AuthService, AuthGuardService]
})
export class CoreModule {}
