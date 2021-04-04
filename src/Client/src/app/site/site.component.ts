import { Component, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { DomSanitizer } from '@angular/platform-browser';
import { MatIconRegistry } from '@angular/material/icon';
import { DrawerItemModel } from '../core/layout/drawer/drawer.item.model';
import { DrawerComponent } from '../core/layout/drawer/drawer.component';

@Component({
  selector: 'dh-site',
  templateUrl: './site.component.html',
  styleUrls: ['./site.component.scss']
})
export class SiteComponent {
  @ViewChild(DrawerComponent, { static: true }) sidenav: DrawerComponent;
  title = 'Detroit Harps';
  facebookUrl = 'https://www.facebook.com/DetroitHarpsYouthGFC/';
  twitterUrl = 'https://twitter.com/DetroitHarps';
  instagramUrl = 'https://www.instagram.com/detroitharps/';
  shopUrl = 'https://www.oneills.com/shop-by-team/gaa/usa/detroit-harps-gaa.html';
  youtubeUrl = 'https://www.youtube.com/channel/UCkbd2IjrNAD94FB06Zh4cPA';
  router: Router;

  drawerItems: DrawerItemModel[] = [
    { text: 'Home', icon: 'home', path: '/' },
    { text: 'Schedule', icon: 'event', path: '/schedule' },
    { text: 'Contact', icon: 'chat', path: '/contact' },
    { text: 'Photos', icon: 'folder', path: '/photos' },
    { text: 'About', icon: 'info', path: '/about' },
    { text: 'Support Us', icon: 'thumb_up', path: '/support' }
  ];

  constructor(matIconRegistry: MatIconRegistry, sanitizer: DomSanitizer, router: Router) {
    this.router = router;
    matIconRegistry.addSvgIcon('facebook', sanitizer.bypassSecurityTrustResourceUrl('assets/icons/facebook.svg'));
    matIconRegistry.addSvgIcon('twitter', sanitizer.bypassSecurityTrustResourceUrl('assets/icons/twitter.svg'));
    matIconRegistry.addSvgIcon('instagram', sanitizer.bypassSecurityTrustResourceUrl('assets/icons/instagram.svg'));
    matIconRegistry.addSvgIcon('shop', sanitizer.bypassSecurityTrustResourceUrl('assets/icons/shop.svg'));
    matIconRegistry.addSvgIcon('youtube', sanitizer.bypassSecurityTrustResourceUrl('assets/icons/youtube.svg'));
    matIconRegistry.addSvgIcon('logo', sanitizer.bypassSecurityTrustResourceUrl('assets/icons/detroitharpslogo.svg'));
  }

  close() {
    this.sidenav.close();
  }
}
