import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuardService as AuthGuard } from './services/auth-guard.service'

const routes: Routes = [
  {
    path: 'login',
    loadChildren: '../auth/auth.module#AuthModule'
  },
  {
    path: 'admin',
    loadChildren: '../admin/admin.module#AdminModule',
    canActivate: [ AuthGuard ] 
  },
  {
    path: '',
    loadChildren: '../site/site.module#SiteModule'
  },
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class CoreRoutingModule { }