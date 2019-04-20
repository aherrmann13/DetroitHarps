import { Component, ViewChild, OnInit } from '@angular/core';
import { DrawerItemModel } from '../core/layout/drawer/drawer.item.model';
import { DrawerComponent } from '../core/layout/drawer/drawer.component';
import { AuthService } from '../core/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'dh-admin',
  templateUrl: './admin.component.html',
  styleUrls: [ './admin.component.scss']
})
export class AdminComponent implements OnInit {

    @ViewChild(DrawerComponent) sidenav: DrawerComponent;
    title = 'Detroit Harps Admin';
    drawerItems: DrawerItemModel[] = [
      { text: 'Home', icon: 'home', path: '' },
      { text: 'Registration', icon: 'assignment', path: 'registration' },
      { text: 'Schedule', icon: 'event', path: 'schedule' },
      { text: 'Contact', icon: 'chat', path: 'contact' }
    ];

    constructor(private _router: Router, private _authService: AuthService) {
      _authService.handleAuthentication(
        _ => {
          this._router.navigate(['/admin'])
        },
        _ => {
          this._router.navigate(['/'])
        }
      );
    }

    ngOnInit() {
      if (localStorage.getItem('isLoggedIn') === 'true') {
        this._authService.renewTokens();
      }
    }

    close() {
      this.sidenav.close();
    }

    logout() {
      this._authService.logout();
      this._router.navigate(['/']);
    }
}
