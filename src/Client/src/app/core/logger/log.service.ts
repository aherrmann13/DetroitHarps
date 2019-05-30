import { Injectable, InjectionToken, Optional, Inject } from '@angular/core';
import { LogLevel } from './log-level';
import { LogPublisherService, LogEntry } from './log-publisher.service';

export const MIN_LOG_LEVEL = new InjectionToken<LogLevel>('MIN_LOG_LEVEL');

// based on this
// https://www.codemag.com/article/1711021/Logging-in-Angular-Applications
@Injectable()
export class LogService {
  constructor(
    private _publisherService: LogPublisherService,
    @Optional() @Inject(MIN_LOG_LEVEL) private _logLevel?: LogLevel
  ) {
    this._logLevel = _logLevel ? _logLevel : LogLevel.Info;
  }

  logDebug(msg: any) {
    return this.log(LogLevel.Debug, msg);
  }
  logInfo(msg: any) {
    return this.log(LogLevel.Info, msg);
  }
  logError(msg: any) {
    if (msg instanceof Error) {
      return this.log(LogLevel.Error, (<Error>msg).stack);
    } else {
      return this.log(LogLevel.Error, msg);
    }
  }

  log(level: LogLevel, msg: any) {
    if (this.shouldLog(level)) {
      for (const publisher of this._publisherService.publishers) {
        publisher.log(this.getEntry(level, JSON.stringify(msg)));
      }
    }
  }

  private shouldLog(level: LogLevel): boolean {
    return level >= this._logLevel;
  }

  private getEntry(level: LogLevel, msg: string): LogEntry {
    return {
      entryDate: new Date(),
      message: msg,
      level: level
    };
  }
}
