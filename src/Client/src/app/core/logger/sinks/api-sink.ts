import { LogEntry, LogSink } from '../log-publisher.service';
import { LogLevel } from '../log-level';
import { Client, ClientErrorModel } from '../../client/api.client';

export class ApiSink implements LogSink {
  constructor(private _client: Client) {}

  log(entry: LogEntry) {
    switch (entry.level) {
      case LogLevel.Debug:
      case LogLevel.Info:
        break;
      case LogLevel.Error:
        this.logError(entry);
        break;
    }
  }

  private logError(entry: LogEntry): void {
    this._client.clientError(this.getModel(entry)).subscribe(
      x => x,
      // TODO: how to handle here?
      // if api is down, will loop forever
      error => console.error(error)
    );
  }

  private getModel(entry: LogEntry): ClientErrorModel {
    return new ClientErrorModel({
      timestamp: entry.entryDate,
      // TODO: generate unique id per page laod
      sessionId: '',
      message: entry.message
    });
  }
}
