import { Component, ViewChild} from '@angular/core';
import { Router } from '@angular/router';
import { MatSidenav } from '@angular/material/sidenav';
import { DomSanitizer } from '@angular/platform-browser';
import { MatIconRegistry} from '@angular/material/icon';
import { DrawerItemModel } from '../shared/layout/drawer/drawer.item.model';
import { DrawerComponent } from '../shared/layout/drawer/drawer.component';


@Component({
  selector: 'dh-site',
  templateUrl : './site.component.html',
  styleUrls: [ './site.component.scss' ]
})
export class SiteComponent {
  @ViewChild(DrawerComponent) sidenav: DrawerComponent;
  title = 'Detroit Harps';
  facebookUrl = 'https://www.facebook.com/DetroitHarpsYouthGFC/';
  twitterUrl = 'https://twitter.com/DetroitHarps';
  instagramUrl = 'https://www.instagram.com/detroitharps/';
  shopUrl = 'https://www.oneills.com/shop-by-team/gaa/usa/detroit-harps-gaa.html';
  router = this._router;

  drawerItems: DrawerItemModel[] = [
      { text: 'Home', icon: 'home', path: '/' },
      { text: 'Schedule', icon: 'event', path: '/schedule' },
      { text: 'Contact', icon: 'chat', path: '/contact' },
      { text: 'Photos', icon: 'folder', path: '/photos' },
      { text: 'About', icon: 'info', path: '/about' },
      { text: 'Support Us', icon: 'thumb_up', path: '/support' }
    ];


  constructor(
      private _matIconRegistry: MatIconRegistry,
        private _sanitizer: DomSanitizer,
        private _router: Router) {
        _matIconRegistry
            .addSvgIcon('facebook',
            _sanitizer.bypassSecurityTrustResourceUrl('assets/icons/facebook.svg'));
        _matIconRegistry
            .addSvgIcon('twitter',
            _sanitizer.bypassSecurityTrustResourceUrl('assets/icons/twitter.svg'));
        _matIconRegistry
            .addSvgIcon('instagram',
            _sanitizer.bypassSecurityTrustResourceUrl('assets/icons/instagram.svg'));
        _matIconRegistry
            .addSvgIcon('shop',
            _sanitizer.bypassSecurityTrustResourceUrl('assets/icons/shop.svg'));
        _matIconRegistry
            .addSvgIcon('logo',
            _sanitizer.bypassSecurityTrustResourceUrl('assets/icons/detroitharpslogo.svg'));
  }

  close() {
    this.sidenav.close();
  }
}
