import { ModuleWithProviders }  from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ScheduleComponent } from './schedule/schedule.component';

const defaultRoutes = [
  {
    path: '',
    component: HomeComponent
  }
]

const routes: Routes = [
  { path: 'home',  component: HomeComponent },
  { path: 'schedule', component: ScheduleComponent }
];

const appRoutes = [
  ...defaultRoutes,
  ...routes
]

export const routing: ModuleWithProviders = RouterModule.forRoot(appRoutes);