import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import * as auth0 from 'auth0-js';
import { environment } from '../../../environments/environment';

@Injectable()
export class AuthService {
  private _accessToken: string;
  private _expiresAt: number;

  auth0 = new auth0.WebAuth({
    ...environment.auth0,
    responseType: 'token id_token',
    scope: 'openid'
  });

  constructor() {
    this._accessToken = '';
    this._expiresAt = 0;
    this.getValuesFromLocalStorage();
  }

  get accessToken(): string {
    return this._accessToken;
  }

  public login(): void {
    this.auth0.authorize();
  }

  // using callbacks because the router was not working
  // properly when injected here
  // TODO: use router here
  public handleAuthentication(successCallback: Function, errorCallback: Function): void {
    this.auth0.parseHash((err, authResult) => {
      if (authResult && authResult.accessToken && authResult.idToken) {
        this.localLogin(authResult);
        successCallback();
      } else if (err) {
        errorCallback();
      }
    });
  }

  public renewTokens(): void {
    this.auth0.checkSession({}, (err, authResult) => {
      if (authResult && authResult.accessToken && authResult.idToken) {
        this.localLogin(authResult);
      } else if (err) {
        alert(`Could not get a new token (${err.error}: ${err.error_description}).`);
        this.logout();
      }
    });
  }

  public logout(): void {
    // Remove tokens and expiry time
    this._accessToken = '';
    this._expiresAt = 0;
    localStorage.removeItem('token');
    localStorage.removeItem('expiresAt');
  }

  public isAuthenticated(): boolean {
    // Check whether the current time is past the
    // access token's expiry time
    return new Date().getTime() < this._expiresAt;
  }

  private localLogin(authResult): void {
    // Set the time that the access token will expire at
    const expiresAt = authResult.expiresIn * 1000 + new Date().getTime();
    this._accessToken = authResult.accessToken;
    this._expiresAt = expiresAt;

    localStorage.setItem('token', authResult.accessToken);
    localStorage.setItem('expiresAt', expiresAt.toString());
  }

  private getValuesFromLocalStorage(): void {
    const accessToken = localStorage.getItem('token');
    const expiresAt = localStorage.getItem('expiresAt');

    this._accessToken = accessToken ? accessToken : this._accessToken;
    this._expiresAt = expiresAt && !isNaN(+expiresAt) ? parseInt(expiresAt, 10) : this._expiresAt;
  }
}
