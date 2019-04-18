import { LogEntry, LogSink } from "../log-publisher.service";
import { LogLevel } from "../log-level";

export class ConsoleSink implements LogSink {
    log(entry: LogEntry) {
        switch(entry.level){
            case LogLevel.Debug:
                console.debug(this.getLog(entry))
                break;
            case LogLevel.Info:
                console.log(this.getLog(entry))
                break;
            case LogLevel.Error:
                console.error(this.getLog(entry))
                break;
        }
    }
    
    getLog(entry: LogEntry): string {
        return `[${this.getLevelAsString(entry.level)}] ` +
            `[${entry.entryDate.toLocaleString()}] ` +
            `${entry.message}`;
    }

    getLevelAsString(level: LogLevel): string {
        switch(level){
            case LogLevel.Debug:
                return "debug";
            case LogLevel.Info:
                return "info";
            case LogLevel.Error:
                return "error";
            default:
                // this is slower than a switch, only used as default
                return Object.keys(LogLevel).find(key => LogLevel[key] === level);
        }
    }
}