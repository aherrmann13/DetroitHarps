import { Component, ViewChild, Input } from '@angular/core';
import { Router } from '@angular/router';
import { MatSidenav } from '@angular/material/sidenav';

import { DrawerItemModel } from './drawer.item.model';

@Component({
  selector: 'dh-drawer',
  templateUrl: './drawer.component.html',
  styleUrls: ['./drawer.component.scss']
})
export class DrawerComponent {
  @Input() items: DrawerItemModel[];

  router = this._router;

  @ViewChild('sidenav')
  private sidenav: MatSidenav;

  constructor(private _router: Router) {}

  open() {
    this.sidenav.open();
  }

  close() {
    this.sidenav.close();
  }

  toggle() {
    this.sidenav.toggle();
  }
}
