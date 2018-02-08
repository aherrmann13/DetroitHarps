import { ModuleWithProviders }  from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ScheduleComponent } from './schedule/schedule.component';
import { ContactComponent } from './contact/contact.component';
import { PhotosComponent } from './photos/photos.component';
import { SinglePhotoComponent } from './photos/single-photo.component';
import { AboutComponent } from './about/about.component';
import { RegisterComponent } from './register/register.component'
import { SupportComponent } from './support/support.component'

const defaultRoutes = [
  {
    path: '',
    component: HomeComponent
  }
]

const routes: Routes = [
  { path: 'home',  component: HomeComponent },
  { path: 'schedule', component: ScheduleComponent },
  { path: 'contact', component: ContactComponent },
  { path: 'photos', component: PhotosComponent },
  { path: 'photos/:groupName/:id', component: SinglePhotoComponent },
  { path: 'about', component: AboutComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'support', component: SupportComponent }
];

const appRoutes = [
  ...defaultRoutes,
  ...routes
]

export const routing: ModuleWithProviders = RouterModule.forRoot(appRoutes);