import { Component, ViewChild} from '@angular/core';
import { Router } from '@angular/router';
import { MatSidenav } from '@angular/material/sidenav';
import { DomSanitizer } from '@angular/platform-browser';
import { MatIconRegistry} from '@angular/material/icon';


@Component({
  selector: 'app-root',
  templateUrl : './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  @ViewChild('sidenav') sidenav: MatSidenav;
  title = 'Detroit Harps';
  facebookUrl = 'https://www.facebook.com/DetroitHarpsYouthGFC/';
  twitterUrl = 'https://twitter.com/DetroitHarps';
  instagramUrl = 'https://www.instagram.com/detroitharps/';
  shopUrl = 'https://www.oneills.com/shop-by-team/gaa/usa/detroit-harps-gaa.html';
  router = this._router;


  constructor(
      private _mdIconRegistry: MatIconRegistry, 
        private _sanitizer: DomSanitizer,
        private _router: Router) {
        _mdIconRegistry
            .addSvgIcon('facebook',
            _sanitizer.bypassSecurityTrustResourceUrl('assets/icons/facebook.svg'));
        _mdIconRegistry
            .addSvgIcon('twitter',
            _sanitizer.bypassSecurityTrustResourceUrl('assets/icons/twitter.svg'));
        _mdIconRegistry
            .addSvgIcon('instagram',
            _sanitizer.bypassSecurityTrustResourceUrl('assets/icons/instagram.svg'));
        _mdIconRegistry
            .addSvgIcon('shop',
            _sanitizer.bypassSecurityTrustResourceUrl('assets/icons/shop.svg'));
        _mdIconRegistry
            .addSvgIcon('logo',
            _sanitizer.bypassSecurityTrustResourceUrl('assets/icons/detroitharpslogo.svg'));
  }

  close() {
    this.sidenav.close();
  }
}
