import { Injectable } from "@angular/core";
import { LogLevel } from "./log-level";
import { ConsoleSink } from "./sinks/console-sink";

export interface LogSink {
    log: (entry: LogEntry) => void;
}

export interface LogEntry {
    entryDate: Date,
    message: string,
    level: LogLevel
}

@Injectable()
export class LogPublisherService {
    publishers: Array<LogSink>;
    
    constructor() {
        this.publishers = [
            new ConsoleSink()
        ]
    }
}