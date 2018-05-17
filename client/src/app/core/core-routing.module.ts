import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthComponent } from '../auth/auth.component';

const routes: Routes = [
  {
    path: 'login',
    loadChildren: '../auth/auth.module#AuthModule'
  },
  {
    path: 'admin',
    loadChildren: '../admin/admin.module#AdminModule'
  },
  {
    path: '',
    loadChildren: '../site/site.module#SiteModule'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class CoreRoutingModule { }