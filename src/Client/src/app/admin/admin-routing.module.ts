import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuardService as AuthGuard } from '../core/services/auth-guard.service';
import { AdminComponent } from './admin.component';
import { RegistrationComponent } from './registration/registration.component';
import { CallbackComponent } from './callback/callback.component';
import { ScheduleComponent } from './schedule/schedule.component';
import { ContactComponent } from './contact/contact.component';

const routes: Routes = [
  {
    path: '',
    component: AdminComponent,
    children: [
      { path: 'registration', component: RegistrationComponent },
      { path: 'schedule', component: ScheduleComponent },
      { path: 'callback', component: CallbackComponent },
      { path: 'contact', component: ContactComponent }
    ],
    canActivate: [AuthGuard]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule {}
