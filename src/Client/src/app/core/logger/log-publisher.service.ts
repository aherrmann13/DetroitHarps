import { Injectable } from "@angular/core";
import { LogLevel } from "./log-level";
import { ConsoleSink } from "./sinks/console-sink";
import { ApiSink } from "./sinks/api-sink";
import { Client } from "../client/api.client";

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
    
    constructor(client: Client) {
        this.publishers = [
            new ConsoleSink(),
            new ApiSink(client)
        ]
    }
}