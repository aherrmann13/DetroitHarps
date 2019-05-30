import { NgModule } from '@angular/core';
import { LogService, MIN_LOG_LEVEL } from './log.service';
import { LogPublisherService } from './log-publisher.service';
import { LogLevel } from './log-level';

@NgModule({
  declarations: [],
  imports: [],
  providers: [LogService, LogPublisherService, { provide: MIN_LOG_LEVEL, useValue: LogLevel.Debug }]
})
export class LoggerModule {}
