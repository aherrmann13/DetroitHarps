import { Component, ViewChild } from '@angular/core';
import { DrawerItemModel } from '../shared/layout/drawer/drawer.item.model';
import { DrawerComponent } from '../shared/layout/drawer/drawer.component';

@Component({
  selector: 'dh-admin',
  templateUrl: './admin.component.html'
})
export class AdminComponent {

    @ViewChild(DrawerComponent) sidenav: DrawerComponent;
    title = 'Detroit Harps Admin';

    drawerItems: DrawerItemModel[] = [
        { text: 'Home', icon: 'home', path: '' },
        { text: 'Registration', icon: 'assignment', path: 'registration' }
      ];

    close() {
        this.sidenav.close();
    }
}
