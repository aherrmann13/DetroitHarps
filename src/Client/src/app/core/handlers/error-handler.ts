import { ErrorHandler, Injectable, Optional, InjectionToken, Inject, Injector, inject, NgZone } from '@angular/core';
import { LogService } from '../logger/log.service';
import { Router } from '@angular/router';
import { LoggerModule } from '../logger/logger.module';
import { MatSnackBar } from '@angular/material';
// based on
// https://blog.angularindepth.com/expecting-the-unexpected-best-practices-for-error-handling-in-angular-21c3662ef9e4
@Injectable()
export class GlobalErrorHandler implements ErrorHandler {
  private _logger: LogService;
  private _router: Router;
  private _snackbar: MatSnackBar;
  private _zone: NgZone;

  constructor(private _injector: Injector) {
    this._logger = _injector.get(LogService);
    this._zone = _injector.get(NgZone);
  }

  handleError(error: any) {
    this.setRouter();
    this.setSnackbar();

    this._logger.logError(error);

    this._zone.run(() => {
      this._snackbar.open('an error occurred', 'Dismiss', { duration: 10000 });
    });

    if (this.isAdmin()) {
      this._router.navigate(['/admin']);
    } else {
      this._router.navigate(['/']);
    }
  }

  //router doesnt exist when this class is instantiated
  private setRouter() {
    this._router = this._router ? this._router : this._injector.get(Router);
  }

  private setSnackbar() {
    this._snackbar = this._snackbar ? this._snackbar : this._injector.get(MatSnackBar);
  }

  private isAdmin() {
    return this._router.url.startsWith('/admin');
  }
}
