import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuardService as AuthGuard } from '../core/services/auth-guard.service';
import { AdminComponent } from './admin.component';
import { RegistrationComponent } from './registration/registration.component';
import { CallbackComponent } from './callback/callback.component';

const routes: Routes = [
  { 
    path: '', 
    component: AdminComponent,
    children: [
      { 
        path: 'registration', 
        component: RegistrationComponent
      },
      { path: 'callback',  component: CallbackComponent }
    ],
    canActivate: [ AuthGuard ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
