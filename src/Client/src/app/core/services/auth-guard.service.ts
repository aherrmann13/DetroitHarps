import { Injectable } from '@angular/core';
import { Router, CanActivate, CanActivateChild, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AuthService } from './auth.service';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class AuthGuardService implements CanActivate {
  constructor(public auth: AuthService, public router: Router) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): boolean | Observable<boolean> | Promise<boolean> {
    if (!this.auth.isAuthenticated() && !this.isCallback(state)) {
      this.auth.login();
      return false;
    }
    return true;
  }

  private isCallback(state: RouterStateSnapshot): boolean {
    return state.url.startsWith('/admin/callback');
  }
}
