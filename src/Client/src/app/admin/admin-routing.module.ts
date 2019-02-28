import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AdminComponent } from './admin.component';
import { RegistrationComponent } from './registration/registration.component';

const routes: Routes = [
  { path: '', component: AdminComponent, children: [
    { path: 'registration',  component: RegistrationComponent }
  ]}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
